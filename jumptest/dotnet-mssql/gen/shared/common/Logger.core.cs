using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data.Common;

namespace jumptest
{

    public interface ILogWriter
    {
        void Write(string level, string message, string filePath, int lineNumber, string memberName);
    }

    public abstract class BaseLogWriter
    {
        protected readonly string ProgramName;
        protected readonly object Lock = new();

        protected BaseLogWriter()
        {
             ProgramName = Process.GetCurrentProcess().ProcessName ?? "UnknownProgram";
        }

        protected string FormatLogMessage(string level, string message, string filePath, int lineNumber, string memberName )
        {
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            string currentUser = Environment.UserName;
            return $"{timestamp} {level} {currentUser} {ProgramName} {filePath}:{lineNumber}.{memberName} {message}\n";
        }
    }

    public class LogFileWriter : BaseLogWriter, ILogWriter
    {
<<<<<<< HEAD
=======
        // Directory (relative to the process's working directory, i.e. the
        // "bin" folder the binary is launched from) that log files land in.
        private const string LogDir = "logs";

>>>>>>> main
        private readonly string _baseFileName;
        private string _currentFileName;

        public LogFileWriter()
            : base()
        {
<<<<<<< HEAD
            _baseFileName = $"{ProgramName}.log";
=======
            Directory.CreateDirectory(LogDir);
            _baseFileName = Path.Combine(LogDir, $"{ProgramName}.log");
>>>>>>> main
            _currentFileName = _baseFileName;
        }

        public void Write(string level, string message, string filePath, int lineNumber, string memberName)
        {
            lock (Lock)
            {
                DateTime now = DateTime.UtcNow;

                // Check if the file needs to be rolled over based on file's last write time
                if (File.Exists(_currentFileName))
                {
                    DateTime lastWriteTime = File.GetLastWriteTimeUtc(_currentFileName);
                    if (lastWriteTime.Date < now.Date)
                    {
                        RollOverLogFile(lastWriteTime);
                    }
                }

                string logMessage = FormatLogMessage(level, message, filePath, lineNumber, memberName);
                File.AppendAllText(_currentFileName, logMessage + Environment.NewLine);
            }
        }

        private void RollOverLogFile(DateTime lastWriteTime)
        {
            lock (Lock)
            {
                if (File.Exists(_currentFileName))
                {
<<<<<<< HEAD
                    string newFileName = $"{ProgramName}-{lastWriteTime:yyyy-MM-dd}.log";
=======
                    string newFileName = Path.Combine(LogDir, $"{ProgramName}-{lastWriteTime:yyyy-MM-dd}.log");
>>>>>>> main
                    File.Move(_currentFileName, newFileName);
                }

                _currentFileName = _baseFileName;
            }
        }
    }

    public class Logger
    {	
        [Flags]
        public enum Level 
        {
            None = 0,
            Error = 1 << 0,
            Info = 1 << 1,
            Debug = 1 << 2,
            Warning = 1 << 3,
            Sql = 1 << 4,
            Data = 1 << 5
        }

        private static List<ILogWriter> LogWriters = null;
        public static Level enabledLevels = Level.Error; // Error is always enabled


        protected static void Init()
        {
            if (LogWriters == null)
            {
                string logWriterNames = Config.getString("logwriters");
                
                LogWriters = CreateLogWriters(logWriterNames);

                string logLevel = Config.getString("loglevel", "info").ToLower();
                enabledLevels = ParseLogLevels(logLevel) | Level.Error; // Error is always enabled
            }
        }

