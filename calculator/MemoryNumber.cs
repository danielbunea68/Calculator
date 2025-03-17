using System.ComponentModel;

namespace calculator
{
    public class MemoryNumber : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int memoryBase = 10;
        public int MemoryBase
        {
            get { return memoryBase; }
            set { memoryBase = value; OnPropertyChanged(nameof(MemoryBase)); }
        }

        private string memoryValue = "0";
        public string MemoryValue
        {
            get { return memoryValue; }
            set { memoryValue = value; OnPropertyChanged(nameof(MemoryValue)); }
        }

        public MemoryNumber(string value)
        {
            memoryValue = value;
            memoryBase = 10;
        }

        public MemoryNumber(string value, int memoryBase)
        {
            memoryValue = value;
            this.memoryBase = memoryBase;
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
