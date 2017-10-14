/* Copyright (c) Julian Polanc 2017
*
* The LED-Light-With-Encoder-IO project is an LED light application for the Raspberry Pi 3 under Windows IoT. 
* The speed (milliseconds) of the running light is determined by a rotary encoder.
*/

namespace LEDLauflichtMitDrehgeberIO
{
    #region [ REFERENCES ]

    using System;
    using Windows.Devices.Gpio;
    using Windows.UI;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

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
        ///     Incremental start value for subtract and add method.
        /// </summary>
        private GpioPinValue INCREMENTALSTARTVALUE;

        #endregion [ GPIO PIN VALUES ]

        #region [ GPIO I/O VALUES ]

        /// <summary>
        ///     Drive mode High
        /// </summary>
        private GpioPinValue High = GpioPinValue.High;

        /// <summary>
        ///     Drive mode low
        /// </summary>
        private GpioPinValue Low = GpioPinValue.Low;

        #endregion [ GPIO I/O VALUES ]

        #region [ CONSTANT INTEGER BUTTON PINS ]

        /// <summary>
        ///     Incremental pin clkW
        /// </summary>
        private const int INCREMENTAL_CLOCKWISE = 17;

        /// <summary>
        ///     Incremental pin dt
        /// </summary>
        private const int INCREMENTAL_COUNTERCLOCKWISE = 18;

        #endregion [ CONSTANT INTEGER BUTTON PINS ]

        #region [ BUTTON REGION ]

        /// <summary>
        ///     Incremental Button (sw);
        /// </summary>
        private const int BUTTON_1 = 5;

        #endregion [ BUTTON REGION ]

        #endregion [ PRIVATE ATTRIBUTES ]

        #region [ PUBLIC ATTRIBUTES ]

        #region [ CONFIG STUFF ]

        /// <summary>
        ///     Incremental start counter (starts at 1000 milliseconds = 1 Sec.)
        /// </summary>
        public int IncrementalCounter = 1000;

        #endregion [ CONFIG STUFF ]

        #region [ GPIO PINS ]

        /// <summary>
        ///     LED White
        /// </summary>
        public GpioPin Pin1;

        /// <summary>
        ///     LED Red
        /// </summary>
        public GpioPin Pin2;

        /// <summary>
        ///     LED Red
        /// </summary>
        public GpioPin Pin3;

        /// <summary>
        ///     LED Red
        /// </summary>
        public GpioPin Pin4;

        /// <summary>
        ///     LED Red
        /// </summary>
        public GpioPin Pin5;

        /// <summary>
        ///     LED Red
        /// </summary>
        public GpioPin Pin6;

        /// <summary>
        ///     LED Red
        /// </summary>
        public GpioPin Pin7;

        /// <summary>
        ///     LED White
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

        /// <summary>
        ///     Incremental Button
        /// </summary>
        public GpioPin Pin11;

        #endregion [ GPIO PINS ]

        #endregion [ PUBLIC ATTRIBUTES ]

        public MainPage()
        {
            this.InitializeComponent();

            GpioController controller = GpioController.GetDefault();

            if (controller == null)
            {
                throw new Exception("No Gpio Pin installed");
            }
            else
            {
                this.CallInitMethodesForOpenPin(controller);
            }
        }

        #region [ EVENTHANDLER ]

        /// <summary>
        ///     Counter Methodes for add.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pin9_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            if (e.Edge == GpioPinEdge.RisingEdge)
            {
                GpioPinValue dirValue = this.Pin10.Read();
                GpioPinValue clkState = this.Pin9.Read();

                if ((this.INCREMENTALSTARTVALUE == this.High) && (dirValue == this.Low) && (clkState == this.High))
                {
                    if (this.IncrementalCounter < 100)
                    {
                        this.IncrementalCounter = this.IncrementalCounter + 5;
                    }
                    else
                    {
                        if (this.IncrementalCounter >= 3000)
                        {
                            this.CallDispatcherAsync("MAXIMUM", sender, e);
                        }
                        else
                        {
                            this.IncrementalCounter = this.IncrementalCounter + 100;
                        }
                    }
                }
            }

