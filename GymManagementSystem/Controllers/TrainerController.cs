using GymManagementSystem.DataModels;
using GymManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrainer() {
            var trainers = await trainerService.GetAllTrainersAsync();
            return Ok(trainers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainer(int id)
        {
            var trainer = await trainerService.GetTrainerByIdAsync(id);

            return Ok(trainer);
        }
        [HttpPost]
        public async Task<IActionResult> AddTrainer(TrainerModel NewTrainer)
        {
            if (NewTrainer == null) return NotFound();
            TrainerModel AddedTrainer = await trainerService.CreateTrainerAsync(NewTrainer);
            return Ok(AddedTrainer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainer(int id , TrainerModel trainer) 
        {
            var IsUpdated=await trainerService.UpdateTrainerAsync(id, trainer);
            return IsUpdated ? NoContent() : BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteTrainer(int id)
        {
            var IsDeleted=await trainerService.DeleteTrainerAsync(id);
            return IsDeleted ? NoContent() : BadRequest();
        }
    }
}
