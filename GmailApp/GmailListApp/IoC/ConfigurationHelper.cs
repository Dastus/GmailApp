using StructureMap;
using GmailListApp.Service;

namespace GmailListApp.IoC
{
    public class ConfigurationHelper
    {
        public static void ConfigureDependencies(ConfigurationExpression temp)
        {
            temp.For<IEmailManager>().Use<GmailManager>();
        }
    }

}