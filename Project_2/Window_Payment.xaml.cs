//ЗАОКОННЫЙ КОД ОКНА ПОИСКА
using System.Windows;


namespace Project_2
{
    /// <summary>
    /// Логика взаимодействия для Window_Payment.xaml
    /// </summary>
    public partial class Window_Payment : Window
    {
        public Window_Payment(Payment payment)
        {
            InitializeComponent();
            grid.DataContext = payment;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
