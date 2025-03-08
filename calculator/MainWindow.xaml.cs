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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
           /// ProgramerMode programerMode = new(this.DataContext)
           // programerMode.ShowDialog();
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            //StandardMode standardMode = new(this.DataContext)
           // standardMode.ShowDialog();
            
        }
    }
}