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
using GetOrdersEvents.Function.Domain.Settings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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

        [Function("GetOrdersEventsFunc")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, 
            [FromBody]RequestEvent requestEvent)
        {

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = req.CreateResponse(HttpStatusCode.OK);

            using (FtpClient ftpClient = new("edi.akanea.com", "edieol/virbsh", "4ueTaL69JHeU"))
            {
                try
                {
                    ftpClient.Connect();
                    var files = ftpClient.GetListing("/out")
                        .Where(x => x.Modified >= requestEvent.StartDate && x.Modified <= requestEvent.EndDate);
                    List<Event> events = new();

                    if (files.Any())
                    {
                        foreach (var file in files)
                        {
                            var resultb = ftpClient.DownloadBytes(out byte[] bytes, file.FullName);
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
                    _logger.LogError(ex, "Error while retrieving files from SFTP");
                    response = req.CreateResponse(HttpStatusCode.InternalServerError);
                }
                finally
                {
                    ftpClient.Disconnect();
                }
            }
            return response;
        }
    }
}
