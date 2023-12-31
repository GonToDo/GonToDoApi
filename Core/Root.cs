namespace GonToDoApi.Core;

public class Root(string status, string message, object? data = null)
{
    public string Status { get; set; } = status;
    public string Message { get; set; } = message;
    public object? Data { get; set; } = data;
}