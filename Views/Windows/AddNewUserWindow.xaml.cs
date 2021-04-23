using DbWpfApp.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace DbWpfApp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewUserWindow.xaml
    /// </summary>
    public partial class AddNewUserWindow : Window
    {
        public AddNewUserWindow()
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
