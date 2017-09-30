namespace LEDLauflichtMitDrehgeberIO
{
    #region [ REFERENCES ]

    using System;
    using Windows.Devices.Gpio;
    using Windows.UI.Xaml.Controls;

    #endregion [ REFERENCES ]

    public sealed partial class MainPage : Page
    {
        #region [ PRIVATE ATTRIBUTES ]

        #region [ CONSTANT INTEGER GPIO PINS ]

        /// <summary>
        ///     LED GPIO PIN 1
        /// </summary>
        private const int LED_1 = 21;

        /// <summary>
        ///     LED GPIO PIN 2
        /// </summary>
        private const int LED_2 = 20;

        /// <summary>
        ///     LED GPIO PIN 3
        /// </summary>
        private const int LED_3 = 26;

        /// <summary>
        ///     LED GPIO PIN 4
        /// </summary>
        private const int LED_4 = 19;

        /// <summary>
        ///     LED GPIO PIN 5
        /// </summary>
        private const int LED_5 = 16;

        /// <summary>
        ///     LED GPIO PIN 6
        /// </summary>
        private const int LED_6 = 13;

        /// <summary>
        ///     LED GPIO PIN 7
        /// </summary>
        private const int LED_7 = 6;

        /// <summary>
        ///     LED GPIO PIN 8
        /// </summary>
        private const int LED_8 = 12;

        #endregion [ CONSTANT INTEGER GPIO PINS ]

        #region [ GPIO PIN VALUES ]

        /// <summary>
        ///     GPIO Pin Value for LED_1
        /// </summary>
        private GpioPinValue LED_1_VALUE;

        /// <summary>
        ///     GPIO Pin Value for LED_2
        /// </summary>
        private GpioPinValue LED_2_VALUE;

        /// <summary>
        ///     GPIO Pin Value for LED_3
        /// </summary>
        private GpioPinValue LED_3_VALUE;

        /// <summary>
        ///     GPIO Pin Value for LED_4
        /// </summary>
        private GpioPinValue LED_4_VALUE;

        /// <summary>
        ///     GPIO Pin Value for LED_5
        /// </summary>
        private GpioPinValue LED_5_VALUE;

        /// <summary>
        ///     GPIO Pin Value for LED_6
        /// </summary>
        private GpioPinValue LED_6_VALUE;

        /// <summary>
        ///     GPIO Pin Value for LED_7
        /// </summary>
        private GpioPinValue LED_7_VALUE;

        /// <summary>
        ///     GPIO Pin Value for LED_8
        /// </summary>
        private GpioPinValue LED_8_VALUE;

        /// <summary>
        ///     GPIO Pin Value for Incremental Clockwise Pin
        /// </summary>
        private GpioPinValue LED_9_VALUE;

        /// <summary>
        ///     GPIO Pin Value for Incremental CounterClockwise Pin
        /// </summary>
        private GpioPinValue LED_10_VALUE;

        #endregion [ GPIO PIN VALUES ]

        #region [ GPIO PIN DRIVEMODES ]

        /// <summary>
        ///
        /// </summary>
        private GpioPinDriveMode Output = GpioPinDriveMode.Output;

        #endregion [ GPIO PIN DRIVEMODES ]

        #region [ GPIO I/O VALUES ]

        /// <summary>
        ///
        /// </summary>
        private GpioPinValue High = GpioPinValue.High;

        /// <summary>
        ///
        /// </summary>
        private GpioPinValue Low = GpioPinValue.Low;

        #endregion [ GPIO I/O VALUES ]

        #region [ CONSTANT INTEGER BUTTON PINS ]

        /// <summary>
        ///
        /// </summary>
        private const int BUTTON_INCREMENTAL_CLOCKWISE = 17;

        /// <summary>
        ///
        /// </summary>
        private const int BUTTON_INCREMENTAL_COUNTERCLOCKWISE = 18;

        #endregion [ CONSTANT INTEGER BUTTON PINS ]

        #region [ BUTTON REGION ]

        /// <summary>
        ///
        /// </summary>
        private const int BUTTON_1;

        #endregion [ BUTTON REGION ]

        #region [ INCREMENTAL GPIO PINS ]

        /// <summary>
        ///
        /// </summary>
        private GpioPin Incremental_Pin_Clockwise;

        /// <summary>
        ///
        /// </summary>
        private GpioPin Incremental_Pin_CounterClockwise;

        #endregion [ INCREMENTAL GPIO PINS ]

        #endregion [ PRIVATE ATTRIBUTES ]

        #region [ PUBLIC ATTRIBUTES ]

        #region [ GPIO PINS ]

        /// <summary>
        ///     LED Weiß
        /// </summary>
        public GpioPin Pin1;

        /// <summary>
        ///     LED Weiß
        /// </summary>
        public GpioPin Pin2;

        /// <summary>
        ///     LED Rot
        /// </summary>
        public GpioPin Pin3;

        /// <summary>
        ///     LED Rot
        /// </summary>
        public GpioPin Pin4;

        /// <summary>
        ///     LED Gruen
        /// </summary>
        public GpioPin Pin5;

        /// <summary>
        ///     LED Gruen
        /// </summary>
        public GpioPin Pin6;

        /// <summary>
        ///     LED Gelb
        /// </summary>
        public GpioPin Pin7;

        /// <summary>
        ///     LED Gelb
        /// </summary>
        public GpioPin Pin8;

        /// <summary>
        ///     Incremental Clockwise pin
        /// </summary>
        public GpioPin Pin9;

        /// <summary>
        ///     Incremental Counterclockwise pin
        /// </summary>
        public GpioPin Pin10;

        #endregion [ GPIO PINS ]

        #endregion [ PUBLIC ATTRIBUTES ]

        public MainPage()
        {
            this.InitializeComponent();

            GpioController gpioController = GpioController.GetDefault();

            if (gpioController == null)
            {
                throw new Exception("No Gpio Pin installed");
            }

            bool needed = false;
            if (needed)
            {
                // Turn all LED´s ON
                this.CallInitMethodesForON(gpioController);

                // Turn all LED´s OFF
                this.CallInitMethodesForOFF(gpioController);
            }
        }

        #region [ CALL METHODES ]

        /// <summary>
        ///
        /// </summary>
        private void CallInitMethodesForON(GpioController gpioController)
        {
            this.InitSegmentPort_On_1(gpioController);
            this.InitSegmentPort_On_2(gpioController);
            this.InitSegmentPort_On_3(gpioController);
            this.InitSegmentPort_On_4(gpioController);
            this.InitSegmentPort_On_5(gpioController);
            this.InitSegmentPort_On_6(gpioController);
            this.InitSegmentPort_On_7(gpioController);
            this.InitSegmentPort_On_8(gpioController);
        }

        /// <summary>
        ///
        /// </summary>
        private void CallInitMethodesForOFF(GpioController gpioController)
        {
            this.InitSegmentPort_Off_1(gpioController);
            this.InitSegmentPort_Off_2(gpioController);
            this.InitSegmentPort_Off_3(gpioController);
            this.InitSegmentPort_Off_4(gpioController);
            this.InitSegmentPort_Off_5(gpioController);
            this.InitSegmentPort_Off_6(gpioController);
            this.InitSegmentPort_Off_7(gpioController);
            this.InitSegmentPort_Off_8(gpioController);
        }

        #endregion [ CALL METHODES ]

        #region [ INIT METHODES ]

        #region [ LED_1 ON/OFF ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_1(GpioController controller)
        {
            this.Pin1 = controller.OpenPin(LED_1);
            this.LED_1_VALUE = this.High;
            this.Pin1.Write(this.LED_1_VALUE);
            this.Pin1.SetDriveMode(this.Output);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_Off_1(GpioController controller)
        {
            this.LED_1_VALUE = this.Low;
            this.Pin1.Write(this.LED_1_VALUE);
        }

        #endregion [ LED_1 ON/OFF ]

        #region [ LED_2 ON/OFF ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_2(GpioController controller)
        {
            this.Pin2 = controller.OpenPin(LED_2);
            this.LED_2_VALUE = this.High;
            this.Pin2.Write(this.LED_1_VALUE);
            this.Pin2.SetDriveMode(this.Output);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_Off_2(GpioController controller)
        {
            this.LED_2_VALUE = this.Low;
            this.Pin2.Write(this.LED_2_VALUE);
        }

        #endregion [ LED_2 ON/OFF ]

        #region [ LED_3 ON/OFF ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_3(GpioController controller)
        {
            this.Pin3 = controller.OpenPin(LED_3);
            this.LED_3_VALUE = this.High;
            this.Pin3.Write(this.LED_3_VALUE);
            this.Pin3.SetDriveMode(this.Output);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_Off_3(GpioController controller)
        {
            this.LED_3_VALUE = this.Low;
            this.Pin3.Write(this.LED_3_VALUE);
        }

        #endregion [ LED_3 ON/OFF ]

        #region [ LED_4 ON/OFF ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_4(GpioController controller)
        {
            this.Pin4 = controller.OpenPin(LED_4);
            this.LED_4_VALUE = this.High;
            this.Pin4.Write(this.LED_4_VALUE);
            this.Pin4.SetDriveMode(this.Output);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_Off_4(GpioController controller)
        {
            this.LED_4_VALUE = this.Low;
            this.Pin4.Write(this.LED_4_VALUE);
        }

        #endregion [ LED_4 ON/OFF ]

        #region [ LED_5 ON/OFF ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_5(GpioController controller)
        {
            this.Pin5 = controller.OpenPin(LED_5);
            this.LED_5_VALUE = this.High;
            this.Pin5.Write(this.LED_5_VALUE);
            this.Pin5.SetDriveMode(this.Output);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_Off_5(GpioController controller)
        {
            this.LED_5_VALUE = this.Low;
            this.Pin5.Write(this.LED_5_VALUE);
        }

        #endregion [ LED_5 ON/OFF ]

        #region [ LED_6 ON/OFF ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_6(GpioController controller)
        {
            this.Pin6 = controller.OpenPin(LED_6);
            this.LED_6_VALUE = this.High;
            this.Pin6.Write(this.LED_6_VALUE);
            this.Pin6.SetDriveMode(this.Output);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_Off_6(GpioController controller)
        {
            this.LED_6_VALUE = this.Low;
            this.Pin6.Write(this.LED_6_VALUE);
        }

        #endregion [ LED_6 ON/OFF ]

        #region [ LED_7 ON/OFF ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_7(GpioController controller)
        {
            this.Pin7 = controller.OpenPin(LED_7);
            this.LED_7_VALUE = this.High;
            this.Pin7.Write(this.LED_7_VALUE);
            this.Pin7.SetDriveMode(this.Output);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_Off_7(GpioController controller)
        {
            this.LED_7_VALUE = this.Low;
            this.Pin7.Write(this.LED_7_VALUE);
        }

        #endregion [ LED_7 ON/OFF ]

        #region [ LED_8 ON/OFF ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_8(GpioController controller)
        {
            this.Pin8 = controller.OpenPin(LED_8);
            this.LED_8_VALUE = this.High;
            this.Pin8.Write(this.LED_8_VALUE);
            this.Pin8.SetDriveMode(this.Output);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_Off_8(GpioController controller)
        {
            this.LED_8_VALUE = this.Low;
            this.Pin8.Write(this.LED_8_VALUE);
        }

        #endregion [ LED_8 ON/OFF ]

        #endregion [ INIT METHODES ]
    }
}