//ЗАОКОННЫЙ КОД
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Payment payment;
        ObservableCollection<Payment> paymentList = new ObservableCollection<Payment>();
        public MainWindow()
        {
            InitializeComponent();
            payment = new Payment();
            stackpanelPayment.DataContext = payment;
            listBox.DataContext = paymentList;
        }

        private void FillData()
        {
            paymentList.Clear();
            foreach (var payment in Payment.GetPayments()) paymentList.Add(payment);

        }

        private void buttonView_Click(object sender, RoutedEventArgs e) => FillData();


        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                payment.Insert();
                FillData();
            }
            catch
            {
                MessageBox.Show("Ошибка добавления данных"); 
            }
        }

        private void buttonFind_Click(object sender, RoutedEventArgs e)
        {
            Payment payment1 = Payment.Find(textBoxName.Text);
            if (payment1 == null) MessageBox.Show("Запись не найдена");
            else MessageBox.Show(payment1.ToString());
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex == -1) return;
            payment.PaymentId = ((Payment)listBox.SelectedItem).PaymentId;
            if (textBoxName.Text.Length > 0) payment.PaymentName = textBoxName.Text;
            else payment.PaymentName = ((Payment)listBox.SelectedItem).PaymentName;
            DateTime date = Convert.ToDateTime(datePickerDate.Text);
            if (date != null) payment.PaymentDate = date;
            else payment.PaymentDate = ((Payment)listBox.SelectedItem).PaymentDate;
            decimal price = Convert.ToDecimal(textBoxPrice.Text);
            if (price != null) payment.Price = price;
            else payment.Price = ((Payment)listBox.SelectedItem).Price;
            payment.Update();
            FillData();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex == -1) return;
            Payment.Delete(((Payment)listBox.SelectedItem).PaymentId);
            FillData();
        }

    }
}
