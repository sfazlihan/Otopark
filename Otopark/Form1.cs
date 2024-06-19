using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Otopark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'otoparkDataSet.AracKayitlari' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.aracKayitlariTableAdapter.Fill(this.otoparkDataSet.AracKayitlari);
            // TODO: Bu kod satırı 'otoparkDataSet.AracAnlik' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.aracAnlikTableAdapter.Fill(this.otoparkDataSet.AracAnlik);
            // TODO: Bu kod satırı 'otoparkDataSet.AracKayitlari' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.aracKayitlariTableAdapter.Fill(this.otoparkDataSet.AracKayitlari);
            // TODO: Bu kod satırı 'otoparkDataSet.AracAnlik' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.aracAnlikTableAdapter.Fill(this.otoparkDataSet.AracAnlik);
            this.aracKayitlariTableAdapter.Fill(this.otoparkDataSet.AracKayitlari);
            this.aracAnlikTableAdapter.Fill(this.otoparkDataSet.AracAnlik);
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox3.SelectedItem = comboBox3.Items[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "1":
                    panel1.BackColor = Color.Red; break;
                case "2":
                    panel2.BackColor = Color.Red; break;
                case "3":
                    panel3.BackColor = Color.Red; break;
                case "4":
                    panel4.BackColor = Color.Red; break;
                case "5":
                    panel5.BackColor = Color.Red; break;
                case "6":
                    panel10.BackColor = Color.Red; break;
                case "7":
                    panel9.BackColor = Color.Red; break;
                case "8":
                    panel8.BackColor = Color.Red; break;
                case "9":
                    panel7.BackColor = Color.Red; break;
                case "10":
                    panel6.BackColor = Color.Red; break;
                case "11":
                    panel11.BackColor = Color.Red; break;
                case "12":
                    panel12.BackColor = Color.Red; break;
                case "13":
                    panel13.BackColor = Color.Red; break;
                case "14":
                    panel14.BackColor = Color.Red; break;
                case "15":
                    panel15.BackColor = Color.Red; break;
                case "16":
                    panel16.BackColor = Color.Red; break;
                case "17":
                    panel17.BackColor = Color.Red; break;
                case "18":
                    panel18.BackColor = Color.Red; break;
                case "19":
                    panel19.BackColor = Color.Red; break;
                case "20":
                    panel20.BackColor = Color.Red; break;
            }

            //Veritabanına Ekleme
            string connectionString = "Data Source=DESKTOP-4I9L00G\\SQLEXPRESS;Initial Catalog=otopark;Integrated Security=True;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string v1 = textBox4.Text;
                string v2 = comboBox3.SelectedItem.ToString();
                string v3 = DateTime.Now.ToString();
                string v4 = comboBox1.SelectedItem.ToString();

                using (SqlCommand command = new SqlCommand("INSERT INTO AracAnlik (plaka, aracturu, giriszamani, parknumarasi) VALUES (@value1, @value2, @value3, @value4)", connection))
                {
                    command.Parameters.AddWithValue("@value1", v1);
                    command.Parameters.AddWithValue("@value2", v2);
                    command.Parameters.AddWithValue("@value3", v3);
                    command.Parameters.AddWithValue("@value4", v4);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            Form1_Load(sender, e);
            textBox4.Clear();
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

            string connectionString = "Data Source=DESKTOP-4I9L00G\\SQLEXPRESS;Initial Catalog=otopark;Integrated Security=True;Encrypt=False;";
            SqlConnection connectionn = new SqlConnection(connectionString);

            // Seçilen satırın hücre değerlerini alma
            int secilenSatirIndex = dataGridView1.CurrentCell.RowIndex;
            string grid_id = dataGridView1.Rows[secilenSatirIndex].Cells[0].Value.ToString();
            string grid_plaka = dataGridView1.Rows[secilenSatirIndex].Cells[1].Value.ToString();
            string grid_aracturu = dataGridView1.Rows[secilenSatirIndex].Cells[2].Value.ToString();
            string grid_girissaati = dataGridView1.Rows[secilenSatirIndex].Cells[3].Value.ToString();
            string grid_parknumarasi = dataGridView1.Rows[secilenSatirIndex].Cells[4].Value.ToString();
            Console.WriteLine(grid_girissaati);
            string cikiszamani = DateTime.Now.ToString();
            TimeSpan parkSuresi = DateTime.Now.Subtract(DateTime.Parse(grid_girissaati));

            string parksuresi = parkSuresi.ToString().Substring(0, parkSuresi.ToString().Length - 8);

            int carpan = int.Parse(parksuresi.ToString().Substring(0, 2));
            string ucret = (((carpan * 20) + 20) + " TL");

            DialogResult result = MessageBox.Show(string.Format("{0} plakalı aracı çıkarmak istiyor musunuz? Ücret : {1}", grid_plaka, ucret), "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    connectionn.Open();
                    SqlCommand commandd = new SqlCommand("SELECT * FROM AracAnlik WHERE ID = @id", connectionn);
                    commandd.Parameters.AddWithValue("@id", grid_id);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        int v1 = int.Parse(grid_id);
                        string v2 = grid_plaka;
                        string v3 = grid_aracturu;
                        string v4 = grid_girissaati;
                        string v5 = cikiszamani;
                        string v6 = parksuresi;
                        string v7 = ucret;
                        string v8 = grid_parknumarasi;

                        using (SqlCommand command = new SqlCommand("INSERT INTO AracKayitlari (_id, plaka, aracturu, giriszamani, cikiszamani, sure, ucret, parknumarasi) VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8)", connection))
                        {
                            command.Parameters.AddWithValue("@value1", v1);
                            command.Parameters.AddWithValue("@value2", v2);
                            command.Parameters.AddWithValue("@value3", v3);
                            command.Parameters.AddWithValue("@value4", v4);
                            command.Parameters.AddWithValue("@value5", v5);
                            command.Parameters.AddWithValue("@value6", v6);
                            command.Parameters.AddWithValue("@value7", v7);
                            command.Parameters.AddWithValue("@value8", v8);
                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }


                    //Silme İşlemi
                    using (SqlCommand command = new SqlCommand("DELETE FROM AracAnlik WHERE _id = @id", connectionn))
                    {
                        command.Parameters.AddWithValue("@id", grid_id);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Araç Çıkışı Başarı İle Yapıldı.");
                    textBox4.Clear();
                    switch (grid_parknumarasi)
                    {
                        case "1":
                            panel1.BackColor = Color.Chartreuse; break;
                        case "2":
                            panel2.BackColor = Color.Chartreuse; break;
                        case "3":
                            panel3.BackColor = Color.Chartreuse; break;
                        case "4":
                            panel4.BackColor = Color.Chartreuse; break;
                        case "5":
                            panel5.BackColor = Color.Chartreuse; break;
                        case "6":
                            panel10.BackColor = Color.Chartreuse; break;
                        case "7":
                            panel9.BackColor = Color.Chartreuse; break;
                        case "8":
                            panel8.BackColor = Color.Chartreuse; break;
                        case "9":
                            panel7.BackColor = Color.Chartreuse; break;
                        case "10":
                            panel6.BackColor = Color.Chartreuse; break;
                        case "11":
                            panel11.BackColor = Color.Chartreuse; break;
                        case "12":
                            panel12.BackColor = Color.Chartreuse; break;
                        case "13":
                            panel13.BackColor = Color.Chartreuse; break;
                        case "14":
                            panel14.BackColor = Color.Chartreuse; break;
                        case "15":
                            panel15.BackColor = Color.Chartreuse; break;
                        case "16":
                            panel16.BackColor = Color.Chartreuse; break;
                        case "17":
                            panel17.BackColor = Color.Chartreuse; break;
                        case "18":
                            panel18.BackColor = Color.Chartreuse; break;
                        case "19":
                            panel19.BackColor = Color.Chartreuse; break;
                        case "20":
                            panel20.BackColor = Color.Chartreuse; break;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Hata : " + ex); }
            }
            connectionn.Close();
            Form1_Load(sender, e);
        }
    }
}