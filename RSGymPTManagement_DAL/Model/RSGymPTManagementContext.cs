using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPTManagement_DAL.Model
{
    public class RSGymPTManagementContext : DbContext
    {
        #region Constructor()

        public RSGymPTManagementContext() : base("name = RSGymDB")
        {

        }

        #endregion

        #region Criação da bd

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
        #endregion

        #region Tabels in memory

        public DbSet<Client> Clients { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<PersonalTrainer> PersonalTrainers { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion
    }
}
