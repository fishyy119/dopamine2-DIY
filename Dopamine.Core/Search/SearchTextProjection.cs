using System;
using System.Collections.Generic;
using System.Linq;

namespace Dopamine.Core.Search
{
    public sealed class SearchTextProjection
    {
        private readonly string[] values;

        public SearchTextProjection(string original, params string[] readings)
        {
            var projections = new HashSet<string>(StringComparer.Ordinal) { SearchText.Normalize(original) };
            foreach (string reading in readings)
            {
                string normalized = SearchText.Normalize(reading);
                projections.Add(normalized);
                projections.Add(SearchText.Compact(normalized));
            }
            this.values = projections.ToArray();
        }

        public bool Contains(string normalizedTerm)
        {
            return this.values.Any(value => value.IndexOf(normalizedTerm, StringComparison.Ordinal) >= 0);
        }
    }
}
