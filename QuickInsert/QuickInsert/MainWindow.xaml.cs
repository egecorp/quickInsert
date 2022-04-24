using QuickInsert.DAL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickInsert
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var repository = ItemRepository.GetSingleton();

                textBoxItems.Text = repository.GetJson();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textBoxItems.Width = form.Width;
            textBoxItems.Height = form.Height - toolBarPanel.ActualHeight;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ItemRepository.Save(textBoxItems.Text);
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        private void Form_Button_Click(object sender, RoutedEventArgs e)
        {

            clipboardForm c = new clipboardForm();
            c.ShowDialog();

        }
    }
}
