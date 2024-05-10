using LegacyApp.Core.Entities.Abstract;

namespace LegacyApp.Core.Resources.Base.Abstract;

public interface IBaseEntityRepository<T> where T : class, IEntity, new()
{
    T GetById(int id);
}
