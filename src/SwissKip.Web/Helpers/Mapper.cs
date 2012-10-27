namespace SwissKip.Web.Helpers
{
    using System;
    using System.Linq;

    using Omu.ValueInjecter;

    public static class Mapper
    {
        public static T Map<T>(object source)
        {
            return (T)Activator.CreateInstance(typeof(T))
                .InjectFrom(source);
        }

        public static T Map<T>(object source, string[] ignoreProperties)
        {
            return (T)Activator.CreateInstance(typeof(T))
                .InjectFrom(new IgnoreProperties(ignoreProperties), source);
        }

        public static void Map(this object destiny, object source)
        {
            destiny.InjectFrom(source);
        }

        public static void Map(this object destiny, object source, string[] ignoreProperties)
        {
            destiny.InjectFrom(new IgnoreProperties(ignoreProperties), source);
        }
    }

    public class IgnoreProperties : LoopValueInjection
    {
        private readonly string[] ignoreProperties;

        public IgnoreProperties(string[] ignoreProperties)
        {
            this.ignoreProperties = ignoreProperties;
        }

        protected override bool UseSourceProp(string sourcePropName)
        {
            return !ignoreProperties.Contains(sourcePropName);
        }
    }
}