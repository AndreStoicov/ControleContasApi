using System;
using FluentValidation;
using SimpleInjector;

namespace HubFintech.ControleContas.Api.Configuration.Factories
{
    public class FluentValidatorFactory : ValidatorFactoryBase
    {
        private readonly Container container;

        public FluentValidatorFactory(Container container)
        {
            this.container = container;
        }
               
        public override IValidator CreateInstance(Type validatorType)
        {
            IServiceProvider provider = container;
            object instance = provider.GetService(validatorType);
            return instance as IValidator;
        }
    }
}