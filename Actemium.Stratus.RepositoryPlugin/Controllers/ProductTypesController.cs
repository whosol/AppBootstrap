using System;
using System.Linq;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.Contracts.Base;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProductTypesController : StratusBaseController<ProductType, ProductTypeDto, ProductTypesDto>
    {
        public ProductTypesController(IStratusUnitOfWork uow, ILogger logger)
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

        public override ProductTypesDto GetAll()
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

        public override ProductTypeDto GetById(int id)
        {
            var productType = uow.ProductTypes.FindById(id);
            return productType != null ? CreateDto(productType) : null;
        }
    }
}
