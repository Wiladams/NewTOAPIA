namespace NewTOAPIA.Media
{
    using NewTOAPIA;
    using NewTOAPIA.BCL;

    public class AudioEventQueue : Observer<AudioEvent>
    {
        public BlockingBoundedQueue<AudioEvent> AudioEvents { get; private set; }

        #region Constructors
        /// <summary>
        /// We don't want the default constructor being called
        /// </summary>
        private AudioEventQueue()
        {
        }

        public AudioEventQueue(int count)
        {
            AudioEvents = new BlockingBoundedQueue<AudioEvent>(count);
        }
        #endregion

        #region Observer
        public override void OnCompleted()
        {
            // When completed, break the enumerator
        }

        public override void OnNext(AudioEvent data)
        {
            AudioEvents.Enqueue(data);
        }
        #endregion
    }
}
