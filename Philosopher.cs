using System;
using System.Threading;

namespace DiningPhilosophers
{
    class Philosopher
    {
        private int id;
        private int leftFork, rightFork;
        private Table table;

        public Philosopher(int id, Table table)
        {
            this.id = id;
            this.table = table;
            this.rightFork = id;
            this.leftFork = (id + 1) % 5;
            new Thread(Run).Start();
        }

        public void Run()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Philosopher {id} is thinking {i + 1} times");

                while (true)
                {
                    table.CallWaiter();

                    if (table.TryGetForks(rightFork, leftFork))
                    {
                        Console.WriteLine($"Philosopher {id} is eating {i + 1} times");
                        table.PutForks(rightFork, leftFork);
                        table.DismissWaiter();
                        break;
                    }

                    table.DismissWaiter();
                    Thread.Sleep(10);
                }
            }
        }
    }
}