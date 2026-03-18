using APBD_Cw1_s31263.Core;
using APBD_Cw1_s31263.Exceptions;
using APBD_Cw1_s31263.Models;
using APBD_Cw1_s31263.Services;

var equipmentService = new EquipmentService();
var userService = new UserService();
var penaltyCalculator = new DefaultPenaltyCalculator();
var rentalService = new RentalService(equipmentService, userService, penaltyCalculator);
var reportService = new ReportService(equipmentService, rentalService, userService);

Console.WriteLine("Dodanie sprzętu");
var laptop1 = new Laptop("MacBook Pro", 1.6, 512, 16);
var laptop2 = new Laptop("ThinkPad T14", 1.4, 256, 8);
var camera = new Camera("Sony A7 III", 0.6, 24, true);
var projector = new Projector("Epson 4K", 4.2, true, 3000);

equipmentService.AddEquipment(laptop1);
equipmentService.AddEquipment(laptop2);
equipmentService.AddEquipment(camera);
equipmentService.AddEquipment(projector);
Console.WriteLine("Dodano 4 urządzenia.\n");

Console.WriteLine("Dodanie użytkowników");
var student = new Student("Jan", "Kowalski");
var employee = new Employee("Dr Anna", "Nowak");

userService.AddUser(student);
userService.AddUser(employee);
Console.WriteLine("Dodano 2 użytkowników.\n");

Console.WriteLine("Wypożyczenie sprzętu");
rentalService.CreateRental(student.Id, laptop1.Id, DateTime.Now, DateTime.Now.AddDays(3));
Console.WriteLine($"Student '{student.FirstName}' wypożyczył '{laptop1.Name}'.\n");

try
{
    Console.Write($"Próba wypożyczenia niedostępnego '{laptop1.Name}': ");
    rentalService.CreateRental(employee.Id, laptop1.Id, DateTime.Now, DateTime.Now.AddDays(1));
}
catch (EquipmentUnavailableException ex)
{
    Console.WriteLine($"ERROR ({ex.Message})");
}

try
{
    Console.Write($"Próba przekroczenia limitu wypożyczeń dla Studenta: ");
    rentalService.CreateRental(student.Id, laptop2.Id, DateTime.Now, DateTime.Now.AddDays(2)); // To przejdzie (2/2)
    rentalService.CreateRental(student.Id, camera.Id, DateTime.Now,
        DateTime.Now.AddDays(2));
}
catch (TooManyRentalsException ex)
{
    Console.WriteLine($"ERROR ({ex.Message})");
}

Console.WriteLine();

Console.WriteLine("Terminowy zwrot sprzętu");
var studentRentals = rentalService.GetUserRentals(student.Id);
var activeLaptop1Rental = studentRentals.First(r => r.Equipment.Id == laptop1.Id && r.IsRented);

rentalService.ReturnEquipment(activeLaptop1Rental.Id);
Console.WriteLine($"Zwrócono '{laptop1.Name}' w terminie. Naliczona kara: {activeLaptop1Rental.AppliedPenalty} PLN.\n");

Console.WriteLine("Nieterminowy zwrot sprzętu");
var pastStartDate = DateTime.Now.AddDays(-10);
var pastEndDate = DateTime.Now.AddDays(-3);

rentalService.CreateRental(employee.Id, projector.Id, pastStartDate, pastEndDate);
var employeeRentals = rentalService.GetUserRentals(employee.Id);
var overdueProjectorRental = employeeRentals.First(r => r.Equipment.Id == projector.Id && r.IsRented);

rentalService.ReturnEquipment(overdueProjectorRental.Id);
Console.WriteLine(
    $"Zwrócono '{projector.Name}' po terminie. Naliczona kara (3 dni opóźnienia * stawka): {overdueProjectorRental.AppliedPenalty} PLN.\n");

Console.WriteLine("Generowanie raportu końcowego");
string report = reportService.GenerateSummaryReport();
Console.WriteLine(report);