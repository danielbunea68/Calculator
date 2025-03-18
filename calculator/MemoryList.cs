using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace calculator
{
    public class MemoryList
    {
        public Stack<MemoryNumber> Numbers { get; set; }

        public MemoryNumber SelectedNumber { get; set; }

        public MemoryList()
        {
            Numbers = new Stack<MemoryNumber>();

           /// Numbers.Push(new MemoryNumber("0"));
        }

        public void PushNumber(MemoryNumber number)
        {
            Numbers.Push(number);
        }


        //public void AddNumber(double value)
        //{
        //    if (Numbers.Count == 0)
        //    {
        //        Numbers.Push(new MemoryNumber(value.ToString()));
        //        return;
        //    }

        //    if (double.TryParse(Numbers.Peek().MemoryValue, out double number))
        //    {
        //        double newValue = number + value;
        //        Numbers.Peek().MemoryValue = newValue.ToString();
        //    }
        //}

        //public void SubtractNumber(double value)
        //{
        //    if (Numbers.Count == 0)
        //    {
        //        Numbers.Push(new MemoryNumber(value.ToString()));
        //        return;
        //    }

        //    if (double.TryParse(Numbers.Peek().MemoryValue, out double number))
        //    {
        //        double newValue = number - value;
        //        Numbers.Peek().MemoryValue = newValue.ToString();
        //    }
        //}


        public void AddNumber(double value)
        {
            if (Numbers.Count > 0)
            {



                // Afișează dialogul pentru a selecta o intrare din memorie
                string selectedMemory = ShowMemorySelectionDialog("Add to Memory");

                if (!string.IsNullOrEmpty(selectedMemory))
                {
                    double selectedValue = double.Parse(selectedMemory);
                    int selectedIndex = Numbers.ToList().FindIndex(m => m.MemoryValue == selectedValue.ToString());

                    if (selectedIndex != -1)
                    {
                        if (double.TryParse(Numbers.ElementAt(selectedIndex).MemoryValue, out double number))
                        {
                            double newValue = number + value;
                            Numbers.ElementAt(selectedIndex).MemoryValue = newValue.ToString();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No memory available.", "Error");
            }
        }

        public void SubtractNumber(double value)
        {
            if (Numbers.Count > 0)
            {


                // Afișează dialogul pentru a selecta o intrare din memorie
                string selectedMemory = ShowMemorySelectionDialog("Subtract from Memory");

                if (!string.IsNullOrEmpty(selectedMemory))
                {
                    double selectedValue = double.Parse(selectedMemory);
                    int selectedIndex = Numbers.ToList().FindIndex(m => m.MemoryValue == selectedValue.ToString());

                    if (selectedIndex != -1)
                    {
                        if (double.TryParse(Numbers.ElementAt(selectedIndex).MemoryValue, out double number))
                        {
                            double newValue = number - value;
                            Numbers.ElementAt(selectedIndex).MemoryValue = newValue.ToString();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No memory available.", "Error");
            }
        }
        private string ShowMemorySelectionDialog(string operation)
        {
            if (Numbers.Count == 0)
            {
                MessageBox.Show("No memory available.", "Error");
                return null;
            }

            // Crează un text cu intrările din memorie
            string memoryText = string.Join("\n", Numbers.Select((value, index) => $"{index + 1}. {value.MemoryValue}"));

            // Afișează un dialog pentru a selecta o intrare din memorie
            string input = Interaction.InputBox(
                $"Select the memory entry to {operation}:\n{memoryText}",
                operation,
                "1");

            // Verifică dacă inputul este valid
            if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= Numbers.Count)
            {
                return Numbers.ElementAt(selectedIndex - 1).MemoryValue; // Returnează valoarea selectată
            }

            return null; // Returnează null dacă inputul nu este valid
        }

        public MemoryNumber PopNumber()
        {
            if (Numbers.Count > 0)
            {
                return Numbers.Pop();
            }
            else
            {
                return null;
            }
        }


        public MemoryNumber PeekNumber()
        {
            if (Numbers.Count > 0)
            {
                return Numbers.Peek();
            }
            else
            {
                return null;
            }
        }
      
    }
}
