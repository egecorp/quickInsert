using QuickInsert.DAL;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuickInsert
{
    /// <summary>
    /// Логика взаимодействия для clipboardForm.xaml
    /// </summary>
    public partial class ClipboardForm : Window
    {

        private OneItem ItemM2 { get; set; }
        private OneItem ItemM1 { get; set; }
        private OneItem ItemC { get; set; }
        private OneItem ItemP1 { get; set; }
        private OneItem ItemP2 { get; set; }

        private List<OneItem> _itemList = null;
        
        private int _totalCount = 0;

        private const string HiddenValue = "Value is hidden";

        private readonly MainWindow _mainWindow;
        public ClipboardForm(MainWindow myMainWindow)
        {
            _mainWindow = myMainWindow;
            InitializeComponent();

        }

        private Task CloseForm()
        {
            return new Task(() =>
            {
                Thread.Sleep(300);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    this.DialogResult = true;
                    this.Close();
                }));

            });
        }

        private void NavigationDown()
        {
            if (--Navigation.CurrentIndex < 0)
            {
                Navigation.CurrentIndex = _totalCount - 1;
            }
        }

        private void NavigationUp()
        {
            if (++Navigation.CurrentIndex >= _totalCount)
            {
                Navigation.CurrentIndex = 0;
            }
        }

        private void clipboardForm1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.DialogResult = false;
                this.Close();
            }
            else if ((e.Key == Key.C) || (e.Key == Key.Enter))
            {
                Console.WriteLine("Item = " + ItemC.Value);
                Clipboard.SetText(ItemC.Value);
                if (ItemC.MoveDownAfterCopy)
                {
                    NavigationUp();
                }
                
                MainBorder.Background = Brushes.White;
                

                CloseForm().Start();
               
            }
            else if ((e.Key == Key.S) || (e.Key == Key.Z) || (e.Key == Key.Up))
            {
                NavigationDown();
                UpdateLabels();
            }
            else if ((e.Key == Key.X) || (e.Key == Key.Down))
            {
                NavigationUp();
                UpdateLabels();
            }
            else if (e.Key == Key.Tab)
            {
                this.DialogResult = false;
                this.Close();
                _mainWindow.Show();
            }
            
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            _itemList = ItemRepository.GetSingleton().GetItems();
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            _totalCount = _itemList.Count;
            ItemM2 = GetByOffset(-2);
            ItemM1 = GetByOffset(-1);
            ItemC = GetByOffset(0);
            ItemP1 = GetByOffset(1);
            ItemP2 = GetByOffset(2);

            buttonM2.Content = ItemM2.Name;
            buttonM1.Content = ItemM1.Name;
            buttonC.Content = ItemC.Name;
            buttonP1.Content = ItemP1.Name;
            buttonP2.Content = ItemP2.Name;

            valueM2.Content = ItemM2.IsValueHidden ? HiddenValue : ItemM2.Value;
            valueM1.Content = ItemM1.IsValueHidden ? HiddenValue : ItemM1.Value;
            valueC.Content = ItemC.IsValueHidden ? HiddenValue : ItemC.Value;
            valueP1.Content = ItemP1.IsValueHidden ? HiddenValue : ItemP1.Value;
            valueP2.Content = ItemP2.IsValueHidden ? HiddenValue : ItemP2.Value;

            shortKeyM2.Content = ItemM2.KeyShortCuts ?? "";
            shortKeyM1.Content = ItemM1.KeyShortCuts ?? "";
            shortKeyC.Content = ItemC.KeyShortCuts ?? "";
            shortKeyP1.Content = ItemP1.KeyShortCuts ?? "";
            shortKeyP2.Content = ItemP2.KeyShortCuts ?? "";
        }

        private OneItem GetByOffset(int offset)
        {
            int offsetIndex = Navigation.CurrentIndex + offset;
            if (offsetIndex < 0) offsetIndex += _totalCount;
            if (offsetIndex >= _totalCount) offsetIndex = offsetIndex % _totalCount;
            return _itemList[offsetIndex];
        }
    }
}
