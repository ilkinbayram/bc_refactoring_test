using LegacyApp.Core.Entities;
using LegacyApp.Core.Repositories.Abstracts;
using LegacyApp.Core.Resources.Constant;
using LegacyApp.Core.Resources.ValueObjects;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp.Core.Repositories;

public class ClientRepository : IClientRepository
{
    public Client GetById(int id)
    {
        Client client = null;

        var connectionString = ConfigurationManager.ConnectionStrings[ApplicationConstants.ConnectionStringName].ConnectionString;

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand("uspGetClientById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@clientId", SqlDbType.Int) { Value = id });

            try
            {
                connection.Open();
                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        client = new Client
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ClientId")),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                            ClientStatus = ClientStatus.FromValue(reader["ClientStatus"].ToString())
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // BURADA LOGLAMA OLSA DAHA YAXSI OLAR...

                Console.WriteLine($"ERROR DETAIL: {ex.Message}");
                throw;
            }
        }

        return client;
    }
}
