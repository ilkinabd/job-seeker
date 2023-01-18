namespace JobSeekerApi.Contexts;
using Microsoft.Extensions.Configuration;
using Npgsql;
public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    private readonly string? _masterConnectionString;
    public DapperContext(IConfiguration configuration)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SqlConnection");
        _masterConnectionString = _configuration.GetConnectionString("MasterSqlConnection");
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        NpgsqlLoggingConfiguration.InitializeLogging(loggerFactory);
    }
    public NpgsqlConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);

    public NpgsqlConnection CreateMasterConnection()
=> new NpgsqlConnection(_masterConnectionString);
}