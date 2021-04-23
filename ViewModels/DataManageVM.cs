using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DbWpfApp.Models;
using DbWpfApp.ViewModels.Base;
using DbWpfApp.Views.Windows;

namespace DbWpfApp.ViewModels
{
    internal class DataManageVM : ViewModel
    {
        #region Window

        private string _Title = "My MVVM Program ";

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
            /// set=> Set(ref _title, value);    Одинаковая запись
        }

        /// <summary>
        /// Статус программы
        /// </summary>
        private string _Status = @"Готово! ";

        /// <summary>
        /// Статус программы
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion

        #region All Tables

        //все отделы
        private List<Department> _AllDepartments = DataWorker.GetAllDepartments();

        public List<Department> AllDepartments
        {
            get { return _AllDepartments; }
            set => Set(ref _AllDepartments, value);
        }

        private List<Position> _AllPositions = DataWorker.GetAllPositions();

        public List<Position> AllPositions
        {
            get { return _AllPositions; }
            set => Set(ref _AllPositions, value);
        }

        private List<User> _AllUsers = DataWorker.GetAllUsers();

        public List<User> AllUsers
        {
            get { return _AllUsers; }
            set => Set(ref _AllUsers, value);
        }

        #endregion

        #region Commands For open new Windows

        private RelayCommand _OpenAddNewDepartmentWindow;

        public RelayCommand OpenAddNewDepartmentWindow
        {
            get
            {
                return _OpenAddNewDepartmentWindow ?? new RelayCommand(obj =>
                {
                    OpenAddDepartmentWindowMetod();
                });

            }
        }

        private RelayCommand _OpenAddNewPositionWindow;

