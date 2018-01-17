using System;
using System.Data.SqlClient;
using System.Configuration;

namespace GenericTable
{
    class Program
    {
       static public Random random = new Random();


       static  void Main(string[] args)
        {
            TableGeneric(200);

            Console.WriteLine("Таблица заполнена!");
            
        }

        /// <summary>
        /// Заполняем базу данных
        /// </summary>
        /// <param name="count">Количество работников</param>
        static private void TableGeneric(int count)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            for (int i = 0; i < count; i++)
            {
                var employee = new Employee
                {
                    FIO = $"{RandomString(7)} {RandomString(10)}",
                    NameDepartmet = $"Отдел №{random.Next(1, 10)}",
                    Salary = $"{random.Next(100, 900)}"
                };

                var sql = $@"INSERT INTO Employee (FIO, Department, Salary) 
                VALUES (N'{employee.FIO}', N'{employee.NameDepartmet}', '{employee.Salary}')";

                Console.WriteLine(sql);
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();

                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        /// <summary>
        /// Создаем случайную строку
        /// </summary>
        /// <param name="count">Максимальное возможное количество букв</param>
        /// <returns></returns>
        static private string RandomString(int count)
        {
            int n = random.Next(5, count);
            string str = String.Empty;
            char[] arrayChar = new char[n];
            for (int i = 0; i < n; i++)
            {
                arrayChar[i] = i == 0 ? (char)random.Next(0x0410, 0x42F) : (char)random.Next(0x0430, 0x44F);
                str += arrayChar[i];
            }
            return str;
        }
    }
}