        private static Level ParseLogLevels(string logLevelString)
        {
            Level levels = Level.None;
            
            if (string.IsNullOrEmpty(logLevelString))
                return Level.None; // Default fallback (Error will be added automatically)

            string[] levelNames = logLevelString.Split(',', ';', '|', ' ');
            
            foreach (string levelName in levelNames)
            {
                string trimmedLevel = levelName.Trim().ToLower();
                
                switch (trimmedLevel)
                {
                    case "error":
                        levels |= Level.Error;
                        break;
                    case "info":
                        levels |= Level.Info;
                        break;
                    case "debug":
                        levels |= Level.Debug;
                        break;
                    case "warning":
                    case "warn":
                        levels |= Level.Warning;
                        break;
                    case "sql":
                        levels |= Level.Sql;
                        break;
                    case "data":
                        levels |= Level.Data;
                        break;
                    case "all":
                        levels = Level.Error | Level.Info | Level.Debug | Level.Warning | Level.Sql | Level.Data;
                        break;
                    case "none":
                        levels = Level.None;
                        break;
                }
            }
            
            // If no valid levels were found, return None (Error will be added automatically)
            if (levels == Level.None)
                levels = Level.None;
                
            return levels;
        }

        public static List<ILogWriter> CreateLogWriters(string logWriterClasses)
        {
            var logWriters = new List<ILogWriter>();
            var classNames = logWriterClasses.Split(',');

            foreach (var className in classNames)
            {
                var type = Assembly.GetExecutingAssembly().GetType(className);
                if (type == null || !typeof(ILogWriter).IsAssignableFrom(type))
                {
                    throw new InvalidOperationException($"Class {className} does not exist or does not implement ILogWriter.");
                }

                var instance = Activator.CreateInstance(type) as ILogWriter;
                if (instance != null)
                {
                    logWriters.Add(instance);
                }
            }

            return logWriters;
        }

