using FitABit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Core.Contracts
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseViewModel>> GetExercisesForChestDay();
        Task<IEnumerable<ExerciseViewModel>> GetExercisesForBackDay();
        Task<IEnumerable<ExerciseViewModel>> GetExercisesForLegDay();
        Task<bool> AddDetails(DetailViewModel model);

        Task<IEnumerable<DetailViewModel>> SeeResults(string exerciseId,string userId);

    }
}
