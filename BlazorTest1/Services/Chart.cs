using Microsoft.AspNetCore.Blazor.Browser.Interop;

namespace no.rag.NerdDice.Services
{
    public static class Chart
    {
        public static void UpdateChart(ChartData chartData)
        {
            RegisteredFunction.Invoke<object>("drawChart", chartData);
        }
    }

    public class ChartData
    {
        public string[] labels { get; set; }
        public string[] values { get; set; }
        public string caption { get; set; }
    }
}
