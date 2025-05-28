namespace BunkerGame.Backend.Services;

using Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class BackupService : BackgroundService
{
    private readonly BackupCreator _backupCreator;
    private readonly TimeSpan _interval = TimeSpan.FromDays(1); // запуск раз в сутки

    public BackupService()
    {
        string dbPath = "/Users/eugene/DataGripProjects/BunkerGameDb";  // путь к базе
        string backupDir = Path.Combine(AppContext.BaseDirectory, "Backup");  // папка Backup
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
