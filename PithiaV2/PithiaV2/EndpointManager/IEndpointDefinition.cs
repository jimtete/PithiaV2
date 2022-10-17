namespace PithiaV2.EndpointManager;

public interface IEndpointDefinition
{
    void DefineServices(IServiceCollection services);

    void DefineEndpoints(WebApplication app);
}