namespace RMS.Monitoring.Device.BarcodeReader
{
    public class Honeywell : global::RMS.Monitoring.Device.BarcodeReader.BarcodeReader
    {
        public Honeywell(string model, string deviceManagerName, string deviceManagerID)
            : base("Honeywell", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
