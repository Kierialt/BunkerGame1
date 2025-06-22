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
    private readonly TimeSpan _interval = TimeSpan.FromDays(1); // runs once a day

    public BackupService(IConfiguration configuration)
    {
        // Read the relative path from appsettings.json
        string? dbRelativePath = configuration.GetValue<string>("DbPath");

        // Convert to absolute path (relative to the project folder)
        string dbPath = Path.Combine(AppContext.BaseDirectory, dbRelativePath ?? throw new InvalidOperationException());

        // Path to the backups folder
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