            this.CallDispatcherIncremental(this.IncrementalCounter, sender, e);
        }

        /// <summary>
        ///     Counter Methodes for subtract.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pin10_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            if (e.Edge == GpioPinEdge.RisingEdge)
            {
                GpioPinValue dirValue = this.Pin10.Read();
                GpioPinValue clkState = this.Pin9.Read();

                if ((this.INCREMENTALSTARTVALUE == this.High) && (dirValue == this.High) && (clkState == this.Low))
                {
                    if (this.IncrementalCounter > 100)
                    {
                        this.IncrementalCounter = this.IncrementalCounter - 100;
                    }
                    else
                    {
                        if (this.IncrementalCounter == 0)
                        {
                            this.IncrementalCounter = 0;
                            this.CallDispatcherAsync("MINIMUN", sender, e);
                        }
                        else
                        {
                            this.IncrementalCounter = this.IncrementalCounter - 5;
                        }
                    }
                }
            }

            this.CallDispatcherIncremental(this.IncrementalCounter, sender, e);
        }

        /// <summary>
        ///     Start Led chaser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pin11_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            GpioController controller = GpioController.GetDefault();

            if (e.Edge == GpioPinEdge.RisingEdge)
            {
                if (this.Pin11.Read() == this.High)
                {
                    this.CallDispatcherAsync("ON", sender, e);

                    while (this.Pin11.Read() == High)
                    {
                        this.InitSegmentPort_On_1(controller);
                        System.Threading.Tasks.Task.Delay(this.IncrementalCounter).Wait();
                        this.InitSegmentPort_Off_1(controller);

                        this.InitSegmentPort_On_2(controller);
                        System.Threading.Tasks.Task.Delay(this.IncrementalCounter).Wait();
                        this.InitSegmentPort_Off_2(controller);

                        this.InitSegmentPort_On_3(controller);
                        System.Threading.Tasks.Task.Delay(this.IncrementalCounter).Wait();
                        this.InitSegmentPort_Off_3(controller);

                        this.InitSegmentPort_On_4(controller);
                        System.Threading.Tasks.Task.Delay(this.IncrementalCounter).Wait();
                        this.InitSegmentPort_Off_4(controller);

                        this.InitSegmentPort_On_5(controller);
                        System.Threading.Tasks.Task.Delay(this.IncrementalCounter).Wait();
                        this.InitSegmentPort_Off_5(controller);

                        this.InitSegmentPort_On_6(controller);
                        System.Threading.Tasks.Task.Delay(this.IncrementalCounter).Wait();
                        this.InitSegmentPort_Off_6(controller);

                        this.InitSegmentPort_On_7(controller);
                        System.Threading.Tasks.Task.Delay(this.IncrementalCounter).Wait();
                        this.InitSegmentPort_Off_7(controller);

                        this.InitSegmentPort_On_8(controller);
                        System.Threading.Tasks.Task.Delay(this.IncrementalCounter).Wait();
                        this.InitSegmentPort_Off_8(controller);
                    }
                }
            }
        }

        /// <summary>
        ///     Set values for XAML Textboxes.
        /// </summary>
        /// <param name="status"></param>
        private void CallDispatcherAsync(string status, GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                switch (status)
                {
                    case "ON": LED_STATUS.Background = new SolidColorBrush(Colors.Green); LED_STATUS.Text = status; break;
                    case "OFF": LED_STATUS.Background = new SolidColorBrush(Colors.Red); LED_STATUS.Text = status; break;
                    case "MINIMUN": return;
                    case "MAXIMUM": return;
                    default: return;
                }
            });
        }

        #endregion [ EVENTHANDLER ]

        #region [ CALL METHODES ]

        /// <summary>
        ///     Set count value for XAML Textbox.
        /// </summary>
        /// <param name="status"></param>
        private void CallDispatcherIncremental(int count, GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            var taskIncremental = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                INCREMENTAL_COUNT.Text = count.ToString();
            });
        }

        /// <summary>
        ///     Init Mehtode for open pins, set drive mode and gpio value
        /// </summary>
        /// <param name="controller"></param>
        private void CallInitMethodesForOpenPin(GpioController controller)
        {
            #region [ INIT PINS FOR LED ]

            /// Open Pin 1 for drivemode output and value Low
            this.Pin1 = controller.OpenPin(LED_1);
            this.Pin1.SetDriveMode(GpioPinDriveMode.Output);
            this.Pin1.Write(GpioPinValue.Low);

            /// Open Pin 2 for drivemode output and value Low
            this.Pin2 = controller.OpenPin(LED_2);
            this.Pin2.SetDriveMode(GpioPinDriveMode.Output);
            this.Pin2.Write(GpioPinValue.Low);

            /// Open Pin 3 for drivemode output and value Low
            this.Pin3 = controller.OpenPin(LED_3);
            this.Pin3.SetDriveMode(GpioPinDriveMode.Output);
            this.Pin3.Write(GpioPinValue.Low);

            /// Open Pin 4 for drivemode output and value Low
            this.Pin4 = controller.OpenPin(LED_4);
            this.Pin4.SetDriveMode(GpioPinDriveMode.Output);
            this.Pin4.Write(GpioPinValue.Low);

            /// Open Pin 5 for drivemode output and value Low
            this.Pin5 = controller.OpenPin(LED_5);
            this.Pin5.SetDriveMode(GpioPinDriveMode.Output);
            this.Pin5.Write(GpioPinValue.Low);

            /// Open Pin 6 for drivemode output and value Low
            this.Pin6 = controller.OpenPin(LED_6);
            this.Pin6.SetDriveMode(GpioPinDriveMode.Output);
            this.Pin6.Write(GpioPinValue.Low);

            /// Open Pin 7 for drivemode output and value Low
            this.Pin7 = controller.OpenPin(LED_7);
            this.Pin7.SetDriveMode(GpioPinDriveMode.Output);
            this.Pin7.Write(GpioPinValue.Low);

            /// Open Pin 8 for drivemode output and value Low
            this.Pin8 = controller.OpenPin(LED_8);
            this.Pin8.SetDriveMode(GpioPinDriveMode.Output);
            this.Pin8.Write(GpioPinValue.Low);

            #endregion [ INIT PINS FOR LED ]

            #region [ INCREMENT EVENTHANDLER ]

            this.Pin9 = controller.OpenPin(INCREMENTAL_CLOCKWISE);
            this.Pin10 = controller.OpenPin(INCREMENTAL_COUNTERCLOCKWISE);

            if (this.Pin9.IsDriveModeSupported(GpioPinDriveMode.InputPullUp)) {
                this.Pin9.SetDriveMode(GpioPinDriveMode.Input);
            }

            if (this.Pin10.IsDriveModeSupported(GpioPinDriveMode.InputPullUp)) {
                this.Pin10.SetDriveMode(GpioPinDriveMode.Input);
            }

            this.Pin9.DebounceTimeout = TimeSpan.Zero;
            this.Pin10.DebounceTimeout = TimeSpan.Zero;

            this.INCREMENTALSTARTVALUE = this.Pin10.Read();

            this.Pin9.ValueChanged += this.Pin9_ValueChanged;
            this.Pin10.ValueChanged += this.Pin10_ValueChanged;

            #endregion [ INCREMENT EVENTHANDLER ]

            #region [ BUTTON EVENTHANDLER ]

            this.Pin11 = controller.OpenPin(BUTTON_1);

            if (this.Pin11.IsDriveModeSupported(GpioPinDriveMode.InputPullUp)) {
                this.Pin11.SetDriveMode(GpioPinDriveMode.InputPullUp);
            } else {
                this.Pin11.SetDriveMode(GpioPinDriveMode.Input);
            }
            this.Pin11.DebounceTimeout = TimeSpan.FromMilliseconds(1);
            this.Pin11.ValueChanged += this.Pin11_ValueChanged;

            #endregion [ BUTTON EVENTHANDLER ]
        }

        #endregion [ CALL METHODES ]

        #region [ INIT METHODES ]

        #region [ LED_1 ON/OFF ]

        /// <summary>
        ///     Turn on LED Number 1
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_1(GpioController controller)
        {
            this.LED_1_VALUE = this.High;
            this.Pin1.Write(this.LED_1_VALUE);
        }

        /// <summary>
        ///     Turn off LED Number 1
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
        ///     Turn on LED Number 2
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_2(GpioController controller)
        {
            this.LED_2_VALUE = this.High;
            this.Pin2.Write(this.LED_2_VALUE);
        }

        /// <summary>
        ///     Turn off LED Number 2
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
        ///     Turn on LED Number 3
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_3(GpioController controller)
        {
            this.LED_3_VALUE = this.High;
            this.Pin3.Write(this.LED_3_VALUE);
        }

        /// <summary>
        ///     Turn off LED Number 3
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
        ///     Turn on LED Number 4
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_4(GpioController controller)
        {
            this.LED_4_VALUE = this.High;
            this.Pin4.Write(this.LED_4_VALUE);
        }

        /// <summary>
        ///     Turn off LED Number 4
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
        ///     Turn on LED Number 5
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_5(GpioController controller)
        {
            this.LED_5_VALUE = this.High;
            this.Pin5.Write(this.LED_5_VALUE);
        }

        /// <summary>
        ///     Turn off LED Number 5
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
        ///     Turn on LED Number 6
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_6(GpioController controller)
        {
            this.LED_6_VALUE = this.High;
            this.Pin6.Write(this.LED_6_VALUE);
        }

        /// <summary>
        ///     Turn off LED Number 6
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
        ///     Turn on LED Number 7
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_7(GpioController controller)
        {
            this.LED_7_VALUE = this.High;
            this.Pin7.Write(this.LED_7_VALUE);
        }

        /// <summary>
        ///     Turn off LED Number 7
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
        ///     Turn on LED Number 7
        /// </summary>
        /// <param name="controller"></param>
        public void InitSegmentPort_On_8(GpioController controller)
        {
            this.LED_8_VALUE = this.High;
            this.Pin8.Write(this.LED_8_VALUE);
        }

        /// <summary>
        ///     Turn off LED Number 8
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