using Microsoft.Data.SqlClient;
using testKol.Exceptions;
using testKol.Models.DTOs;
using DateTime = System.DateTime;
using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;

namespace testKol.Services;

public class VisitService : IVisitService
{
    private readonly string _connectionString;

    public VisitService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default") ?? string.Empty;
    }
    
    public async Task<ClientVisitDTO> GetClientVisit(int id)
    {
        await using SqlConnection con = new SqlConnection(_connectionString);
        await using SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        string query =
            @"Select v.date, c.first_name, c.last_name, c.date_of_birth, m.mechanic_id, m.licence_number, s.name, vs.service_fee
                    From Visit v
                    join Client AS c ON c.client_id = v.client_id
                    join Mechanic AS m ON m.mechanic_id = v.mechanic_id
                    join Visit_Service AS vs ON vs.visit_id = v.visit_id
                    join Service AS s ON s.service_id = vs.service_id
                    WHERE v.visit_id = @id";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
        
        await con.OpenAsync();
            
        var reader = await cmd.ExecuteReaderAsync();
        ClientVisitDTO? visit = null;
        List<VisitServ> visitServicesList = [];
        while (await reader.ReadAsync())
        {
            if (visit == null)
            {
                visit = new ClientVisitDTO
                {
                    Date = (DateTime)reader["date"],
                };
                Client client = new Client();
                client.FirstName = reader["first_name"].ToString();
                client.LastName = reader["last_name"].ToString();
                visit.Client = client;
                Mechanic mechanic = new Mechanic();
                mechanic.MechanicId = (int)reader["mechanic_id"];
                mechanic.LicenceNumber = reader["licence_number"].ToString();
                visit.Mechanic = mechanic;
            }
            VisitServ visitServ = new VisitServ();
            visitServ.Name = (string)reader["name"];
            visitServ.ServiceFee = (decimal)reader["service_fee"];
            visitServicesList.Add(visitServ);
        }
        visit.VisitServices = visitServicesList;

        if (visit is null)
        {
            throw new NotFoundException("Visit service not found");
        }
        
        return visit;
    }

    public async Task AddClientVisit(VisitAddDTO visitAdd)
    {
        await using SqlConnection con = new SqlConnection(_connectionString);
        await using SqlCommand cmd = new SqlCommand();
        
        cmd.Connection = con;
        
        await con.OpenAsync();

        
        var query = "Select 1 From Visit where Visit_id = @id";
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@id", visitAdd.VisitId);
        int? res = (int?) await cmd.ExecuteScalarAsync();
        if (res is not null)
        {
            throw new ConflictException("Visit with such id already exists");
        }
           
        
        
    }
}