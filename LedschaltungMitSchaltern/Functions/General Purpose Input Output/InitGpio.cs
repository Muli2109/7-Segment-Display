namespace LedschaltungMitSchaltern.Functions.Interface_Classes
{
    #region [ REFERENCES ]

    using Windows.Devices.Gpio;
    using System;

    #endregion [ REFERENCES ]

    public class InitGpio
    {
        #region [ PRIVATE ATTRIBUTES ]

        private GpioPin gpio1;
        private GpioPin gpio2;

        #endregion [ PRIVATE ATTRIBUTES  ]

        #region [ CONSTRUCTOR ]

        public InitGpio(GpioPin pin1, GpioPin pin2)
        {
            this.gpio1 = pin1;
            this.gpio2 = pin2;
        }

        #endregion [ CONSTRUCTOR ]

        public Boolean InitGPIO()
        {
            bool returnValue = true;

            var gpio = GpioController.GetDefault();

            // Check if any gpio is connected
            if(gpio == null)
            {
                this.gpio1 = null;
                this.gpio2 = null;

                returnValue = false;
            }
            return returnValue;
        }
    }
}
