using Store.Data.UnitOfWork;
using Store.Domain;
using Store.Interfaces.Repository;
using Store.Interfaces.Service;
using Store.Models.Product;
using Store.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Insert(ProductModel model)
        {
            await _productRepository.AddAsync(new Product(model));
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ProductListModel>> List()
        {
            var dbList = await _productRepository.List();
            var list = new List<ProductListModel>();

            foreach (var product in dbList)
            {
                list.Add(FillListModel(product));
            }

            return list;               
        }

        public async Task<ProductListModel> Get(Guid id)
        {
            var product = await _productRepository.GetById(id);
            return FillListModel(product);
        }

        public async Task<ProductListModel> Update(EditProductModel editModel)
        {
            var product = await _productRepository.GetById(editModel.Id);

            if(product != null)
            {
                product.SetChangeDate();
                product.SetName(editModel.Name);
                product.SetDesription(editModel.Description);
                product.SetPrice(editModel.Price);
                product.SetStock(editModel.Stock);

                await _unitOfWork.CommitAsync();
            }            

            return FillListModel(product);
        }

        private ProductListModel FillListModel(Product product)
        {
            var model = new ProductListModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.FormatValue(),
                Stock = product.Stock
            };           

            return model;
        }
    }
}
