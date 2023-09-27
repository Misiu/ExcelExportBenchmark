using System.Diagnostics;
using SpreadCheetah;
using SpreadCheetah.Styling;
using Syncfusion.XlsIO;

namespace WinformsExport
{
    public partial class Form1 : Form
    { public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var worksheets = (int)sheetsNud.Value;
            var rows = (int)rowsNud.Value;

            var stopwatch = Stopwatch.StartNew();

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

            stopwatch.Stop();

            //dispose of workbook
            workbook.Close();
            excelEngine.Dispose();
            await stream.DisposeAsync();

            //await Task.Delay(1000);

            //open spreadsheet using useshell
            var processInfo = new ProcessStartInfo("syncfusion.xlsx")
            {
                UseShellExecute = true
            };
            Process.Start(processInfo);

            MessageBox.Show($"Exported {worksheets} worksheets with {rows} rows each in {stopwatch.ElapsedMilliseconds} ms");

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var worksheets = (int)sheetsNud.Value;
            var rows = (int)rowsNud.Value;

            var stopwatch = Stopwatch.StartNew();

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

            stopwatch.Stop();

            //dispose of spreadsheet
            await stream.DisposeAsync();

            var processInfo = new ProcessStartInfo("spreadcheetah.xlsx")
            {
                UseShellExecute = true
            };
            Process.Start(processInfo);

            MessageBox.Show($"Exported {worksheets} worksheets with {rows} rows each in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}