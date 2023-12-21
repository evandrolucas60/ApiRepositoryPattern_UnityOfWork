using APICatalog.Context;
using APICatalog.Models;

namespace APICatalog.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetProductByPrice()
        {
            return Get().OrderBy(c => c.Price ).ToList();
        }
    }
}
