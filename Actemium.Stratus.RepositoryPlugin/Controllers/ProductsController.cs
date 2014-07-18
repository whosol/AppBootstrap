using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System.Linq;
using System.Web.Http;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IUnitOfWork uow;
        public ProductsController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public ProductsDto Get()
        {
            return new ProductsDto 
            {
                Products = uow.Products.FindAll()
                .OrderBy(p=>p.ProductUniqueId)
                .Select(p=> new ProductDto 
                {
                    Id = p.Id,
                    ProductUniqueId = p.ProductUniqueId
                })
            };
        }
    }
}
