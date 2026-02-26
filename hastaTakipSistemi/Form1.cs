using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace hastaTakipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        frmSqlBaglanti bgl = new frmSqlBaglanti();


        // Kayýt Formunu Açan Buton
        private void btnKayit_Click(object sender, EventArgs e)
        {
            frmKayit fr = new frmKayit();
            fr.Show();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtKulAdi.Text) && !string.IsNullOrEmpty(txtSifre.Text))
            {
                // Baðlantýyý alýyoruz
                SqlConnection baglantiNesnesi = frmSqlBaglanti.baglanti();

                SqlCommand giris = new SqlCommand("girisYap", baglantiNesnesi);
                giris.CommandType = CommandType.StoredProcedure;

                // SQL'deki parametre isimlerinin @ iþareti ile baþladýðýndan emin ol
                giris.Parameters.AddWithValue("@kulAdi", txtKulAdi.Text);
                giris.Parameters.AddWithValue("@sifre", txtSifre.Text);

                SqlDataReader dr = giris.ExecuteReader();

                if (dr.Read())
                {
                    MessageBox.Show("Giriþ Ýþlemi Baþarýlý", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
             frmAnaSayfa fr = new frmAnaSayfa();
                    fr.Show();
                    this.Hide(); // Giriþ yaptýktan sonra giriþ formunu kapat


                }
                else
                {
                    MessageBox.Show("Hatalý Kullanýcý Adý veya Þifre", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                baglantiNesnesi.Close(); // Baðlantýyý kapatmayý unutma
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanlarý doldurunuz.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    } 
