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
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BillPlzAPIContext(
                serviceProvider.GetRequiredService<DbContextOptions<BillPlzAPIContext>>()))
            {
                // Look for any movies.
                if (context.ItemObject.Count() > 0)
                {
                    return;   // DB has been seeded
                }

                context.ItemObject.AddRange(
                    new ItemObject
                    {
                        itemId=1,
                        itemName="Pork Belly",
                        itemPrice=28,
                        itemCount=1,
                        itemURL= "https://www.google.co.nz/url?sa=i&source=images&cd=&cad=rja&uact=8&ved=2ahUKEwi5vInIu-DeAhWBfX0KHWDxDd4QjRx6BAgBEAU&url=https%3A%2F%2Fwww.olivemagazine.com%2Frecipes%2Fmeat-and-poultry%2Fbest-ever-pork-belly-recipes%2F&psig=AOvVaw1cTFKzw4moV5BDOoWA6Kz0&ust=1542717199438862"
                    }


                );
                context.SaveChanges();
            }
        }
    }
}
