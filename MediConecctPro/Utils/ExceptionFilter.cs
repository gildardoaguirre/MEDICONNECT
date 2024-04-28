using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediConecctPro.Utils
{
    public class FiltersAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<FiltersAttribute> logger;
        public FiltersAttribute(ILogger<FiltersAttribute> logger)
        {
            this.logger = logger;
        }
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Handle(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Capturar excepciones en tareas sincronas
        /// </summary>
        /// <param name="context">Context from application</param>
        public override void OnException(ExceptionContext context)
        {
            Handle(context);
        }

        /// <summary>
        /// Error logs
        /// </summary>
        /// <param name="context">Context from application</param>
        private void Handle(ExceptionContext context)
        {
            string exception = context.Exception.Message;

            // Logger
            logger.LogError(context.Exception, exception);

            // Result for our application
            ObjectResult result = new("Ha ocurrido un error interno.")
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.Result = result;
        }
    }
}
