using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace hastaTakipSistemi
{
    public partial class frmAnaSayfa : Form
    {
        public frmAnaSayfa()
        {
            InitializeComponent();
        }

        // Bağlantı nesnesini form genelinde tanımlıyoruz
        SqlConnection baglanti = frmSqlBaglanti.baglanti();

        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            Listele();
            DurumDoldur();
            BolumDoldur();
            txtTarih.Text = DateTime.Now.ToShortDateString();
        }

        private void Listele()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.listele", baglanti); 
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Listeleme Hatası: " + ex.Message); }
        }

        private void DurumDoldur()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.durumDoldur", baglanti); 
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                txtDurum.DataSource = dt;
                txtDurum.DisplayMember = "durumAd"; // SQL'deki kolon adın
                txtDurum.ValueMember = "durumID";   // SQL'deki kolon adın
                txtDurum.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show("Durum Doldurma Hatası: " + ex.Message); }
        }

        private void BolumDoldur()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.bolumDoldur", baglanti); // dbo eki eklendi
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                txtBolum.DataSource = dt;
                txtBolum.DisplayMember = "bolumAd"; // SQL'deki kolon adın
                txtBolum.ValueMember = "bolumID";   // SQL'deki kolon adın
                txtBolum.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show("Bölüm Doldurma Hatası: " + ex.Message); }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    txtAd.Text = dataGridView1.CurrentRow.Cells["hAd"].Value.ToString();
                    txtSoyad.Text = dataGridView1.CurrentRow.Cells["hSoyad"].Value.ToString();
                    txtTc.Text = dataGridView1.CurrentRow.Cells["hTc"].Value.ToString();
                    txtTel.Text = dataGridView1.CurrentRow.Cells["hTel"].Value.ToString();
                    txtYas.Text = dataGridView1.CurrentRow.Cells["hYas"].Value.ToString();
                    txtCinsiyet.Text = dataGridView1.CurrentRow.Cells["hCinsiyet"].Value.ToString();
                    txtSikayet.Text = dataGridView1.CurrentRow.Cells["hSikayet"].Value.ToString();

                    object tarihVal = dataGridView1.CurrentRow.Cells["kTarih"].Value;
                    txtTarih.Text = (tarihVal != DBNull.Value) ? Convert.ToDateTime(tarihVal).ToShortDateString() : DateTime.Now.ToShortDateString();

                    txtDurum.SelectedValue = dataGridView1.CurrentRow.Cells["hDurum"].Value;
                    txtBolum.SelectedValue = dataGridView1.CurrentRow.Cells["hBolum"].Value;

                    bool ex = false;
                    if (dataGridView1.CurrentRow.Cells["hExMi"].Value != DBNull.Value)
                        ex = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["hExMi"].Value);

                    rbEvet.Checked = ex;
                    rbHayir.Checked = !ex;
                }
            }
            catch { }
        }

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("dbo.kaydet", baglanti); // dbo eklendi
                komut.CommandType = CommandType.StoredProcedure;

                komut.Parameters.AddWithValue("@ad", txtAd.Text);
                komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@tel", txtTel.Text);

                short yas = 0;
                short.TryParse(txtYas.Text, out yas);
                komut.Parameters.AddWithValue("@yas", yas);

                komut.Parameters.AddWithValue("@cins", txtCinsiyet.Text);
                komut.Parameters.AddWithValue("@sikayet", txtSikayet.Text);

                DateTime tarih;
                if (!DateTime.TryParse(txtTarih.Text, out tarih)) { tarih = DateTime.Now; } // Tarih hatası çözümü
                komut.Parameters.AddWithValue("@tarih", tarih);

                komut.Parameters.AddWithValue("@durum", txtDurum.SelectedValue ?? DBNull.Value);
                komut.Parameters.AddWithValue("@bolum", txtBolum.SelectedValue ?? DBNull.Value);
                komut.Parameters.AddWithValue("@ex", rbEvet.Checked);

                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Kayıt Başarılı");
                Listele();
            }
            catch (Exception ex) { MessageBox.Show("Hata Oluştu: " + ex.Message); }
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            try
            {
                // HATA ÇÖZÜMÜ: Tablo adını 'tbl_HastaBillgi' (çift L) yaptım
                SqlCommand komut = new SqlCommand("DELETE FROM dbo.tbl_HastaBillgi WHERE ID=@id", baglanti);
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells["ID"].Value);

                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Kayıt Silindi");
                Listele();
            }
            catch (Exception ex) { MessageBox.Show("Silme Hatası: " + ex.Message); }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("dbo.guncelle", baglanti);
                komut.CommandType = CommandType.StoredProcedure;

                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells["ID"].Value);
                komut.Parameters.AddWithValue("@ad", txtAd.Text);
                komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@tel", txtTel.Text);

                short yas = 0;
                short.TryParse(txtYas.Text, out yas);
                komut.Parameters.AddWithValue("@yas", yas);

                komut.Parameters.AddWithValue("@cins", txtCinsiyet.Text);
                komut.Parameters.AddWithValue("@sikayet", txtSikayet.Text);

                DateTime tarih;
                if (!DateTime.TryParse(txtTarih.Text, out tarih)) { tarih = DateTime.Now; }
                komut.Parameters.AddWithValue("@tarih", tarih);

                komut.Parameters.AddWithValue("@durum", txtDurum.SelectedValue ?? DBNull.Value);
                komut.Parameters.AddWithValue("@bolum", txtBolum.SelectedValue ?? DBNull.Value);
                komut.Parameters.AddWithValue("@ex", rbEvet.Checked);

                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Güncelleme Başarılı");
                Listele();
            }
            catch (Exception ex) { MessageBox.Show("Güncelleme Hatası: " + ex.Message); }
        }

        private void btnFormuTemizle_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls) { if (c is TextBox) c.Text = ""; }
            txtTarih.Text = DateTime.Now.ToShortDateString();
            txtDurum.SelectedIndex = -1;
            txtBolum.SelectedIndex = -1;
            rbEvet.Checked = false;
            rbHayir.Checked = true;
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();  
        }
    }
}