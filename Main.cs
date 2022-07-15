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

namespace Kitaplık
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=BQRAYVZDGN;Initial Catalog=DbKitaplik;Integrated Security=True");
        void Listele()
        {
            SqlCommand komut = new SqlCommand("Select * From TBL_Kitaplar", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void Turler()
        {
            SqlCommand komut = new SqlCommand("Select * From TBL_Türler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbxTur.DisplayMember = "TurAd";
            CmbxTur.DataSource = dt;
        }
        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
            Turler();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutekle = new SqlCommand("insert into TBL_Kitaplar (KitapAd, Yazar, Sayfa, Fiyat, Yayınevi, KitapKod, KitapTur) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
            komutekle.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            komutekle.Parameters.AddWithValue("@p2", txtYazar.Text);
            komutekle.Parameters.AddWithValue("@p3", txtSayfa.Text);
            komutekle.Parameters.AddWithValue("@p4", txtFiyat.Text);
            komutekle.Parameters.AddWithValue("@p5", txtYayınevi.Text);
            komutekle.Parameters.AddWithValue("@p6", txtKitapKodu.Text);
            komutekle.Parameters.AddWithValue("@p7", CmbxTur.SelectedIndex);
            komutekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Veri Tabanına Eklendi", "Bilgi ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKitapID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKitapAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSayfa.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtYayınevi.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtKitapKodu.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete From TBL_Kitaplar where KitapID=@p1", baglanti);
            komutsil.Parameters.AddWithValue("@p1", txtKitapID.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Veri Tabanından Silindi", "Bilgi ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }
        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update TBL_Kitaplar set KitapAd=@p1, Yazar=@p2, Sayfa=@p3, Fiyat=@p4, Yayınevi=@p5,KitapKod=@p6, KitapTur=@p7 where KitapID=@p8", baglanti);
            komutguncelle.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2", txtYazar.Text);
            komutguncelle.Parameters.AddWithValue("@p3", txtSayfa.Text);
            komutguncelle.Parameters.AddWithValue("@p4", txtFiyat.Text);
            komutguncelle.Parameters.AddWithValue("@p5", txtYayınevi.Text);
            komutguncelle.Parameters.AddWithValue("@p6", txtKitapKodu.Text);
            komutguncelle.Parameters.AddWithValue("@p7", CmbxTur.SelectedIndex);
            komutguncelle.Parameters.AddWithValue("@p8", txtKitapID.Text);
            komutguncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Bilgisi Güncellendi", "Bilgi ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Listele();
        }
    }
}
