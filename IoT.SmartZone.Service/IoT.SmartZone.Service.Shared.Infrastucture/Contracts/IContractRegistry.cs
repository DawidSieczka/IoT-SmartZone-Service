using System.Reflection;
using IoT.SmartZone.Service.Shared.Abstractions.Contracts;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Contracts;

public interface IContractRegistry
{
    IContractRegistry Register<T>() where T : class, IContract;

    IContractRegistry RegisterPath(string path);

    IContractRegistry RegisterPath<TRequest, TResponse>(string path)
        where TRequest : class where TResponse : class;

    IContractRegistry RegisterPathWithRequest<TRequest>(string path)
        where TRequest : class;

    IContractRegistry RegisterPathWithResponse<TResponse>(string path)
        where TResponse : class;

    void Validate(IEnumerable<Assembly> assemblies);
}