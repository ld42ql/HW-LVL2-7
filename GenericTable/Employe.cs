using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericTable
{
    /// <summary>
    /// Рабочий
    /// </summary>
    class Employee
    {
        private int id;
        private string fio = String.Empty;
        private string nameDepartmet = String.Empty;
        private string salary = String.Empty;

        /// <summary>
        /// Индивидуальный номер
        /// </summary>
        public int ID
        {
            get => this.id;
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Имя рабочего
        /// </summary>
        public string FIO
        {
            get => this.fio;
            set
            {
                this.fio = value;
            }
        }

        /// <summary>
        /// Ставка рабочего
        /// </summary>
        public string Salary
        {
            get => this.salary;
            set
            {
                this.salary = value;
            }
        }

        /// <summary>
        /// Название отдела
        /// </summary>
        public string NameDepartmet
        {
            get => this.nameDepartmet;
            set
            {
                this.nameDepartmet = value;
            }
        }
    }
}
