using APBD_Cw1_s31263.Enums;

namespace APBD_Cw1_s31263.Models;

public abstract class Equipment(string name, double weight)
{
    private static int _nextId = 0;

    public int Id { get; } = _nextId++;
    public string Name { get; set; } = name;
    public double Weight { get; set; } = weight;
    public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;
}