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
using DbWpfApp.Models;
using DbWpfApp.ViewModels;

namespace DbWpfApp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditDepartmentWindow.xaml
    /// </summary>
    public partial class EditDepartmentWindow : Window
    {
        public EditDepartmentWindow(Department selecteDepartment)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedDepartment = selecteDepartment;
            DataManageVM.DepartmentName = selecteDepartment.Name;
        }
    }
}
