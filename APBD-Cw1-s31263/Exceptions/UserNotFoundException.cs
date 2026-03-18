namespace APBD_Cw1_s31263.Exceptions;

public class UserNotFoundException(int userId)
    : Exception($"User with id {userId} not found");