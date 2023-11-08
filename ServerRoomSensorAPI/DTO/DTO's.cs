namespace ServerRoomSensorAPI.DTO
{
    public record struct TemperatureDTO(int SensorID, decimal Temperature, DateTime Time);

    public record struct SensorDTO(int RoomID, string Designation);
    public record struct ServerRoomDTO(string location, string region);
    public record struct HumidityDTO(int SensorID, decimal Humidity, DateTime Time);
    //public class TemperatureDTO
    //{
    //    public int ID { get; set; }
    //    public int SensorID { get; set; }
    //    public decimal temperature { get; set; }
    //    public DateTime Time { get; set; }
    //}

}
