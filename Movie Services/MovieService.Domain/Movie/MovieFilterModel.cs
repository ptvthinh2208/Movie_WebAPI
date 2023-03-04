using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Domain.Movie
{
    public class MovieFilterModel
    {
        public string Actor { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        private int alwaysShow { get; set; }
        public int AlwaysShow   // property
        {
            get { return alwaysShow; }   // get method
            set { alwaysShow = value; }  // set method
        }
    }
}
