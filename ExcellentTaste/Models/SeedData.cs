using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Data.ExcellentTasteContext(serviceProvider.GetRequiredService<
                    DbContextOptions<Data.ExcellentTasteContext>>()))
            {
                // Look for any movies.
                if (context.Product.Any())
                {
                    return;   // DB has been seeded
                }
                //Seed
                context.DietTag.AddRange(
                    new DietTag()
                    {
                        Name = "Gluten-free"
                    },
                    new DietTag()
                    {
                        Name = "Lactose-free"
                    },
                    new DietTag()
                    {
                        Name = "Vegan"
                    },
                    new DietTag()
                    {
                        Name = "Veggie"
                    }
                    );
                context.Product.AddRange(
                    new Product()
                    {
                        Name = "Coffee",
                        Description = "Black coffee.",
                        Price = 3,
                        Tax = 9,
                        Available = true
                    },
                    new Product()
                    {
                        Name = "Tea",
                        Description = "Green tea.",
                        Price = 3,
                        Tax = 9,
                        Available = true
                    },
                    new Product()
                    {
                        Name = "Ice Tea",
                        Description = "Homemade ice tea.",
                        Price = 3,
                        Tax = 9,
                        Available = true
                    },
                    new Product()
                    {
                        Name = "Club Sandwich",
                        Description = "Sandwich with egg, tomato, bacon, chicken, salad and mayonaise.",
                        Price = 6,
                        Tax = 9,
                        Available = true
                    },
                    new Product()
                    {
                        Name = "Croque monsieur (ham)",
                        Description = "Hot sandwich with ham and cheese.",
                        Price = 5,
                        Tax = 9,
                        Available = true
                    },
                    new Product()
                    {
                        Name = "Croque monsieur",
                        Description = "Hot sandwich with cheese.",
                        Price = 4,
                        Tax = 9,
                        Available = true
                    },
                    new Product()
                    {
                        Name = "Tomato soup",
                        Description = "Tomato soup with meatballs",
                        Price = 5,
                        Tax = 9,
                        Available = true
                    },
                    new Product()
                    {
                        Name = "Mushroom soup",
                        Description = "",
                        Price = 5,
                        Tax = 9,
                        Available = true
                    }
                    );
                context.Categories.AddRange(
                    new Category()
                    {
                        Name = "Hot Drink"
                    }
                    ,
                    new Category()
                    {
                        Name = "Cold Drink"
                    },
                    new Category()
                    {
                        Name = "Soup"
                    },
                    new Category()
                    {
                        Name = "Lunch"
                    }
                    );
                context.Order.AddRange(
                    new Order()
                    {
                        StartTime = DateTime.Now
                    },
                    new Order()
                    {
                        StartTime = DateTime.Now.AddDays(1)
                    }
                    );
                context.Table.AddRange(
                    new Table()
                    {
                        Name = "Tafel 1",
                        Seats = 4
                    },
                    new Table()
                    {
                        Name = "Tafel 2",
                        Seats = 4
                    },
                    new Table()
                    {
                        Name = "Tafel 3",
                        Seats = 3
                    },
                    new Table()
                    {
                        Name = "Tafel 4",
                        Seats = 3
                    },
                    new Table()
                    {
                        Name = "Tafel 5",
                        Seats = 6
                    },
                    new Table()
                    {
                        Name = "Tafel 6",
                        Seats = 2
                    },
                    new Table()
                    {
                        Name = "Tafel 7",
                        Seats = 2
                    }
                    );
                context.SaveChanges();
                ///INFO: Add the many-to-many relations 
                ///the database won't have generated any Ids prior to saving 
                ///the models contained within these linked tables
                context.ProductCategories.AddRange(
                    new ProductCategory
                    {
                        Category = context.Categories.FirstOrDefault(cat => cat.Name == "Hot Drink"),
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Coffee")
                    },
                    new ProductCategory
                    {
                        Category = context.Categories.FirstOrDefault(cat => cat.Name == "Hot Drink"),
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Tea")
                    },
                    new ProductCategory
                    {
                        Category = context.Categories.FirstOrDefault(cat => cat.Name == "Cold Drink"),
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Ice Tea")
                    },
                    new ProductCategory
                    {
                        Category = context.Categories.FirstOrDefault(cat => cat.Name == "Lunch"),
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Club Sandwich")
                    },
                    new ProductCategory
                    {
                        Category = context.Categories.FirstOrDefault(cat => cat.Name == "Lunch"),
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Croque monsieur (ham)")
                    },
                    new ProductCategory
                    {
                        Category = context.Categories.FirstOrDefault(cat => cat.Name == "Lunch"),
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Croque monsieur")
                    },
                    new ProductCategory
                    {
                        Category = context.Categories.FirstOrDefault(cat => cat.Name == "Soup"),
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Tomato soup")
                    },
                    new ProductCategory
                    {
                        Category = context.Categories.FirstOrDefault(cat => cat.Name == "Soup"),
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Mushroom soup")
                    }
                    );
                context.ProductTags.AddRange(
                    new ProductTag
                    {
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Coffee"),
                        DietTag = context.DietTag.FirstOrDefault(tag => tag.Name == "Gluten-free")
                    },
                    new ProductTag
                    {
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Coffee"),
                        DietTag = context.DietTag.FirstOrDefault(tag => tag.Name == "Lactose-free")
                    },
                    new ProductTag
                    {
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Tea"),
                        DietTag = context.DietTag.FirstOrDefault(tag => tag.Name == "Gluten-free")
                    },
                    new ProductTag
                    {
                        Product = context.Product.FirstOrDefault(prod => prod.Name == "Tea"),
                        DietTag = context.DietTag.FirstOrDefault(tag => tag.Name == "Lactose-free")
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
