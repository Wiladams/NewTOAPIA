
namespace NewTOAPIA
{
    
    public class Observer<T> : System.IObserver<T>
    {
        public virtual void OnNext(T data)
        {
        }

        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(System.Exception excep)
        {
            throw excep;
        }

    }
    
}