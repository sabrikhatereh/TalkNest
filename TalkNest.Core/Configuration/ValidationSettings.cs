using System.Collections.Generic;

namespace TalkNest.Core.Configuration
{
    public class ValidationSettings
    {
        public List<string> InvalidWords { get; set; } = new();
    }
}
