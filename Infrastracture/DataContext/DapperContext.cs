using Npgsql;

namespace Infrastracture.DataContext;

public class DapperContext
{
    private readonly string _context="Host=localhost;Port=5432;Database=dapper-db;User Id=postgres;Password=2810";

    public NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(_context);
    }
}