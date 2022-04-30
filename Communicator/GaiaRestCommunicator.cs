using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ASRR.Gaia.Service;
using Newtonsoft.Json;
using NLog;

namespace ASRR.Gaia.Controller
{
    public class GaiaRestCommunicator
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public bool isAllowed = false;
        private readonly NodeInformationService _localUptimeService;

        public GaiaRestCommunicator(string company, string project, string application)
        {
            _localUptimeService = new NodeInformationService(company, project, application);

            // Task<bool> task = Task.Run(async () => await RegisterApplicationAsync());
            // _logger.Info($"Uptime tool responded with {task.Result}");
            // isAllowed = task.Result;

            new Thread(() =>
            {
                while (true)
                {
                    //_logger.Info("Sending response to uptime tool");
                    Task.Run(async () => await UpdateNodeAsync());
                    Thread.Sleep(10000);
                }
            }).Start();
        }

        public async Task UpdateNodeAsync()
        {
            _logger.Info("Updating node");
            var payload = JsonConvert.SerializeObject(_localUptimeService.GetUpdate());
            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var response = await client.PutAsync("https://gaia.kube.asrr.nl/api/v1/application/update-node", httpContent);
            _logger.Info(response);
        }
    }
}