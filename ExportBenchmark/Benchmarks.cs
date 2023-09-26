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
    [Arguments(100, 600000)]
    public void ExportWithSyncfusion(int worksheets, int rows)
    {
        using var excelEngine = new ExcelEngine();
        excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
        var workbook = excelEngine.Excel.Workbooks.Create(worksheets);

        for (var i = 0; i < worksheets; i++)
        {

            //get worksheet
            var worksheet = workbook.Worksheets[i];
            //set worksheet name
            worksheet.Name = $"Sheet {i}";

            //add header
            worksheet.Range["A1"].Text = $"Sample description {i}";
            //merge header cells
            worksheet.Range["A1:B1"].Merge();

            worksheet.Range["A2"].Text = "Data";
            worksheet.Range["B2"].Text = "Value";

            var data = new List<DataRecord>();

            //this simulates loading data from the database
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

        workbook.SaveAs("sample.xlsx");
    }

    [Benchmark]
    [Arguments(10, 600)]
    [Arguments(100, 600)]
    [Arguments(100, 6000)]
    [Arguments(100, 60000)]
    [Arguments(100, 600000)]
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
            // A spreadsheet must contain at least one worksheet.
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

                // Cells are inserted row by row.
                var dataRow = new[]
                {
                    new DataCell(dateTime),
                    new DataCell(j)
                };

                // Rows are inserted from top to bottom.
                await spreadsheet.AddRowAsync(dataRow);
            }
        }


        // Remember to call Finish before disposing.
        // This is important to properly finalize the XLSX file.
        await spreadsheet.FinishAsync();
    }
}