using Application.Interfaces;
using ClamAV.Net.Client.Results;
using ClamAV.Net.Client;
using Core.Exceptions.AV;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ClamAVService : IClamAVService
    {
        const string connectionString = "tcp://127.0.0.1:3310";
        private readonly ILogger<ClamAVService> _logger;

        public ClamAVService(ILogger<ClamAVService> logger)
        {
            _logger = logger;
        }

        public async Task AVCheck(List<IFormFile> files)
        {
            _logger.LogInformation($"Connecting to AV Server={connectionString}");
            IClamAvClient clamAvClient = ClamAvClient.Create(new Uri(connectionString));

            _logger.LogInformation($"Ping AV Server={connectionString}");
            await clamAvClient.PingAsync().ConfigureAwait(false);

            foreach (var file in files)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                ScanResult scanResult = await clamAvClient.ScanDataAsync(memoryStream).ConfigureAwait(false);
                _logger.LogInformation($"Scan result : FileName - {file.FileName} , Infected - {scanResult.Infected}");

                if (scanResult.Infected)
                {
                    throw new InfectedFileException($"Scan result : FileName - {file.FileName} , Infected - {scanResult.Infected} , Virus name - {scanResult.VirusName}");
                }
            }
        }
    }
}
