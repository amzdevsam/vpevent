using Azure;
using FluentFTP;
using GetOrdersEvents.Function.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetOrdersEvents.Function.Domain.Services
{
    public class FtpService : IFtpService
    {
        private readonly ILogger _logger;

        public FtpService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FtpService>();
        }

        public byte[] DownloadBytes(string fullName)
        {
            byte[] bytes = null;
            using (FtpClient ftpClient = new("edi.akanea.com", "edieol/virbsh", "4ueTaL69JHeU"))
            {
                try
                {
                    ftpClient.Connect();
                    ftpClient.DownloadBytes(out bytes, fullName); 
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while retrieving files from SFTP");
                }
                finally
                {
                    ftpClient.Disconnect();
                }
            }
            return bytes;
        }

        public IEnumerable<FtpListItem> GetFiles(DateTime startDate, DateTime endDate)
        {
            IEnumerable<FtpListItem> files = new List<FtpListItem>();
            using (FtpClient ftpClient = new("edi.akanea.com", "edieol/virbsh", "4ueTaL69JHeU"))
            {
                try
                {
                    ftpClient.Connect();
                    files = ftpClient.GetListing("/out")
                        .Where(x => x.Modified >= startDate && x.Modified <= endDate);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while retrieving files from SFTP");
                }
                finally
                {
                    ftpClient.Disconnect();
                }
            }
            return files;
        }
    }
}
