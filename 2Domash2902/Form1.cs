using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _2Domash2902
{
    public partial class Form1 : Form
    {
        private DB db;
        public Form1()
        {
            db = new DB();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM items";
            if (db.MySqlReturnData(query, dataGridView1) != null)
            {
                MessageBox.Show("Запрос успешно выполнен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        

        private void ExportToCsvOrTxt(string filePath, DataGridView dataGridView)
        {
            string delimiter = Path.GetExtension(filePath).ToLower() == ".csv" ? "," : "\t"; // Разделитель для CSV или TXT

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Записываем заголовки столбцов
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    writer.Write(dataGridView.Columns[i].HeaderText);
                    if (i < dataGridView.Columns.Count - 1)
                        writer.Write(delimiter);
                }
                writer.WriteLine();

                // Записываем данные
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        writer.Write(row.Cells[i].Value);
                        if (i < dataGridView.Columns.Count - 1)
                            writer.Write(delimiter);
                    }
                    writer.WriteLine();
                }
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;
                    ExportToCsvOrTxt(filePath, dataGridView1);
                    MessageBox.Show("Export successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

