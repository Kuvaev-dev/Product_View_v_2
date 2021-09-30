using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductView_2_0.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProductView_2_0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Products = ProductRep.GetProducts();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NewProduct() => View();
        
        public IActionResult Edit(int id)
        {
            ViewBag.Edit_product = ProductRep.GetProducts().Find(i => i.id == id);
            return View();
        }
        
        public IActionResult Del(int id)
        {
            ProductRep.Delete(id);
            return Redirect("/Home/Index");
        }
        
        public IActionResult EditProduct(Product product)
        {
            if (CheckProduct(product))
                ProductRep.UpdateProducts(product);
            return Redirect("/Home/Index");
        }
        
        public IActionResult Add(Product product)
        {
            if (CheckProduct(product))
                ProductRep.AddProducts(product);
            
            return Redirect("/Home/NewProduct");
        }
        
        private bool CheckProduct(Product product)
        {
            bool rez = false;
            if (product.model != null && product.model != string.Empty)
                if (product.product != null && product.product != string.Empty)
                    if (product.salesman != null && product.salesman != string.Empty)
                        if (product.category != null && product.category != string.Empty)
                            if (product.description != null && product.description != string.Empty)
                                if (product.price != null)
                                    if (product.img != null && product.img != string.Empty && IsUrl(product.img))
                                        rez = true;
            return rez;
        }
        
        private bool IsUrl(string url)
        {
            bool rez;
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute) && url.EndsWith(".png") || url.EndsWith(".jpg"))
                rez = true;
            else
                rez = false;
            return rez;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
