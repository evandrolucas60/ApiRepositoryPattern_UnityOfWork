using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ProductsController(IUnitOfWork context)
        {
            _uow = context;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Product>> GetProductsPrices()
        {
            return _uow.ProductRepository.GetProductByPrice().ToList();
        }

        // api/produtos
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _uow.ProductRepository.Get().ToList();
        }

        // api/produtos/1
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Product> Get(int id)
        {
            var product = _uow.ProductRepository.GetById(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        //  api/produtos
        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            _uow.ProductRepository.Add(product);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = product.ProductId }, product);
        }

        // api/produtos/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _uow.ProductRepository.Update(product);
            _uow.Commit();
            return Ok();
        }

        //  api/produtos/1
        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            var product = _uow.ProductRepository.GetById(p => p.ProductId == id);
            //var produto = _uow.Produtos.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _uow.ProductRepository.Delete(product);
            _uow.Commit();
            return product;
        }
    }
}
