#nullable enable

using QuickInsert.DAL;
using System;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace QuickInsert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NotifyIcon _mNotifyIcon;

        private readonly UserActivityHook _keyboardHook;

        private bool _modalShown;
        private bool _isCtrl;
        private bool _isWin;
        private bool _isC;

        public MainWindow()
        {
            InitializeComponent();
            _mNotifyIcon = new NotifyIcon
            {
                BalloonTipText = "Open application",
                BalloonTipTitle = "Quick Insert",
                Text = "Quick insert",
                Icon = new System.Drawing.Icon("quickInsert.ico"),
                Visible = true
            };

            _mNotifyIcon.DoubleClick += new EventHandler(m_notifyIcon_Click);

            _keyboardHook = new UserActivityHook(); 
            _keyboardHook.KeyDown += new KeyEventHandler(MyKeyDown);
            _keyboardHook.KeyUp += new KeyEventHandler(MyKeyUp);

        }

        private void m_notifyIcon_Click(object? sender, EventArgs e)
        {
            this.Show();
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

            _keyboardHook.Start(false, true);
            this.Hide();
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
            ShowModal();
        }

        private void CloseSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void ShowModal()
        {
            _modalShown = true;
            ClipboardForm c = new ClipboardForm(this);
            c.ShowDialog();
            _modalShown = false;
        }



        private void MyKeyDown(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.LControlKey)
            {
                _isCtrl = true;
            }
            else if (e.KeyData ==  Keys.LWin)
            {
                _isWin = true;
            }
            else if (e.KeyData == Keys.C)
            {
                _isC = true;
            }

            CheckAllKeys();
        }

        private void MyKeyUp(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.LControlKey)
            {
                _isCtrl = false;
            }
            else if (e.KeyData == Keys.LWin)
            {
                _isWin = false;
            }
            else if (e.KeyData == Keys.C)
            {
                _isC = false;
            }

            CheckAllKeys();
        }


        private void CheckAllKeys()
        {
            if (_isCtrl && _isWin && _isC )
            {
                if (!_modalShown) ShowModal();
            }
        }

        private void form_Unloaded(object sender, RoutedEventArgs e)
        {
            _keyboardHook.Stop();
        }

        private void form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            _mNotifyIcon.Icon = null;
            _mNotifyIcon.Visible = false;
            _mNotifyIcon.Dispose();
            Environment.Exit(0);
        }
        
    }
}
