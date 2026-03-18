namespace APBD_Cw1_s31263.Models;

public class Employee(string firstName, string lastName) : User(firstName, lastName)
{
    public override int GetMaxActiveRentals() => 5;
}