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
                    ItemURL = "https://www.google.co.nz/url?sa=i&source=images&cd=&cad=rja&uact=8&ved=2ahUKEwi5vInIu-DeAhWBfX0KHWDxDd4QjRx6BAgBEAU&url=https%3A%2F%2Fwww.olivemagazine.com%2Frecipes%2Fmeat-and-poultry%2Fbest-ever-pork-belly-recipes%2F&psig=AOvVaw1cTFKzw4moV5BDOoWA6Kz0&ust=1542717199438862",
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
