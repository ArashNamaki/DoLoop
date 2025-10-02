using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using DoLoop.Models;

namespace DoLoop.Services
{
    public static class TaskStorage
    {
        private static readonly string AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DoLoop");

        private static readonly string FilePath = Path.Combine(AppFolder, "tasks.json");

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = {new JsonStringEnumConverter()}
        };

        public static void Save(IEnumerable<TaskItem> tasks)
        {
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);

            var json = JsonSerializer.Serialize(tasks, Options);
            File.WriteAllText(FilePath, json);
        }

        public static List<TaskItem> Load()
        {
            if (!File.Exists(FilePath))
                return new List<TaskItem>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json , Options) ?? new List<TaskItem>();
        }
    }
}
