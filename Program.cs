using System;
using System.Threading;

namespace DiningPhilosophers
{
    class Table
    {
        private SemaphoreSlim[] forks = new SemaphoreSlim[5];

        // Семафор на 2 токени
        private SemaphoreSlim tokens = new SemaphoreSlim(2, 2);

        public Table()
        {
            for (int i = 0; i < 5; i++)
            {
                forks[i] = new SemaphoreSlim(1, 1);
            }
        }

        public void GetToken() { tokens.Wait(); }
        public void ReturnToken() { tokens.Release(); }
        public void GetFork(int id) { forks[id].Wait(); }
        public void PutFork(int id) { forks[id].Release(); }
    }

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

                table.GetToken(); 

                table.GetFork(rightFork);
                table.GetFork(leftFork);

                Console.WriteLine($"Philosopher {id} is eating {i + 1} times");

                table.PutFork(leftFork);
                table.PutFork(rightFork);

                table.ReturnToken(); 
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table();
            for (int i = 0; i < 5; i++)
            {
                new Philosopher(i, table);
            }
        }
    }
}