using System;
using System.Globalization;
using System.Web.Mvc;

namespace ADOPets.Web.Common.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var modelState = new ModelState { Value = valueResult };

            object actualValue = null;
            try
            {
                actualValue = valueResult.AttemptedValue.Contains(",")
                    ? Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.GetCultureInfo("fr-FR"))
                    : Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }
}