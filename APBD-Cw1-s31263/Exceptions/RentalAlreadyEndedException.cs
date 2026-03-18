namespace APBD_Cw1_s31263.Exceptions;

public class RentalAlreadyEndedException(int rentalId)
    : Exception($"Rental {rentalId} already ended");