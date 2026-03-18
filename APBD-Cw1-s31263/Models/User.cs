namespace APBD_Cw1_s31263.Models;

public abstract class User(string firstName, string lastName)
{
    private static int _nextId = 0;

    public int Id { get; } = _nextId++;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;

    public abstract int GetMaxActiveRentals();
}