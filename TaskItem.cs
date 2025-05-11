using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskKeeper
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public override string ToString()
        {
            var due = DueDate.HasValue
                  ? $" (до {DueDate:yyyy-MM-dd})"
                  : string.Empty;
            return (IsCompleted ? "[Выполненна] " : "[В работе] ") + Title + due;
        }
    }
}
