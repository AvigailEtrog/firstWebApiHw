using Entities;

namespace Service
{
    //Rename folder name to Services
    public interface IUserService
    {
        int checkPassword(string password);
        User createNewUser(User user);
        Task<string> getUserById(int id);
        Task<User> getUserByUserNameAndPassword(string UserName, string Password);
        Task update(int id, User userToUpdate);
    }
}