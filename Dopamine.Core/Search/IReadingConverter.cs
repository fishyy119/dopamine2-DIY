using System;

namespace Dopamine.Core.Search
{
    public interface IReadingConverter : IDisposable
    {
        SearchTextProjection Convert(string text);
    }
}
