using System;
using ITFUtil;

namespace itfgen
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                try
                {
                    var gen = new ITFGenerator(args[0]);
                    Console.WriteLine(gen.Svg);
                }
                catch (ArgumentException aex)
                {
                    Console.WriteLine("Error generating ITF: " + aex.Message);
                }
            }
            else
            {
                Console.WriteLine("-- iftgen --");
                Console.WriteLine("A ITF barcode SVG generator: give me the digits, I'll give you a SVG string with the ITF barcode.");
                Console.WriteLine("https://github.com/ermogenes/itf-barcode-svg-generator");
            }
        }
    }
}
