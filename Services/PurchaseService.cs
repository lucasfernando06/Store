using FluentValidation.Results;
using Store.Data.UnitOfWork;
using Store.Domain;
using Store.Interfaces.Repository;
using Store.Interfaces.Service;
using Store.Models.ItemProduct;
using Store.Models.Product;
using Store.Models.Purchase;
using Store.Models.User;
using Store.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IPurchaseRepository purchaseRepository, IUnitOfWork unitOfWork,
            IProductRepository productRepository, IAuthService authService)
        {
            _purchaseRepository = purchaseRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _authService = authService;
        }

        public async Task Insert(PurchaseModel model)
        {
            var loggedUser = await _authService.GetLoggedUser();

            if (!model.Products.Any())
                throw new Exception("A compra está vazia");

            var purchase = await CreateNewPurchase(model, loggedUser);

            await _purchaseRepository.AddAsync(purchase);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Purchase> CreateNewPurchase(PurchaseModel model, User user)
        {
            var purchase = new Purchase(model.Name, user);
            var list = new List<ItemProduct>();

            foreach (var productModel in model.Products)
            {
                var product = await _productRepository.GetById(productModel.Id);

                if (product != null)
                {
                    if (product.Stock < productModel.Amount)
                        throw new Exception("O produto " + product.Name + " possui apenas " + product.Stock + " unidade(s)");

                    list.Add(new ItemProduct(purchase, product, productModel.Amount));
                    product.SetStock(product.Stock - productModel.Amount);
                }
            }

            purchase.SetItens(list);
            purchase.CalculateValue();
            purchase.Validation();

            return purchase;            
        }    

        public async Task<IEnumerable<PurchaseListModel>> List()
        {
            var loggedUser = await _authService.GetLoggedUser();

            var list = new List<PurchaseListModel>();     
            var dbList = await _purchaseRepository.List(loggedUser);

            foreach (var purchase in dbList)            
                list.Add(FillListModel(purchase));           

            return list;
        }

        public async Task<PurchaseListModel> Get(Guid id)
        {
            var loggedUser = await _authService.GetLoggedUser();

            var purchase = await _purchaseRepository.Get(id, loggedUser);
            return FillListModel(purchase);        
        }        

        private PurchaseListModel FillListModel(Purchase purchase)
        {
            var model = new PurchaseListModel
            {
                Id = purchase.Id,
                Title = purchase.Title,
                User = new UserListModel
                {
                    Name = purchase.User.Name,
                    Cpf = purchase.User.Cpf
                },                
                Value = purchase.Value.FormatValue(),
                RegistrationDate = purchase.RegistrationDate
            };

            if (purchase.Itens != null && purchase.Itens.Any())
            {
                model.Products = purchase.Itens.Select(i => new ItemProductListModel
                {
                    Description = i.Product.Name,
                    Name = i.Product.Name,
                    UnitPrice = i.Product.Price.FormatValue(),
                    Stock = i.Product.Stock,
                    Total = i.Value.FormatValue(),
                    Amount = i.Amount
                });
            }

            return model;
        }
    }
}
