using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.SellerDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.RapidApiServices.CatalogProductServices;
using MultiShop.WebUI.Models.Requests;
using MultiShop.DtoLayer.RapidApiDtos.CatalogProductDtos;

namespace MultiShop.WebUI.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    [Route("Seller/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISellerService _sellerService;
        private readonly ILoginService _loginService;
        private readonly IProductSearchService _productSearchService;

        public ProductController(IProductService productService, ICategoryService categoryService, ISellerService sellerService, ILoginService loginService, IProductSearchService productSearchService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sellerService = sellerService;
            _loginService = loginService;
            _productSearchService = productSearchService;
        }

        private async Task<ResultSellerDto> GetCurrentSeller()
        {
            var sellers = await _sellerService.GetAllSellersAsync();
            if (sellers == null)
            {
                return null;
            }
            return sellers.FirstOrDefault(x => x.OwnerUserId == _loginService.GetUserId && x.Status == "Approved");
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            var values = await _productService.GetProductsBySellerIdAsync(seller.SellerId);
            if (values == null)
            {
                return View(new List<ResultProductDto>());
            }
            return View(values);
        }

        [Route("CreateProduct")]
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            await CategoryDropdown();
            return View();
        }

        [Route("CreateProduct")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            if (createProductDto == null)
            {
                await CategoryDropdown();
                return View();
            }
            ModelState.Remove("ProductPrice");
            createProductDto.ProductPrice = ParsePrice(Request.Form["ProductPrice"]);
            if (!ModelState.IsValid)
            {
                await CategoryDropdown();
                return View(createProductDto);
            }
            createProductDto.SellerId = seller.SellerId;
            var error = await _productService.CreateProductAsync(createProductDto);
            if (error != null)
            {
                ModelState.AddModelError("", error);
                await CategoryDropdown();
                return View(createProductDto);
            }
            return RedirectToAction("Index");
        }

        [Route("SearchMarket")]
        [HttpGet]
        public async Task<IActionResult> SearchMarket(string q)
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return Unauthorized();
            }
            if (string.IsNullOrWhiteSpace(q))
            {
                return Json(new List<ProductSearchDto>());
            }
            var values = await _productSearchService.SearchAsync(new ProductSearchRequest { ProductName = q });
            return Json(values);
        }

        [Route("UpdateProduct/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            var values = await _productService.GetProductByIdAsync(id);
            if (values == null || values.SellerId != seller.SellerId)
            {
                return RedirectToAction("Index");
            }
            await CategoryDropdown();
            return View(values);
        }

        [Route("UpdateProduct/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            if (updateProductDto == null)
            {
                return RedirectToAction("Index");
            }
            ModelState.Remove("ProductPrice");
            updateProductDto.ProductPrice = ParsePrice(Request.Form["ProductPrice"]);
            if (!ModelState.IsValid)
            {
                await CategoryDropdown();
                return View(updateProductDto);
            }
            var current = await _productService.GetProductByIdAsync(updateProductDto.ProductId);
            if (current == null || current.SellerId != seller.SellerId)
            {
                return RedirectToAction("Index");
            }
            updateProductDto.SellerId = seller.SellerId;
            var error = await _productService.UpdateProductAsync(updateProductDto);
            if (error != null)
            {
                ModelState.AddModelError("", error);
                await CategoryDropdown();
                return View(updateProductDto);
            }
            return RedirectToAction("Index");
        }

        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            var current = await _productService.GetProductByIdAsync(id);
            if (current == null || current.SellerId != seller.SellerId)
            {
                return RedirectToAction("Index");
            }
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }

        private decimal ParsePrice(string price)
        {
            if (string.IsNullOrWhiteSpace(price))
            {
                return 0;
            }
            var normalized = price.Trim().Replace(",", ".");
            decimal result;
            if (decimal.TryParse(normalized, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result))
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

            var variantMap = new Dictionary<string, string>();
            foreach (var x in categoryList)
            {
                variantMap[x.CategoryID] = x.VariantAttributes ?? "";
            }
            ViewBag.CategoryVariantMap = System.Text.Json.JsonSerializer.Serialize(variantMap);
        }
    }
}
