using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillPlzAPI.Models
{
    public class SeedData
    {
        public static void InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new ItemContext(
            serviceProvider.GetRequiredService<DbContextOptions<ItemContext>>()))
            {
                // child object
                var newItem = new BillPlzAPI.Models.Item
                {
                    ItemName = "Pork Belly",
                    ItemPrice = 28,
                    ItemCount = 1,
                    ItemURL = "https://billplzblob.blob.core.windows.net/itemimages/porkbelly.jpg",
                    Height = "700",
                    Width = "700",
                    Uploaded = "11/10/2018 10:09:52 PM"
                };
                if (context.Item.Count() == 0)
                {
                    context.Item.AddRange(newItem);
                    context.SaveChanges();
                };
                return;
            }
        }
    }
}
