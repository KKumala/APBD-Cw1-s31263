using APBD_Cw1_s31263.Exceptions;
using APBD_Cw1_s31263.Models;

namespace APBD_Cw1_s31263.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = [];

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User GetUserById(int userId)
    {
        return _users.FirstOrDefault(user => user.Id == userId)
               ?? throw new UserNotFoundException(userId);
    }

    public List<User> GetAll()
    {
        return _users;
    }
}