using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace calculator
{
    /// <summary>
    /// Interaction logic for Programer.xaml
    /// </summary>
    public partial class Programer : Window
    {
        private Calculator calculator;
        public Programer()
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

        private void BackspaceBtn_Click(object sender, RoutedEventArgs e)
        {
            calculator.Backspace();
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

        private void OpenStandardMode(object sender, RoutedEventArgs e)
        {
            MainWindow standardWind = new();
            standardWind.ShowDialog();
        }

        private void OpenProgrammerMode(object sender, RoutedEventArgs e)
        {
            Programer programerWind = new();
            programerWind.ShowDialog();
        }

        private void BaseRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton selectedButton = sender as RadioButton;

            if (selectedButton != null)
            {
                // Setăm baza în calculator
                if (selectedButton == HexRadioButton)
                {
                    calculator.SetBase(16); // Baza hexazecimală
                }
                else if (selectedButton == DecRadioButton)
                {
                    calculator.SetBase(10); // Baza decimală
                }
                else if (selectedButton == OctRadioButton)
                {
                    calculator.SetBase(8); // Baza octală
                }
                else if (selectedButton == BinRadioButton)
                {
                    calculator.SetBase(2); // Baza binară
                }
            }
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            calculator.Cut();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            calculator.Copy();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            calculator.Paste();
        }

        private void DigitGrouping_Click(object sender, RoutedEventArgs e)
        {
            calculator.DigitGrouping();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            calculator.About();
        }
    }


}
