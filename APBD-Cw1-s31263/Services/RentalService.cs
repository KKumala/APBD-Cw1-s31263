using APBD_Cw1_s31263.Core;
using APBD_Cw1_s31263.Enums;
using APBD_Cw1_s31263.Exceptions;
using APBD_Cw1_s31263.Models;

namespace APBD_Cw1_s31263.Services;

public class RentalService(
    IEquipmentService equipmentService,
    IUserService userService,
    IPenaltyCalculator penaltyCalculator) : IRentalService
{
    private readonly List<Rental> _rentals = [];

    public void CreateRental(int userId, int equipmentId, DateTime from, DateTime to)
    {
        var equipment = equipmentService.GetEquipmentById(equipmentId);
        var user = userService.GetUserById(userId);

        if (equipment.Status != EquipmentStatus.Available)
        {
            throw new EquipmentUnavailableException(equipment.Id);
        }

        var activeUserRentals = GetAll().Count(rental =>
            rental.IsRented
            && rental.User == user);

        if (activeUserRentals >= user.GetMaxActiveRentals())
        {
            throw new TooManyRentalsException(activeUserRentals);
        }

        var rentalConflict = _rentals.Any(rental =>
            rental.IsRented
            && rental.Equipment == equipment
            && rental.Overlaps(from, to));

        if (rentalConflict)
        {
            throw new RentalConflictException(equipment.Id, from, to);
        }

        var newRental = new Rental(user, equipment, from, to);
        equipment.Status = EquipmentStatus.Unavailable;
        _rentals.Add(newRental);
    }

    public Rental GetRentalById(int rentalId)
    {
        return _rentals.FirstOrDefault(rental => rental.Id == rentalId)
               ?? throw new RentalNotFoundException(rentalId);
    }

    public void ReturnEquipment(int rentalId)
    {
        var rental = GetRentalById(rentalId);

        if (rental is null)
        {
            throw new RentalNotFoundException(rentalId);
        }

        if (!rental.IsRented)
        {
            throw new RentalAlreadyEndedException(rentalId);
        }

        var returnDate = DateTime.Now;
        var penalty = penaltyCalculator.CalculatePenalty(rental.To, returnDate);

        rental.MarkAsReturned(returnDate, penalty);
        equipmentService.SetAvailable(rental.Equipment.Id);
    }

    public List<Rental> GetUserRentals(int userId)
    {
        var user = userService.GetUserById(userId);
        return GetAll().Where(rental => rental.User.Id == user.Id).ToList();
    }

    public List<Rental> GetAll()
    {
        return _rentals;
    }

    public List<Rental> GetOverdueRentals()
    {
        return GetAll().Where(rental => rental.IsOverdue).ToList();
    }
}