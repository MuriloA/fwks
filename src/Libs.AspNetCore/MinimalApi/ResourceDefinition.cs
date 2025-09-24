using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Libs.AspNetCore.MinimalApi;

public abstract class ResourceDefinition
{
    private string TypeName => GetType().Name.Replace("Resource", string.Empty);

    public virtual string Prefix => TypeName.Kebaberize();
    public virtual string[] Tags => [TypeName.Pascalize()];
    public virtual int[] Versions => [1];
    public virtual int[] Problems => [StatusCodes.Status500InternalServerError];

    public virtual void Configure(RouteGroupBuilder builder)
    {
    }
}