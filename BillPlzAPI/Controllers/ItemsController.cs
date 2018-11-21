using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BillPlzAPI.Models;
using BillPlzAPI.Helpers;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.Extensions.Configuration;

namespace BillPlzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemObjectsController : ControllerBase
    {
        private readonly ItemContext _context;
        private IConfiguration _configuration;

        public ItemObjectsController(ItemContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/ItemObjects
        [HttpGet]
        public IEnumerable<Item> GetItemObject()
        {
            return _context.Item;
        }

        // GET: api/ItemObjects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemObject = await _context.Item.FindAsync(id);

            if (itemObject == null)
            {
                return NotFound();
            }

            return Ok(itemObject);
        }

        // PUT: api/ItemObjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemObject([FromRoute] int id, [FromBody] Item itemObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemObject.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(itemObject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemObjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItemObjects
        [HttpPost]
        public async Task<IActionResult> PostItemObject([FromBody] Item itemObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Item.Add(itemObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemObject", new { id = itemObject.ItemId }, itemObject);
        }

        // DELETE: api/ItemObjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemObject = await _context.Item.FindAsync(id);
            if (itemObject == null)
            {
                return NotFound();
            }

            _context.Item.Remove(itemObject);
            await _context.SaveChangesAsync();

            return Ok(itemObject);
        }

        private bool ItemObjectExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }


        // GET: api/ItemObjects/ItemNames
        [Route("itemNames")]
        [HttpGet]
        public async Task<List<string>> GetItemNamesList()
        {
            var items = (from m in _context.Item
                         group m by m.ItemName into m
                         select m.Key);

            var returned = await items.ToListAsync();

            return returned;
        }


        [HttpPost, Route("upload")]
        public async Task<IActionResult> UploadFile([FromForm]ItemImage itemImage)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }
            try
            {
                using (var stream = itemImage.Image.OpenReadStream())
                {
                    var cloudBlock = await UploadToBlob(itemImage.Image.FileName, null, stream);
                    //// Retrieve the filename of the file you have uploaded
                    //var filename = provider.FileData.FirstOrDefault()?.LocalFileName;
                    if (string.IsNullOrEmpty(cloudBlock.StorageUri.ToString()))
                    {
                        return BadRequest("An error has occured while uploading your file. Please try again.");
                    }

                    Item imageSourceData = new Item();
                    imageSourceData.ItemName = itemImage.ItemName;

                    System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                    imageSourceData.Height = image.Height.ToString();
                    imageSourceData.Width = image.Width.ToString();
                    imageSourceData.ItemURL = cloudBlock.SnapshotQualifiedUri.AbsoluteUri;
                    imageSourceData.Uploaded = DateTime.Now.ToString();

                    _context.Item.Add(imageSourceData);
                    await _context.SaveChangesAsync();

                    return Ok($"File: {itemImage.ItemName} has successfully uploaded");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occured. Details: {ex.Message}");
            }


        }

        private async Task<CloudBlockBlob> UploadToBlob(string filename, byte[] imageBuffer = null, System.IO.Stream stream = null)
        {

            var accountName = _configuration["AzureBlob:name"];
            var accountKey = _configuration["AzureBlob:key"]; ;
            var storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer imagesContainer = blobClient.GetContainerReference("images");

            string storageConnectionString = _configuration["AzureBlob:connectionString"];

            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    // Generate a new filename for every new blob
                    var fileName = Guid.NewGuid().ToString();
                    fileName += GetFileExtention(filename);

                    // Get a reference to the blob address, then upload the file to the blob.
                    CloudBlockBlob cloudBlockBlob = imagesContainer.GetBlockBlobReference(fileName);

                    if (stream != null)
                    {
                        await cloudBlockBlob.UploadFromStreamAsync(stream);
                    }
                    else
                    {
                        return new CloudBlockBlob(new Uri(""));
                    }

                    return cloudBlockBlob;
                }
                catch (StorageException ex)
                {
                    return new CloudBlockBlob(new Uri(""));
                }
            }
            else
            {
                return new CloudBlockBlob(new Uri(""));
            }

        }

        private string GetFileExtention(string fileName)
        {
            if (!fileName.Contains("."))
                return ""; //no extension
            else
            {
                var extentionList = fileName.Split('.');
                return "." + extentionList.Last(); //assumes last item is the extension 
            }
        }
    }
}