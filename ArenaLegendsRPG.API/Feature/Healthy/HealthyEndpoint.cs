namespace ArenaLegendsRPG.API.Feature.Healthy;

public static class HealthyEndpoint
{
    public static IEndpointRouteBuilder MapHealthEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/healthy", () => Results.Ok());

        return app;
    }
}