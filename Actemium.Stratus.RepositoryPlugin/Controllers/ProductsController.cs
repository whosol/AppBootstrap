using System;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;
using System.Data.Entity;
using Actemium.Stratus.DataObjects;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProductsController : BaseController<Product, ProductDto, ProductsDto>
    {
        public ProductsController(IUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public override ProductDto CreateDto(Product dataObject)
        {
            return new ProductDto
            {
                Id = dataObject.Id,
                ProductUniqueId = dataObject.ProductUniqueId,
                ProductType = dataObject.ProductType.Name
            };
        }

        public override ProductDto Get(int id)
        {
            var product = uow.Products.FindBy(p => p.Id == id)
                .Include(p=>p.ProductType)
                .SingleOrDefault();

            return product != null ? CreateDto(product) : null;
        }

        public override ProductsDto Get()
        {
            return new ProductsDto
            {
                Products = uow.Products.FindAll()
                    .Include(p => p.ProductType)
                    .OrderBy(p => p.ProductUniqueId)
                    .ToList()
                    .Select(p => CreateDto(p))
            };
        }

        public ProductDto Get(string productUniqueId)
        {
            var product = uow.Products.FindBy(p => p.ProductUniqueId == productUniqueId)
                .Include(p => p.ProductType)
                .SingleOrDefault();

            return product != null ? CreateDto(product) : null;
        }
    }
}
