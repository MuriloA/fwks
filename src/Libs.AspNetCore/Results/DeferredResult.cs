using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Results;

public class DeferredResult : IResult
{
    public Task ExecuteAsync(HttpContext httpContext)
    {
        return Task.CompletedTask;
    }
}