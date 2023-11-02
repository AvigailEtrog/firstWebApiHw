using Entities;

namespace Repository
{
    //Rename folder name to Repositories
    public interface IUserRepository
    {
        User createNewUser(User user);
        // getUserById should return User.  
        Task<string> getUserById(int id);
        Task<User> getUserByUserNameAndPassword(string UserName, string Password);
        // Update should return User.  
        Task update(int id, User userToUpdate);
    }
}