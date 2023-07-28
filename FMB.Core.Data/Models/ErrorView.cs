namespace FMB.Core.API.Models;

public record ErrorView
{
    public string ErrorCode { get; }
    public string? ErrorMessage { get; }

    public ErrorView(string errorCode, string? errorMessage = null)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}