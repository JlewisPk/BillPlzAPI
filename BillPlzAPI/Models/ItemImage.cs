using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillPlzAPI.Models
{
    public class ItemImage
    {
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public IFormFile Image { get; set; }
    }
}
