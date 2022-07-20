using Spectre.Console.Cli;

namespace Comparer
{
    internal static class Program
    {
        internal static int Main(string[] args)
        {
            var app = new CommandApp<ComparerCommand>();
            app.Configure(config =>
            {
                config.UseStrictParsing();
                config.SetApplicationName("comparer");
                config.SetApplicationVersion("0.1");
                config.AddExample(new[] { "registry.pol", "-c" });
            });

            return app.Run(args);
        }
    }
}
