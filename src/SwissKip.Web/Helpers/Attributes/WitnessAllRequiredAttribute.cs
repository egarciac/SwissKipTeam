namespace SwissKip.Web.Helpers.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SwissKip.Web.Models;

    [AttributeUsage(AttributeTargets.Class)]
    public class WitnessAllRequiredAttribute : ValidationAttribute
    {
        public WitnessAllRequiredAttribute()
        {
            this.ErrorMessage = "All fields are required";
        }

        public override bool IsValid(object value)
        {
            WitnessAddModel witness = (WitnessAddModel)value;

            /* 
             * La "Vista" correspondiente a este modelo es una tabla donde se agrega filas dinámicamente, donde cada fila corresponde a un witness.
             * Si todos los campos de un witness se encuentran en blanco, no se lo toma en cuenta al momento de procesar los datos.
             * Por lo tanto, si un witness tiene todos sus campos en blanco se necesita pasar la validación.
             */
            if (string.IsNullOrEmpty(witness.Email) && 
                string.IsNullOrEmpty(witness.Id.ToString()) && 
                string.IsNullOrEmpty(witness.LastName))
                return true;

            return !string.IsNullOrEmpty(witness.Email) &&
                   !string.IsNullOrEmpty(witness.Id.ToString()) &&
                   !string.IsNullOrEmpty(witness.LastName);
        }
    }
}