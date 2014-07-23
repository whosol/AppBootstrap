using System;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProductTypesController : BaseController<ProductType, ProductTypeDto, ProductTypesDto>
    {
        public ProductTypesController(IUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public override ProductTypeDto CreateDto(ProductType dataObject)
        {
            return new ProductTypeDto
            {
                Id = dataObject.Id,
                Name = dataObject.Name
            };
        }

        public override ProductTypesDto Get()
        {
            return new ProductTypesDto
            {
                ProductTypes = new[]
                { 
                    new ProductTypeDto
                    {
                        Id = -1,
                        Name = "[All]"
                    }
                }
                .Concat(uow.ProductTypes.FindAll()
                .OrderBy(pt => pt.Name)
                .ToList()
                .Select(pt => CreateDto(pt)))
            };
        }

        public override ProductTypeDto Get(int id)
        {
            var productType = uow.ProductTypes.FindById(id);
            return productType != null ? CreateDto(productType) : null;
        }
    }
}
