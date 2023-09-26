namespace ExportBenchmark;

public class DataRecord
{
    public DataRecord(DateTime dateTime, int value)
    {
        Date = dateTime;
        Value = value;
    }

    public int Value { get; }

    public DateTime Date { get; }
}