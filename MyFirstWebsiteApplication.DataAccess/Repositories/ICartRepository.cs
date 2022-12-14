using MyFirstWebsiteApplication.DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebsiteApplication.DataAccess.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        int IncrementCartItem(Cart cart, int count);
    }
}
