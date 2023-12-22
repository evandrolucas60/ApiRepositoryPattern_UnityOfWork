using APICatalog.Context;
using APICatalog.DTOs;
using APICatalog.Models;
using APICatalog.Repository;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork context, IMapper mapper)
        {
            _uow = context;
            _mapper = mapper;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            //return _context.Categories.Include(p => p.Products).AsNoTracking().ToList();
            var categories = _uow.CategoryRepository.GetCategoriesProducts().ToList();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);

            return categoriesDto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            var categories = _uow.CategoryRepository.Get().ToList();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);
            return categoriesDto; 
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _uow.CategoryRepository.GetById(c => c.CategoryId == id);

            if (category is null)
            {
                return NotFound("Categoria não encontrada");
            }
            var categoriesDto = _mapper.Map<CategoryDTO>(category);
            return categoriesDto;
        }

        [HttpPost]
        public ActionResult Post(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            _uow.CategoryRepository.Add(category);
            _uow.Commit();

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = category.CategoryId }, categoryDTO);
        }

        [HttpPut("{id}:int")]
        public ActionResult Put(int id, CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            _uow.CategoryRepository.Update(category);
            _uow.Commit();

            return Ok(category);
        }


        [HttpDelete("{id:int}")]
        public ActionResult<CategoryDTO> Delete(int id)
        {
            var category = _uow.CategoryRepository.GetById(p => p.CategoryId == id);

            if (category is null) return NotFound("Categoria não localizada...");

            _uow.CategoryRepository.Delete(category);
            _uow.Commit();

            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return categoryDto;
        }
    }
}
