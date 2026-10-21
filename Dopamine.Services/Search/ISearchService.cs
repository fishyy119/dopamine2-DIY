using System;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dopamine.Services.Search
{
    public interface ISearchService : IDisposable
    {
        string SearchText { get; set; }
        event Action<string> DoSearch;
        bool IsMatch(params string[] fields);
        Task PrepareAsync(IEnumerable<string> fields);
        void ClearCache();
    }
}
