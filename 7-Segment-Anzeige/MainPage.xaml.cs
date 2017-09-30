namespace _7_Segment_Anzeige
{
    #region [ REFERENCES ]

    using _7_Segment_Anzeige.Exceptions;
    using System.Collections.Generic;
    using Windows.Devices.Gpio;
    using Windows.UI.Xaml.Controls;

    #endregion [ REFERENCES ]

    public sealed partial class MainPage : Page
    {
        #region [ PRIVATE ATTRIBUTES ]

        #region [ CONST INTEGER FOR SEGMENT PINS ]

        /// <summary>
        ///     Segment pin for Anode A
        /// </summary>
        private const int SEGMENT_PIN_A = 19;

        /// <summary>
        ///     Segment pin for Anode B
        /// </summary>
        private const int SEGMENT_PIN_B = 26;

        /// <summary>
        ///     Segment pin for Anode C
        /// </summary>
        private const int SEGMENT_PIN_C = 20;

        /// <summary>
        ///     Segment pin for Anode D
        /// </summary>
        private const int SEGMENT_PIN_D = 16;

        /// <summary>
        ///     Segment pin for Anode E
        /// </summary>
        private const int SEGMENT_PIN_E = 12;

        /// <summary>
        ///     Segment pin for Anode F
        /// </summary>
        private const int SEGMENT_PIN_F = 13;

        /// <summary>
        ///     Segment pin for Anode G
        /// </summary>
        private const int SEGMENT_PIN_G = 6;

        /// <summary>
        ///     Segment pin for Dot Anode
        /// </summary>
        private const int SEGMENT_PIN_DOT = 21;

        #endregion [ CONST INTEGER FOR SEGMENT PINS ]

        #region [ GPIO SEGMENT PIN ]

        /// <summary>
        ///     GPIO pin Pin 1 Value
        /// </summary>
        private GpioPin Pin1;

        /// <summary>
        ///     GPIO pin Pin 2 Value
        /// </summary>
        private GpioPin Pin2;

        /// <summary>
        ///     GPIO pin Pin 3 Value
        /// </summary>
        private GpioPin Pin3;

        /// <summary>
        ///     GPIO pin Pin 4 Value
        /// </summary>
        private GpioPin Pin4;

        /// <summary>
        ///     GPIO pin Pin 5 Value
        /// </summary>
        private GpioPin Pin5;

        /// <summary>
        ///     GPIO pin Pin 6 Value
        /// </summary>
        private GpioPin Pin6;

        /// <summary>
        ///     GPIO pin Pin 7 Value
        /// </summary>
        private GpioPin Pin7;

        /// <summary>
        ///     GPIO pin Pin 8 Value
        /// </summary>
        private GpioPin Pin8;

        #endregion [ GPIO SEGMENT PIN ]

        #region [ GPIO STATE ]

        /// <summary>
        ///     Pin Value High "ON"
        /// </summary>
        private GpioPinValue High = GpioPinValue.High;

        /// <summary>
        ///     Pin Value High "OFF"
        /// </summary>
        private GpioPinValue Low = GpioPinValue.Low;

        #endregion [ GPIO STATE ]

        #region [ GPIO DRIVE MODE ]

        /// <summary>
        ///     GPIO DriveMode "Output"
        /// </summary>
        private GpioPinDriveMode Output = GpioPinDriveMode.Output;

        #endregion [ GPIO DRIVE MODE ]

        #region [ POSSIBLES SEGMENT VALUES ]

        /// <summary>
        ///     Segment Value for GPIO Pin 1
        /// </summary>
        private GpioPinValue SegmentValue1;

        /// <summary>
        ///     Segment Value for GPIO Pin 2
        /// </summary>
        private GpioPinValue SegmentValue2;

        /// <summary>
        ///     Segment Value for GPIO Pin 3
        /// </summary>
        private GpioPinValue SegmentValue3;

        /// <summary>
        ///     Segment Value for GPIO Pin 4
        /// </summary>
        private GpioPinValue SegmentValue4;

        /// <summary>
        ///     Segment Value for GPIO Pin 5
        /// </summary>
        private GpioPinValue SegmentValue5;

        /// <summary>
        ///     Segment Value for GPIO Pin 6
        /// </summary>
        private GpioPinValue SegmentValue6;

        /// <summary>
        ///     Segment Value for GPIO Pin 7
        /// </summary>
        private GpioPinValue SegmentValue7;

        /// <summary>
        ///     Segment Value for GPIO Pin 8
        /// </summary>
        private GpioPinValue SegmentValue8;

        #endregion [ POSSIBLES SEGMENT VALUES ]

        #region [ INIT DICTIONARY ]

        /// <summary>
        ///     Dictionary for Segment Numbers to display
        /// </summary>
        public Dictionary<int, List<int>> numberDictionary = new Dictionary<int, List<int>>();

        #endregion [ INIT DICTIONARY ]

        #endregion [ PRIVATE ATTRIBUTES ]

        #region [ MAIN FUNCTION ]

        /// <summary>
        ///     Main function
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            GpioController gpiocontroller = GpioController.GetDefault();

            if (gpiocontroller == null)
            {
                throw new System.Exception("gpio not installed");
            }
            else
            {
                try
                {
                    this.InitializeNumbersForDisplay();
                }
                catch (ExceptionDictionaryNotFound ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        /// <summary>
        ///     Button eventhandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GpioController gpioController = GpioController.GetDefault();

            this.SetSegmentValueToLow(gpioController);

            foreach (var item in this.numberDictionary)
            {
                if (Count.Text == item.Key.ToString() && Count.Text.Length > 2)
                {
                    if (item.Value[0] == 1)
                    {
                        InitSegmentPort_1(gpioController);
                    } // A
                    if (item.Value[1] == 1)
                    {
                        InitSegmentPort_2(gpioController);
                    } // B
                    if (item.Value[2] == 1)
                    {
                        InitSegmentPort_3(gpioController);
                    } // C
                    if (item.Value[3] == 1)
                    {
                        InitSegmentPort_4(gpioController);
                    } // D
                    if (item.Value[4] == 1)
                    {
                        InitSegmentPort_5(gpioController);
                    } // E
                    if (item.Value[5] == 1)
                    {
                        InitSegmentPort_6(gpioController);
                    } // F
                    if (item.Value[6] == 1)
                    {
                        InitSegmentPort_7(gpioController);
                    } // G
                    if (item.Value[7] == 1)
                    {
                        InitSegmentPort_8(gpioController);
                    } // DOT.
                }
            }
        }

        #endregion [ MAIN FUNCTION ]

        #region [ PUBLIC INIT METHODES ]

        /// <summary>
        ///     Initialize the dictionary for the segment display numbers.
        /// </summary>
        public void InitializeNumbersForDisplay()
        {
            this.numberDictionary.Add(0, new List<int> { 1, 1, 1, 1, 1, 1, 0, 1 }); // 0
            this.numberDictionary.Add(1, new List<int> { 0, 1, 1, 0, 0, 0, 0, 1 }); // 1
            this.numberDictionary.Add(2, new List<int> { 1, 1, 0, 1, 1, 0, 1, 1 }); // 2
            this.numberDictionary.Add(3, new List<int> { 1, 1, 1, 1, 0, 0, 1, 1 }); // 3
            this.numberDictionary.Add(4, new List<int> { 0, 1, 1, 0, 0, 1, 1, 1 }); // 4
            this.numberDictionary.Add(5, new List<int> { 1, 0, 1, 1, 0, 1, 1, 1 }); // 5
            this.numberDictionary.Add(6, new List<int> { 1, 0, 1, 1, 1, 1, 1, 1 }); // 6
            this.numberDictionary.Add(7, new List<int> { 1, 1, 1, 0, 0, 0, 0, 1 }); // 7
            this.numberDictionary.Add(8, new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 }); // 8
            this.numberDictionary.Add(9, new List<int> { 1, 1, 1, 0, 0, 1, 1, 1 }); // 9
        }

        /// <summary>
        ///     Set all Segment Ports to "OFF"
        /// </summary>
        /// <param name="controller"></param>
        public void SetSegmentValueToLow(GpioController controller)
        {
            #region [ SEGMENT PORT 1 OFF ]

            this.SegmentValue1 = this.Low;
            this.Pin1.Write(this.SegmentValue1);

            #endregion [ SEGMENT PORT 1 OFF ]

            #region [ SEGMENT PORT 2 OFF ]

            this.SegmentValue2 = this.Low;
            this.Pin2.Write(this.SegmentValue2);

            #endregion [ SEGMENT PORT 2 OFF ]

            #region [ SEGMENT PORT 3 OFF ]

            this.SegmentValue3 = this.Low;
            this.Pin3.Write(this.SegmentValue3);

            #endregion [ SEGMENT PORT 3 OFF ]

            #region [ SEGMENT PORT 4 OFF ]

            this.SegmentValue4 = this.Low;
            this.Pin4.Write(this.SegmentValue4);

            #endregion [ SEGMENT PORT 4 OFF ]

            #region [ SEGMENT PORT 5 OFF ]

            this.SegmentValue5 = this.Low;
            this.Pin5.Write(this.SegmentValue5);

            #endregion [ SEGMENT PORT 5 OFF ]

            #region [ SEGMENT PORT 6 OFF ]

            this.SegmentValue6 = this.Low;
            this.Pin6.Write(this.SegmentValue6);

            #endregion [ SEGMENT PORT 6 OFF ]

            #region [ SEGMENT PORT 7 OFF ]

            this.SegmentValue7 = this.Low;
            this.Pin7.Write(this.SegmentValue7);

            #endregion [ SEGMENT PORT 7 OFF ]

            #region [ SEGMENT PORT 8 OFF ]

            this.SegmentValue8 = this.Low;
            this.Pin8.Write(this.SegmentValue8);

            #endregion [ SEGMENT PORT 8 OFF ]
        }

        /// <summary>
        ///     Set Segment 1 Port to "ON"
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_1(GpioController controller)
        {
            #region [ INIT SEGMENT PORT 1 ]

            this.Pin1 = controller.OpenPin(SEGMENT_PIN_A);
            this.SegmentValue1 = this.High;
            this.Pin1.Write(this.SegmentValue1);
            this.Pin1.SetDriveMode(this.Output);

            #endregion [ INIT SEGMENT PORT 1 ]
        }

        /// <summary>
        ///     Set Segment 2 Port to "ON"
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_2(GpioController controller)
        {
            #region [ INIT SEGMENT PORT 2 ]

            this.Pin2 = controller.OpenPin(SEGMENT_PIN_B);
            this.SegmentValue2 = this.High;
            this.Pin2.Write(this.SegmentValue2);
            this.Pin2.SetDriveMode(this.Output);

            #endregion [ INIT SEGMENT PORT 2 ]
        }

        /// <summary>
        ///     Set Segment 3 Port to "ON"
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_3(GpioController controller)
        {
            #region [ INIT SEGMENT PORT 3 ]

            this.Pin3 = controller.OpenPin(SEGMENT_PIN_C);
            this.SegmentValue3 = this.High;
            this.Pin3.Write(this.SegmentValue3);
            this.Pin3.SetDriveMode(this.Output);

            #endregion [ INIT SEGMENT PORT 3 ]
        }

        /// <summary>
        ///     Set Segment 4 Port to "ON"
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_4(GpioController controller)
        {
            #region [ INIT SEGMENT PORT 4 ]

            this.Pin4 = controller.OpenPin(SEGMENT_PIN_D);
            this.SegmentValue4 = this.High;
            this.Pin4.Write(this.SegmentValue4);
            this.Pin4.SetDriveMode(this.Output);

            #endregion [ INIT SEGMENT PORT 4 ]
        }

        /// <summary>
        ///     Set Segment 5 Port to "ON"
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_5(GpioController controller)
        {
            #region [ INIT SEGMENT PORT 5 ]

            this.Pin5 = controller.OpenPin(SEGMENT_PIN_E);
            this.SegmentValue5 = this.High;
            this.Pin5.Write(this.SegmentValue5);
            this.Pin5.SetDriveMode(this.Output);

            #endregion [ INIT SEGMENT PORT 5 ]
        }

        /// <summary>
        ///     Set Segment 6 Port to "ON"
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_6(GpioController controller)
        {
            #region [ INIT SEGMENT PORT 6 ]

            this.Pin6 = controller.OpenPin(SEGMENT_PIN_F);
            this.SegmentValue6 = this.High;
            this.Pin6.Write(this.SegmentValue6);
            this.Pin6.SetDriveMode(this.Output);

            #endregion [ INIT SEGMENT PORT 6 ]
        }

        /// <summary>
        ///     Set Segment 7 Port to "ON"
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_7(GpioController controller)
        {
            #region [ INIT SEGMENT PORT 7 ]

            this.Pin7 = controller.OpenPin(SEGMENT_PIN_G);
            this.SegmentValue7 = this.High;
            this.Pin7.Write(this.SegmentValue7);
            this.Pin7.SetDriveMode(this.Output);

            #endregion [ INIT SEGMENT PORT 7 ]
        }

        /// <summary>
        ///     Set Segment 8 Port to "ON"
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_8(GpioController controller)
        {
            #region [ INIT SEGMENT PORT 8 ]

            this.Pin8 = controller.OpenPin(SEGMENT_PIN_DOT);
            this.SegmentValue8 = this.High;
            this.Pin8.Write(this.SegmentValue8);
            this.Pin8.SetDriveMode(this.Output);

            #endregion [ INIT SEGMENT PORT 8 ]
        }

        #endregion [ PUBLIC INIT METHODES ]
    }
}