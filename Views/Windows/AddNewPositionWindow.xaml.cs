using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using DbWpfApp.ViewModels;

namespace DbWpfApp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewPositionWindow.xaml
    /// </summary>
    public partial class AddNewPositionWindow : Window
    {
        public AddNewPositionWindow()
        {
            InitializeComponent();
        }

        private void PreviewTextInput(object sendet, TextCompositionEventArgs e)
        {
            Regex eRegex = new Regex("[^0-9]+");
            e.Handled = eRegex.IsMatch(e.Text);
        }
    }
}
