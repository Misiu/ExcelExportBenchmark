using BenchmarkDotNet.Attributes;
using SpreadCheetah;
using SpreadCheetah.Styling;
using Syncfusion.XlsIO;

namespace ExportBenchmark;

[MemoryDiagnoser]
public class Benchmarks
{
    [Benchmark]
    [Arguments(10, 600)]
    [Arguments(100, 600)]
    [Arguments(100, 6000)]
    [Arguments(100, 60000)]
    //[Arguments(100, 600000)]
    public async Task ExportWithSyncfusion(int worksheets, int rows)
    {
        await using var stream = File.Create("syncfusion.xlsx");

        using var excelEngine = new ExcelEngine();
        excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
        var workbook = excelEngine.Excel.Workbooks.Create(worksheets);

        for (var i = 0; i < worksheets; i++)
        {
            var worksheet = workbook.Worksheets[i];
            worksheet.Name = $"Sheet {i}";

            worksheet.Range["A1"].Text = $"Sample description {i}";
            worksheet.Range["A1:B1"].Merge();

            worksheet.Range["A2"].Text = "Data";
            worksheet.Range["B2"].Text = "Value";

            var data = new List<DataRecord>();

            for (var j = 0; j < rows; j++)
            {
                var dateTime = new DateTime(2023, 1, 1).AddSeconds(j);
                data.Add(new DataRecord(dateTime, j));
            }

            var importDataOptions = new ExcelImportDataOptions
            {
                FirstRow = 3,
                FirstColumn = 1,
                IncludeHeader = false
            };

            worksheet.ImportData(data, importDataOptions);
        }

        workbook.SaveAs(stream);
    }

    [Benchmark]
    [Arguments(10, 600)]
    [Arguments(100, 600)]
    [Arguments(100, 6000)]
    [Arguments(100, 60000)]
    //[Arguments(100, 600000)]
    public async Task ExportWithSpreadCheetah(int worksheets, int rows)
    {
        await using var stream = File.Create("spreadcheetah.xlsx");

        var options = new SpreadCheetahOptions
        {
            CompressionLevel = SpreadCheetahCompressionLevel.Optimal
        };
        await using var spreadsheet = await Spreadsheet.CreateNewAsync(stream, options);

        var headerStyle = new Style();
        headerStyle.Font.Bold = true;
        var headerStyleId = spreadsheet.AddStyle(headerStyle);

        for (var i = 0; i < worksheets; i++)
        {
            await spreadsheet.StartWorksheetAsync($"Sheet {i}");

            var header1 = new[]
            {
                new StyledCell($"Sample description {i}", headerStyleId),
            };

            var header2 = new[]
            {
                new StyledCell("Data", headerStyleId),
                new StyledCell("Value", headerStyleId)
            };

            await spreadsheet.AddRowAsync(header1);

            spreadsheet.MergeCells("A1:B1");

            await spreadsheet.AddRowAsync(header2);

            for (var j = 0; j < rows; j++)
            {
                var dateTime = new DateTime(2023, 1, 1).AddSeconds(j);

                var dataRow = new[]
                {
                    new DataCell(dateTime),
                    new DataCell(j)
                };

                await spreadsheet.AddRowAsync(dataRow);
            }
        }

        await spreadsheet.FinishAsync();
    }
}