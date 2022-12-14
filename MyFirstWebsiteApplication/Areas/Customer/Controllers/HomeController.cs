using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebsiteApplication.DataAccess.Repositories;
using MyFirstWebsiteApplication.DataAccess.ViewModels;
using MyFirstWebsiteApplication.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MyFirstWebsiteApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int? ProductId)
        {
            Cart cart = new Cart()
            {
                Product = _unitOfWork.Product.GetT(x => x.Id == ProductId,
                includeProperties: "Category"),
                Count = 1,
                ProductId = (int)ProductId
            };
            return View(cart);

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Cart cart)
        {
            if (ModelState.IsValid)
            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cart.ApplicationUserId = claims.Value;

                var cartItem = _unitOfWork.Cart.GetT(x => x.ProductId == cart.ProductId &&
                x.ApplicationUserId == claims.Value);


                if (cartItem == null)
                {
                    _unitOfWork.Cart.Add(cart);


                }
                else
                {
                    _unitOfWork.Cart.IncrementCartItem(cartItem, cart.Count);
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }





        //    HttpContext.Session.SetInt32("SessionCart", _unitOfWork
        //        .Cart.GetAll(x => x.ApplicationUserId == claims.Value).ToList().Count);
        //}

        //else
        //{
        //    _unitOfWork.Cart.IncrementCartItem(cartItem, cart.Count);
        //    _unitOfWork.Save();
        //}



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}