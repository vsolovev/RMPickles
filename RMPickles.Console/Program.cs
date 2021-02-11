using Autofac;
using NLog;
using RMPickles.Core;
using System;
using System.Reflection;
using System.Text;

namespace RMPickles.Console
{    
    class Program
    {


        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);
        static void Main(string[] args)
        {
            /*args = new string[]
            {
                @"--feature-directory=c:\Users\HyDrA\Desktop\Pick\Features",
                @"--output-directory=c:\Users\HyDrA\Desktop\Pick\FeaturesOutput",
                @"--resource-directory=C:/Users/HyDrA/Desktop/Pick/Resources"
            };*/
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(Runner).Assembly);            
            builder.RegisterModule<PicklesModule>();
            builder.RegisterType<CommandLineArgumentParser>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            builder.RegisterType<MarkdownProvider>().As<IMarkdownProvider>();
            var container = builder.Build();

            var configuration = container.Resolve<IConfiguration>();

            var commandLineArgumentParser = container.Resolve<CommandLineArgumentParser>();
            var shouldContinue = commandLineArgumentParser.Parse(args, configuration, System.Console.Out);

            if (shouldContinue)
            {
                if (Log.IsInfoEnabled)
                {
                    Log.Info(
                        "Pickles v.{0}{1}",
                        Assembly.GetExecutingAssembly().GetName().Version,
                        Environment.NewLine);
                    new ConfigurationReporter().ReportOn(configuration, message => Log.Info(message));
                }

                var runner = container.Resolve<Runner>();

                try
                {
                    runner.Run(container);

                    if (Log.IsInfoEnabled)
                    {
                        Log.Info("Pickles completed successfully");
                    }
                }
                catch (Exception ex)
                {
                    if (Log.IsFatalEnabled)
                    {
                        Log.Fatal(ex, "Pickles did not complete successfully");
                    }
                    System.Console.ReadKey();
                }
            }
        }
    }
}
