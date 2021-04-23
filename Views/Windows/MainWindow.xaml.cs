using System.Windows;
using System.Windows.Controls;
using DbWpfApp.ViewModels;

namespace DbWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllDepartmentsView;
        public static ListView AllPositionView;
        public static ListView AllUsersView;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            AllDepartmentsView = ViewAllDepartments;
            AllPositionView = ViewAllPositions;
            AllUsersView = ViewAllUsers;
        }
    }
}
