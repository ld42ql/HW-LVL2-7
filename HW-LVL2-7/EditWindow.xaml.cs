using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace HW_LVL2_7
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
       public  DataRow resultRow { get; set; }

        public EditWindow(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;
            
            applyBtn.Click += saveEmployee;
        }

        /// <summary>
        /// Сохраняем данные
        /// </summary>
        private void saveEmployee(object sender, RoutedEventArgs e)
        {
            resultRow["FIO"] = txtFIO.Text;
            resultRow["Department"] = txtDepartmet.Text;
            resultRow["Salary"] = txtSalary.Text;
            DialogResult = true;
        }
    }
}
