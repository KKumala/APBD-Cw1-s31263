namespace APBD_Cw1_s31263.Exceptions;

public class RentalNotFoundException(int rentalId)
    : Exception($"Rental with id {rentalId} not found");