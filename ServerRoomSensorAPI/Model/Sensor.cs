namespace ServerRoomSensorAPI.Model
{
    public class Sensor
    {
        public int ID { get; set; }
        public ServerRoom Room { get; set; }
        public string Designation { get; set; }
    }
}
