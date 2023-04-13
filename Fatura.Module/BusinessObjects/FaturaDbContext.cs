using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EF.DesignTime;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.XtraSpellChecker.Parser;

namespace Fatura.Module.BusinessObjects
{
    //public class FaturaContextInitializer : DbContextTypesInfoInitializerBase {
    //	protected override DbContext CreateDbContext() {
    //		DbContextInfo contextInfo = new DbContextInfo(typeof(FaturaDbContext), new DbProviderInfo(providerInvariantName: "System.Data.SqlClient", providerManifestToken: "2008"));
    //           return contextInfo.CreateInstance();
    //	}
    //}
    //[TypesInfoInitializer(typeof(FaturaContextInitializer))]
    public class FaturaDbContext : DbContext
    {
        public FaturaDbContext()
            : base("FaturaDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FaturaDbContext, Migrations.Configuration>());
        }

        public FaturaDbContext(String connectionString)
            : base(connectionString)
        {
        }
        public FaturaDbContext(DbConnection connection)
            : base(connection, false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<FaturaDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<ModuleInfo> ModulesInfo { get; set; }
        public DbSet<PermissionPolicyRole> Roles { get; set; }
        public DbSet<PermissionPolicyTypePermissionObject> TypePermissionObjects { get; set; }
        public DbSet<PermissionPolicyUser> Users { get; set; }
        public DbSet<FileData> FileData { get; set; }
        public DbSet<DashboardData> DashboardData { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
        public DbSet<ReportDataV2> ReportDataV2 { get; set; }
        public DbSet<ModelDifference> ModelDifferences { get; set; }
        public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }


        public DbSet<Country> Countrys { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City>Citys { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Lading> Ladings { get; set; }
        public DbSet<IncomeExpense> IncomeExpenses { get;set; }

        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<StudentLesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson>LessonNames { get; set; }
    }
}