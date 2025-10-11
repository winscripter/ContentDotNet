using ContentDotNet;
using ContentDotNet.Extensions.Ttf;

var ttfStream = File.OpenRead("Font.ttf");
var ttfReader = new TtfReader(ttfStream);

var rc = new RecursionCounter(5000);
while (ttfReader.ReadNextTable())
{
    rc.Increment();
    var table = ttfReader.ActiveTable;
    Console.WriteLine($"Table {ttfReader.ActiveTableIndex}: {table.Tag} - Offset: {table.Offset}, Length: {table.Length}, FCC: {ttfReader.ActiveTable.Tag.ValueText}");
}
