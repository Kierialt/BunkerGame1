namespace BunkerGame.Backend.Utils;
using System;
using System.IO;

public class BackupCreator
{
    private readonly string _sourceDbPath;
    private readonly string _backupDir;

    public BackupCreator(string sourceDbPath, string backupDir)
    {
        _sourceDbPath = sourceDbPath;
        _backupDir = backupDir;
    }

    public void CreateBackup()
    {
        if (!Directory.Exists(_backupDir))
        {
            Directory.CreateDirectory(_backupDir);
        }

        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string backupFilePath = Path.Combine(_backupDir, $"backup_{timestamp}.db");

        File.Copy(_sourceDbPath, backupFilePath, overwrite: true);
        Console.WriteLine($"Backup created: {backupFilePath}");
    }
}