using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Dashboards;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Domain.Model.Objetives;
using CM.BalancedScoreboard.Domain.Model.Projects;
using CM.BalancedScoreboard.Domain.Model.Users;

namespace CM.BalancedScoreboard.Data.Repository.Implementation
{
    public class BsContext : DbContext, IDbContext
    {
        public DbSet<IndicatorType> IndicatorTypes { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<User> Users { get; set; }

        public BsContext()
        {
            this.Configuration.LazyLoadingEnabled = false ;
        }

        public virtual new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void SetModified<TEntity>(TEntity entity) where TEntity : class
        {
            this.Entry(entity).State = EntityState.Modified;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Objective>().HasMany(d => d.Indicators).WithMany(i => i.Objectives).Map(di =>
            {
                di.ToTable("Objective_Indicators");
                di.MapLeftKey("ObjectiveId");
                di.MapRightKey("IndicatorId");
            });

            modelBuilder.Entity<Dashboard>().HasMany(d => d.Indicators).WithMany(i => i.Dashboards).Map(di =>
            {
                di.ToTable("Dashboard_Indicators");
                di.MapLeftKey("DashboardId");
                di.MapRightKey("IndicatorId");
            });
        }
    }
}
