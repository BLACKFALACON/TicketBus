using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SQLUygulama
{

    public partial class Form2 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=.;Initial Catalog =Otobus_db;Integrated Security=True");
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut=new SqlCommand("DELETE FROM bilet WHERE koltukNo=" + this.Text, baglanti);
            comboBox1.Text = "";
            komut.ExecuteNonQuery();
            baglanti.Close();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM bilet WHERE koltukNo=" + this.Text, baglanti);
            komut.ExecuteNonQuery();
            komut = new SqlCommand("INSERT INTO (koltukNo,adi,soyadi,cinsiyet )VALUES(@P1,@P2,@P3,@P4)",baglanti);
            komut.Parameters.AddWithValue("@P1", this.Text);
            komut.Parameters.AddWithValue("@P2", textBox1.Text);
            komut.Parameters.AddWithValue("@P3", textBox2.Text);
            komut.Parameters.AddWithValue("@P4", comboBox1.Text);
            komut.ExecuteNonQuery();
            komut.Parameters.Clear();
            baglanti.Close();
            this.Close();
        }
    }
}
