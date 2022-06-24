using indiGo.Business.Repositories.Abstract;
using indiGo.Core.Entities;
using indiGo.Core.Identity;
using indiGo.Core.ViewModels;
using indiGo.Core.ViewModels.PageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace indiGo.Web.Controllers;

[Authorize(Roles = "ADMIN")]
public class AdminController : Controller
{
    private readonly IRepository<Product, int> _productRepository;

    public AdminController(IRepository<Product, int> productRepository)
    {
        _productRepository = productRepository;
    }

    public IActionResult Products()
    {
        var products = _productRepository.Get().ToList();
        var model = new ProductPageViewModel()
        {
            Products = products
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult AddProduct()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddProduct(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            if (ModelState["Price"].Errors.Count <= 0) return View(model);
            ModelState["Price"].Errors.Clear();
            ModelState["Price"].Errors.Add("Uygun bir tutar girilmedi");
            return View(model);
        }

        var product = new Product()
        {
            Name = model.Name,
            Price = model.Price,
            Category = model.Category
        };

        _productRepository.Insert(product);
        _productRepository.Save();

        TempData["ProductCreated"] = "success";
        return View();
    }

    public IActionResult DeleteProduct(int id)
    {
        var product = _productRepository.GetById(id);
        _productRepository.Delete(product);
        _productRepository.Save();
        TempData["ProductDeleted"] = "success";
        return RedirectToAction("Products");
    }

    [HttpPost]
    public IActionResult UpdateProduct(ProductPageViewModel model)
    {
        var product = _productRepository.GetById(model.Product.Id);
        if (product == null)
        {
            TempData["ProductUpdated"] = "error";
        }
        product.Name = model.Product.Name;
        product.Price = model.Product.Price;
        _productRepository.Update(product);
        _productRepository.Save();
        TempData["ProductUpdated"] = "success";
        return RedirectToAction("Products");
    }
}