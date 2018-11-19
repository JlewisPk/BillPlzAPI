using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BillPlzAPI.Models
{
    public class BillPlzAPIContext : DbContext
    {
        public BillPlzAPIContext (DbContextOptions<BillPlzAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BillPlzAPI.Models.ItemObject> ItemObject { get; set; }
    }
}
