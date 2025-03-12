using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace calculator
{
    public class Calculator : INotifyPropertyChanged
    {
        private string currentInput = "0";
        private double firstOperand = 0;
        private double secondOperand = 0;
        private string operation = "";
        private bool isNewEntry = true;
        private double memory = 0;

        public string CurrentInput
        {
            get => currentInput;
            set
            {
                currentInput = value;
                OnPropertyChanged(nameof(CurrentInput));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Adăugare cifre
        public void AppendNumber(string number)
        {
            if (isNewEntry || CurrentInput == "0")
            {
                CurrentInput = number;
            }
            else
            {
                CurrentInput += number;
            }
            isNewEntry = false;
        }

        // Setarea operației matematice
        public void SetOperation(string op)
        {
            if (!isNewEntry)
            {
                firstOperand = double.Parse(CurrentInput);
                isNewEntry = true;
                operation = op;
            }
        }

        // Calcularea rezultatului
        public void CalculateResult()
        {
            if (!string.IsNullOrEmpty(operation))
            {
                secondOperand = double.Parse(CurrentInput);
                switch (operation)
                {
                    case "+": CurrentInput = (firstOperand + secondOperand).ToString(); break;
                    case "-": CurrentInput = (firstOperand - secondOperand).ToString(); break;
                    case "×": CurrentInput = (firstOperand * secondOperand).ToString(); break;
                    case "÷":
                        if (secondOperand != 0)
                            CurrentInput = (firstOperand / secondOperand).ToString();
                        else
                            CurrentInput = "Eroare";
                        break;
                }
                operation = "";
                isNewEntry = true;
            }
        }

        // Ștergere totală
        public void Clear()
        {
            CurrentInput = "0";
            firstOperand = 0;
            secondOperand = 0;
            operation = "";
            isNewEntry = true;
        }

        // Ștergere doar a ultimului număr introdus
        public void ClearEntry()
        {
            CurrentInput = "0";
            isNewEntry = true;
        }

        // Inversarea semnului
        public void Negate()
        {
            if (double.TryParse(CurrentInput, out double value))
            {
                CurrentInput = (-value).ToString();
            }
        }
        public void Invert()
        {
            if (double.TryParse(CurrentInput, out double value) && value != 0)
            {
                CurrentInput = (1 / value).ToString();
                
            }

        }

        // Calcul procentual
        public void Percent()
        {
            if (double.TryParse(CurrentInput, out double value))
            {
                CurrentInput = (value / 100).ToString();
            }
        }

        // Radical pătrat
        public void SquareRoot()
        {
            if (double.TryParse(CurrentInput, out double value) && value >= 0)
            {
                CurrentInput = Math.Sqrt(value).ToString();
            }
            else
            {
                CurrentInput = "Eroare";
            }
        }

        // Ridicare la pătrat
        public void Square()
        {
            if (double.TryParse(CurrentInput, out double value))
            {
                CurrentInput = (value * value).ToString();
            }
        }

        // Operații pe memoria calculatorului
        public void MemoryClear() => memory = 0;
        public void MemoryRecall() => CurrentInput = memory.ToString();
        public void MemoryAdd()
        {
            if (double.TryParse(CurrentInput, out double value))
                memory += value;
        }
        public void MemorySubtract()
        {
            if (double.TryParse(CurrentInput, out double value))
                memory -= value;
        }
        public void MemoryStore()
        {
            if (double.TryParse(CurrentInput, out double value))
                memory = value;
        }

        // Gestionare tastatură
        public void HandleKeyPress(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                AppendNumber((e.Key - Key.D0).ToString());
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                AppendNumber((e.Key - Key.NumPad0).ToString());
            }
            else if (e.Key == Key.Add) SetOperation("+");
            else if (e.Key == Key.Subtract) SetOperation("-");
            else if (e.Key == Key.Multiply) SetOperation("×");
            else if (e.Key == Key.Divide) SetOperation("÷");
            else if (e.Key == Key.Enter) CalculateResult();
            else if (e.Key == Key.Back) ClearEntry();
            else if (e.Key == Key.Delete) Clear();
        }
    }
}
