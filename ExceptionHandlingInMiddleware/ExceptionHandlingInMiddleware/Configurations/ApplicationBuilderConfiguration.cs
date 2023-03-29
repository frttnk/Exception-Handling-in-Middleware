namespace ExceptionHandlingInMiddleware.Configurations;

public static class ApplicationBuilderConfiguration
{
    public static IApplicationBuilder ErrorHandler(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<ExceptionMiddlewareConfiguration>();
}
