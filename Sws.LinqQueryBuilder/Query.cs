using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sws.LinqQueryBuilder
{
    public class Query
    {
        public string Condition { get; set; }
        public Rule[] Rules { get; set; }
        public bool Valid { get; set; }
    }

    public class Rule
    {
        public string Id { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string Input { get; set; }
        public string Operator { get; set; }
        public float Value { get; set; }
        public string Condition { get; set; }
        public Rule[] Rules { get; set; }
    }
}
