using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebsiteApplication.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICartRepository Cart { get; }
        IApplicationUser ApplicationUser { get; }

        void Save();
    }
}
