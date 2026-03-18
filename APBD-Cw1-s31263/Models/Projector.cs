namespace APBD_Cw1_s31263.Models;

public class Projector(string name, double weight, bool hasBuiltInSpeakers, int brightnessLumens)
    : Equipment(name, weight)
{
    public bool HasBuiltInSpeakers { get; set; } = hasBuiltInSpeakers;
    public int BrightnessLumens { get; set; } = brightnessLumens;
}