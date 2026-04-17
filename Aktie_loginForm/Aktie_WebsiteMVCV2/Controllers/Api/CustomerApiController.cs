using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Aktie_WebsiteMVCV2.Controllers.Api
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private string connectionString =
            "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-V251_10665995;User ID=DMA-CSD-V251_10665995;Password=Password1!;Trust Server Certificate=True";

        [HttpGet]
        public IActionResult GetCustomers()
        {
            List<object> customers = new();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT KundeID, Email, KundeNavn FROM Customers";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new
                        {
                            KundeID = reader["KundeID"],
                            Email = reader["Email"],
                            KundeNavn = reader["KundeNavn"]
                        });
                    }
                }
            }

            return Ok(customers);
        }
    }
}