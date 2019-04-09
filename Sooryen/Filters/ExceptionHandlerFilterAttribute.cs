using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Sooryen.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate)]
    public sealed class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {


        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext != null)
            {
                var paramList = actionExecutedContext.ActionContext.ActionArguments.ToDictionary(modelStateKey => modelStateKey.Key, modelStateKey => modelStateKey.Value);
                var paramDetails = JsonConvert.SerializeObject(new { ErrorPara = paramList });
                var errorCode ="1111";
                var errorMessage = "InternalServerErrorMessage";
                if (actionExecutedContext.Exception is System.Data.SqlClient.SqlException )
                {
                    errorCode = "1200";
                    errorMessage = "SqlExceptionErrorMessage";
                }
                var innerException = "";
                var exception = actionExecutedContext.Exception as DbEntityValidationException;
                if (exception != null)
                {
                    //get validation error details
                    var sqlexception = exception;
                    var errorMessages = sqlexception.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                    // Join the list to a single string.
                    innerException = string.Join("; ", errorMessages);
                }
                //Save in error log 
            }
        }
    }
}