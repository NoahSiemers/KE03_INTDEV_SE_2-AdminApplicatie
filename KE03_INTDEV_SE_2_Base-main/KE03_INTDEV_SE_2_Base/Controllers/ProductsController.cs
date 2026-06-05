using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_2_Base.Helpers;
using KE03_INTDEV_SE_2_Base.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index(
            string? search,
            string? category,
            string sortBy = "id",
            string sortDirection = "asc",
            bool lowStockOnly = false,
            int page = 1)
        {
            const int pageSize = 15;
            const int lowStockLimit = 7;

            var allProducts = _productRepository.GetAllProducts().ToList();

            IEnumerable<Product> filteredProducts = allProducts;

            if (!string.IsNullOrWhiteSpace(search))
            {
                filteredProducts = filteredProducts.Where(p =>
                    p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.Category.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.Supplier.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                filteredProducts = filteredProducts.Where(p => p.Category == category);
            }

            if (lowStockOnly)
            {
                filteredProducts = filteredProducts.Where(p => p.Stock <= lowStockLimit);
            }

            filteredProducts = SortProducts(filteredProducts, sortBy, sortDirection);

            var totalProducts = filteredProducts.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            if (page < 1)
            {
                page = 1;
            }

            if (totalPages > 0 && page > totalPages)
            {
                page = totalPages;
            }

            var products = filteredProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new ProductIndexViewModel
            {
                Products = products,
                LowStockProducts = allProducts
                    .Where(p => p.Stock <= lowStockLimit)
                    .OrderBy(p => p.Stock)
                    .ToList(),

                Search = search,
                Category = category,
                SortBy = sortBy,
                SortDirection = sortDirection,
                LowStockOnly = lowStockOnly,
                CurrentPage = page,
                TotalPages = totalPages,
                Categories = ProductCategories.All
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new ProductCreateViewModel
            {
                Categories = ProductCategories.All
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreateViewModel viewModel)
        {
            viewModel.Categories = ProductCategories.All;

            if (!ProductCategories.All.Contains(viewModel.Category))
            {
                ModelState.AddModelError("Category", "Kies een geldige categorie.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var product = new Product
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                Stock = viewModel.Stock,
                Supplier = viewModel.Supplier,
                Category = viewModel.Category,
                MainImageUrl = viewModel.MainImageUrl,
                Description = viewModel.Description,
                AddedDate = DateTime.Now
            };

            if (!string.IsNullOrWhiteSpace(viewModel.MainImageUrl))
            {
                product.Images.Add(new ProductImage
                {
                    ImageUrl = viewModel.MainImageUrl,
                    AltText = $"{viewModel.Name} hoofdafbeelding"
                });
            }

            if (!string.IsNullOrWhiteSpace(viewModel.SubImageUrl))
            {
                product.Images.Add(new ProductImage
                {
                    ImageUrl = viewModel.SubImageUrl,
                    AltText = $"{viewModel.Name} subafbeelding"
                });
            }

            if (!string.IsNullOrWhiteSpace(viewModel.SpecificationName) &&
                !string.IsNullOrWhiteSpace(viewModel.SpecificationValue))
            {
                product.Specifications.Add(new ProductSpecification
                {
                    SpecName = viewModel.SpecificationName,
                    SpecValue = viewModel.SpecificationValue
                });
            }

            _productRepository.AddProduct(product);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var firstSpecification = product.Specifications.FirstOrDefault();

            var viewModel = new ProductCreateViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Supplier = product.Supplier,
                Category = product.Category,
                MainImageUrl = product.MainImageUrl,
                SubImageUrl = product.Images
                    .FirstOrDefault(i => i.ImageUrl != product.MainImageUrl)?.ImageUrl,
                Description = product.Description,
                SpecificationName = firstSpecification?.SpecName,
                SpecificationValue = firstSpecification?.SpecValue,
                Categories = ProductCategories.All
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCreateViewModel viewModel)
        {
            viewModel.Categories = ProductCategories.All;

            if (!ProductCategories.All.Contains(viewModel.Category))
            {
                ModelState.AddModelError("Category", "Kies een geldige categorie.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var product = _productRepository.GetProductById(viewModel.Id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = viewModel.Name;
            product.Price = viewModel.Price;
            product.Stock = viewModel.Stock;
            product.Supplier = viewModel.Supplier;
            product.Category = viewModel.Category;
            product.MainImageUrl = viewModel.MainImageUrl;
            product.Description = viewModel.Description;

            product.Images.Clear();

            if (!string.IsNullOrWhiteSpace(viewModel.MainImageUrl))
            {
                product.Images.Add(new ProductImage
                {
                    ImageUrl = viewModel.MainImageUrl,
                    AltText = $"{viewModel.Name} hoofdafbeelding"
                });
            }

            if (!string.IsNullOrWhiteSpace(viewModel.SubImageUrl))
            {
                product.Images.Add(new ProductImage
                {
                    ImageUrl = viewModel.SubImageUrl,
                    AltText = $"{viewModel.Name} subafbeelding"
                });
            }

            product.Specifications.Clear();

            if (!string.IsNullOrWhiteSpace(viewModel.SpecificationName) &&
                !string.IsNullOrWhiteSpace(viewModel.SpecificationValue))
            {
                product.Specifications.Add(new ProductSpecification
                {
                    SpecName = viewModel.SpecificationName,
                    SpecValue = viewModel.SpecificationValue
                });
            }

            _productRepository.UpdateProduct(product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);

            return RedirectToAction("Index");
        }

        private IEnumerable<Product> SortProducts(IEnumerable<Product> products, string sortBy, string sortDirection)
        {
            var descending = sortDirection == "desc";

            return sortBy.ToLower() switch
            {
                "product" => descending
                    ? products.OrderByDescending(p => p.Name)
                    : products.OrderBy(p => p.Name),

                "prijs" => descending
                    ? products.OrderByDescending(p => p.Price)
                    : products.OrderBy(p => p.Price),

                "categorie" => descending
                    ? products.OrderByDescending(p => p.Category)
                    : products.OrderBy(p => p.Category),

                "voorraad" => descending
                    ? products.OrderByDescending(p => p.Stock)
                    : products.OrderBy(p => p.Stock),

                "toegevoegd" => descending
                    ? products.OrderByDescending(p => p.AddedDate)
                    : products.OrderBy(p => p.AddedDate),

                "leverancier" => descending
                    ? products.OrderByDescending(p => p.Supplier)
                    : products.OrderBy(p => p.Supplier),

                _ => descending
                    ? products.OrderByDescending(p => p.Id)
                    : products.OrderBy(p => p.Id)
            };
        }
    }
}