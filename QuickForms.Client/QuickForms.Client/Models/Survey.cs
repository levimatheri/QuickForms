using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.Client.Models
{
    public class Survey
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public dynamic? Content { get; set; }
    }
}
