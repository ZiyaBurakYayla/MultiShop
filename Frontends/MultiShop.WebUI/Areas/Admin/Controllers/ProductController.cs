using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;

namespace MultiShop.WebUI.Controllers.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISellerService _sellerService;
        private readonly IProductDetailService _productDetailService;
        private readonly IProductImageService _productImageService;

        public ProductController(IProductService productService, ICategoryService categoryService, ISellerService sellerService, IProductDetailService productDetailService, IProductImageService productImageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sellerService = sellerService;
            _productDetailService = productDetailService;
            _productImageService = productImageService;
        }

        [Route("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var values = await _productService.GetProductWithCategoryAsync();
            if (values == null)
            {
                return View(new List<ResultProductWithCategoryDto>());
            }
            await SellerNames();
            return View(values);
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _productService.GetAllProductsAsync();
            if (values == null)
            {
                return View(new List<ResultProductDto>());
            }
            await SellerNames();
            return View(values);
        }

        [Route("ProductView/{id}")]
        public async Task<IActionResult> ProductView(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("ProductListWithCategory");
            }
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return RedirectToAction("ProductListWithCategory");
            }

            var model = new AdminProductViewModel
            {
                Product = product,
                Detail = await _productDetailService.GetByProductIdProductDetailAsync(id),
                Images = await _productImageService.GetByProductIdProductImageAsync(id)
            };

            var categories = await _categoryService.GetAllCategoriesAsync();
            var category = categories?.FirstOrDefault(x => x.CategoryID == product.CategoryId);
            model.CategoryName = category?.CategoryName;

            if (!string.IsNullOrEmpty(product.SellerId))
            {
                var sellers = await _sellerService.GetAllSellersAsync();
                var seller = sellers?.FirstOrDefault(x => x.SellerId == product.SellerId);
                model.SellerName = seller?.StoreName;
            }

            return View(model);
        }

        [Route("CreateProduct")]
        [HttpGet]
        public async Task<IActionResult> CreateProduct(string productName, string productImageUrl, string productPrice)
        {
            await CategoryDropdown();
            await SellerDropdown();
            var createProductDto = new CreateProductDto
            {
                ProductName = productName,
                ProductImageUrl = productImageUrl,
                ProductPrice = ParsePrice(productPrice)
            };
            return View(createProductDto);
        }

        [Route("CreateProduct")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                return RedirectToAction("ProductListWithCategory");
            }
            ModelState.Remove("ProductPrice");
            createProductDto.ProductPrice = ParsePrice(Request.Form["ProductPrice"]);

            if (string.IsNullOrEmpty(createProductDto.SellerId))
            {
                ModelState.AddModelError("", "Ürünün atanacağı satıcıyı seçiniz.");
            }

            if (!ModelState.IsValid)
            {
                await CategoryDropdown();
                await SellerDropdown();
                return View(createProductDto);
            }

            var error = await _productService.CreateProductAsync(createProductDto);
            if (error != null)
            {
                ModelState.AddModelError("", error);
                await CategoryDropdown();
                await SellerDropdown();
                return View(createProductDto);
            }
            return RedirectToAction("ProductListWithCategory");
        }

        private decimal ParsePrice(string price)
        {
            if (string.IsNullOrWhiteSpace(price))
            {
                return 0;
            }
            var normalized = price.Trim().Replace(",", ".");
            var digits = "";
            foreach (var c in normalized)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    digits += c;
                }
            }
            decimal result;
            if (decimal.TryParse(digits, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result))
            {
                return result;
            }
            return 0;
        }

        private async Task CategoryDropdown()
        {
            var categoryList = await _categoryService.GetAllCategoriesAsync();
            List<SelectListItem> categoryValues = (from x in categoryList
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;
        }

        private async Task SellerDropdown()
        {
            var sellers = await _sellerService.GetAllSellersAsync();
            var sellerValues = new List<SelectListItem>();
            if (sellers != null)
            {
                foreach (var x in sellers)
                {
                    if (x.Status == "Approved")
                    {
                        sellerValues.Add(new SelectListItem { Text = x.StoreName, Value = x.SellerId });
                    }
                }
            }
            ViewBag.SellerValues = sellerValues;
        }

        private async Task SellerNames()
        {
            var sellers = await _sellerService.GetAllSellersAsync();
            if (sellers != null)
            {
                ViewBag.SellerNames = sellers.ToDictionary(x => x.SellerId, x => x.StoreName);
            }
        }
    }
}
