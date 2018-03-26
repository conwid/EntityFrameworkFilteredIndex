using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilteredIndex
{
    internal class FilteredIndexSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(CreateIndexOperation createIndexOperation)
        {
            if (createIndexOperation.AnonymousArguments.ContainsKey("Where"))
            {
                using (var writer = Writer())
                {
                    writer.Write("CREATE ");

                    if (createIndexOperation.IsUnique)
                    {
                        writer.Write("UNIQUE ");
                    }

                    if (createIndexOperation.IsClustered)
                    {
                        writer.Write("CLUSTERED ");
                    }

                    writer.Write("INDEX ");
                    writer.Write(Quote(createIndexOperation.Name));
                    writer.Write(" ON ");
                    writer.Write(Name(createIndexOperation.Table));
                    writer.Write("(");
                    writer.Write(createIndexOperation.Columns.Join(Quote));
                    writer.Write(")");
                    writer.Write(" WHERE " + createIndexOperation.AnonymousArguments["Where"]);
                    Statement(writer);
                }
            }
            else
            {
                base.Generate(createIndexOperation);
            }
        }
    }
}
