using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillPlzAPI.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public int ItemCount { get; set; }
        public string ItemURL { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Uploaded { get; set; }
    }
}
