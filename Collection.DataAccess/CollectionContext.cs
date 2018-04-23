using Collection.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection.DataAccess
{
    public partial class CollectionContext : DbContext
    {
        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Piece> Pieces { get; set; }

        public CollectionContext()
        {
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new CollectionDBInitializer());
        }
    }
}
