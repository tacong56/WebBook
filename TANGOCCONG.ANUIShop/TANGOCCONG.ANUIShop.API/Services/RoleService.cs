using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Helpers;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.Data.EF;

namespace TANGOCCONG.ANUIShop.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly ANUIShopDbContext _context;
        private readonly ConnectionStrings _conn;
        public RoleService(ANUIShopDbContext context, IOptions<ConnectionStrings> conn)
        {
            _context = context;
            _conn = conn.Value;
        }

        public async Task<List<RoleDataReponse>> GetList()
        {
            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();
            string query = "SELECT * FROM approles ORDER BY Id";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            List<RoleDataReponse> entries = new List<RoleDataReponse>();
            try
            {
                while (await reader.ReadAsync())
                {
                    RoleDataReponse item = new RoleDataReponse()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString()
                    };
                    entries.Add(item);
                    // do something with 'value'
                }

                return entries;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
