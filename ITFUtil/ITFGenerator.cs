using System;
using System.Collections.Generic;
using System.Linq;

namespace ITFUtil
{
    public class ITFGenerator
    {
        private string header = "<svg xmlns=\"http://www.w3.org/2000/svg\" height=\"58\" width=\"{0}\" style=\"background:white\">";
        private string bar = "<rect x=\"{0}\" y=\"4\" width=\"{1}\" height=\"50\" style=\"fill:black\"/>";
        private string footer = "</svg>";

        private Dictionary<char, int[]> conversionTable = new Dictionary<char, int[]>
        {
            { '0', new int[] { 2, 2, 4, 4, 2 } },
            { '1', new int[] { 4, 2, 2, 2, 4 } },
            { '2', new int[] { 2, 4, 2, 2, 4 } },
            { '3', new int[] { 4, 4, 2, 2, 2 } },
            { '4', new int[] { 2, 2, 4, 2, 4 } },
            { '5', new int[] { 4, 2, 4, 2, 2 } },
            { '6', new int[] { 2, 4, 4, 2, 2 } },
            { '7', new int[] { 2, 2, 2, 4, 4 } },
            { '8', new int[] { 4, 2, 2, 4, 2 } },
            { '9', new int[] { 2, 4, 2, 4, 2 } }
        };

        public string OriginalBarcode { get; private set; }
        private List<int[]> _ITFCode;
        private int[] _ITFCodeStart = new int[] { 2, 2, 2, 2 };
        private int[] _ITFCodeEnd = new int[] { 4, 2, 2, 0 };
        public string Svg { get; set; }

        public ITFGenerator(string barcode)
        {
            foreach (char c in barcode.ToCharArray())
            {
                if (!char.IsDigit(c))
                {
                    throw new ArgumentException($"Code must contains only digits. Non-digit: {c.ToString()}");
                }
            }
            if (barcode.Length % 2 != 0)
            {
                throw new ArgumentException($"Code must have even number of digits. Length: {barcode.Length}");
            }

            this.OriginalBarcode = barcode;
            this._ITFCode = this.ToITFCode();
            this.Svg = this.ToSvg();
        }

        private List<int[]> ToITFCode()
        {
            List<int[]> intCodigo = new List<int[]>();
            foreach (char c in this.OriginalBarcode.ToCharArray())
            {
                intCodigo.Add(conversionTable[c]);
            }

            return intCodigo;
        }

        private string ToSvg()
        {

            // SVG header
            string svg = "";
            svg += String.Format(header, this._ITFCode.Count() * 14 + 24);
            svg += "\n";

            int x = 0;

            // Start marker
            int[] startItems = this._ITFCodeStart;
            for (int n = 0; n < 3; n+=2)
            {
                svg += "\t";
                svg += String.Format(bar, x, startItems[n]);
                x += startItems[n] + startItems[n+1];
                svg += "\n";
            }

            // Bar for input code
            for (int i = 0; i <= (this._ITFCode.Count()-2); i+=2)
            {
                int[] odd = this._ITFCode[i];
                int[] even = this._ITFCode[i+1];

                for(int n = 0; n < 5; n++)
                {
                    svg += "\t";
                    svg += String.Format(bar, x, odd[n]);
                    x += odd[n] + even[n];
                    svg += "\n";
                }
            }

            // Terminator
            int[] endIitems = this._ITFCodeEnd;
            for (int n = 0; n < 3; n+=2)
            {
                svg += "\t";
                svg += String.Format(bar, x, endIitems[n]);
                x += endIitems[n] + endIitems[n + 1];
                svg += "\n";
            }

            // Footer SVG
            svg += footer;
            return svg;
        }

    }
}