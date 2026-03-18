using APBD_Cw1_s31263.Models;

namespace APBD_Cw1_s31263.Services;

public interface IRentalService
{
    public void CreateRental(int userId, int equipmentId, DateTime from, DateTime to);
    public Rental GetRentalById(int rentalId);
    public void ReturnEquipment(int rentalId);
    public List<Rental> GetUserRentals(int userId);
    public List<Rental> GetAll();
    public List<Rental> GetOverdueRentals();
}