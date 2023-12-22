using APICatalog.Context;

namespace APICatalog.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProductRepository _productRepo;
        private CategoryRepository _categoryRepo;
        public ApplicationDbContext _context { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepo = _productRepo ?? new ProductRepository(_context);
            }
        }


        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepo = _categoryRepo ?? new CategoryRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose(); 
        }
    }
}
