using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Text.Json;

namespace TaskKeeper
{
    public class TaskManager
    {
        private const string FileName = "tasks.json";
        private readonly List<TaskItem> _tasks;
        public TaskManager()
        {
            _tasks = Load();
        }
        public IReadOnlyList<TaskItem> Tasks => _tasks;

        public void Add(string title)
        {
            _tasks.Add(new TaskItem { Title = title });
            Save();
        }
        public void Remove(Guid id)
        {
            _tasks.RemoveAll(t => t.Id == id);
            Save();
        }
        public void Toggle(Guid id)
        {
            var t = _tasks.FirstOrDefault(x => x.Id == id);
            if (t != null)
            {
                t.IsCompleted = !t.IsCompleted;
                Save();
            }
        }
        private List<TaskItem> Load()
        {
            if (!File.Exists(FileName))
                return new List<TaskItem>();

            try
            {
                var json = File.ReadAllText(FileName);
                return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch
            {
                return new List<TaskItem>();
            }
        }
        private void Save()
        {
            var opts = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FileName, JsonSerializer.Serialize(_tasks, opts));
        }
    }
}

