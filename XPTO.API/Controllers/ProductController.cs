using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPTO.API.Dtos;
using XPTO.Domain;
using XPTO.Repository;
using XPTO.Repository.Interfaces;

namespace XPTO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository ProductRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = ProductRepository;
        }

        [HttpGet]// api/Product
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _productRepository.GetAllProductAsync();
                var results = _mapper.Map<ProductDto[]>(products);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }
        }
        [HttpGet("{ProductId}")]// api/Product/{id}
        public async Task<IActionResult> Get(int ProductId)
        {
            try
            {
                var product = await _productRepository.GetProductAsyncById(ProductId);
                var results = _mapper.Map<ProductDto>(product);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("ByTitle/{title}")]// api/Product/ByTitle/{title}
        public async Task<IActionResult> ByTitle(string title)
        {
            try
            {
                var product = await _productRepository.GetProductAsyncByTitle(title);

                var results = _mapper.Map<ProductDto[]>(product);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("ByBarCode/{barCode}")]// api/Product/ByBarCode/{barCode}
        public async Task<IActionResult> ByBarCode(string barCode)
        {
            try
            {
                var product = await _productRepository.GetProductAsyncByBarCode(barCode);

                var results = _mapper.Map<ProductDto>(product);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(ProductDto model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);

                product.DateInsert = DateTime.Now;
                _productRepository.Add(product);

                if (await _productRepository.SaveChangesAsync())
                {
                    return Created($"/api/Product/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Put(int productId, ProductDto model)
        {
            try
            {
                var product = await _productRepository.GetProductAsyncById(productId);
                if (product == null) return NotFound();

                var productNew = _mapper.Map<Product>(model);

                productNew.Id = productId;
                productNew.DateInsert = product.DateInsert;
                productNew.DateDeactivated = product.DateDeactivated;
                productNew.Active = product.Active;

                _productRepository.Update(productNew);

                if (await _productRepository.SaveChangesAsync())
                {
                    return Created($"/api/Product/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                var product = await _productRepository.GetProductAsyncById(productId);
                if (product == null) return NotFound();

                product.Active = false;
                product.DateDeactivated = DateTime.Now;

                _productRepository.Update(product);

                if (await _productRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }
    }
}