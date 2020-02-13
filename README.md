# itf-barcode-svg-generator
A ITF barcode SVG generator: give me the digits, I'll give you a SVG string with the ITF barcode.

Usage:
```cs
try
{
    var gen = new ITFUtil.ITFGenerator("12345678901234567890123456789012345678901234");
    string svgString = gen.Svg;
}
catch (ArgumentException aex)
{
    Console.WriteLine("Error generating ITF: " + aex.Message);
}
 ```

CLI usage:
```powershell
PS (..)\itf-barcode-svg-generator\itfgen> dotnet run 12345678901234567890123456789012345678901234 > out.svg
```

Generated `out.svg`:

<img src="out.svg" width="640" />

## Thanks

This work is based (almost a port) on @carlsmith's: https://gist.github.com/carlsmith/643cefd7a4712f36cc8b.

## Refs on ITF barcodes

https://en.wikipedia.org/wiki/Interleaved_2_of_5
https://en.wikipedia.org/wiki/File:Decoding_Interleaved_2_of_5.jpg

https://www.ttrix.com/apple/iphone/boletoscan/boletoanatomia.html
http://www.bb.com.br/docs/pub/emp/mpe/dwn/PadraoCodigoBarras.pdf

https://www.invertexto.com/codigo-barras
http://www.barcodenet.com.br/cgi-bin/webcex1p.asp
https://www.barcode-generator.de/V2/pt/index.jsp