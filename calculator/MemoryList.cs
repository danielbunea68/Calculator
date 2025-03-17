using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Numbers.Push(new MemoryNumber("0"));
        }

        public void PushNumber(MemoryNumber number)
        {
            Numbers.Push(number);
        }


        public void AddNumber(double value)
        {
            if (Numbers.Count == 0)
            {
                Numbers.Push(new MemoryNumber(value.ToString()));
                return;
            }

            if (double.TryParse(Numbers.Peek().MemoryValue, out double number))
            {
                double newValue = number + value;
                Numbers.Peek().MemoryValue = newValue.ToString();
            }
        }

        public void SubtractNumber(double value)
        {
            if (Numbers.Count == 0)
            {
                Numbers.Push(new MemoryNumber(value.ToString()));
                return;
            }

            if (double.TryParse(Numbers.Peek().MemoryValue, out double number))
            {
                double newValue = number - value;
                Numbers.Peek().MemoryValue = newValue.ToString();
            }
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
