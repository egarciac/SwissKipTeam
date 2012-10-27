namespace SwissKip.Web.Data
{
    using System;
    using System.Linq;

    using DapperExtensions.Mapper;

    public class KeyAssignedAttribute : Attribute
    {
    }

    public class CustomClassMapper<T> : AutoClassMapper<T> where T : class
    {
        protected override void AutoMap()
        {
            base.AutoMap();
            var keyProperty = this.Properties.SingleOrDefault(x => x.KeyType != KeyType.NotAKey) as PropertyMap;
            if (keyProperty != null)
            {
                var keyAssigned = Attribute.IsDefined(keyProperty.PropertyInfo, typeof(KeyAssignedAttribute));
                if (keyAssigned)
                {
                    keyProperty.Key(KeyType.Assigned);
                }
            }
        }
    }
}