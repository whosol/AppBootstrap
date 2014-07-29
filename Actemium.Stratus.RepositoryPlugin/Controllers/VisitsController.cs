using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.Contracts.Base;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using WhoSol.Contracts;
using WhoSol.Contracts.Enums;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    [RoutePrefix("api/visits")]
    public class VisitsController : StratusBaseController<Visit, VisitDto, VisitsDto>
    {
        private readonly IImporter importer;

        public VisitsController(IStratusUnitOfWork uow, ILogger logger, IImporter importer)
            : base(uow, logger)
        {
            this.importer = importer;
        }

        public override VisitDto CreateDto(Visit dataObject)
        {
            return CreateVisitDto(dataObject);
        }

        public override VisitsDto GetAll()
        {
            return GetByPageAndSize(0, 100);
        }

        public override VisitDto GetById(int id)
        {
            return Get(id, false);
        }

        public VisitsDto GetByPageAndSize(int page, int pageSize, bool getXml = false)
        {
            var query = uow.Visits.FindAll()
                .Include(v => v.Product)
                .OrderBy(v => v.EndTime);

            if (page > 0)
            {
                query = (IOrderedQueryable<Visit>)query.Skip((page - 1) * pageSize);
            }

            var visits = query.Take(pageSize).ToList();

            return new VisitsDto
            {
                Visits = visits.Select(v => CreateVisitDto(v)).ToArray(),
                Total = uow.Visits.FindAll().Count(),
                Page = page,
                PageSize = pageSize
            };
        }

        [Route("{id}/getXml={getXml}")]
        public VisitDto Get(int? id, bool getXml = false)
        {
            if (id != null)
            {
                return CreateVisitDto(uow.Visits.FindBy(v => v.Id == id).Include(v => v.Product).SingleOrDefault(), getXml);
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
                    .Select(v => CreateVisitDto(v, getXml)),
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


        private static VisitDto CreateVisitDto(Visit v, bool getXml = false)
        {
            return v != null ?
                new VisitDto
                {
                    ProductUniqueId = v.Product.ProductUniqueId,
                    Id = v.Id,
                    StartTime = v.StartTime,
                    EndTime = v.EndTime,
                    Duration = v.Duration,
                    Status = v.Status.ToString(),
                    VisitXml = getXml ? v.VisitXml : null
                } : null;
        }
    }
}
