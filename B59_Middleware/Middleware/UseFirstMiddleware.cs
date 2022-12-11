namespace Middleware_B59;

public static class UseMiddlewareMethod
{
    public static void UseFirstMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<FirstMiddleware>();
    }

    public static void UseSecondMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<SecondMiddleware>();
    }
}