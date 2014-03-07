namespace RMS.Monitoring.Device.WebCamera
{
    public class WebCameraService
    {
        private WebCamera wc;

        public WebCameraService(string brand, string model, string deviceManagerName, string deviceManagerID)
        {
            if (brand.ToLower() == "lenovo") wc = new Lenovo(model, deviceManagerName, deviceManagerID);
            else if (brand.ToLower() == "lg") wc = new LG(model, deviceManagerName, deviceManagerID);
        }

        public int Monitoring()
        {
            return wc.CheckDeviceManager();
        }
    }
}
