using MyFirstWebsiteApplication.DataAccess.Data;
using MyFirstWebsiteApplication.DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebsiteApplication.DataAccess.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int IncrementCartItem(Cart cart, int count)
        {
            cart.Count += count;
            return cart.Count;
        }
    }
}
