using System;
using System.Configuration;
using System.Threading;
using System.Web.Http;
using Homework.App_Start;

namespace Homework
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.ConfigureAutoMapper();

            string threadConf = ConfigurationManager.AppSettings["MinThreads"];
            if (threadConf != null)
            {
                int minThreads = Int32.Parse(threadConf);

                // we strongly recommend that customers set the minimum configuration value for
                // IOCP and WORKER threads to something larger than the default value. We can't
                // give one-size-fits-all guidance on what this value should be because the right
                // value for one application will likely be too high or low for another application.
                // This setting can also impact the performance of other parts of complicated
                // applications, so each customer needs to fine-tune this setting to their specific
                // needs. A good starting place is 200 or 300, then test and tweak as needed.
                // https://docs.microsoft.com/en-us/azure/azure-cache-for-redis/cache-faq#important-details-about-threadpool-growth
                ThreadPool.SetMinThreads(minThreads, minThreads);
            }
        }
    }
}
