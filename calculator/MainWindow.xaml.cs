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
        private string Operation { get; set; } = string.Empty;
        
        private string output = string.Empty;
        public string Output { 
            get
            {
                return output;
            }

            set
            {
                output = value;
                OnPropertyChanged(nameof(Output));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            DivideBtn.Content = "\u00F7";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            Output += ((Button)sender).Content.ToString();
            Output = FormatOutput(Output);
        }

        private string FormatOutput(string value)
        {
            if (int.TryParse(value, out int number))
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
    }
}