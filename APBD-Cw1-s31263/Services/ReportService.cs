using System.Text;
using APBD_Cw1_s31263.Enums;

namespace APBD_Cw1_s31263.Services;

public class ReportService(
    IEquipmentService equipmentService,
    IRentalService rentalService,
    IUserService userService) : IReportService
{
    public string GenerateSummaryReport()
    {
        var allEquipment = equipmentService.GetAll();
        var allRentals = rentalService.GetAll();

        var availableCount = equipmentService.GetAvailable().Count;
        var activeRentals = allRentals.Count(rental => rental.IsRented);
        var unavailableCount = allEquipment.Count(e => e.Status == EquipmentStatus.Unavailable);
        var overdueRentals = rentalService.GetOverdueRentals();

        var sb = new StringBuilder();
        sb.AppendLine("RAPORT STANU WYPOŻYCZALNI");
        sb.AppendLine($"Liczba zarejestrowanych użytkowników: {userService.GetAll().Count}");
        sb.AppendLine($"Zarejestrowany sprzęt ogółem: {allEquipment.Count}");
        sb.AppendLine($"  - Dostępny: {availableCount}");
        sb.AppendLine($"  - Wypożyczony: {activeRentals}");
        sb.AppendLine($"  - Niedostępny: {unavailableCount}");
        sb.AppendLine($"Nieoddany sprzęt, który jest po terminie: {overdueRentals.Count}");

        return sb.ToString();
    }
}