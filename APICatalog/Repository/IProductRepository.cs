using APICatalog.Models;

namespace APICatalog.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductByPrice();
    }
}
