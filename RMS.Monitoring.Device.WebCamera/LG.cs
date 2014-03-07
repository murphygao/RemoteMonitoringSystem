namespace RMS.Monitoring.Device.WebCamera
{
    public class LG : Monitoring.Device.WebCamera.WebCamera
    {
        public LG(string model, string deviceManagerName, string deviceManagerID)
            : base("LG", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
