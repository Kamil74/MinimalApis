namespace Minimal.Api;

public class MapPoint
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public static bool TryParse(string? value, out MapPoint? point)
    {
        try
        {
            var splitValue = value.Split(',').Select(double.Parse).ToArray();
            point = new MapPoint
            {
                Latitude = splitValue[0],
                Longitude = splitValue[1]
            };
            return true;
        

        }
        catch (Exception)
        {
            point = null;
            return false;
        
            
        }
    }
}