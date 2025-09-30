using System;

namespace DataAccess
{
    public interface IDataRepository
    {
        object[] ReadAll(string path);
        void WriteAll(string path, object[] objects);
    }
}
