namespace RMS.Monitoring.Device.PerformanceCounter
{
    public class PerformanceCounter : Device
    {
        public string categoryName;

        public PerformanceCounter(string categoryName)
        {
            this.categoryName = categoryName;
        }

    }
}
