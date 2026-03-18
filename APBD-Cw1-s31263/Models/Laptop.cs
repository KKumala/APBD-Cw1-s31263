namespace APBD_Cw1_s31263.Models;

public class Laptop(string name, double weight, int memoryCapacity, int memoryGb) : Equipment(name, weight)
{
    public int MemoryCapacity { get; set; } = memoryCapacity;
    public int MemoryGb { get; set; } = memoryGb;
}