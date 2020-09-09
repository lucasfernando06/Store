using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Models;
using Store.Interfaces.Service;
using Store.Models.Product;
using System;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [ApiController]
    public class PurchaseController : BaseController
    {
        private readonly IPurchaseService _purchaseService;     

        public PurchaseController(IPurchaseService purchaseService, IAuthService authService) : base(authService)
        {
            _purchaseService = purchaseService;          
        }

        [Authorize("Bearer")]
        [HttpPost("/purchases")]
        public async Task<IResultModel> Insert(PurchaseModel model)
        {
            await _purchaseService.Insert(model);
            return Response(Ok());
        }

        [Authorize("Bearer")]
        [HttpGet("/purchases")]
        public async Task<IResultModel> GetPurchases()
        {         
            return Response(await _purchaseService.List());
        }

        [Authorize("Bearer")]
        [HttpGet("/purchases/{id}")]
        public async Task<IResultModel> GetPurchase(Guid id)
        {           
            return Response(await _purchaseService.Get(id));
        }       
    }
}
