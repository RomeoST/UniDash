using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniDash.DAL.EF;

namespace UniDash.DAL.Infrastructure
{
    public class DataBaseFactory : Disposable ,IDataBaseFactory
    {
        private DutContext dataContext;
        private static string _connectionString = "";

        public DataBaseFactory() { }
        public DataBaseFactory(string connectionString) => _connectionString = connectionString;

        public DutContext Get() => dataContext ?? (dataContext = new DutContext(new DbContextOptionsBuilder<DutContext>().UseSqlServer(_connectionString).Options));

        protected override void DisposeCore() => dataContext?.Dispose();
    }
}
