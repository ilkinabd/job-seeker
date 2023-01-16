namespace JobSeekerApi.Extensions;
using JobSeekerApi.Migrations;
using FluentMigrator.Runner;
public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
            var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            try
            {
                databaseService.CreateDatabase("job_seeker");
                migrationService.ListMigrations();
                migrationService.MigrateUp();
            }
            catch
            {
                //log errors or ...
                throw;
            }
        }
        return host;
    }
}