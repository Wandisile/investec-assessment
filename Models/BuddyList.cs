using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestecConsoleApplication.Models
{
    class BuddyList
    {
        public Guid group { get; set; }
        public List<string> Buddies { get; set; }
    }
}
