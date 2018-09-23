using System;
using UniDash.DAL.EF;

namespace UniDash.DAL.Infrastructure
{
    public interface IDataBaseFactory : IDisposable
    {
        DutContext Get();
    }
}
