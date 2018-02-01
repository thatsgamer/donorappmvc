using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DonorAppVersion2.Models
{
    public partial class sampleEntities : DbContext
    {
        public sampleEntities()
            : base("name=sampleEntities")
        {

        }

        public virtual DbSet<AdminDetails> AdminDetails { get; set; }

        public virtual DbSet<AdminSettings> AdminSettings { get; set; }

        public virtual DbSet<ParentDonorCycleAgencies> ParentDonorCycleAgencies { get; set; }

        public virtual DbSet<ParentPayments> ParentPayments { get; set; }

        public virtual DbSet<DonorCycleEgg> DonorCycleEgg { get; set; }

        public virtual DbSet<DonorCycleUpdate> DonorCycleUpdates { get; set; }

        public virtual DbSet<Donor> Donors { get; set; }

        public virtual DbSet<MatchRequestedByParent> MatchRequestedByParents { get; set; }

        public virtual DbSet<Parent> Parents { get; set; }

        public virtual DbSet<ParentsRegisteredWithPartner> ParentsRegisteredWithPartners { get; set; }

        public virtual DbSet<Partner> Partners { get; set; }

        public virtual DbSet<PartnerAndTheirContacts> PartnerAndTheirContacts { get; set; }

        public virtual DbSet<PotentialMatches> PotentialMatches { get; set; }

        public virtual DbSet<MedicalQuestions> MedicalQuestions { get; set; }

        public virtual DbSet<MedicalReportUpdate> MedicalReportUpdate { get; set; }

        public virtual DbSet<MedicalReportsQuestions> MedicalReportsQuestions { get; set; }
    }
}