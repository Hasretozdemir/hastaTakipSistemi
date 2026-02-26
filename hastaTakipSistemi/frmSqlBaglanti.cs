using System.Data.SqlClient;


namespace hastaTakipSistemi
{
    internal class frmSqlBaglanti
    {
        public static SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=HASRET;Initial Catalog=db_HastaneYonetim;Integrated Security=True");
            if (baglan.State == System.Data.ConnectionState.Closed)
            {
                baglan.Open();
            }
            return baglan;
        }
    }
}