namespace SwissKip.Web.Helpers.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using SwissKip.Web.Models;

    public class BirthdayAttribute : ValidationAttribute,IClientValidatable
    {
        public BirthdayAttribute( )
        {
            this.ErrorMessage = "The Birthday is invalid";
        }

        public override bool IsValid(object value)
        {
            try
            {
                var birthdate = (Birthday)value;
                var year = birthdate.Year;
                var month = birthdate.Month;
                var day = birthdate.Day;

                /* El usuario puede no ingresar una fecha de nacimiento.
                 * Por lo tanto, si todos los campos son nulos, es una fecha vacia pero válida. */
                if (year == null && month == null && day == null)
                    return true;

                if (year != null && month != null && day != null)
                {
                    DateTime temp;
                    return DateTime.TryParse(string.Format("{0}-{1}-{2}", year.Value, month.Value, day.Value), out temp);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
                {
                    ErrorMessage = this.ErrorMessage, 
                    ValidationType = "birthday"
                };
            yield return rule;
        }
    }
}