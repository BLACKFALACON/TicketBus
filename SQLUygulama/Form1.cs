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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=.;Initial Catalog =Otobus_db;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            int pad = 0,x=50,y=50,sayac=0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button btn = new Button();
                    btn.Width = btn.Height = 50;
                    if (j%2==0 && j!=0)
                    {
                        pad += 20;
                    }
                    btn.Left = (x * j + pad);
                    btn.Top = y * i;
                    btn.BackColor = Color.Green;
                    sayac++;
                    btn.Text = sayac.ToString();
                    btn.Name = "Koltuk" + sayac.ToString();
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Click += Btn_Click;
                    panel1.Controls.Add(btn);
                }
                pad = 0;
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT*FROM bilet", baglanti);
            SqlDataReader oku = komut.ExecuteReader();// veri tabanındaki verileri satır satır okur
            while (oku.Read())
            {
                if (oku["cinsiyet"].ToString()=="E")
                {
                    ((Button)panel1.Controls["Koltuk" + oku["koltukNo"].ToString()]).BackColor = Color.Blue;
                }
                else
                {
                    ((Button)panel1.Controls["Koltuk" + oku["koltukNo"].ToString()]).BackColor = Color.Pink;
                }
            }
            baglanti.Close();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Text=((Button)sender).Text;
            SqlCommand komut = new SqlCommand("SELECT*FROM bilet WHERE koltukNo="+((Button)sender).Text, baglanti);
            baglanti.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                frm2.Controls["textBox1"].Text = oku["adi"].ToString();
                frm2.Controls["textBox2"].Text = oku["soyadi"].ToString();
                frm2.Controls["comboBox1"].Text = oku["cinsiyet"].ToString();     
            }
            baglanti.Close();
            frm2.ShowDialog();
            if (frm2.comboBox1.Text == "E")
            {
                ((Button)sender).BackColor = Color.Blue;
            }
            else if (frm2.comboBox1.Text == "K")
            {
                ((Button)sender).BackColor = Color.Pink;
            }
            else
                ((Button)sender).BackColor = Color.Green;

        }
    }
}
