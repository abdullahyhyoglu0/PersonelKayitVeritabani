using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qqq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-8S2D0E3\\SQLEXPRESS01;Initial Catalog=personelVeriTabani;Integrated Security=True;Encrypt=False");
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
      
            baglanti.Open();
            SqlCommand cmd=new SqlCommand("Update Tbl_Personel Set perAd=@s2,perSoyad=@s3,perSehir=@s4,perMaas=@s5,perDurum=@s6,perMeslek=@s7 where perId=@s1",baglanti);
            cmd.Parameters.AddWithValue("@s1", txtıd.Text);
            cmd.Parameters.AddWithValue("@s2",txtad.Text);
            cmd.Parameters.AddWithValue("@s3", txtsoyad.Text);
            cmd.Parameters.AddWithValue("@s4", cmbsehir.Text);
            cmd.Parameters.AddWithValue("@s5",maskmaas.Text);
            cmd.Parameters.AddWithValue("@s6", label8.Text);
            cmd.Parameters.AddWithValue("@S7", txtmeslek.Text);
            cmd.ExecuteNonQuery();
            
            
            baglanti.Close();

        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet1.Tbl_Personel);
        }
        void Temizle()
        {

            txtad.Text = "";
            txtmeslek.Text = "";
            txtsoyad.Text = "";
            txtıd.Text = "";
            cmbsehir.Text = "";
            maskmaas.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                SqlCommand KOMUT = new SqlCommand("insert into Tbl_Personel(perAd,perSoyad,perSehir,perMaas,perMeslek,perDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                KOMUT.Parameters.AddWithValue("@p1", txtad.Text);
                KOMUT.Parameters.AddWithValue("@p3", cmbsehir.Text);
                KOMUT.Parameters.AddWithValue("@p4", maskmaas.Text);
                KOMUT.Parameters.AddWithValue("@p2", txtsoyad.Text);
                KOMUT.Parameters.AddWithValue("@p5", txtmeslek.Text);
                KOMUT.Parameters.AddWithValue("@P6", label8.Text);
                MessageBox.Show("Personel başarıyla kaydedildi");


                KOMUT.ExecuteNonQuery();




                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "True";
            label8.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "False";
            label8.Visible = false;
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtıd.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbsehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            maskmaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtmeslek.Text= dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            label8.Visible = false;
            if (label8.Text == "False")
            {
                radioButton1.Checked = true;

            }
            else
            {
                radioButton2.Checked = true;
            }

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bu personeli silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                try
                {
                
                    baglanti.Open();

                    SqlCommand a = new SqlCommand("delete from Tbl_Personel where perId=@k1", baglanti);

                    a.Parameters.AddWithValue("@k1", txtıd.Text);
                    a.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Personel Başarıyla Silindi.");

                }
               


            catch (SqlException sql)
            {
                MessageBox.Show(sql.Message + " " + sql.ErrorCode);



            } }
            else
            {
                MessageBox.Show("Kullanıcı silinmedi veya Bulunamadı.", "Kullanıcı Silinemedi", MessageBoxButtons.OK);
            }
            
        }
        }
    }


