using FluentFTP;

namespace GetOrdersEvents.Function.Domain.Services
{
    public interface IFtpService
    {
        byte[] DownloadBytes(string fullName);
        IEnumerable<FtpListItem> GetFiles(DateTime startDate, DateTime endDate);
    }
}