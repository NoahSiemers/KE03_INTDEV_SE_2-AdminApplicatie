using DataAccessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            if (context.Customers.Any())
            {
                return;
            }
      
            var customers = new Customer[]
            {
                new Customer { Name = "Noah", Address = "123 Elm St" , Active=true},
                new Customer { Name = "Gebruiker 1", Address = "456 Oak St", Active = true },
                new Customer { Name = "Gebruiker 2", Address = "789 Pine St", Active = true }
            };
            context.Customers.AddRange(customers);

            var orders = new Order[]
            {
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01")},
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[1], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[2], OrderDate = DateTime.Parse("2021-03-01")}
            };  
            context.Orders.AddRange(orders);

            var products = new Product[]
            {
                new Product { Name = "Boormachine GBM 10 RE", Description = "Boormachine GBM 10 RE", Price = 100.99m, Category = "Gereedschap", Stock = 24, AddedDate = DateTime.Parse("2026-01-12"), Supplier = "Bosch", MainImageUrl = "/images/products/Bosch.jfif" },
                new Product { Name = "JMV Spaanplaatschroeven", Description = "Spaanplaatschroeven 4.0x16mm", Price = 9.99m, Category = "Schroeven", Stock = 3, AddedDate = DateTime.Parse("2026-01-15"), Supplier = "JMV", MainImageUrl = "/images/products/Schroef.webp" },
                new Product { Name = "vidaXL Schep", Description = "Schep zwart 68,5 cm", Price = 24.99m, Category = "Tuin", Stock = 18, AddedDate = DateTime.Parse("2026-01-20"), Supplier = "vidaXL", MainImageUrl = "/images/products/Schep.jpg" },
                new Product { Name = "Bosch PST 650 Decoupeerzaag", Description = "Decoupeerzaag op snoer", Price = 59.99m, Category = "Gereedschap", Stock = 11, AddedDate = DateTime.Parse("2026-01-22"), Supplier = "Bosch", MainImageUrl = "/images/products/Zaag.jpg" },
                new Product { Name = "Kreator Combinatietang", Description = "Combinatietang basic 150mm", Price = 14.99m, Category = "Gereedschap", Stock = 7, AddedDate = DateTime.Parse("2026-01-25"), Supplier = "Kreator", MainImageUrl = "/images/products/Tang.jpg" },
                new Product { Name = "BGS Steekringsleutel 36mm", Description = "Steekringsleutel 36mm", Price = 19.99m, Category = "Gereedschap", Stock = 5, AddedDate = DateTime.Parse("2026-01-28"), Supplier = "BGS", MainImageUrl = "/images/products/Steekringsleutel.jpg" },
                new Product { Name = "Makita Slijpschijf", Description = "Slijpschijf voor metaal", Price = 8.49m, Category = "Gereedschap", Stock = 42, AddedDate = DateTime.Parse("2026-02-01"), Supplier = "Makita" },
                new Product { Name = "DeWalt Accuboormachine", Description = "Accuboormachine 18V", Price = 149.99m, Category = "Elektra", Stock = 9, AddedDate = DateTime.Parse("2026-02-05"), Supplier = "DeWalt" },
                new Product { Name = "vidaXL Schroefset", Description = "Schroefset met verschillende maten", Price = 12.99m, Category = "Schroeven", Stock = 60, AddedDate = DateTime.Parse("2026-02-08"), Supplier = "vidaXL" },
                new Product { Name = "Bosch Haakse Slijper", Description = "Haakse slijper 750W", Price = 79.99m, Category = "Gereedschap", Stock = 14, AddedDate = DateTime.Parse("2026-02-10"), Supplier = "Bosch" },
                new Product { Name = "Kreator Waterpas", Description = "Waterpas 60 cm", Price = 6.99m, Category = "Meetapparatuur", Stock = 33, AddedDate = DateTime.Parse("2026-02-12"), Supplier = "Kreator" },
                new Product { Name = "BGS Ringsleutelset", Description = "Set ringsleutels", Price = 34.99m, Category = "Gereedschap", Stock = 20, AddedDate = DateTime.Parse("2026-02-15"), Supplier = "BGS" },
                new Product { Name = "Makita Klopboor", Description = "Klopboor voor steen en beton", Price = 89.99m, Category = "Elektra", Stock = 6, AddedDate = DateTime.Parse("2026-02-18"), Supplier = "Makita" },
                new Product { Name = "JMV Pluggen Set", Description = "Pluggen set 100 stuks", Price = 4.99m, Category = "Schroeven", Stock = 100, AddedDate = DateTime.Parse("2026-02-20"), Supplier = "JMV" },
                new Product { Name = "vidaXL Hark", Description = "Tuin hark met houten steel", Price = 16.99m, Category = "Tuin", Stock = 22, AddedDate = DateTime.Parse("2026-02-22"), Supplier = "vidaXL" },
                new Product { Name = "DeWalt Cirkelzaag", Description = "Cirkelzaag voor hout", Price = 129.99m, Category = "Elektra", Stock = 4, AddedDate = DateTime.Parse("2026-02-25"), Supplier = "DeWalt" },
                new Product { Name = "Bosch Schuurmachine", Description = "Schuurmachine voor hout", Price = 69.99m, Category = "Gereedschap", Stock = 13, AddedDate = DateTime.Parse("2026-02-27"), Supplier = "Bosch" },
                new Product { Name = "Kreator Meetlint", Description = "Meetlint 5 meter", Price = 5.49m, Category = "Meetapparatuur", Stock = 55, AddedDate = DateTime.Parse("2026-03-01"), Supplier = "Kreator" }
            };
            context.Products.AddRange(products);

            var parts = new Part[]
            {
                new Part { Name = "Tandwiel", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen"},
                new Part { Name = "M5 Boutje", Description = "Bevestiging van panelen, buizen of interne modules"},
                new Part { Name = "Hydraulische cilinder", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen"},
                new Part { Name = "Koelvloeistofpomp", Description = "Koeling van de motor of elektronische systemen."}
            };
            context.Parts.AddRange(parts);

            var productImages = new ProductImage[]
            {
                new ProductImage { Product = products[0], ImageUrl = "/images/products/Bosch.jfif", AltText = "Boormachine GBM 10 RE hoofdafbeelding"},
                new ProductImage { Product = products[0], ImageUrl = "/images/products/Bosch1.jpg", AltText = "Boormachine GBM 10 RE extra afbeelding 1"},
                new ProductImage { Product = products[0], ImageUrl = "/images/products/Bosch2.jpg", AltText = "Boormachine GBM 10 RE extra afbeelding 2"},
                new ProductImage { Product = products[1], ImageUrl = "/images/products/Schroef.webp", AltText = "JMV spaanplaatschroeven hoofdafbeelding"},
                new ProductImage { Product = products[2], ImageUrl = "/images/products/Schep.jpg", AltText = "vidaXL Schep hoofdafbeelding"},
                new ProductImage { Product = products[3], ImageUrl = "/images/products/Zaag.jpg", AltText = "Bosch PST 650 Decoupeerzaag hoofdafbeelding"},
                new ProductImage { Product = products[4], ImageUrl = "/images/products/Tang.jpg", AltText = "Kreator combinatietang hoofdafbeelding"},
                new ProductImage { Product = products[5], ImageUrl = "/images/products/Steekringsleutel.jpg", AltText = "BGS Steekringsleutel hoofdafbeelding"}
            };
            context.ProductImages.AddRange(productImages);

            var productSpecifications = new ProductSpecification[]
            {
                new ProductSpecification { Product = products[0], SpecName = "Type", SpecValue = "Hovercraft"},
                new ProductSpecification { Product = products[0], SpecName = "Gebruik", SpecValue = "Transport"},
                new ProductSpecification { Product = products[1], SpecName = "Materiaal", SpecValue = "Metaal en leer"},
                new ProductSpecification { Product = products[2], SpecName = "Categorie", SpecValue = "Verdediging"}
            };
            context.ProductSpecifications.AddRange(productSpecifications);

			var functions = new Function[]
            {
	            new Function { Name = "Manager" },
	            new Function { Name = "Magazijnmedewerker" },
	            new Function { Name = "Verkoop" },
	            new Function { Name = "Administratie" },
	            new Function { Name = "IT" }
            };
			context.Functions.AddRange(functions);

			var staff = new Staff[]
			{
	        new Staff { FirstName = "Jan", LastName = "de Vries", Email = "jan@matrixinc.nl", Function = functions[0], Active = true },
	        new Staff { FirstName = "Lisa", LastName = "Bakker", Email = "lisa@matrixinc.nl", Function = functions[2], Active = true },
	        new Staff { FirstName = "Tom", LastName = "Janssen", Email = "tom@matrixinc.nl", Function = functions[1], Active = false }
			};
			context.Staff.AddRange(staff);

			context.SaveChanges();

            context.Database.EnsureCreated();
        }
    }
}
