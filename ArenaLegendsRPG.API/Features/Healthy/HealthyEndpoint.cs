namespace ArenaLegendsRPG.API.Features.Healthy;

public static class HealthyEndpoint
{
    public static IEndpointRouteBuilder MapHealthyEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/healthy", () => Results.Ok());

        return app;
    }
}