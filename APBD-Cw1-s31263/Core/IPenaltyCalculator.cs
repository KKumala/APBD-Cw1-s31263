namespace APBD_Cw1_s31263.Core;

public interface IPenaltyCalculator
{
    decimal CalculatePenalty(DateTime endDate, DateTime returnDate);
}