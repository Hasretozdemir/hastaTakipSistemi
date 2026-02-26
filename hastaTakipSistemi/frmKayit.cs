using System;
using System.Data;
using System.Data.SqlClient; // Küçük 'd' olan System.data hata verebilir, doğrusu budur.
using System.Windows.Forms;


namespace hastaTakipSistemi
{
    public partial class frmKayit : Form
    {
        public frmKayit()
        {
            InitializeComponent();
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            if (txtKulAdi.Text != "" && txtSifre.Text != "")
            {
                // Bağlantıyı static metodun üzerinden doğrudan çağırıyoruz
                using (SqlCommand kayit = new SqlCommand("kayitol", frmSqlBaglanti.baglanti()))
                {
                    // Doğru yazım şekli budur:
                    kayit.CommandType = CommandType.StoredProcedure;

                    // Parametrelerin hepsi aynı blok içinde olmalı
                    kayit.Parameters.AddWithValue("@kulAdi", txtKulAdi.Text);
                    kayit.Parameters.AddWithValue("@sifre", txtSifre.Text);

                    kayit.ExecuteNonQuery();

                    MessageBox.Show("Kayıt İşlemi Başarılı", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}