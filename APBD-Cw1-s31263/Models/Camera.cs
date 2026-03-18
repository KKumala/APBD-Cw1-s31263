namespace APBD_Cw1_s31263.Models;

public class Camera(string name, double weight, int megapixels, bool hasOpticalZoom) : Equipment(name, weight)
{
    public int Megapixels { get; set; } = megapixels;
    public bool HasOpticalZoom { get; set; } = hasOpticalZoom;
}