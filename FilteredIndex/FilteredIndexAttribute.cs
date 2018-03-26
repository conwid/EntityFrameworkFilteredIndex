using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilteredIndex
{
    public sealed class FilteredIndexAttribute : IndexAttribute
    {
        public FilteredIndexAttribute(string name) : base(name) { }
        public string Where { get; set; }
    }
}
