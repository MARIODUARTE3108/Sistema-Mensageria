using Microsoft.Extensions.Options;
using Projeto05.Services.Api.Settings;
using System.Reflection;
using System.Text;

namespace Projeto05.Services.Api.Helpers
{
    public class LogHelper
    {
        private readonly LogSettings _logSettings;

        public LogHelper(IOptions<LogSettings> logSettings)
        {
            _logSettings = logSettings.Value;
        }
        public void Create(string message, LogType logType)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var file = Path.Combine($"{path}\\{_logSettings.FileName}");

            if (!File.Exists(file))
            {
                var stream = File.Create(file);
                stream.Close();
            }

            using(var stream = File.AppendText(file))
            {
                var builder = new StringBuilder();

                builder.Append(DateTime.Now.ToString("dd/MM/yyyy HH:nn:ss"));
                builder.Append(" - ");
                builder.Append(logType.ToString());
                builder.Append(" - ");
                builder.Append(message);

               stream.WriteLine(builder.ToString());
            }
        }
    }
    public enum LogType
    {
        INFO, ERROR
    }
}
