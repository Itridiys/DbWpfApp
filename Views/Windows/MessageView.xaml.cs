using System.Windows;

namespace DbWpfApp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MessageView.xaml
    /// </summary>
    public partial class MessageView : Window
    {
        public MessageView(string text)
        {
            InitializeComponent();
            MessageText.Text = text;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO: Сделать в разрезе патерна. Пример есть в проекте MvvMWpf
            this.Close();
        }
    }
}
