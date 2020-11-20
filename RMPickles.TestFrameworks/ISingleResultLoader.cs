using System.IO;

using RMPickles.Core.ObjectModel;

namespace RMPickles.Core.TestFrameworks
{
    public interface ISingleResultLoader
    {
        SingleTestRunBase Load(FileInfo fileInfo);
    }
}