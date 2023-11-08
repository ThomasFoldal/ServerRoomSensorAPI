global using ServerRoomSensorAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ServerRoomSensorAPI.Data
{
    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options) : base(options)
        {

        }
        public DbSet<Alarm> Alarm { get; set; }
        public DbSet<AlarmType> AlarmType { get; set; }
        public DbSet<Humidity> Humidity { get; set; }
        public DbSet<Sensor> Sensor { get; set; }
        public DbSet<ServerRoom> ServerRoom { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Sound> Sound { get; set; }
        public DbSet<Temperature> Temperature { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = "server=localhost;user=FoldalAdmin;password=Adminmyphp;database=ServerRoomSensors";
            var serverVersion = new MySqlServerVersion(new Version(10, 11, 4));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}