namespace BunkerGame.Backend.Services;

using Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;


public class BackupService: BackgroundService
{
    private readonly BackupCreator _backupCreator;
    private readonly TimeSpan _interval = TimeSpan.FromDays(1); // запуск раз в сутки

    public BackupService(IConfiguration configuration)
    {
        // Читаем относительный путь из appsettings.json
        string dbRelativePath = configuration.GetValue<string>("DbPath");

        // Преобразуем в абсолютный путь (относительно папки проекта)
        string dbPath = Path.Combine(AppContext.BaseDirectory, dbRelativePath);

        // Путь к папке для бэкапов
        string backupDir = Path.Combine(AppContext.BaseDirectory, "Backup");

        _backupCreator = new BackupCreator(dbPath, backupDir);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _backupCreator.CreateBackup();
            await Task.Delay(_interval, stoppingToken);
        }
    }
}
