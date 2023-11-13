namespace JogoRpg.Api.Application.Models;

public class ResultResponse
{
    public bool Success { get; set; } = true;
    public object Data { get; set; }
    public string Error { get; set; }
    public string Message { get; set; }
    public ResultResponse()
    {
    }

}