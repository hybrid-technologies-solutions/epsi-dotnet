using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstractions
{
    public interface IRepository<T> :
        IReaderRepository<T>,
        IWriterRepository<T>
        where T : class
    {
    }
}
