namespace DalApi;

public interface ICrud<T> where T : struct
{
 public int Add(T obj);
 public void Delete(int ID);
 public void Update(T obj);
 public IEnumerable<T?> GetAll(Func<T?, bool>? predict = null);
 public T GetByCondition(Func<T?, bool> predict);
}