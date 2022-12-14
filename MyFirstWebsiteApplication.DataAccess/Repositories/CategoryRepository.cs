using MyFirstWebsiteApplication.DataAccess.Data;
using MyFirstWebsiteApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebsiteApplication.DataAccess.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categoryDB = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (categoryDB != null)
            {
                categoryDB.CatName = category.CatName;
                categoryDB.DisplayOrder = category.DisplayOrder;
            }
        }
    }
}