        public static void Error(string message, [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string memberName = "")
        {
            Init();
            if ((enabledLevels & Level.Error) != 0)
            {
                Write("ERROR", message, filePath, lineNumber, memberName);
            }
        }
        
        public static void Error(string message, Exception x,  [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string memberName = "")
        {
            Init();
            if ((enabledLevels & Level.Error) != 0)
            {
                string m = string.Empty;
                string s = string.Empty;

                while (x != null)
                {
                    m += x.Message;
                    m += "\n";
                    s += x.StackTrace;
                    s += "\n";				
                    x = x.InnerException;
                }
                
                Write("ERROR", m + "\n" + s, filePath, lineNumber, memberName);
            }
        }

        public static void Info( string message,  [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string memberName = "" )
        {
            Init();
            if ((enabledLevels & Level.Info) != 0)
            {
                Write("INFO", message, filePath, lineNumber, memberName );
            }
        }

        public static void Warn( string message,  [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string memberName = "" )
        {
            Init();
            if ((enabledLevels & Level.Warning) != 0)
            {
                Write("INFO", message, filePath, lineNumber, memberName );
            }
        }

        public static void Debug( string message,  [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string memberName = "" )
        {
            Init();
            if ((enabledLevels & Level.Debug) != 0)
            {
                Write("DEBUG", message, filePath, lineNumber, memberName);
            }
        }

        public static void Warning( string message,  [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string memberName = "" )
        {
            Init();
            if ((enabledLevels & Level.Warning) != 0)
            {
                Write("WARNING", message, filePath, lineNumber, memberName);
            }
        }

        public static void Sql( string message,  [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string memberName = "" )
        {
            Init();
            if ((enabledLevels & Level.Sql) != 0)
            {
                Write("SQL", message, filePath, lineNumber, memberName);
            }
        }

        public static void Data( string message,  [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string memberName = "" )
        {
            if ((enabledLevels & Level.Data) == 0) return;

            Init();
            
            Write("DATA", message, filePath, lineNumber, memberName);
            
        }

        private static void Write( string level, string message, string filePath = "",
         int lineNumber = 0, string memberName = "" )
        {
            foreach( var logWriter in LogWriters)
            {
                logWriter.Write(level, message, filePath, lineNumber, memberName);
            }
        }
    }

    public class LogTableWriter : BaseLogWriter, ILogWriter
    {
        private static readonly IDatabaseProvider _provider = DatabaseProviderFactory.CreateProvider();
        private static string _connectionString = null;
        private static string _tableName = null;
        private static string _insertSql = null;
        private static bool _initialized = false;
        private static readonly object _initLock = new object();

        private static void InitializeIfNeeded()
        {
            if (!_initialized)
            {
                lock (_initLock)
                {
                    if (!_initialized)
                    {
                        try
                        {
                            _connectionString = Config.getString("db.connection");
                            _tableName = _provider.FormatTableName("core.log");
                            _insertSql = $"INSERT INTO {_tableName} (level, timestamp, principalname, program, filepath, linenumber, membername, message) " +
                                        "VALUES (@level, @timestamp, @principalname, @program, @filepath, @linenumber, @membername, @message);";
                            _initialized = true;
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to initialize LogTableWriter: {ex.Message}");
                            _initialized = true; // Prevent repeated attempts
                        }
                    }
                }
            }
        }

        public void Write(string level, string message, string filePath, int lineNumber, string memberName )
        {
            lock (Lock)
            {
                try
                {
                    InitializeIfNeeded();
                    
                    if (_connectionString == null)
                    {
                        // Database logging not available, skip silently
                        return;
                    }

                    using (DbConnection connection = _provider.CreateConnection(_connectionString))
                    {
                        //Console.WriteLine("LogTableWriter: connection.Open()");
                        //connection.Open();

                        //Console.WriteLine("LogTableWriter: _insertSql: " + _insertSql);

                        var command = _provider.CreateCommand(_insertSql, connection);
                        command.Parameters.Add(_provider.CreateParameter());
                        command.Parameters[0].ParameterName = "@level";
                        command.Parameters[0].Value = level;
                        
                        command.Parameters.Add(_provider.CreateParameter());
                        command.Parameters[1].ParameterName = "@timestamp";
                        command.Parameters[1].Value = DateTime.UtcNow;
                        
                        command.Parameters.Add(_provider.CreateParameter());
                        command.Parameters[2].ParameterName = "@principalname";
                        command.Parameters[2].Value = Environment.UserName;
                        
                        command.Parameters.Add(_provider.CreateParameter());
                        command.Parameters[3].ParameterName = "@program";
                        command.Parameters[3].Value = ProgramName;
                        
                        command.Parameters.Add(_provider.CreateParameter());
                        command.Parameters[4].ParameterName = "@filepath";
                        command.Parameters[4].Value = filePath;
                        
                        command.Parameters.Add(_provider.CreateParameter());
                        command.Parameters[5].ParameterName = "@linenumber";
                        command.Parameters[5].Value = lineNumber;
                        
                        command.Parameters.Add(_provider.CreateParameter());
                        command.Parameters[6].ParameterName = "@membername";
                        command.Parameters[6].Value = memberName;
                        
                        command.Parameters.Add(_provider.CreateParameter());
                        command.Parameters[7].ParameterName = "@message";
                        command.Parameters[7].Value = message;
                        
                        //Console.WriteLine("LogTableWriter: command.ExecuteNonQuery()");
                        command.ExecuteNonQuery();

                        //Console.WriteLine("LogTableWriter: connection.Close()");    
                        connection.Close();
                    }
                }
                catch(Exception x)
                {
                    Console.Error.WriteLine($"LogTableWriter error: {x.Message}");
                    Console.Error.WriteLine(x.StackTrace);
                }
            }
        }
    }

    public class LogConsoleWriter : BaseLogWriter, ILogWriter
    {
        public void Write(string level, string message, string filePath, int lineNumber, string memberName)
        {
            lock (Lock)
            {
                string logMessage = FormatLogMessage(level, message, filePath, lineNumber, memberName);
								ConsoleColor initialColor = Console.ForegroundColor;
    
                if (level == "ERROR")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine(logMessage);
                Console.ForegroundColor = initialColor;
            }
        }
    }

}
