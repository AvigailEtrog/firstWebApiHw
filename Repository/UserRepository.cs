using Entities;
using System.Text.Json;
using System;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        //rename the variable to filePath etc. (clean code) 
        string url = "../myUsers.txt";
        public async Task<User> getUserByUserNameAndPassword(string UserName, string Password)
        {
            using (StreamReader reader = System.IO.File.OpenText(url))
            {
                string? currentUserInFile;
                while ((currentUserInFile = await reader.ReadLineAsync()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.UserName == UserName && user.Password == Password)
                        return user;
                }

            }
            return null;
        }





        public User createNewUser(User user)
        {
            int numberOfUsers = System.IO.File.ReadLines(url).Count();
            //change to UserId !
            user.userId = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText(url, userJson + Environment.NewLine);
            return user;

        }

        public async Task update(int id, User userToUpdate)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(url))
            {
                string currentUserInFile;
                while ((currentUserInFile = await reader.ReadLineAsync()) != null)
                {

                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.userId == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(url);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                System.IO.File.WriteAllText(url, text);
            }
        }

        public async Task<string> getUserById(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText(url))
            {
                string? currentUserInFile;
                while ((currentUserInFile = await reader.ReadLineAsync()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.userId == id)
                        return user.ToString();
                }
            }
            return null;


        }


    }
}