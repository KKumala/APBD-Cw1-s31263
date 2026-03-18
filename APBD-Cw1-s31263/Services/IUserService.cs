using APBD_Cw1_s31263.Models;

namespace APBD_Cw1_s31263.Services;

public interface IUserService
{
    public void AddUser(User user);
    public User GetUserById(int userId);
    public List<User> GetAll();
}