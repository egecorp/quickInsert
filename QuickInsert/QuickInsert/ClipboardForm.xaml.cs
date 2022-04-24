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

namespace QuickInsert
{
    /// <summary>
    /// Логика взаимодействия для clipboardForm.xaml
    /// </summary>
    public partial class clipboardForm : Window
    {
        public clipboardForm()
        {
            InitializeComponent();
        }

        private void clipboardForm1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
