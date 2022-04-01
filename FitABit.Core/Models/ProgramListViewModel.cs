using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Core.Models
{
    public class ProgramListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Difficulty { get; set; }

        public ICollection<ExerciseViewModel> Exercises { get; set; }

    }
}