        public RelayCommand OpenAddNewPositionWindow
        {
            get
            {
                return _OpenAddNewPositionWindow ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindowMetod();
                });

            }
        }

        private RelayCommand _OpenAddNewUserWindow;

        public RelayCommand OpenAddNewUserWindow
        {
            get
            {
                return _OpenAddNewUserWindow ?? new RelayCommand(obj =>
                {
                    OpenAddUserWindowMetod();
                });

            }
        }

        private RelayCommand _OpenEditItem;

        public RelayCommand OpenEditItem
        {
            get
            {
                return _OpenEditItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";

                    if (SelectedTabItem.Name == "UserTab" && SelectedUser != null)
                    {
                        OpenEditUserWindowMetod();
                    }

                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindowMetod();
                    }

                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindowMetod();
                    }

                });

            }
        }


        #endregion

        #region Edit Windows

        private RelayCommand _EditUser;

        public RelayCommand EditUser
        {
            get
            {
                return _EditUser ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    string noPositionStr = "Не выбрана новая должность";
                    if (SelectedUser != null)
                    {
                        if (UserPosition != null)
                        {
                            resultStr = DataWorker.EditUser(SelectedUser, UserName, UserSurName, UserPhone,
                                UserPosition);

                            UpdateAllDataView();
                            SetNullvaluesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else
                        {
                            ShowMessageToUser(noPositionStr);
                        }
                    }
                    else
                    {
                        ShowMessageToUser(resultStr);
                    }
                });
            }
        }

        private RelayCommand _EditPosition;

        public RelayCommand EditPosition
        {
            get
            {
                return _EditPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана позиция сотрудника";
                    string noPositionStr = "Не выбран новый отдела";
                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {
                            resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionMaxnumber, PositionSalary, PositionDepartment);

                            UpdateAllDataView();
                            SetNullvaluesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else
                        {
                            ShowMessageToUser(noPositionStr);
                        }
                    }
                    else
                    {
                        ShowMessageToUser(resultStr);
                    }
                });
            }
        }

        private RelayCommand _EditDepartment;

        public RelayCommand EditDepartment
        {
            get
            {
                return _EditDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран отдел";

                    if (SelectedDepartment != null)
                    {
                        resultStr = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);

                        UpdateAllDataView();
                        SetNullvaluesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else
                    {
                        ShowMessageToUser(resultStr);
                    }
                });
            }
        }

        #endregion


        #region Metods for open new Windows

        private void OpenEditDepartmentWindowMetod()
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(SelectedDepartment);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }

        private void OpenEditPositionWindowMetod()
        {
            EditPositionWindow editDepartmentWindow = new EditPositionWindow(SelectedPosition);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }

        private void OpenEditUserWindowMetod()
        {
            EditUserWindow editUserWindow = new EditUserWindow(SelectedUser);
            SetCenterPositionAndOpen(editUserWindow);
        }

        private void OpenAddDepartmentWindowMetod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionAndOpen(newDepartmentWindow);
        }
        private void OpenAddPositionWindowMetod()
        {
            AddNewPositionWindow newpositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpen(newpositionWindow);
        }
        private void OpenAddUserWindowMetod()
        {
            AddNewUserWindow newUserWindow = new AddNewUserWindow();
            SetCenterPositionAndOpen(newUserWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        #region Свойства для отдела

        public static string DepartmentName { get; set; }

        #endregion

        #region свойства для позиций

        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxnumber { get; set; }
        public static Department PositionDepartment { get; set; }

        #endregion

        #region свойства для Пользователей

        public static string UserName { get; set; }
        public static string UserSurName { get; set; }
        public static string UserPhone { get; set; }
        public static Position UserPosition { get; set; }


        #endregion

        #region Сво-ва для выделенного эл-та

        private TabItem _SelectedTabItem;
        public TabItem SelectedTabItem { get => _SelectedTabItem ; set => Set(ref _SelectedTabItem, value); }
        public static User SelectedUser
        {
            get;
            set;
        }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }

        #endregion

        #region Commands to add

        private RelayCommand _AddNewDepartment;

        public RelayCommand AddNewDepartment
        {
            get
            {
                return _AddNewDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "";
                    if (string.IsNullOrWhiteSpace(DepartmentName))
                    {
                        SetRedVlockControll(window, "NameBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullvaluesToProperties();
                        window.Close();
                    }

                });
            }
        }

        private RelayCommand _AddNewPosition;

        public RelayCommand AddNewPosition
        {
            get
            {
                return _AddNewPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "";
                    if (string.IsNullOrWhiteSpace(PositionName))
                    {
                        SetRedVlockControll(window, "NameBlock");
                    }

                    if (PositionSalary == 0)
                    {
                        SetRedVlockControll(window, "SalaryBlock"); // "SalaryBlock"
                    }

                    if (PositionMaxnumber == 0)
                    {
                        SetRedVlockControll(window, "MaxNumberBlock"); //"MaxNumberBlock"
                    }

                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Укажите Отдела");
                    }

                    else
                    {
                        resultStr = DataWorker.CreatePasition(PositionName, PositionSalary, PositionMaxnumber, PositionDepartment);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullvaluesToProperties();
                        window.Close();
                    }
                });
            }
        }

        private RelayCommand _AddNewUser;

        public RelayCommand AddNewUser
        {
            get
            {
                return _AddNewUser ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "";
                    if (string.IsNullOrWhiteSpace(UserName))
                    {
                        SetRedVlockControll(window, "NameBlock");
                    }

                    if (string.IsNullOrWhiteSpace(UserSurName))
                    {
                        SetRedVlockControll(window, "SurNameBlock"); // "SurNameBlock"
                    }

                    if (string.IsNullOrWhiteSpace(UserPhone))
                    {
                        SetRedVlockControll(window, "PhoneBlock"); //"PhoneBlock"
                    }

                    if (UserPosition == null)
                    {
                        MessageBox.Show("Укажите Должность");
                    }

                    else
                    {
                        resultStr = DataWorker.CreateUser(UserName, UserSurName, UserPhone, UserPosition);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullvaluesToProperties();
                        window.Close();
                    }
                });
            }
        }


        #endregion

        #region Command to Delete

        private RelayCommand _DeleteItem;

        public RelayCommand DeleteItem
        {
            get
            {
                return _DeleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";

                    if (SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                    {
                        resultStr = DataWorker.DeleteUser(SelectedUser);
                        UpdateAllDataView();
                    }

                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }

                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }

                    SetNullvaluesToProperties();
                    ShowMessageToUser(resultStr);
                });
            }
        }

        #endregion

        #region Update Views

        private void SetNullvaluesToProperties()
        {
            ///Для отдела
            DepartmentName = null;

            /// Для позиции
            PositionName = null;
            PositionSalary = 0;
            PositionMaxnumber = 0;
            PositionDepartment = null;

            /// Для пользователя
            UserName = null;
            UserPosition = null;
            UserPhone = null;
            UserSurName = null;
        }

        private void UpdateAllDataView()
        {
            UpdateDepartmentViews();
            UpdatePositionViews();
            UpdateUsersViews();
        }

        private void UpdateDepartmentViews()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }

        private void UpdatePositionViews()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionView.ItemsSource = null;
            MainWindow.AllPositionView.Items.Clear();
            MainWindow.AllPositionView.ItemsSource = AllPositions;
            MainWindow.AllPositionView.Items.Refresh();
        }

        private void UpdateUsersViews()
        {
            AllUsers = DataWorker.GetAllUsers();
            MainWindow.AllUsersView.ItemsSource = null;
            MainWindow.AllUsersView.Items.Clear();
            MainWindow.AllUsersView.ItemsSource = AllUsers;
            MainWindow.AllUsersView.Items.Refresh();
        }

        #endregion

        /// <summary>
        /// Show MessageBox
        /// </summary>
        /// <param name="messege"></param>
        private void ShowMessageToUser(string messege)
        {
            MessageView messageView = new MessageView(messege);
            SetCenterPositionAndOpen(messageView);
        }

        private void SetRedVlockControll(Window window, string blocName)
        {
            Control bloControl = window.FindName(blocName) as Control;
            bloControl.BorderBrush = Brushes.Red;
        }

    }
}
