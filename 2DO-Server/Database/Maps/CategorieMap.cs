using _2DO_Server.Database.Data;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DO_Server.Database.Maps
{
    public class CategorieMap : ClassMap<Categorie>
    {
        public CategorieMap()
        {
            Table("Categories");

            Id(x => x.ID).GeneratedBy.Native();
            Map(x => x.Version).Not.Nullable();
            Map(x => x.Name).Length(50).Not.Nullable();
        }
    }
}
