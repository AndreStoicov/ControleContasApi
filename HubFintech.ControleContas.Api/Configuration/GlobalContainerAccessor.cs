using System;
using SimpleInjector;

namespace HubFintech.ControleContas.Api.Configuration
{
    public interface IGlobalContainerAccessor
    {
        object GetInstance(Type t);
        Container Container { get; }
    }
    
    public class GlobalContainerAccessor : IGlobalContainerAccessor
    {
        private readonly Container _container;

        public GlobalContainerAccessor(Container container)
        {
            _container = container;
        }

        public Container Container => _container;

        public object GetInstance(Type t)
        {
            return _container.GetInstance(t);
        }
    }
}