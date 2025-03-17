using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace calculator
{
    /// <summary>
    /// Interaction logic for Memory.xaml
    /// </summary>
    public partial class Memory : Window
    {

        public Memory(object dataContext)
        {
            DataContext = dataContext;
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //(DataContext as MemoryListNumber).SelectedNumber = (sender as ListBox).SelectedItem as MemoryNumber;
        }
        public int SelectedIndex
        {
            get { return MemoryListBox.SelectedIndex; }
        }
    }
}
