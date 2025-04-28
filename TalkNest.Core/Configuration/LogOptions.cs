using System.IO;

namespace TalkNest.Core.Configuration
{
    public class LogOptions
    {
        public string AppName { get; set; } = "TalkNest";
        public string Level { get; set; }
        public FileOptions File { get; set; }
        public string LogTemplate { get; set; }
    }
}
