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

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double temp = 0;

        string operation = "";
        string output = "";
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            DivideBtn.Content = "\u00F7";
        }

       

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Name;
            switch (name)
            {
                case "OneBtn":
                    output += "1";
                    OutputTextBlock.Text = output;
                    break;
                case "TwoBtn":
                    output += "2";
                    OutputTextBlock.Text = output;
                    break;
                case "ThreeBtn":
                    output += "3";
                    OutputTextBlock.Text = output;
                    break;
                case "FourBtn":
                    output += "4";
                    OutputTextBlock.Text = output;
                    break;
                case "FiveBtn":
                    output += "5";
                    OutputTextBlock.Text = output;
                    break;
                case "SixBtn":
                    output += "6";
                    OutputTextBlock.Text = output;
                    break;
                case "SevenBtn":
                    output += "7";
                    OutputTextBlock.Text = output;
                    break;
                case "EightBtn":
                    output += "8";
                    OutputTextBlock.Text = output;
                    break;
                case "NineBtn":
                    output += "9";
                    OutputTextBlock.Text = output;
                    break;

            }
        }

        private void PerformOperation(string op)
        {
            if (!string.IsNullOrEmpty(output))
            {
                temp = double.Parse(output);
                output = "";
                operation = op;
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
            double outputTemp = double.Parse(output);
            switch (operation)
            {
                case "Plus":
                    output = (temp + outputTemp).ToString();
                    break;
                case "Minus":
                    output = (temp - outputTemp).ToString();
                    break;
                case "Times":
                    output = (temp * outputTemp).ToString();
                    break;
                case "Divide":
                    output = outputTemp != 0 ? (temp / outputTemp).ToString() : "Error";
                    break;
            }
            OutputTextBlock.Text = output;
            operation = "";
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            temp = 0;
            output = "";
            operation = "";
            OutputTextBlock.Text = "0";
        }

        private void ClearEntryBtn_Click(object sender, RoutedEventArgs e)
        {
            output = "";
            OutputTextBlock.Text = "0";
        }
    }
}