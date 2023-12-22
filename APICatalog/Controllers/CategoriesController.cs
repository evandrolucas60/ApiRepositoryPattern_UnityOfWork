using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CategoriesController(IUnitOfWork context)
        {
            _uow = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            //return _context.Categories.Include(p => p.Products).AsNoTracking().ToList();
            return _uow.CategoryRepository.GetCategoriesProducts().ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return _uow.CategoryRepository.Get().ToList(); 
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> Get(int id)
        {
            var category = _uow.CategoryRepository.GetById(c => c.CategoryId == id);

            if (category is null)
            {
                return NotFound("Categoria não encontrada");
            }
            
            return category;
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            _uow.CategoryRepository.Add(category);
            _uow.Commit();
            return new CreatedAtRouteResult("ObterCategoria",
                new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}:int")]
        public ActionResult Put(int id, Category category)
        {
            _uow.CategoryRepository.Update(category);
            _uow.Commit();

            return Ok(category);
        }


        [HttpDelete("{id:int}")]
        public ActionResult<Category> Delete(int id)
        {
            var category = _uow.CategoryRepository.GetById(p => p.CategoryId == id);

            if (category is null) return NotFound("Categoria não localizada...");

            _uow.CategoryRepository.Delete(category);
            _uow.Commit();

            return category;
        }
    }
}
