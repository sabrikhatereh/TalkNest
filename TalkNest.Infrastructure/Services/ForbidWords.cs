using Microsoft.Extensions.Options;
using TalkNest.Core.Abstractions.Services;
using TalkNest.Core.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TalkNest.Infrastructure.Services
{
    public class ForbidWords : IForbidWords
    {
        private readonly ValidationSettings settings;
        private Task<List<string>>? _cachedForbidWords; // Cached task to store the forbidden words
        private readonly object _lock = new(); // Lock for thread safety

        public ForbidWords(IOptions<ValidationSettings> settings)
        {
            this.settings = settings.Value;
        }
        // Loads the forbidden words from the settings also gives us the flexibility to load it from a database or any other source.
        public async Task<List<string>> LoadForbidWords()
        {
            if (_cachedForbidWords == null)
            {
                lock (_lock)
                {
                    if (_cachedForbidWords == null)
                    {
                        _cachedForbidWords = LoadFromSourceAsync();
                    }
                }
            }

            return await _cachedForbidWords;
        }

        private async Task<List<string>> LoadFromSourceAsync()
        {
            return settings.InvalidWords ?? new List<string>();
        }

    }
}
