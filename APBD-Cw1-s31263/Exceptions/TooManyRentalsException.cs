namespace APBD_Cw1_s31263.Exceptions;

public class TooManyRentalsException(int userId)
    : Exception($"There is too many active rentals for user {userId}.");