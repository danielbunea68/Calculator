using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace calculator
{
    public partial class MainWindow : Window
    {
        private Calculator calculator;

        public MainWindow()
        {
            InitializeComponent();
            calculator = new Calculator();
            DataContext = calculator; // Legăm clasa Calculator la UI
            this.KeyDown += OnKeyDown;
        }

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.AppendNumber(((Button)sender).Content.ToString());
        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.SetOperation(((Button)sender).Content.ToString());
        }

        private void EqualsBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.CalculateResult();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.Clear();
        }

        private void ClearEntryBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.ClearEntry();
        }

        private void PrecentBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.Percent();
        }

        private void InvertBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.Invert();
        }

        private void SquaredBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.Square();
        }

        private void SqrtBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.SquareRoot();
        }

        private void NegateBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.Negate();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            calculator.HandleKeyPress(e);
        }

        // Funcții pentru memoria calculatorului
        private void MemoryClearBtn_Click(object sender, RoutedEventArgs e) => calculator.MemoryClear();
        private void MemoryRecallBtn_Click(object sender, RoutedEventArgs e) => calculator.MemoryRecall();
        private void MemoryAddBtn_Click(object sender, RoutedEventArgs e) => calculator.MemoryAdd();
        private void MemorySubstractBtn_Click(object sender, RoutedEventArgs e) => calculator.MemorySubtract();
        private void MemoryStoreBtn_Click(object sender, RoutedEventArgs e) => calculator.MemoryStore();
        private void MemoryShowBtn_Click(object sender, RoutedEventArgs e)
        {
            Memory _memory = new(calculator);
            _memory.ShowDialog();
        }
    }
}
