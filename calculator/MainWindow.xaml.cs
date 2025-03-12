using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        double TempValue = 0;
        int temval = 0; 
        private string Operation { get; set; } = string.Empty;

        private string output = string.Empty;
        public string Output
        {
            get
            {
                return output;
            }

            set
            {
                output = value;
                OnPropertyChanged(nameof(Output));
                OnPropertyChanged(nameof(Output));
                OnPropertyChanged(nameof(HexValue));
                OnPropertyChanged(nameof(DecValue));
                OnPropertyChanged(nameof(OctValue));
                OnPropertyChanged(nameof(BinValue));
            }
        }

        public string HexValue => ConvertToHex(Output);
        public string DecValue => ConvertToDecimal(Output);
        public string OctValue => ConvertToOctal(Output);
        public string BinValue => ConvertToBinary(Output);



        private string memoryValue = "0";
        private MemoryList memoryList;

        public string MemoryValue
        {
            get { return memoryValue; }
            set { memoryValue = value; OnPropertyChanged(nameof(MemoryValue)); }
        }


        public MemoryList MemoryList { get => memoryList; set => memoryList = value; }



        public MainWindow()
        {
            InitializeComponent();

            MemoryList = new MemoryList(); // Inițializăm obiectul
            DataContext = this;
            this.KeyDown += new KeyEventHandler(OnKeyDown);
            DivideBtn.Content = "\u00F7";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string ConvertToHex(string value)
        {
            string cleanedValue = value.Replace(",", "");

            // Verificăm dacă valoarea este validă
            if (long.TryParse(cleanedValue, out long num))
            {
                return num.ToString("X"); // conversia în hex
            }
            return "Invalid Input";
        }

        // Conversia la Decimal
        private string ConvertToDecimal(string value)
        {
            string cleanedValue = value.Replace(",", "");

            // Verificăm dacă valoarea este validă
            if (long.TryParse(cleanedValue, out long num))
            {
                return num.ToString(); // conversia în decimal
            }
            return "Invalid Input";
        }

        // Conversia la Octal
        private string ConvertToOctal(string value)
        {
            string cleanedValue = value.Replace(",", "");

            // Verificăm dacă valoarea este validă
            if (long.TryParse(cleanedValue, out long num))
            {
                return Convert.ToString(num, 8); // conversia în octal
            }
            return "Invalid Input";
        }

        // Conversia la Binare
        private string ConvertToBinary(string value)
        {
            string cleanedValue = value.Replace(",", "");

            // Verificăm dacă valoarea este validă
            if (long.TryParse(cleanedValue, out long num))
            {
                return Convert.ToString(num, 2); // conversia în binar
            }
            return "Invalid Input";
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            string keyString = new KeyConverter().ConvertToString(e.Key);

            if (keyString is not null && keyString.Length == 1 && char.IsDigit(keyString[0]))
            {
                Output += keyString;
                Output = FormatOutput(Output);
                return;
            }

            if (e.Key == Key.OemPlus || e.Key == Key.Add)
            {
                PerformOperation("Plus");
            }
            else if (e.Key == Key.OemMinus || e.Key == Key.Subtract)
            {
                PerformOperation("Minus");
            }
            else if (e.Key == Key.Oem2 || e.Key == Key.Divide)
            {
                PerformOperation("Divide");
            }
            else if (e.Key == Key.Multiply || e.Key == Key.Oem8)
            {
                PerformOperation("Times");
            }

            switch (e.Key)
            {
                case Key.Return:
                    PerformComputation();
                    break;

                case Key.Back when Output.Length > 0:
                    Output = Output[..^1];
                    break;

                case Key.Escape:
                    Clear();
                    break;

                default:
                    break;
            }
        }

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            Output += ((Button)sender).Content.ToString();
            Output = FormatOutput(Output);
        }

        private string FormatOutput(string value)
        {
            if (double.TryParse(value, out double number))
            {
                return number.ToString("N0", CultureInfo.CurrentCulture);
            }
            return string.Empty;
        }


        private void PerformOperation(string op)
        {
            if (!string.IsNullOrEmpty(Output))
            {
                if (!string.IsNullOrEmpty(Operation))
                {
                    PerformComputation();
                }
                TempValue = double.Parse(Output);
                Operation = op;
                Output = "";
            }
        }
        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation("Minus");
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation("Plus");
        }

        private void TimesBtn_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation("Times");
        }

        private void DivideBtn_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation("Divide");
        }
        private void EqualsBtn_Click(object sender, RoutedEventArgs e)
        {
            PerformComputation();
        }

        private void PerformComputation()
        {
            if (Output.Length == 0)
                return;

            double outputTemp = double.Parse(Output);
            switch (Operation)
            {
                case "Plus":
                    Output = (TempValue + outputTemp).ToString();
                    break;
                case "Minus":
                    Output = (TempValue - outputTemp).ToString();
                    break;
                case "Times":
                    Output = (TempValue * outputTemp).ToString();
                    break;
                case "Divide":
                    Output = outputTemp != 0 ? (TempValue / outputTemp).ToString() : "Error";
                    break;
            }
            Output = FormatOutput(Output);
            Operation = "";
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            TempValue = 0;
            Output = "";
            Operation = "";
        }

        private void ClearEntryBtn_Click(object sender, RoutedEventArgs e)
        {
            Output = "";
        }

        private void PrecentBtn_Click(object sender, RoutedEventArgs e)
        {

            if (double.TryParse(Output, out double value))
            {
                Output = (value / 100).ToString();
                TempValue = double.Parse(Output);
            }
        }

        private void InvertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Output, out double value) && value != 0)
            {
                Output = (1 / value).ToString();
                TempValue = double.Parse(Output);
            }

        }

        private void SquaredBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Output, out double value))
            {
                Output = (value * value).ToString();
                TempValue = double.Parse(Output);
            }

        }

        private void SqrtBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Output, out double value) && value >= 0)
            {
                Output = Math.Sqrt(value).ToString();
                TempValue = double.Parse(Output);
            }
        }

        private void NegateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Output, out double value))
            {
                Output = (-value).ToString();
                TempValue = double.Parse(Output);
            }
        }

        private void MemoryRecallBtn_Click(object sender, RoutedEventArgs e)
        {
            Output = MemoryList.PeekNumber()?.MemoryValue ?? "0";
        }

        private void MemoryClearBtn_Click(object sender, RoutedEventArgs e)
        {
            MemoryList.Numbers.Clear();
        }

        private void MemoryAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Output, out double value))
            {
                MemoryList.AddNumber(value);
            }
        }

        private void MemorySubstractBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Output, out double value))
            {
                MemoryList.SubtractNumber(value);
            }
        }

        private void MemoryStoreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Output, out double value))
            {
                MemoryList.PushNumber(new MemoryNumber(value.ToString()));
            }
        }

        private void MemoryShowBtn_Click(object sender, RoutedEventArgs e)
        {
            Memory _memory = new(this.DataContext);
            _memory.ShowDialog();
        }


    }
}