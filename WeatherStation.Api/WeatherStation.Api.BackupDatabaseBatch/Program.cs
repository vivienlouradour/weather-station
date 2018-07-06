using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherStation.Api.BackupDatabaseBatch
{
    class Program
    {
        private static string _dbBackupFile;
        private static string _dbFile;
        private static string _backupsDirectory;
        private static readonly string _logFile = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".log";
        
        static void Main(string[] args)
        {
            Print(Level.INFO, "Starting backup database batch...");
            if (args.Length != 2)
            {
                Print(Level.ERROR,
                    "Need 2 parameters : database file and backups directory path");
                Environment.Exit(1);
            }

            _dbFile = args[0];
            _backupsDirectory = args[1];
            
            
            if (!File.Exists(_dbFile))
            {
                Print(Level.ERROR, $"1rs parameter error : database file ({_dbFile}) doesn't exist.");
                Environment.Exit(1);
            }

            if (!Directory.Exists(_backupsDirectory))
            {
                Print(Level.ERROR, $"2nd parameter error : directory ({_backupsDirectory}) for backups doesn't exist.");
                Environment.Exit(1);
            }

            _dbFile = Path.GetFullPath(_dbFile);
            _backupsDirectory = Path.GetFullPath(_backupsDirectory);

            _dbBackupFile = Path.Combine(_backupsDirectory, $"{Path.GetFileName(_dbFile)}.backup");
            while (true)
            {
                Backup();
                
                
                //Wait 1 day
                Print(Level.INFO, "Waiting 1 day...");
                Task.Delay(TimeSpan.FromDays(1)).Wait();
            }
            
        }

        private static void Backup()
        {
            try
            {
                File.Copy(_dbFile, _dbBackupFile, true);
                Print(Level.INFO, $"{_dbFile} have been saved to {_dbBackupFile}.");
            }
            catch (Exception ex)
            {
                Print(Level.ERROR, $"Exception occured while backuping file : {ex.Message}.");
            }
        }
        
        

        private static void Print(Level level, string message)
        {
            string formattedMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {level} : {message}";
            
            Console.WriteLine(formattedMessage);
            using (StreamWriter writer = new StreamWriter(_logFile, true, System.Text.Encoding.UTF8))
                    writer.WriteLine(formattedMessage);
        }


        private enum Level
        {
            ERROR,
            INFO
        }
    }
}