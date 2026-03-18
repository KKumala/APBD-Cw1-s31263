namespace APBD_Cw1_s31263.Models;

public class Rental(User user, Equipment equipment, DateTime from, DateTime to)
{
    private static int _nextId = 0;

    public int Id { get; set; } = _nextId++;
    public User User { get; set; } = user;
    public Equipment Equipment { get; set; } = equipment;
    public DateTime From { get; set; } = from >= to ? throw new ArgumentException("Invalid time range") : from;
    public DateTime To { get; set; } = to;
    public DateTime? ReturnDate { get; private set; }
    public decimal? AppliedPenalty { get; private set; }

    public bool IsRented => ReturnDate == null;
    public bool IsOverdue => IsRented && DateTime.Now > To;
    public bool Overlaps(DateTime from, DateTime to) => !(From > to || from > To);

    public void MarkAsReturned(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        AppliedPenalty = penalty;
    }
}