using APBD_Cw1_s31263.Enums;
using APBD_Cw1_s31263.Exceptions;
using APBD_Cw1_s31263.Models;

namespace APBD_Cw1_s31263.Services;

public class EquipmentService : IEquipmentService
{
    private readonly List<Equipment> _equipments = [];

    public void AddEquipment(Equipment equipment)
    {
        _equipments.Add(equipment);
    }

    public Equipment GetEquipmentById(int equipmentId)
    {
        return _equipments.FirstOrDefault(equipment => equipment.Id == equipmentId)
               ?? throw new EquipmentNotFoundException(equipmentId);
    }

    public List<Equipment> GetAll()
    {
        return _equipments;
    }

    public List<Equipment> GetAvailable()
    {
        return GetAll().Where(equipment => equipment.Status == EquipmentStatus.Available).ToList();
    }

    public void SetAvailable(int equipmentId)
    {
        GetEquipmentById(equipmentId).Status = EquipmentStatus.Available;
    }

    public void SetUnavailable(int equipmentId)
    {
        GetEquipmentById(equipmentId).Status = EquipmentStatus.Unavailable;
    }
}