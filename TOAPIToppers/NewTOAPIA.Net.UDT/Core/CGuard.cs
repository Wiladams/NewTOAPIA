namespace NewTOAPIA.Net.Udt
{
    using System.Threading;

    public class CGuard
    {
        private Mutex m_Mutex;            // Alias name of the mutex to be protected
        private bool m_iLocked;                       // Locking status

        //
        // Automatically lock in constructor
        public CGuard(Mutex alock)
        {
            m_Mutex = alock;
            //m_iLocked()

            m_iLocked = m_Mutex.WaitOne();
        }

        // Automatically unlock in destructor
        ~CGuard()
        {
            //if (WAIT_FAILED != m_iLocked)
            m_Mutex.ReleaseMutex();
        }

        public static void enterCS(Mutex alock)
        {
            alock.WaitOne();
        }

        public static void leaveCS(Mutex alock)
        {
            alock.ReleaseMutex();
        }

    }
}