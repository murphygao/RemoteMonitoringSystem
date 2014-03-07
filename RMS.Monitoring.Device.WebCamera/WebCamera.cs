namespace RMS.Monitoring.Device.WebCamera
{
    public class WebCamera : Device
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="deviceManagerName">Device's name is shown in Device Manager.</param>
        /// <param name="deviceManagerID"></param>
        public WebCamera(string brand, string model, string deviceManagerName, string deviceManagerID)
            : base(brand, model, deviceManagerName, deviceManagerID)
        {
        }

    }
}
