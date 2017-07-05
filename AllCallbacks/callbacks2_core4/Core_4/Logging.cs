using log4net.Appender;
using log4net.Core;
using NServiceBus;


public static class Logging
{

    public static void ConfigureLogging()
    {
        SetLoggingLibrary.Log4Net<RollingFileAppender>(null,
            a =>
            {
                a.CountDirection = 1;
                a.DatePattern = "yyyy-MM-dd";
                a.RollingStyle = RollingFileAppender.RollingMode.Composite;
                a.MaxFileSize = 1024 * 1024;
                a.MaxSizeRollBackups = 10;
                a.LockingModel = new FileAppender.MinimalLock();
                a.StaticLogFileName = true;
                a.File = "logfile.txt";
                a.AppendToFile = true;
                a.ImmediateFlush = true;
            });

        SetLoggingLibrary.Log4Net<ColoredConsoleAppender>(null,
            consoleAppender =>
            {
                PrepareColors(consoleAppender);
                consoleAppender.Threshold = Level.Info;
            }
        );
    }

    static void PrepareColors(ColoredConsoleAppender consoleAppender)
    {
        var warn = new ColoredConsoleAppender.LevelColors
        {
            Level = Level.Warn,
            ForeColor = ColoredConsoleAppender.Colors.Yellow
        };
        consoleAppender.AddMapping(warn);
        var error = new ColoredConsoleAppender.LevelColors
        {
            Level = Level.Error,
            ForeColor = ColoredConsoleAppender.Colors.Red
        };
        consoleAppender.AddMapping(error);
    }
}