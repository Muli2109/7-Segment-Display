namespace LedschaltungMitSchaltern
{
    #region [ REFERENCES ]

    using LedschaltungMitSchaltern.Functions.Interface_Classes;
    using System;
    using Windows.Devices.Gpio;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Core;

    #endregion [ REFERENCES ]

    public sealed partial class MainPage : Page
    {
        #region [ PRIVATE CONST ATTRIBUTES ]

        #region [ LED REGION ]

        /// <summary>
        /// 
        /// </summary>
        private const int LED_PIN1 = 19;

        /// <summary>
        /// 
        /// </summary>
        private const int LED_PIN2 = 16;

        /// <summary>
        /// 
        /// </summary>
        private const int LED_PIN3 = 26;

        #endregion [ LED REGION ]

        #region [ BUTTON REGION ]

        /// <summary>
        /// 
        /// </summary>
        private const int BUTTON_1 = 6;

        /// <summary>
        /// 
        /// </summary>
        private const int BUTTON_2 = 12;

        /// <summary>
        /// 
        /// </summary>
        private const int BUTTON_3 = 20;

        #endregion [ BUTTON REGION ]

        #region [ GPIO LED STATE ]

        /// <summary>
        /// 
        /// </summary>
        private GpioPinValue High = GpioPinValue.High;

        /// <summary>
        /// 
        /// </summary>
        private GpioPinValue Low = GpioPinValue.Low;

        #endregion [ GPIO LED STATE ]

        #region [ GPIO VALUE PINS ]

        /// <summary>
        /// 
        /// </summary>
        private GpioPinValue ValuePin1;

        /// <summary>
        /// 
        /// </summary>
        private GpioPinValue ValuePin2;

        /// <summary>
        /// 
        /// </summary>
        private GpioPinValue ValuePin3;

        #endregion [ GPIO VALUE PINS ]

        #region [ GPIO DRIVE MODE ]

        /// <summary>
        /// 
        /// </summary>
        private GpioPinDriveMode Output = GpioPinDriveMode.Output;

        #endregion [ GPIO DRIVE MODE ]

        #endregion [ PRIVATE CONST ATTRIBUTES ]

        #region [ PUBLIC ATTRIBUTES ]

        #region [ CONFIG ATTRIBUTES ]

        /// <summary>
        ///     Counter for Display 1-9
        /// </summary>
        public int Counter = 0;

        #endregion [ CONFIG ATTRIBUTES ]

        #region [ GPIO PINS ]

        /// <summary>
        ///     LED Rot
        /// </summary>
        public GpioPin Pin1;

        /// <summary>
        ///     LED Weiß
        /// </summary>
        public GpioPin Pin2;

        /// <summary>
        ///     Button 1
        /// </summary>
        public GpioPin Pin3;

        /// <summary>
        ///     Button 2
        /// </summary>
        public GpioPin Pin4;

        /// <summary>
        ///     Button 2
        /// </summary>
        public GpioPin Pin5;

        /// <summary>
        ///     Button 2
        /// </summary>
        public GpioPin Pin6;

        #endregion [ GPIO PINS ]

        #region [ SYSTEM START ATTRIBUTES ]

        /// <summary>
        ///     Bool for checking if any gpio is connected
        /// </summary>
        public bool GpioCheck;

        /// <summary>
        ///     Bool for checking if Button 1 is connected
        /// </summary>
        public bool ButtonCheck1;

        /// <summary>
        ///     Bool for checking if Button 2 is connected
        /// </summary>
        public bool ButtonCheck2;

        /// <summary>
        ///     Bool for checking if Button 2 is connected
        /// </summary>
        public bool ButtonCheck3;

        #endregion [ SYSTEM START ATTRIBUTES ]

        #endregion [ PUBLIC ATTRIBUTES ]

        #region [ MAIN PAGE ]

        public MainPage()
        {
            InitializeComponent();

            // GPIO Check
            InitGpio gpioCheck = new InitGpio(this.Pin1, this.Pin2);

            this.GpioCheck = gpioCheck.InitGPIO();

            GpioController gpioController = GpioController.GetDefault();

            if (this.GpioCheck)
            {
                #region [ INIT LED 1 ]

                this.Pin1 = gpioController.OpenPin(LED_PIN1);
                this.ValuePin1 = this.High;
                this.Pin1.Write(this.ValuePin1);
                this.Pin1.SetDriveMode(this.Output);

                #endregion [ INIT LED 1 ]

                #region [ INIT LED 2 ]

                this.Pin2 = gpioController.OpenPin(LED_PIN2);
                this.ValuePin2 = this.High;
                this.Pin2.Write(this.ValuePin2);
                this.Pin2.SetDriveMode(this.Output);

                #endregion [ INIT LED 2 ]

                #region [ INIT LED 3 ]

                this.Pin5 = gpioController.OpenPin(LED_PIN3);
                this.ValuePin3 = this.High;
                this.Pin5.Write(this.ValuePin3);
                this.Pin5.SetDriveMode(this.Output);

                #endregion [ INIT LED 3 ]

                #region [ INIT BUTTON 1 ]

                this.Pin3 = gpioController.OpenPin(BUTTON_1);

                // Check if input pull-up resistors are supported
                if (this.Pin3.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                {
                    this.Pin3.SetDriveMode(GpioPinDriveMode.InputPullUp);
                }
                else
                {
                    this.Pin3.SetDriveMode(GpioPinDriveMode.Input);
                }
                // Set a debounce timeout to filter out switch bounce noise from a button press
                this.Pin3.DebounceTimeout = TimeSpan.FromMilliseconds(50);

                // Register for the ValueChanged event so our buttonPin_ValueChanged 
                // function is called when the button is pressed
                this.Pin3.ValueChanged += this.Pin3_ValueChanged;

                #endregion [ INIT BUTTON 1 ]

                #region [ INIT BUTTON 2 ]

                this.Pin4 = gpioController.OpenPin(BUTTON_2);

                // Check if input pull-up resistors are supported
                if (this.Pin4.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                {
                    this.Pin4.SetDriveMode(GpioPinDriveMode.InputPullUp);
                }
                else
                {
                    this.Pin4.SetDriveMode(GpioPinDriveMode.Input);
                }
                // Set a debounce timeout to filter out switch bounce noise from a button press
                this.Pin4.DebounceTimeout = TimeSpan.FromMilliseconds(50);

                // Register for the ValueChanged event so our buttonPin_ValueChanged 
                // function is called when the button is pressed
                this.Pin4.ValueChanged += this.Pin4_ValueChanged;

                #endregion [ INIT BUTTON 2 ]

                #region [ INIT BUTTON 3 ]

                this.Pin6 = gpioController.OpenPin(BUTTON_3);

                // Check if input pull-up resistors are supported
                if (this.Pin6.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                {
                    this.Pin6.SetDriveMode(GpioPinDriveMode.InputPullUp);
                }
                else
                {
                    this.Pin6.SetDriveMode(GpioPinDriveMode.Input);
                }
                // Set a debounce timeout to filter out switch bounce noise from a button press
                this.Pin6.DebounceTimeout = TimeSpan.FromMilliseconds(50);

                // Register for the ValueChanged event so our buttonPin_ValueChanged 
                // function is called when the button is pressed
                this.Pin6.ValueChanged += this.Pin6_ValueChanged;

                #endregion [ INIT BUTTON 3 ]
            }
        }

        #endregion [ MAIN PAGE ]

        #region [ PRIVATE METHODES ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pin3_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            // toggle the state of the LED every time the button is pressed
            if (e.Edge == GpioPinEdge.FallingEdge)
            { 

                if (this.ValuePin1 == GpioPinValue.Low)
                {
                    this.ValuePin1 = this.High;
                    this.Pin1.Write(this.High);
                }
                else
                {
                    this.ValuePin1 = this.Low;
                    this.Pin1.Write(this.Low);
                }
            }

            // need to invoke UI updates on the UI thread because this event
            // handler gets invoked on a separate thread.
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                if (e.Edge == GpioPinEdge.FallingEdge)
                {
                    // Counter ++
                    CounterUp();
                    GpioStatus.Text = "Button Pressed";
                }
                else
                {
                    GpioStatus.Text = "Button Released";
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pin4_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            // toggle the state of the LED every time the button is pressed
            if (e.Edge == GpioPinEdge.FallingEdge)
            {
                if (this.ValuePin2 == GpioPinValue.Low)
                {
                    this.ValuePin2 = this.High;
                    this.Pin2.Write(this.High);
                }
                else
                {
                    this.ValuePin2 = this.Low;
                    this.Pin2.Write(this.Low);
                }
            }

            // need to invoke UI updates on the UI thread because this event
            // handler gets invoked on a separate thread.
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                if (e.Edge == GpioPinEdge.FallingEdge)
                {
                    // Counter ++
                    CounterUp();
                    GpioStatus.Text = "Button Pressed";
                }
                else
                {
                    GpioStatus.Text = "Button Released";
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pin6_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            // toggle the state of the LED every time the button is pressed
            if (e.Edge == GpioPinEdge.FallingEdge)
            {
                if (this.ValuePin3 == GpioPinValue.Low)
                {
                    this.ValuePin3 = this.High;
                    this.Pin5.Write(this.High);
                }
                else
                {
                    this.ValuePin3 = this.Low;
                    this.Pin5.Write(this.Low);
                }
            }
            
            // need to invoke UI updates on the UI thread because this event
            // handler gets invoked on a separate thread.
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                if (e.Edge == GpioPinEdge.FallingEdge)
                {
                    GpioStatus.Text = "Button Pressed";
                    // Counter ++
                    CounterUp();
                }
                else
                {
                    GpioStatus.Text = "Button Released";
                }
            });
        }

        private void CounterUp()
        {
            // Everytime a button is pushed the counter counting up.
            if (this.Counter != 8)
            {
                this.Counter++;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        #endregion [ PRIVATE METHODES ]
    } 
}