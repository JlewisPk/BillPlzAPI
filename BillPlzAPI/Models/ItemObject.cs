using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillPlzAPI.Models
{
    public class ItemObject
    {
        [Key]
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int itemPrice { get; set; }
        public int itemCount { get; set; }
        public string itemURL { get; set; }
    }
}
