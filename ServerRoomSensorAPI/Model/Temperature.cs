namespace ServerRoomSensorAPI.Model
{
    public class Temperature
    {
        public int ID { get; set; }
        public Sensor Sensor { get; set; }
        public decimal temperature { get; set; }
        public DateTime Time { get; set; }
    }
}
