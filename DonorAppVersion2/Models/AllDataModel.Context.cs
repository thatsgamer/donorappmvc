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


        public virtual DbSet<DonorCycle> DonorCycles { get; set; }

        public virtual DbSet<DonorCycleUpdate> DonorCycleUpdates { get; set; }

        public virtual DbSet<Donor> Donors { get; set; }

        public virtual DbSet<MatchRequestedByParent> MatchRequestedByParents { get; set; }

        public virtual DbSet<Parent> Parents { get; set; }

        public virtual DbSet<ParentsRegisteredWithPartner> ParentsRegisteredWithPartners { get; set; }

        public virtual DbSet<Partner> Partners { get; set; }

        public virtual DbSet<PartnerAndTheirContacts> PartnerAndTheirContacts { get; set; }
    }
}