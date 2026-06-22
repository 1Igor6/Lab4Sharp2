using System;
using System.Threading;

namespace DiningPhilosophers
{
    class Table
    {
        private SemaphoreSlim[] forks = new SemaphoreSlim[5];
        
        private SemaphoreSlim tokens = new SemaphoreSlim(2, 2);

        public Table()
        {
            for (int i = 0; i < 5; i++)
            {
                forks[i] = new SemaphoreSlim(1, 1);
            }
        }

        public void CallWaiter() { tokens.Wait(); }
        public void DismissWaiter() { tokens.Release(); }

        
        public bool TryGetForks(int right, int left)
        {
            if (forks[right].Wait(0)) 
            {
                if (forks[left].Wait(0)) 
                {
                    return true; 
                }
                forks[right].Release(); 
            }
            return false; 
        }

        public void PutForks(int right, int left)
        {
            forks[left].Release();
            forks[right].Release();
        }
    }
}