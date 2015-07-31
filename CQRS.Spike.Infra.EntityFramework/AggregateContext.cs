using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CQRS.Spike.Infra.EntityFramework
{
  public class AggregateContext : DbContext
  {
    public AggregateContext()
      :this("EventStore")
    {
      
    }

    public AggregateContext(string nameOrConnectionString)
      :base(nameOrConnectionString)
    {
      
    }

    public DbSet<Aggregate> Aggregates { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Configurations.Add(new AggregateMap());
      modelBuilder.Configurations.Add(new EventMap());
    }

    private class EventMap : EntityTypeConfiguration<Event>
    {
      public EventMap()
      {
        HasKey(e => new {e.Id, e.Version});
        Property(e => e.Data).IsRequired();
      }
    }

    private class AggregateMap : EntityTypeConfiguration<Aggregate>
    {
      public AggregateMap()
      {
        HasKey(a => a.Id);
        Property(a => a.Version).IsConcurrencyToken().IsRequired();
        HasMany(a => a.Events);
      }
    }
  }
}