using CoreApiResponse;
using Demo.API.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using System.Net;
using Demo.API.Model;

namespace Demo.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : BaseController
    {
        IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProduct(int productId)
        {
            try
            {
                var coupon = await _productRepository.GetProduct(productId);
                return CustomResult(coupon);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetProducts();
                return CustomResult("Data load sucessfully.", products);
            }
            catch (Exception ex)
            {
                return CustomResult($"{ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try 
            {
                var isSaved=await _productRepository.CreateProduct(product);
                if (isSaved)
                {
                    return CustomResult("Product has been created", product);
                }
                return CustomResult("Produt save failed",product,HttpStatusCode.BadRequest);
            } 
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                var isSaved = await _productRepository.UpdateProduct(product);
                if (isSaved)
                {
                    return CustomResult("Product has been modified", product);
                }
                return CustomResult("Produt modification failed", product, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var isDeleted = await _productRepository.DeleteProduct(id);
                if (isDeleted)
                {
                    return CustomResult("Product has been deleted");
                }
                return CustomResult("Product deletion failed",  HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
