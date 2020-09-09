using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Models;
using Store.Domain.Enum;
using Store.Interfaces.Service;
using Store.Models.Product;
using System;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;       

        public ProductController(IProductService productService, IAuthService authService) : base(authService)
        {
            _productService = productService;          
        }

        [Authorize("Bearer")]
        [HttpPost("/products")]
        public async Task<IResultModel> Insert(ProductModel model)
        {
            ValidateType(UserType.Employee);
            await _productService.Insert(model);
            return Response(Ok());
        }

        [Authorize("Bearer")]
        [HttpGet("/products")]
        public async Task<IResultModel> List()
        {
            return Response(await _productService.List());
        }

        [Authorize("Bearer")]
        [HttpGet("/products/{id}")]
        public async Task<IResultModel> Get(Guid id)
        {
            return Response(await _productService.Get(id));
        }

        [Authorize("Bearer")]
        [HttpPut("/products")]
        public async Task<IResultModel> Update(EditProductModel editModel)
        {
            ValidateType(UserType.Employee);
            return Response(await _productService.Update(editModel), "Produto Alterado com sucesso");
        }
    }
}
