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
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Server.Functions
{
    public class Execution
    {
        private readonly Repository _repository;        
        private readonly ICache _cache;

        public Execution(Repository repository, ICache cache)
        {
            _repository = repository;
            _cache = cache;            
        }

        [FunctionName("execution")]
        [OpenApiOperation(operationId: nameof(Run), tags: new[] { nameof(Execution) })]
        [OpenApiRequestBody(ServerSettings.ContentTypeJson, typeof(Argument), Description = "JSON request commands body")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest request)
        {
            string body = await new StreamReader(request.Body).ReadToEndAsync();
            var argument = JsonConvert.DeserializeObject<Argument>(body);

            if (argument is null)
                return new BadRequestObjectResult("Invalid format body to request commands.");

            var contextType = Assembly.GetAssembly(typeof(Entity))
                                      .GetType(argument.Type);

            if (contextType is null)
                return new BadRequestObjectResult("Invalid type context of request commands.");

            var context = argument.Context.ToObject(contextType);

            if (argument.Operation == "booking")
            {
                await _repository.Save(context);
            }
            
            if (argument.Operation == "save")
                await _repository.Save(context);

            if (argument.Operation == "delete")
                await _repository.Delete(context);

            await _cache.Remove(argument.Source);

            return new OkObjectResult(context);
        }
    }
}