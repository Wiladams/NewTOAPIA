namespace NewTOAPIA
{
    using System;
    using System.Collections.Generic;

    public class Subscription<T> : IDisposable
    {
        public IObserver<T> Observer { get; private set; }
        public Observable<T> Observable { get; private set; }

        public Subscription(Observable<T> observable, IObserver<T> observer)
        {
            Observer = observer;
            Observable = observable;
        }

        public void Dispose()
        {
            Observable.RemoveSubscriber(Observer);
        }
    }

    public class Observable<T> : IObservable<T>
    {
        List<IObserver<T>> Observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            Observers.Add(observer);
            Subscription<T> subber = new Subscription<T>(this, observer);

            return subber;
        }

        internal void RemoveSubscriber(IObserver<T> observer)
        {
            lock (Observers)
            {
                Observers.Remove(observer);
            }
        }

        public void DispatchData(T data)
        {
            lock (Observers)
            {
                foreach (IObserver<T> observer in Observers)
                {
                    observer.OnNext(data);
                }
            }
        }
    }
}