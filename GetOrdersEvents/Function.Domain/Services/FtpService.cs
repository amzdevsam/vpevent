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

        private readonly FtpClient _ftpClient;
        public FtpService(AkaneaFtpCredential akaneaFtpCredential)
        {
            _ftpClient = new FtpClient(akaneaFtpCredential.Host, akaneaFtpCredential.Username, akaneaFtpCredential.Password);
        }

        public byte[] DownloadBytes(string fullName)
        {
            _ftpClient.DownloadBytes(out byte[] bytes, fullName);
            return bytes;
        }

        public IEnumerable<FtpListItem> GetFiles(DateTime startDate, DateTime endDate)
        {
            return _ftpClient.GetListing("/out")
                        .Where(x => x.Modified >= startDate && x.Modified <= endDate);
        }

        public void Connect()
        {
            if (!_ftpClient.IsConnected)
                _ftpClient.Connect();
        }

        public void Disconnect()
        {
            if (_ftpClient.IsConnected)
                _ftpClient.Disconnect();
        }
    }

    public class AkaneaFtpCredential
    {
         public string Host { get; internal set; }
        public string Username { get; internal set; }
        public string Password { get; internal set; }
    }
}
