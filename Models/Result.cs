using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestecConsoleApplication.Models
{
    class Result
    {
        public List<PeopleFilms> results { get; set; }
        public string next { get; set; }
        public int count { get; set; }
    }
}
