using Actemium.Stratus.Contracts;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Actemium.Stratus.RepositoryPlugin.Controllers.Wrapper;
using Ninject.Extensions.Logging;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    [RoutePrefix("api/Results")]
    public class ResultsController : BaseController
    {
        public ResultsController(IUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public ResultsDto Get(int page = 0, int pageSize = 100)
        {
            try
            {
                var query = uow.Results.FindAll()
                    .Include(r => r.Sequence)
                    .Include(r => r.ResultDescription)
                    .Include(r => r.Product)
                    .OrderBy(r => r.Id);

                if (page > 0)
                {
                    query = (IOrderedQueryable<Result>)query.Skip((page - 1) * pageSize);
                }

                var results = query.Take(pageSize).ToArray();


                return new ResultsDto
                {
                    Results = results.Select(r => new
                        ResultDto
                        {
                            ProductUniqueId = r.Product.ProductUniqueId,
                            SequenceName = r.Sequence.Name,
                            ResultName = r.ResultDescription.Name,
                            Value = r.Value,
                            UpperLimit = r.UpperLimit,
                            LowerLimit = r.LowerLimit,
                            Units = r.Units
                        }).ToArray(),
                    Total = uow.Results.FindAll().Count()
                };

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result Get(int id)
        {
            return uow.Results.FindById(id);
        }

        public HttpResponseMessage Get([FromUri]string productUniqueId, [FromUri]string[] sequences = null, [FromUri]bool visitInfo = false, [FromUri]bool sequenceVisitInfo = false)
        {
            var sequenceResultsMap = GetSequenceResultMap(sequences, productUniqueId);

            var sequenceExecutions = SequencesForProductId(productUniqueId);

            var xml = CreateRds3Response(sequenceResultsMap, sequenceExecutions, productUniqueId, visitInfo, sequenceVisitInfo);

            return new HttpResponseMessage { Content = new StringContent(xml.ToString(), Encoding.UTF8, "application/xml") };
        }

        private List<SequenceExecution> SequencesForProductId(string productUniqueId)
        {
            return uow.SequenceExecutions
                .FindBy(se => se.Visit.Product.ProductUniqueId == productUniqueId)
                .Include(se => se.Visit)
                .Include(se => se.Visit.Process)
                .OrderBy(se => se.Sequence.Name)
                .OrderByDescending(se => se.StartTime)
                .ToList();
        }


        private IOrderedEnumerable<IGrouping<string, Result>> GetSequenceResultMap(string[] sequences, string productUniqueId)
        {
            IQueryable<Result> initialQuery;

            if (sequences.Length > 0)
            {
                initialQuery = uow.Results
                                  .FindBy(r => r.Product.ProductUniqueId == productUniqueId &&
                                               sequences.Any(s => r.Sequence.Name == s) &&
                                               r.ResultSource == ResultSource.Product);
            }
            else
            {
                initialQuery = uow.Results
                                  .FindBy(r => r.Product.ProductUniqueId == productUniqueId &&
                                               r.ResultSource == ResultSource.Product);
            }

            var sequenceResultsMap = initialQuery
                    .Include(r => r.ResultDescription)
                    .Include(r => r.SequenceExecution)
                    .Include(r => r.Sequence)
                    .Include(r => r.ParentResult.ResultDescription)
                    .OrderByDescending(r => r.SequenceExecution.StartTime)
                    .ToLookup(r => r.ResultDescription.Name)
                    .OrderBy(r => r.Key)
                    .Select(g => g.FirstOrDefault())
                    .OrderBy(r => r.Id)
                    .ToLookup(r => r.Sequence.Name)
                    .OrderBy(g => g.Key);
            return sequenceResultsMap;
        }

        private List<ResultWrapper> AddChildren(List<Result> results, string parentName = null, int level = 0)
        {
            var remainingResults = parentName == null ?
                results.Where(r => r.ParentResult != null).ToList() :
                results.Where(r => r.ParentResult.ResultDescription.Name != parentName).ToList();

            var resultsMap = parentName == null ?
                results.Where(r => r.ParentResult == null)
                       .Select(r => new ResultWrapper { Result = r, Level = level })
                       .ToDictionary(r => r.Result.ResultDescription.Name) :
                results.Where(r => r.ParentResult != null)
                       .Where(r => r.ParentResult.ResultDescription.Name == parentName)
                       .Select(r => new ResultWrapper { Result = r, Level = level })
                       .ToDictionary(r => r.Result.ResultDescription.Name);

            foreach (var result in remainingResults)
            {
                if (resultsMap.ContainsKey(result.ParentResult.ResultDescription.Name))
                {
                    resultsMap[result.ParentResult.ResultDescription.Name].ChildResults = AddChildren(remainingResults, result.ParentResult.ResultDescription.Name, level + 1);
                }
            }

            return resultsMap.Values.ToList();
        }

        private XElement CreateRds3Response(IOrderedEnumerable<IGrouping<string, Result>> sequenceResultsMap, List<SequenceExecution> sequenceExecutions, string productUniqueId, bool visitInfo, bool sequenceVisitInfo)
        {
            var sequenceWrappers = new List<SequenceWrapper>();
            foreach (var sequence in sequenceResultsMap)
            {
                sequenceWrappers.Add(new SequenceWrapper
                {
                    Name = sequence.Key,
                    Executions = sequenceExecutions.Where(se => se.Sequence != null && se.Sequence.Name == sequence.Key).ToList(),
                    Results = AddChildren(sequence.ToList())
                });
            }

            var flag = sequenceWrappers.Any(s => s.CurrentStatus == (SequenceStatus.Fail | SequenceStatus.Aborted)) || !sequenceWrappers.Any() ? "F" : "P";

            var xml = new XElement("HISTORY", new XAttribute("rds", "3.0"),
                new XAttribute("vin", productUniqueId),
                new XAttribute("flags", flag));

            if (visitInfo)
                xml.Add(CreateRds3HistoricVisits(sequenceWrappers));

            foreach (var sequence in sequenceWrappers)
            {
                xml.Add(sequence.GetRds3(sequenceVisitInfo));
            }
            return xml;
        }

        private XElement CreateRds3HistoricVisits(List<SequenceWrapper> sequences)
        {
            var ret = new XElement("HVISITS");

            ret.Add(sequences.Select(s => s.Executions.Select(se =>
                new XElement("HPF",
                new XAttribute("date", se.StartTime.ToString("dd-MMM-yyyy HH:mm:ss")),
                new XAttribute("flags", se.Status == SequenceStatus.Pass ? "P" : "F"),
                new XAttribute("name", se.Visit.Process.Name)))));

            return ret;
        }

    }
}
