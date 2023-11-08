namespace ServerRoomSensorAPI.Model
{
    public class Alarm
    {
        public int ID { get; set; }
        public AlarmType AlarmType { get; set; }
        public Sensor Sensor { get; set; }
        public DateTime Time { get; set; }
    }
}
