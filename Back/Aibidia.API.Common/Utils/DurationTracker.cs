using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aibidia.API.Common.Utils
{
    public class DurationTracker
    {
        private Dictionary<string, TrackedTask> _tasks = new Dictionary<string, TrackedTask>();

        public void StartTracking(string taskName)
        {
            _tasks.Add(taskName, new TrackedTask());
        }

        public TimeSpan StopTracking(string taskName)
        {
            var task = _tasks[taskName];
            if (!task.EndTime.HasValue)
            {
                task.EndTime = DateTime.Now;
            }

            return task.EndTime.Value - task.StartTime;
        }

        public DateTimeOffset GetStartTime(string taskName)
        {
            return new DateTimeOffset(_tasks[taskName].StartTime);
        }

        public TimeSpan GetDuration(string taskName)
        {
            var task = _tasks[taskName];
            if (task.EndTime.HasValue)
            {
                return task.EndTime.Value - task.StartTime;
            }

            return DateTime.Now - task.StartTime;
        }

        private class TrackedTask
        {
            public TrackedTask()
            {
                StartTime = DateTime.Now;
                EndTime = null;
            }
            public TrackedTask(DateTime startTime)
            {
                StartTime = startTime;
                EndTime = null;
            }
            public DateTime StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }
    }
}