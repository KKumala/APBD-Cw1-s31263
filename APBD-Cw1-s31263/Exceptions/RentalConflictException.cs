namespace APBD_Cw1_s31263.Exceptions;

public class RentalConflictException(int equipmentId, DateTime from, DateTime to)
    : Exception($"Equipment {equipmentId} is already reserved for the period from {from} to {to}.");