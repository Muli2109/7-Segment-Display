namespace LedschaltungMitSchaltern.Functions.General_Purpose_Input_Output
{
    #region [ REFERENCES ]

    using Windows.Devices.Gpio;

    #endregion [ REFERENCES  ]

    public class LedOn
    {
        #region [ PUBLIC ATTRIBUTES ]

        public GpioPinValue high = GpioPinValue.High;
        public GpioPinValue low = GpioPinValue.Low;

        #endregion [ PUBLIC ATTRIBUTES ]

        #region [ PUBLIC METHODES ]

        public void TurnLedOn(object sender, object e, GpioPin pin)
        {
            /*
            pinValue1 = GpioPinValue.Low;
            pin1.Write(pinValue1);
            LED.Fill = redBrush;
            */
        }
        #endregion [ PUBLIC METHODES ]
    }
}
