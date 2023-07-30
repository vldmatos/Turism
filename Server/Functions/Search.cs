using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Server.Settings;
using Services.Infraestructure.Cache;
using Services.Infrastructure.Data;
using Shared;
using Shared.Entities;
using Shared.Entities.Units;
using Shared.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Server.Functions
{
    public class Search
    {
        private readonly Repository _repository;
        private readonly ICache _cache;

        public Search(Repository repository, ICache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        [FunctionName("search")]
        [OpenApiOperation(operationId: nameof(Run), tags: new[] { nameof(Search) })]
        [OpenApiRequestBody(ServerSettings.ContentTypeJson, typeof(Filter), Description = "JSON request search body")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest request)
        {
            string body = await new StreamReader(request.Body).ReadToEndAsync();
            
            var filter = JsonConvert.DeserializeObject<Filter>(body);
            if (filter is null)
                return new BadRequestObjectResult("Invalid filter to execute searching.");

            var view = Assembly.GetAssembly(typeof(View))
                               .GetType(filter.ResultType);

            if (view is null)
                return new BadRequestObjectResult("Invalid type of view to result of search.");           

            if (filter.Operation == "all")
            {
                var cached = await _cache.FindAll<IEnumerable<Entity>>(filter.Source);
                if (cached is null)
                {
                    var result = await _repository.GetAll(new Partner());
                    if (result is not null)
                        await _cache.SetAll(filter.Source, cached, TimeSpan.FromHours(ServerSettings.HoursFromCache));
                }

                return new OkObjectResult(cached);
            }

            return new OkObjectResult(null);
        }
    }
}