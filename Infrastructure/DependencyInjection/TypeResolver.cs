using Spectre.Console.Cli;

namespace SmartProfiler.CLI.Infrastructure.DependencyInjection;

public class TypeResolver : ITypeResolver
{
    private readonly IServiceProvider _provider;

    public TypeResolver(IServiceProvider provider)
    {
        _provider = provider;
    }

    public object Resolve(Type type) => _provider.GetService(type);
}