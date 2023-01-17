namespace JobSeekerApi.Repositories;
using JobSeekerApi.Contracts;
using JobSeekerApi.Contexts;
using JobSeekerApi.Models;
using Dapper;
public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;
    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var query = "SELECT * FROM users";
        using (var connection = _context.CreateConnection())
        {
            var users = await connection.QueryAsync<User>(query);
            return users.ToList();
        }
    }

    public async Task<User> GetUser(long id)
    {
        var parameters = new { ID = id };
        var query = "SELECT id,email FROM users WHERE id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var user = await connection.QueryFirstAsync<User>(query, parameters);
            return user;
        }
    }


    public async Task UpdateUser(long id, User user)
    {
        var parameters = new { ID = id };
        var query = "UPDATE users SET name = @Name WHERE id = @ID";
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { Name = user.Name, ID = id });
        }
    }

    public async Task DeleteUser(long id)
    {
        var parameters = new { ID = id };
        var query = "UPDATE users SET deleted_at = @DeletedAt WHERE id = @ID";
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { DeletedAt = "NOW()", ID = id });
        }
    }

    public async Task<UserDTO> CreateUser(UserCreateDTO user)
    {
        var query = @"INSERT INTO users (
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
            @Name,
            @UserTypeId,
            @Email,
            @Password,
            @DateOfBirth,
            @Gender,
            @IsActive,
            @ContactNumber,
            @SmsNotificationActive,
            @EmailNotificationActive,
            @Image
        ) RETURNING *";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryFirstAsync<UserDTO>(query, new
            {
                Name = user.Name,
                UserTypeId = user.UserTypeId,
                Email = user.Email,
                Password = user.Password,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                IsActive = false,
                ContactNumber = user.ContactNumber,
                SmsNotificationActive = user.SmsNotificationActive,
                EmailNotificationActive = user.EmailNotificationActive,
                Image = new byte[]{},
            });
        }
    }
}