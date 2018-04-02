using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using FluentValidation;
using HubFintech.ControleContas.Api.Configuration.Factories;

namespace HubFintech.ControleContas.Api.Configuration.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (actionContext.ActionArguments.Count > 0)
            {
                var dicionario = new ModelStateDictionary();
                var globalConfig =
                    GlobalConfiguration.Configuration.DependencyResolver.GetService(
                        typeof(IGlobalContainerAccessor)) as IGlobalContainerAccessor;
                IValidatorFactory validationFactory = new FluentValidatorFactory(globalConfig.Container);

                foreach (var arg in actionContext.ActionArguments)
                {
                    if (arg.Value == null)
                        continue;

                    var validator = validationFactory?.GetValidator(arg.Value.GetType());

                    if (validator == null)
                        continue;

                    var result = await validator.ValidateAsync(arg.Value, cancellationToken).ConfigureAwait(false);

                    if (result.IsValid)
                        continue;

                    foreach (var e in result.Errors)
                    {
                        dicionario.AddModelError(e.ErrorCode, e.ErrorMessage);
                    }
                }

                if (dicionario.Count > 0)
                {
                    actionContext.Response =
                        actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, dicionario);
                }
            }
        }
    }
}