using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using DbWpfApp.Models;
using DbWpfApp.ViewModels;

namespace DbWpfApp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow(User selectedUser)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedUser = selectedUser;
            DataManageVM.UserName = selectedUser.Name;
            DataManageVM.UserSurName = selectedUser.SurName;
            DataManageVM.UserPhone = selectedUser.Phone;
            DataManageVM.UserPosition = selectedUser.Position;
        }

        private void PreviewTextInput(object sendet, TextCompositionEventArgs e)
        {
            Regex eRegex = new Regex("[^0-9]+");
            e.Handled = eRegex.IsMatch(e.Text);
        }
    }
}
