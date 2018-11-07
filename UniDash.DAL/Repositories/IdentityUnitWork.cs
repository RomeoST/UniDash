using System.Threading.Tasks;
using UniDash.DAL.EF;
using UniDash.DAL.Infrastructure;
namespace UniDash.DAL.Repositories
{
    /// <summary>
    /// Шаблон UnitOfWork - призван отслеживать все изменения данных, которые мы производим с доменной моделью в рамках бизнес-транзакции.
    /// После того, как бизнес-транзакция закрывается, все изменения попадают в БД в виде единой транзакции.
    /// </summary>
    public class IdentityUnitWork : IUnitOfWork
    {
        private readonly IDataBaseFactory dataBaseFactory;
        private DutContext dataContext;

        public IdentityUnitWork(IDataBaseFactory dataBaseFactory)
        {
            this.dataBaseFactory = dataBaseFactory;
        }

        protected DutContext DataContext => dataContext ?? (dataContext = dataBaseFactory.Get());

        /// <summary>
        /// Збереження всіх даних які відбувалися з БД
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await DataContext.SaveChangesAsync();
        }

        public int Commit()
        {
            return DataContext.SaveChanges();
        }
    }
}
