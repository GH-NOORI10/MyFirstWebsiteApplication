using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MyFirstWebsiteApplication.DataAccess.Repositories;
using MyFirstWebsiteApplication.DataAccess.ViewModels;
using System.Security.Claims;

namespace MyFirstWebsiteApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CartVM itemList { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM itemList = new CartVM()
            {
                ListOfCart = (IEnumerable<Cart>)_unitOfWork.Cart.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Product")
            };
            foreach (var item in itemList.ListOfCart)
            {
                itemList.Total += (item.Product.Price * item.Count);
            }
            return View(itemList);
        }
    }
}
