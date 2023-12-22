using APICatalog.DTOs;
using APICatalog.Models;
using APICatalog.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork context, IMapper mapper)
        {
            _uow = context;
            _mapper = mapper;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProductDTO>> GetProductsPrices()
        {
            var products = _uow.ProductRepository.GetProductByPrice().ToList();
            var productsDto = _mapper.Map<List<ProductDTO>>(products);

            return productsDto;
        }

        // api/produtos
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            var products = _uow.ProductRepository.Get().ToList();
            var productsDto = _mapper.Map<List<ProductDTO>>(products);

            return productsDto;
        }

        // api/produtos/1
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var product = _uow.ProductRepository.GetById(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var productDTO = _mapper.Map<ProductDTO>(product);
            return productDTO;
        }

        //  api/produtos
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _uow.ProductRepository.Add(product);
            _uow.Commit();

            var productDTO = _mapper.Map<ProductDTO>(product);
            return new CreatedAtRouteResult("ObterProduto",
                new { id = product.ProductId }, productDTO);
        }

        // api/produtos/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDto);

            _uow.ProductRepository.Update(product);
            _uow.Commit();
            return Ok();
        }

        //  api/produtos/1
        [HttpDelete("{id}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            var product = _uow.ProductRepository.GetById(p => p.ProductId == id);
            //var produto = _uow.Produtos.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _uow.ProductRepository.Delete(product);
            _uow.Commit();

            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }
    }
}
