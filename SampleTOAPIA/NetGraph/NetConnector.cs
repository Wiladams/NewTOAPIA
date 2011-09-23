using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetGraph
{
    using NewTOAPIA;

    public class NetConnector : Observer<BufferChunk>
    {
        IDisposable subscription;

        public NetConnector(IObservable<BufferChunk> observable)
        {
            subscription = observable.Subscribe(this);
        }

        #region IObserver
        public override void OnNext(BufferChunk chunk)
        {
        }
        #endregion
    }
}
