using Entities;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> createNewUser(User user);
        Task<User> getUserByIdAsync(int id);
        Task<User> getUserByUserNameAndPasswordAsync(string UserName, string Password);
        Task<User> updateAsync(int id, User userToUpdate);
    }
}