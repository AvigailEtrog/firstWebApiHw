

using Entities;
using Repository;

namespace Service
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User createNewUser(User user)
        {
            User newUser = _userRepository.createNewUser(user);
            //You can return newUser directly, and if it's null, the method will return null by default.
            if (newUser != null) return newUser;
            //Unnecessary else
            else return null;
        }
        public async Task<User> getUserByUserNameAndPassword(string UserName, string Password)
        {

            User newUser = await _userRepository.getUserByUserNameAndPassword(UserName, Password);
            //You can return newUser directly, and if it's null, the method will return null by default.
            if (newUser != null) return newUser;
            //Unnecessary else
            else return null;
        }

        public async Task update(int id, User userToUpdate)
        {

            await _userRepository.update(id, userToUpdate);
        }


        public int checkPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }

        public async Task<string> getUserById(int id)
        {
            return await _userRepository.getUserById(id);
        }
    }
}
