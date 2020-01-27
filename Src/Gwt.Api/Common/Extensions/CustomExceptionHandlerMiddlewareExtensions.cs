using Gwt.Api.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Gwt.Api.Common.Extensions
{
  public static class CustomExceptionHandlerMiddlewareExtensions
  {
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
  }
}