using System.ComponentModel;

namespace calculator
{
    public class MemoryNumber : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string memoryValue = "0";
        public string MemoryValue
        {
            get { return memoryValue; }
            set { memoryValue = value; OnPropertyChanged(nameof(MemoryValue)); }
        }

        public MemoryNumber(string value)
        {
            memoryValue = value;
        }

        public override string ToString()
        {
            return MemoryValue;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
