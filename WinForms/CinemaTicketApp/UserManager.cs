using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using BCrypt.Net;

public class UserManager
{
    private const string UsersFile = "users.json";
    private List<User> users = new List<User>();

    public UserManager()
    {
        LoadUsers();
    }

    private void LoadUsers()
    {
        if (!File.Exists(UsersFile))
        {
            // domyślne dane do logowania admina
            users = new List<User>
            {
                new User("admin", BCrypt.Net.BCrypt.HashPassword("admin123"), "Admin")
            };
            SaveUsers();
        }
        else
        {
            string json = File.ReadAllText(UsersFile);
            users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }
    }

    private void SaveUsers()
    {
        string json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(UsersFile, json);
    }

    public bool RegisterUser(string username, string password, string role)
    {
        if (users.Any(u => u.Username == username))
        {
            return false;
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        users.Add(new User(username, hashedPassword, role));

        SaveUsers();
        return true;
    }

    public User? AuthenticateUser(string username, string password)
    {
        User? user = users.FirstOrDefault(u => u.Username == username);
        if (user == null) return null;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
    }

    public bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        User? user = users.FirstOrDefault(u => u.Username == username);
        if (user == null) return false;
    
        if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash)) return false;
    
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        SaveUsers();
        return true;
    }
    

    public bool DeleteUser(string username, string password)
    {
        User? user = AuthenticateUser(username, password);
        if (user == null) return false;

        users.Remove(user);
        SaveUsers();
        return true;
    }
}

public class User
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public User(string username, string passwordHash, string role)
    {
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
    }
}
