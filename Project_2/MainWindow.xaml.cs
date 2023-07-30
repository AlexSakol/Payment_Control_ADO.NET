//Заоконный код главного окна
using System.Data;
using System.Windows;


namespace Project_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable data = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Fill()
        {
            data.Rows.Clear();
            data = Payment.ViewAll();
            personGrid.ItemsSource = data.DefaultView;
        }

        private void ButtonView_Click(object sender, RoutedEventArgs e)
        {
            Fill();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Payment.Update();
            Fill();
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment();
            Window_Payment window_Payment = new Window_Payment(payment);
            if (window_Payment.ShowDialog() == false) return;
            string result = payment.Find();
            if (result == "") MessageBox.Show("Записей не найдено");
            else MessageBox.Show(result);
        }
    }
}
