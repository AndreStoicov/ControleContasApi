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
        private readonly Container container;

        public GlobalContainerAccessor(Container container)
        {
            this.container = container;
        }

        public Container Container => container;

        public object GetInstance(Type t)
        {
            return container.GetInstance(t);
        }
    }
}