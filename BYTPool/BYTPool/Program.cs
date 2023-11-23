using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ObjectPoolPattern
{
    public class Client
    {
        public int MyProperty { get; set; }
    }

    class ObjectPool<T> where T : class, new()
    {
        private int _currentSize = 0;
        private int _counter = 0;
        private ConcurrentBag<T> _bag = new ConcurrentBag<T>();
        public int Size { get { return _currentSize; } }
        public int TotalObject { get { return _counter; } }

        public ObjectPool(int size = 5)
        {
            Interlocked.Exchange(ref _currentSize, size);
        }

        public T AcquireObject()
        {
            if (!_bag.TryTake(out T item))
            {
                if (item == null)
                {
                    if (_counter >= _currentSize)
                        return null;

                    item = new T();

                    Interlocked.Increment(ref _counter);
                }
            }

            return item;
        }

        public void ReleaseObject(T item)
        {
            _bag.Add(item);
        }

        public void IncreaseSize()
        {
            Interlocked.Increment(ref _currentSize);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ObjectPool<Client> pool = new ObjectPool<Client>();

            Console.WriteLine("Pool size is {0}", pool.Size);

            Console.WriteLine("Having the Client Class.");
            var client1 = pool.AcquireObject();

            Console.WriteLine("Releasing the Client");
            if (client1 != null)
                pool.ReleaseObject(client1);

            var clients = new List<Client>();
            for (int i = 0; i < pool.Size; i++)
            {
                clients.Add(pool.AcquireObject());
            }

            Console.WriteLine("All the possible Clients are added to list.");

            var nullClient = pool.AcquireObject();

            if (nullClient == null)
                Console.WriteLine("No more Client class have been found.");

            Console.WriteLine("Increasing the size of the pool");
            pool.IncreaseSize();

            Console.WriteLine("Having a new Client class.");
            var newClient = pool.AcquireObject();


            Console.WriteLine("Releasing the class we got.");
            if (newClient != null)
                pool.ReleaseObject(newClient);

            Console.WriteLine("Releasing all the classes in the list.");

            foreach (var item in clients)
                pool.ReleaseObject(item);
            Console.Read();

        }
    }
}
