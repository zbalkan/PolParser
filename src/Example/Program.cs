using PolParser;

namespace Example
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var pol = Parser.ReadPolFile("Registry.pol");
        }
    }
}
