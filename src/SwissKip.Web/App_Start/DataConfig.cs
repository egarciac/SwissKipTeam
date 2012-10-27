namespace SwissKip.Web.App_Start
{
    using SwissKip.Web.Data;

    public class DataConfig
    {
        public static void RegisterMapper()
        {
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(CustomClassMapper<>);
        }
    }
}