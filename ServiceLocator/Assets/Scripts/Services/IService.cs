public interface IService
{
    T GetService<T>() where T : class;
}