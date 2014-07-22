using System.Net.Http;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System.Web.Http;
using System.Linq;
using System;
using System.Data.Entity;
using Actemium.Stratus.DataObjects;
using System.Xml.Linq;
using System.Net;
using Actemium.Stratus.Contracts.Enums;
using Ninject.Extensions.Logging;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    [RoutePrefix("api/visits")]
    public class VisitsController : BaseController
    {
        private readonly IImporter importer;

        public VisitsController(IUnitOfWork uow, ILogger logger, IImporter importer)
            : base(uow, logger)
        {
            this.importer = importer;
        }

        public VisitsDto Get(int page = 0, int pageSize = 100, bool getXml = false)
        {
            var query = uow.Visits.FindAll()
                .Include(v => v.Product)
                .OrderBy(v => v.EndTime);

            if (page > 0)
            {
                query = (IOrderedQueryable<Visit>)query.Skip((page - 1) * pageSize);
            }

            var visits = query.Take(pageSize).ToArray();

            return new VisitsDto
            {
                Visits = visits.Select(v => new
                    VisitDto
                    {
                        Duration = v.Duration,
                        EndTime = v.EndTime,
                        Id = v.Id,
                        ProductUniqueId = v.Product.ProductUniqueId,
                        StartTime = v.StartTime,
                        Status = v.Status,
                        VisitXml = getXml ? v.VisitXml : null
                    }).ToArray(),
                Total = uow.Visits.FindAll().Count()
            };
        }

        public VisitDto Get(int? id, bool getXml = false)
        {
            if (id != null)
            {
                return CreateVisitDto(getXml, uow.Visits.FindBy(v => v.Id == id).Include(v => v.Product).SingleOrDefault());
            }
            else
            {
                return null;
            }
        }

        [Route("product/{productUniqueId}")]
        public VisitsDto Get(string productUniqueId, bool getXml = false)
        {
            return new VisitsDto
            {
                Visits = uow.Visits.FindBy(v => v.Product.ProductUniqueId == productUniqueId)
                    .Include(v => v.Product)
                    .ToList()
                    .Select(v => CreateVisitDto(getXml, v)),
                Total = uow.Visits.FindBy(v => v.Product.ProductUniqueId == productUniqueId).Count()
            };
        }

        [Route("xml")]
        public HttpResponseMessage Post([FromBody]XDocument visitXml)
        {
            HttpStatusCode response = HttpStatusCode.OK;

            switch (importer.PersistResult(visitXml))
            {
                case PersistStatus.OK:
                    response = HttpStatusCode.OK;
                    break;
                case PersistStatus.Duplicate:
                    response = HttpStatusCode.Conflict;
                    break;
                case PersistStatus.InvalidFormat:
                    response = HttpStatusCode.BadRequest;
                    break;
                case PersistStatus.DatabaseError:
                    response = HttpStatusCode.InternalServerError;
                    break;
                case PersistStatus.NoConnection:
                    response = HttpStatusCode.NotFound;
                    break;
                case PersistStatus.WebApiReject:
                    response = HttpStatusCode.Forbidden;
                    break;
                default:
                    break;
            }

            return new HttpResponseMessage { StatusCode = response };
        }


        private static VisitDto CreateVisitDto(bool getXml, Visit v)
        {
            return v != null ?
                new VisitDto
                {
                    ProductUniqueId = v.Product.ProductUniqueId,
                    Id = v.Id,
                    StartTime = v.StartTime,
                    EndTime = v.EndTime,
                    Duration = v.Duration,
                    Status = v.Status,
                    VisitXml = getXml ? v.VisitXml : null
                } : null;
        }
    }
}
