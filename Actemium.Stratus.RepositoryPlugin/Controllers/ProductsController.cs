using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork uow)
            : base(uow)
        {

        }

        public ProductsDto Get()
        {
            return new ProductsDto
            {
                Products = uow.Products.FindAll()
                .OrderBy(p => p.ProductUniqueId)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    ProductUniqueId = p.ProductUniqueId
                })
            };
        }
    }
}
