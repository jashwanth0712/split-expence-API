using Microsoft.AspNetCore.Mvc;
using practiceWebApi.models;
using System.Data.SqlClient;
using System.Text;

namespace practiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class split : Controller
    {
        private readonly IConfiguration _configuration;
        public split(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("transaction")]
        public string set_transaction(transactions transaction)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ToysCon").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO transactions(group_id, sender , recipient, amount) VALUES('" +
                transaction.group_id+ "','" + transaction.sender + "','" + transaction.recipient+ "','" + transaction.amount.ToString() + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "inserted "+
                transaction.group_id + "','" + transaction.sender + "','" + transaction.recipient + "','" + transaction.amount.ToString() ;
            }
            else
            {
                return "error";
            }
            return "";
        }
        [HttpGet]
        [Route("transactions")]
        public List<Dictionary<string, object>> GetSettlements(int group_id)
        {
            List<Dictionary<string, object>> settlements = new List<Dictionary<string, object>>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ToysCon").ToString());
            SqlCommand cmd = new SqlCommand("SELECT * FROM transactions WHERE group_id = @GroupId", con);
            cmd.Parameters.AddWithValue("@GroupId", group_id);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Dictionary<string, object> settlement = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object value = reader.GetValue(i);

                    settlement.Add(columnName, value);
                }

                settlements.Add(settlement);
            }

            con.Close();

            return settlements;
        }
        [HttpGet]
        [Route("transactions")]
        public List<Dictionary<string, object>> GetSenderReceiver(string sender , string reciever)
        {
            List<Dictionary<string, object>> settlements = new List<Dictionary<string, object>>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ToysCon").ToString());
            SqlCommand cmd = new SqlCommand("SELECT * FROM transactions WHERE sender=@Sender and reciever =@Reciever", con);
            cmd.Parameters.AddWithValue("@Sender", sender);
            cmd.Parameters.AddWithValue("@reciever", reciever);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Dictionary<string, object> settlement = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object value = reader.GetValue(i);

                    settlement.Add(columnName, value);
                }

                settlements.Add(settlement);
            }

            con.Close();

            return settlements;
        }




    }
}
