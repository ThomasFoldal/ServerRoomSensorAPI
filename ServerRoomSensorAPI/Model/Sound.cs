namespace ServerRoomSensorAPI.Model
{
    public class Sound
    {
        public int ID { get; set; }
        public Sensor Sensor { get; set; }
        public decimal SoundLevel { get; set; }
        public DateTime Time { get; set; }
    }
}
