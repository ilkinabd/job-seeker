namespace JobSeekerApi.Repositories;
using JobSeekerApi.Contracts;
using JobSeekerApi.Contexts;
using JobSeekerApi.Models;
using DapperQueryBuilder;
using Dapper;
public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;
    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers(UserParams parameters)
    {
        using (var connection = _context.CreateConnection())
        {
            var query = connection.QueryBuilder($@"SELECT * FROM users /**where**/");

            var filters = new Filters()
            {
                // new Filter($"deleted_at IS NULL")
            };
            if (parameters.Name != null)
            {
                var productVarcharParm = new DbString { 
                    Value = $@"%{parameters.Name}%", 
                    IsFixedLength = true, 
                    Length = 50, 
                    IsAnsi = true 
                };

                filters.Add(new Filter($@"name LIKE {productVarcharParm}"));
            }

            if (parameters.Email != null)
            {
                filters.Add(new Filter($"email LIKE %{parameters.Email}%"));
            }

            if (filters.Count() > 0)
            {
                query.Where(filters);
            }
            var list = query.Query<User>().ToList();

            return list;
        }
    }

    public async Task<User> GetUser(long id)
    {
        using (var connection = _context.CreateConnection())
        {
            var user = await connection.QueryBuilder($@"SELECT id, email, name, created_at FROM users WHERE id = {id}").QueryFirstAsync<User>();
            return user;
        }
    }


    public async Task UpdateUser(long id, User user)
    {
        using (var connection = _context.CreateConnection())
        {
            await connection.QueryBuilder($@"UPDATE users SET name = {user.Name} WHERE id = {id}").QueryAsync();
        }
    }

    public async Task DeleteUser(long id)
    {
        using (var connection = _context.CreateConnection())
        {
            await connection.QueryBuilder($@"UPDATE users SET deleted_at = NOW() WHERE id = {id}").ExecuteAsync();
        }
    }

    public async Task<UserDTO> CreateUser(UserCreateDTO user)
    {
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryBuilder($@"
            INSERT INTO users (
                name, 
                user_type_id,
                email,
                password,
                date_of_birth,
                gender,
                is_active,
                contact_number,
                sms_notification_active,
                email_notification_active,
                image
            ) VALUES 
            (   
                {user.Name},
                {user.UserTypeId},
                {user.Email},
                {user.Password},
                {user.DateOfBirth},
                {user.Gender},
                {false},
                {user.ContactNumber},
                {user.SmsNotificationActive},
                {user.EmailNotificationActive},
                {new byte[] { }}
            ) RETURNING *").QueryFirstAsync<UserDTO>();
        }
    }
}