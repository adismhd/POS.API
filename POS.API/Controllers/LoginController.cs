using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using POS.API.Models;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _config;

        public LoginController(ILogger<LoginController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost]
        public LoginRes Post(LoginReq req)
        {
            var connstr = _config.GetValue<string>("ConnectionStrings:DefaultConnection");
            LoginRes res = new LoginRes();

            var message = ""; var status = ""; 
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                var query = "SELECT * FROM TBL_USER WHERE ID = '"+ req.Username + "' AND PASSWORD = '" + req.Password + "'";

                SqlDataAdapter getData = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                getData.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    status = "SUCCESS";
                    message = "Login Sukses!! Selamat Datang " + dt.Rows[0]["NAME"];
                }
                else
                {
                    status = "FAILED";
                    message = "Login Gagal!! User "+ req.Username +" Tidak Ditemukan";
                }
            
            }

            res.Message = message;
            res.Status = status;
            return res;
        }
    }
}
