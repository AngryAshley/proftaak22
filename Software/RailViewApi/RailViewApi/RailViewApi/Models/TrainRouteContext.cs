using Microsoft.EntityFrameworkCore;

namespace RailViewApi.Models
{
    public class TrainRouteContext : DbContext
    {
        public TrainRouteContext()
        {
        }

        public TrainRouteContext(DbContextOptions<TrainRouteContext> options) : base(options)
        {
        }

        public virtual DbSet<TrainRoute> Routes { get; set; } = null!;
    }

    public class LinksRoute
    {
    }

    public class Properties
    {
        public string? From { get; set; }

        public string? To { get; set; }
    }

    public class Geometry
    {
        public string? Type { get; set; }

        public List<List<double>>? Coordinates { get; set; }
    }

    public class Feature
    {
        public string? Type { get; set; }

        public Properties? Properties { get; set; }

        public Geometry? Geometry { get; set; }
    }

    public class PayloadRoute
    {
        public string? Type { get; set; }

        public List<Feature>? Features { get; set; }
    }

    public class MetaRoute
    {
    }

    public class TrainRoute
    {
        public LinksRoute? Links { get; set; }

        public PayloadRoute? Payload { get; set; }

        public MetaRoute? Meta { get; set; }
    }
}
