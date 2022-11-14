using System.Reflection;

namespace MoviesWebApi.Shared
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
