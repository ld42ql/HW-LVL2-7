using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW_LVL2_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection sqlConnection;
        SqlDataAdapter dataAdapter;
        DataTable dataTable;

        public MainWindow()
        {
            InitializeComponent();

            EditBtn.Click += updateEmployee;
            MouseDoubleClick += updateEmployee;

            AddBtn.Click += insertEmployee;

            DelBtn.Click += deleteEmployee;
        }

        /// <summary>
        /// Команды для работы с таблицей
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            sqlConnection = new SqlConnection(connectionString);
            dataAdapter = new SqlDataAdapter();

            #region Показать работников
            SqlCommand command = new SqlCommand("SELECT * FROM Employee", sqlConnection);
            dataAdapter.SelectCommand = command;
            #endregion

            #region Добавит строку
            command = new SqlCommand(@"INSERT INTO Employee (FIO, Department, Salary)
                                       VALUES (@FIO, @Department, @Salary); 
                                       SET @ID = @@IDENTITY;", sqlConnection);
            command.Parameters.Add("@FIO", SqlDbType.NVarChar, -1, "FIO");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, -1, "Salary");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            dataAdapter.InsertCommand = command;
            #endregion

            #region Обновит строку
            command = new SqlCommand(@"UPDATE Employee SET FIO = @FIO, Department = @Department, Salary = @Salary
                                       WHERE ID = @ID", sqlConnection);
            command.Parameters.Add("@FIO", SqlDbType.NVarChar, -1, "FIO");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, -1, "Salary");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            dataAdapter.UpdateCommand = command;

            #endregion
            #region Удалить строку
            command = new SqlCommand("DELETE FROM Employee WHERE ID = @ID", sqlConnection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            dataAdapter.DeleteCommand = command;
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            #endregion

            listView.DataContext = dataTable.DefaultView;
        }

        /// <summary>
        /// Добавить работника
        /// </summary>
        private void insertEmployee(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dataTable.NewRow();
            EditWindow editWindow = new EditWindow(newRow);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.Value)
            {
                dataTable.Rows.Add(editWindow.resultRow);
                dataAdapter.Update(dataTable);
            }
        }

        /// <summary>
        /// Обновить данные
        /// </summary>
        private void updateEmployee(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)listView.SelectedItem;
            newRow.BeginEdit();
            EditWindow editWindow = new EditWindow(newRow.Row);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                newRow.EndEdit();
                dataAdapter.Update(dataTable);
            }
        }

        /// <summary>
        /// Удалить работника
        /// </summary>
        private void deleteEmployee(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)listView.SelectedItem;

            newRow.Row.Delete();
            dataAdapter.Update(dataTable);
        }
    }
}
