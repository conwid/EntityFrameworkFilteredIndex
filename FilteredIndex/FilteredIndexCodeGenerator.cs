using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilteredIndex
{
    public class FilteredIndexCodeGenerator : CSharpMigrationCodeGenerator
    {        
        protected override void Generate(CreateTableOperation createTableOperation, IndentedTextWriter writer)
        {
            base.Generate(createTableOperation, writer);
        }
        protected override void Generate(ColumnModel column, IndentedTextWriter writer, bool emitName = false)
        {
            if (column.Annotations.ContainsKey("IndexExtension"))
            {
                var annotation = ((IndexAnnotation)column.Annotations["IndexExtension"].NewValue);
                var attribute = (FilteredIndexAttribute)annotation.Indexes.First();
                var where = attribute.Where;
                var newIndex = new CreateIndexOperation() { IsUnique = attribute.IsUnique, Name = attribute.Name, IsClustered = attribute.IsClustered };
                newIndex.AnonymousArguments["Where"] = where;
                Generate(newIndex);
            }
            else
            {
                base.Generate(column, writer, emitName);
            }
        }        
    }
}
