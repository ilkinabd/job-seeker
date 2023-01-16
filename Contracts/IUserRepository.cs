namespace JobSeekerApi.Contracts;

using JobSeekerApi.Models;
public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsers();

    public Task<User> GetUser(long id);

    public Task UpdateUser(long id, User user);

    public Task DeleteUser(long id);

    public Task<UserDTO> CreateUser(UserCreateDTO user);
}