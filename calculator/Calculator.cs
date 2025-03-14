using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace calculator
{
    public class Calculator : INotifyPropertyChanged
    {
        private string output = "0";
        private double firstOperand = 0;
        private double secondOperand = 0;
        private string operation = "";
        private bool isNewEntry = true;
        private double memory = 0;



        private string memoryValue = "0";
        private MemoryList memoryList;

        public string MemoryValue
        {
            get { return memoryValue; }
            set { memoryValue = value; OnPropertyChanged(nameof(MemoryValue)); }
        }


        public MemoryList MemoryList { get => memoryList; set => memoryList = value; }

        public Calculator()
        {
            MemoryList = new MemoryList();
        }
        public string Output
        {
            get => output;
            set
            {
                output = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FormatOutput()
        {
            if (double.TryParse(Output, out double number))
            {
                Output = number.ToString("N0", CultureInfo.CurrentCulture);
            }
        }

        public string ConvertToHex(string value)
        {
            if (currentBase == 16)
            {
                return value; // Copiem output-ul
            }
            string cleanedValue = value.Replace(",", "");

            // Verificăm dacă valoarea este validă
            if (long.TryParse(cleanedValue, out long num))
            {
                return num.ToString("X"); // conversia în hex
            }
            return "Invalid Input";
        }

        // Conversia la Decimal
        public string ConvertToDecimal(string value)
        {
            //if (currentBase == 10)
            //{
            //    return value; // Copiem output-ul
            //}
            //string cleanedValue = value.Replace(",", "");

            //// Verificăm dacă valoarea este validă
            //if (long.TryParse(cleanedValue, out long num))
            //{
            //    return num.ToString(); // conversia în decimal
            //}
            //return "Invalid Input";
            // Curățăm valoarea de caracterele nedorite
            string cleanedValue = value.Replace(",", "");

            // Convertim valoarea în decimal
            long decimalValue = 0;
            foreach (char c in cleanedValue.ToUpper())
            {
                int digitValue = c >= '0' && c <= '9' ? c - '0' : c >= 'A' && c <= 'F' ? c - 'A' + 10 : -1;
                if (digitValue < 0 || digitValue >= currentBase)
                {
                    return "Invalid Input"; // Valoare invalidă
                }
                decimalValue = decimalValue * currentBase + digitValue;
            }

            return decimalValue.ToString();
        }

        // Conversia la Octal
        public string ConvertToOctal(string value)
        {
            //if (currentBase == 8)
            //{
            //    return value; // Copiem output-ul
            //}
            //string cleanedValue = value.Replace(",", "");

            //// Verificăm dacă valoarea este validă
            //if (long.TryParse(cleanedValue, out long num))
            //{
            //    return Convert.ToString(num, 8); // conversia în octal
            //}
            //return "Invalid Input";
            if (currentBase == 8)
            {
                return value; // Copiem output-ul
            }

            // Curățăm valoarea de caracterele nedorite
            string cleanedValue = value.Replace(",", "");

            // Convertim valoarea în decimal
            long decimalValue = 0;
            foreach (char c in cleanedValue.ToUpper())
            {
                int digitValue = c >= '0' && c <= '9' ? c - '0' : c >= 'A' && c <= 'F' ? c - 'A' + 10 : -1;
                if (digitValue < 0 || digitValue >= currentBase)
                {
                    return "Invalid Input"; // Valoare invalidă
                }
                decimalValue = decimalValue * currentBase + digitValue;
            }

            return Convert.ToString(decimalValue, 8);
        }

        // Conversia la Binare
        public string ConvertToBinary(string value)
        {
            //if (currentBase == 2)
            //{
            //    return value; // Copiem output-ul
            //}
            //string cleanedValue = value.Replace(",", "");

            //// Verificăm dacă valoarea este validă
            //if (long.TryParse(cleanedValue, out long num))
            //{
            //    return Convert.ToString(num, 2); // conversia în binar
            //}
            //return "Invalid Input";
            // Verificăm dacă baza curentă este BIN
            if (currentBase == 2)
            {
                return value; // Copiem output-ul
            }

            // Curățăm valoarea de caracterele nedorite
            string cleanedValue = value.Replace(",", "");

            // Convertim valoarea în decimal
            long decimalValue = 0;
            foreach (char c in cleanedValue.ToUpper())
            {
                int digitValue = c >= '0' && c <= '9' ? c - '0' : c >= 'A' && c <= 'F' ? c - 'A' + 10 : -1;
                if (digitValue < 0 || digitValue >= currentBase)
                {
                    return "Invalid Input"; // Valoare invalidă
                }
                decimalValue = decimalValue * currentBase + digitValue;
            }

            return Convert.ToString(decimalValue, 2);
        }



        // Adăugare cifre
        public void AppendNumber(string number)
        {
            if (IsValidInBase(number, currentBase))
            {
                if (isNewEntry || Output == "0")
                {
                    Output = number;

                }
                else
                {
                    Output += number;
                }
                FormatOutput();
                isNewEntry = false;
               
            }
        }

        // Setarea operației matematice
        public void SetOperation(string op)
        {
            if (!isNewEntry)
            {
                firstOperand = double.Parse(Output);
                isNewEntry = true;
                operation = op;
            }
        }

        // Calcularea rezultatului
        public void CalculateResult()
        {
            if (!string.IsNullOrEmpty(operation))
            {
                secondOperand = double.Parse(Output);
                switch (operation)
                {
                    case "+": Output = (firstOperand + secondOperand).ToString(); break;
                    case "-": Output = (firstOperand - secondOperand).ToString(); break;
                    case "×": Output = (firstOperand * secondOperand).ToString(); break;
                    case "÷":
                        if (secondOperand != 0)
                            Output = (firstOperand / secondOperand).ToString();
                        else
                            Output = "Eroare";
                        break;
                    case "%":
                        Output = (firstOperand * (secondOperand / 100)).ToString();
                        break;
                }
                operation = "";
                isNewEntry = true;
            }
        }

        // Ștergere totală
        public void Clear()
        {
            Output = "0";
            firstOperand = 0;
            secondOperand = 0;
            operation = "";
            isNewEntry = true;
        }

        // Ștergere doar a ultimului număr introdus
        public void ClearEntry()
        {
            Output = "0";
            isNewEntry = true;
        }

        // Inversarea semnului
        public void Negate()
        {
            if (double.TryParse(Output, out double value))
            {
                Output = (-value).ToString();
            }
        }
        public void Invert()
        {
            if (double.TryParse(Output, out double value) && value != 0)
            {
                Output = (1 / value).ToString();
                
            }

        }

        // Calcul procentual
        public void Percent()
        {
            if (double.TryParse(Output, out double value))
            {
                Output = (value / 100).ToString();
            }
        }

        // Radical pătrat
        public void SquareRoot()
        {
            if (double.TryParse(Output, out double value) && value >= 0)
            {
                Output = Math.Sqrt(value).ToString();
            }
            else
            {
                Output = "Eroare";
            }
        }

        // Ridicare la pătrat
        public void Square()
        {
            if (double.TryParse(Output, out double value))
            {
                Output = (value * value).ToString();
            }
        }
        public void Backspace()
        {
            if (Output.Length > 0)
            {
                // Ștergem ultimul caracter din Output
                Output = Output.Substring(0, Output.Length - 1);
                FormatOutput();

                // Dacă Output devine gol, setăm la "0"
                if (string.IsNullOrEmpty(Output))
                {
                    Output = "0";
                }
            }
        }

        // Operații pe memoria calculatorului
        public void MemoryClear() => MemoryList.Numbers.Clear();
        public void MemoryRecall() => Output = MemoryList.PeekNumber()?.MemoryValue ?? "0";
        public void MemoryAdd()
        {
            if (double.TryParse(Output, out double value))
                MemoryList.AddNumber(value);
        }
        public void MemorySubtract()
        {
            if (double.TryParse(Output, out double value))
                MemoryList.SubtractNumber(value);
        }
        public void MemoryStore()
        {
            if (double.TryParse(Output, out double value))
                MemoryList.PushNumber(new MemoryNumber(value.ToString()));
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
        private int currentBase = 10; // Baza implicită

        public void SetBase(int baseValue)
        {
            currentBase = baseValue;
        }

        public void ResetBase()
        {
            currentBase = 10; // Resetăm la baza decimală
        }

     

        private bool IsValidInBase(string number, int baseValue)
        {
            // Verificăm dacă numărul este valid în baza specificată
            foreach (char c in number)
            {
                int digitValue = c >= '0' && c <= '9' ? c - '0' : c >= 'A' && c <= 'F' ? c - 'A' + 10 : -1;
                if (digitValue < 0 || digitValue >= baseValue)
                {
                    return false; // Invalid
                }
            }
            return true; // Valid
        }
    }
}
