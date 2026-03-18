namespace APBD_Cw1_s31263.Exceptions;

public class EquipmentUnavailableException(int equipmentId)
    : Exception($"Equipment with id {equipmentId} unavailable");