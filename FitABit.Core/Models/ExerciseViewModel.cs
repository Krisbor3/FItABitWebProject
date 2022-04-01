using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Core.Models
{
    public class ExerciseViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int? RestTime { get; set; }

        public ICollection<DetailViewModel> Details {get; set;}
    }
}
