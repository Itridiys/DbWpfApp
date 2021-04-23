using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DbWpfApp.Models;
using DbWpfApp.ViewModels;

namespace DbWpfApp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditPositionWindow.xaml
    /// </summary>
    public partial class EditPositionWindow : Window
    {
        public EditPositionWindow(Position selectedPosition)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedPosition = selectedPosition;
            DataManageVM.PositionName = selectedPosition.Name;
            DataManageVM.PositionMaxnumber = selectedPosition.MaxNumber;
            DataManageVM.PositionSalary = selectedPosition.Salary;
        }

        private void PreviewTextInput(object sendet, TextCompositionEventArgs e)
        {
            Regex eRegex = new Regex("[^0-9]+");
            e.Handled = eRegex.IsMatch(e.Text);
        }
    }
}
