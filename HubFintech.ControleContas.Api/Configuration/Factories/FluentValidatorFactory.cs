using System;
using FluentValidation;
using SimpleInjector;

namespace HubFintech.ControleContas.Api.Configuration.Factories
{
    public class FluentValidatorFactory : ValidatorFactoryBase
    {
        private readonly Container _container;

        public FluentValidatorFactory(Container container)
        {
            _container = container;
        }
               
        public override IValidator CreateInstance(Type validatorType)
        {
            IServiceProvider provider = _container;
            object instance = provider.GetService(validatorType);
            return instance as IValidator;
        }
    }
}