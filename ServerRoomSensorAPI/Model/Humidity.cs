using System.Reflection.Metadata.Ecma335;

namespace ServerRoomSensorAPI.Model
{
    public class Humidity
    {
        public int ID { get; set; }
        public Sensor Sensor { get; set; }
        public decimal humidity { get; set; }
        public DateTime Time { get; set; }
    }
}
