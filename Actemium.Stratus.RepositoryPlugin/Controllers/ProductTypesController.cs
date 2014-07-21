using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProductTypesController : BaseController
    {
        public ProductTypesController(IUnitOfWork uow)
            : base(uow)
        {

        }

        public ProductTypesDto Get()
        {
            return new ProductTypesDto
            {
                ProductTypes = new[]{ 
                    new ProductTypeDto
                    {
                        Id = -1,
                        Name = "[All]"
                    }
                }
                .Concat(uow.ProductTypes.FindAll()
                .OrderBy(pt => pt.Name)
                .Select(pt => new ProductTypeDto
                {
                    Id = pt.Id,
                    Name = pt.Name
                }))
            };
        }
    }
}
