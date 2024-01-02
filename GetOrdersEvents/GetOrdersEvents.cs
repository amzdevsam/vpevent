using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FluentFTP;
using FluentFTP.Helpers;
using GetOrdersEvents.Function.Domain.Helpers;
using GetOrdersEvents.Function.Domain.Models;
using GetOrdersEvents.Function.Domain.Models.Akanea;
using GetOrdersEvents.Function.Domain.Services;
using GetOrdersEvents.Function.Domain.Settings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Renci.SshNet;

namespace GetOrdersEvents
{
    public class GetOrdersEvents
    {
        private readonly ILogger _logger;
        private readonly SftpSettings _sftpSettings;

        public GetOrdersEvents(ILoggerFactory loggerFactory, IOptions<SftpSettings> sftpSettings)
        {
            _logger = loggerFactory.CreateLogger<GetOrdersEvents>();
            _sftpSettings = sftpSettings.Value;
        }

        [Function("GetOrdersEvents")]
        [OpenApiOperation("GetOrdersEvents-Spec", "GetOrdersEvents", Description = "Get events for an ordering party between two dates")]
        [OpenApiRequestBody("application/json", typeof(RequestEvent))]
        [OpenApiResponseWithoutBody(HttpStatusCode.OK, CustomHeaderType = typeof(HttpResponseData))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req, 
            [FromBody]RequestEvent requestEvent)
        {
            var ftpService = new FtpService(new AkaneaFtpCredential
            {
                Host = "edi.akanea.com",
                Username = "edieol/virbsh",
                Password = "4ueTaL69JHeU"
            });
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            if (requestEvent.StartDate > requestEvent.EndDate)
            {
                _logger.LogError($"[GetOrdersEventsFunc] Invalid request : StartDate superior {JsonConvert.SerializeObject(requestEvent)}");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            var response = req.CreateResponse(HttpStatusCode.OK);

            try
            {
                ftpService.Connect();
                var files = ftpService.GetFiles(requestEvent.StartDate, requestEvent.EndDate);
                List<Event> events = new();

                if (files.Any())
                {
                    foreach(var file in files)
                    {
                        var bytes = ftpService.DownloadBytes(file.FullName);
                        string utfString = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                        XmlSerializer serializer = new(typeof(Flux));
                        using (StringReader reader = new(utfString))
                        {
                            var test = serializer.Deserialize(reader) as Flux;
                            events.AddRange(Mapping.FromFluxAkanea(test).Events);
                        }
                    }
                    await response.WriteAsJsonAsync(events);
                }
                else
                    response = req.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[GetOrdersEventsFunc] Error : {ex.Message}");
                response = req.CreateResponse(HttpStatusCode.InternalServerError);
            }
            finally
            {
                ftpService.Disconnect();
            }
            
               
            return response;
        }
    }
}
