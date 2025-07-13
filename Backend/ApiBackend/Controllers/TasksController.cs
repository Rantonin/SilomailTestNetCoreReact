using ApiBackend.Dal;
using ApiBackend.DTO;
using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = ApiBackend.Models.Task;
using TaskStatus = ApiBackend.Models.TaskStatus;

namespace ApiBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TasksController(AppDbContext db) => _db = db;

        // 2. GET all tasks
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tasks = await _db.Tasks.Include(t => t.AssignedToUser).ToListAsync();
            return Ok(tasks);
        }

        // 3. GET one task with history
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var task = await _db.Tasks.Include(t => t.AssignedToUser)
                                      //.Include(t => t.TaskHistories)
                                      //.ThenInclude(h => h.ChangedByUser)
                                      .FirstOrDefaultAsync(t => t.Id == id);
            return task != null ? Ok(task) : NotFound();
        }

        // 4. POST create task
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var task = new Task
            {
                Title = dto.Title,
                Description = dto.Description,
                AssignedToUserId = dto.AssignedToUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();

            var hist = new TaskHistory
            {
                TaskId = task.Id,
                ChangedByUserId = dto.CreatedByUserId,
                ChangeType = "Creation",
                OldValue = null,
                NewValue = task.Status.ToString(),
                ChangeDate = DateTime.UtcNow
            };
            _db.TaskHistories.Add(hist);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOne), new { id = task.Id }, task);
        }

        // 5. PUT status update
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateTaskStatusDto dto)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            // Validate transitions
            if (task.Status == TaskStatus.ToDo && dto.NewStatus == TaskStatus.Done)
                return BadRequest("Transition ToDo -> Done invalide");

            var old = task.Status;
            task.Status = dto.NewStatus;
            task.UpdatedAt = DateTime.UtcNow;

            //_db.TaskHistories.Add(new TaskHistory
            //{
            //    TaskId = id,
            //    ChangedByUserId = dto.ChangedByUserId,
            //    ChangeType = "StatusChange",
            //    OldValue = old.ToString(),
            //    NewValue = dto.NewStatus.ToString(),
            //    ChangeDate = DateTime.UtcNow
            //});
            await _db.SaveChangesAsync();

            return Ok(task);
        }

        // 6. PUT assign
        [HttpPut("{id}/assign")]
        public async Task<IActionResult> Assign(int id, AssignTaskDto dto)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            var old = task.AssignedToUserId?.ToString();
            task.AssignedToUserId = dto.AssignedToUserId;
            task.UpdatedAt = DateTime.UtcNow;

            _db.TaskHistories.Add(new TaskHistory
            {
                TaskId = id,
                ChangedByUserId = dto.ChangedByUserId,
                ChangeType = "AssignmentChange",
                OldValue = old,
                NewValue = dto.AssignedToUserId?.ToString(),
                ChangeDate = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();

            return Ok(task);
        }
    }
}
