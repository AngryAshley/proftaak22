using Microsoft.EntityFrameworkCore;

namespace RailViewApi.Models
{
    public class TrainContext : DbContext
    {
        public TrainContext()
        {
        }

        public TrainContext(DbContextOptions<TrainContext> options): base(options)
        {
        }

        public virtual DbSet<Train> Trains { get; set; } = null!;
    }

    public class Links
    {
    }

    public class Treinen
    {
        public int TreinNummer { get; set; }
        public string? RitId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Snelheid { get; set; }
        public double Richting { get; set; }
        public double HorizontaleNauwkeurigheid { get; set; }
        public string? Type { get; set; }
        public string? Bron { get; set; }
    }

    public class Payload
    {
        public List<Treinen>? Treinen { get; set; }
    }

    public class Meta
    {
    }

    public class Train
    {
        public Links? Links { get; set; }
        public Payload? Payload { get; set; }
        public Meta? Meta { get; set; }
    }
}
