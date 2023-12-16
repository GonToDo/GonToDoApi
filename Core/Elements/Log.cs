using System.Diagnostics;
using GonToDoApi.Core.Patterns;

namespace GonToDoApi.Core.Elements;

public class Log : SingletonBase<Log> 
{
    private const ConsoleColor InformationColor = ConsoleColor.White;
    private const ConsoleColor SuccessColor = ConsoleColor.Green;
    private const ConsoleColor WarningColor = ConsoleColor.Yellow;
    private const ConsoleColor ErrorColor = ConsoleColor.Red;

    public static bool IsDevelopment = false;
    protected virtual bool NoAllowWrite() => false;
    
    private static string GetDateTime()
    {
        return DateTime.Now.ToString("HH:mm:ss");
    }
    
    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.Gray)
    {
        if (Instance.NoAllowWrite() && IsDevelopment) return;
        
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine($"({GetDateTime()}) - {message}");
        Console.ForegroundColor = previousColor;
    }

    public static void Write(string message, ConsoleColor color = ConsoleColor.Gray)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = previousColor;
    }

    /// <summary>
    /// Show Information(White Color) In Debug Output
    /// </summary>
    /// <param name="message">string value</param>
    public static void Information(string message)
    {
        WriteLine($"(Information): {message}", InformationColor);
    }
    // public static void Info(string message) => Information(message);
    // public static void I(string message) => Information(message);

    
    /// <summary>
    /// Show Success(Green Color) In Debug Output 
    /// </summary>
    /// <param name="message">string value</param>
    public static void Success(string message)
    {
        WriteLine($"(Success): {message}", SuccessColor);
    }
    // public static void S(string message) => Success(message);    

    
    /// <summary>
    /// Show Warning(Yellow Color) In Debug Output
    /// </summary>
    /// <param name="message">string value</param>
    public static void Warning(string message)
    {
        WriteLine($"(Warning): {message}", WarningColor);
    }
    // public static void W(string message) => Warning(message);
    
    
    /// <summary>
    /// Show Error(Red Color) In Debug Output
    /// </summary>
    /// <param name="message">string value</param>
    public static void Error(string message)
    {
        WriteLine($"(Error): {message}", ErrorColor);
    }
    // public static void E(string message) => Error(message);    
}