using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _2Domash2902
{
    internal class DB
    {
        public string StringConMySql = @"server=localhost;port=3306;username=root;password=1234;database=acc";

        public DataTable MySqlReturnData(string query, DataGridView grid)
        {
            try
            {
                using (MySqlConnection myCon = new MySqlConnection(StringConMySql))
                {
                    myCon.Open();
                    if (myCon.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Не удалось установить подключение к базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(query, myCon))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        grid.DataSource = dt;
                        return dt;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Возникла ошибка при выполнении запроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}