using APICatalog.Models;

namespace APICatalog.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesProducts();
    }
}
