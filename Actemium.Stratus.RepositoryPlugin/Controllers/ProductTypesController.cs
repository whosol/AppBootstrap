using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System.Linq;
using System.Web.Http;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProductTypesController : ApiController
    {
        private readonly IUnitOfWork uow;
        public ProductTypesController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public ProductTypesDto Get()
        {
            return new ProductTypesDto
            {
                ProductTypes = uow.ProductTypes.FindAll()
                .OrderBy(pt => pt.Name)
                .Select(pt => new ProductTypeDto
                {
                    Id = pt.Id,
                    Name = pt.Name
                })
            };
        }
    }
}
