namespace APBD_Cw1_s31263.Core;

public class DefaultPenaltyCalculator : IPenaltyCalculator
{
    private const decimal DailyPenaltyRate = 10.0m;
    
    public decimal CalculatePenalty(DateTime endDate, DateTime returnDate)
    {
        if (returnDate <= endDate)
        {
            return 0m;
        }
        
        var overdueDays = (returnDate - endDate).Days;
        
        return DailyPenaltyRate * overdueDays;
    }
}