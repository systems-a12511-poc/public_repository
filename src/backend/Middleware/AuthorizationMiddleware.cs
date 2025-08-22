using Microsoft.AspNetCore.Http;

namespace backend.Middleware;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 次のミドルウェアに処理を渡す
        await _next(context);
    }
}