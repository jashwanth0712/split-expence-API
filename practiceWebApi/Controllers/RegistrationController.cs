using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practiceWebApi.models;
using System.Data.SqlClient;

namespace practiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("reistration")]
        public string registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ToysCon").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(Username, Password, Email, IsActive) VALUES('" +
                registration.UserName+"','" + registration.Password+ "','" + registration.Email + "','" + registration.IsActive.ToString() + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Data Inserted";
            }
            else
            {
                return "error";
            }
            return "";
        }
    }
}
