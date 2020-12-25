using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MonitorFolderService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IEmailSender emailSender;
        private FileSystemWatcher watcher;
        private readonly string directory = @"C:\Users\User\Desktop\MyFolder";
        public Worker(ILogger<Worker> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = directory;
            watcher.Created += OnChanged;
            return base.StartAsync(cancellationToken);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            SendEmail(e.FullPath);
        }

        private void SendEmail(string fullPath)
        {
            _logger.LogInformation("A new message about schedule: {time}", DateTimeOffset.Now);
            var message = new EmailService.Message(new string[] { "tulenbayev7@gmail.com" }, "Subject testing", fullPath);
            _emailSender.SendEmail(message);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                watcher.EnableRaisingEvents = true;
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(4000, stoppingToken);
            }
        }

    }
}
