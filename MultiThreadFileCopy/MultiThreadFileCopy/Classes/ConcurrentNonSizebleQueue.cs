using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MultiThreadFileCopy.Classes
{
    /// <summary>
    /// Реализация очереди, допускающей увеличение до определенного размера, адаптирована под один поток "добавления", один поток "забирания"
    /// </summary>
    public class ConcurrentNonSizebleQueue<T>
    {
        private ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
        private readonly Object lockObj = new Object();

        private volatile int maxSize;
        /// <summary>
        /// Максимальный размер очереди
        /// </summary>
        public int MaxSize
        {
            get
            {
                return maxSize;
            }
            set
            {
                lock (lockObj)
                {
                    if (value > 0)
                        maxSize = value;
                }
            }
        }

        /// <summary>
        /// Текущий размер очереди
        /// </summary>
        public long CurrentSize
        {
            get
            {
                return queue.Count;
            }
            private set
            {
                CurrentSize = value;
            }
        }

        /// <summary>
        /// Конструктор для <see cref="ConcurrentNonSizebleQueue{T}"/>
        /// </summary>
        /// <param name="maxSize">Максимальный размер очереди</param>
        public ConcurrentNonSizebleQueue(int maxSize)
        {
            MaxSize = maxSize;
        }

        /// <summary>
        /// Добавление элемента в очередь.
        /// </summary>
        /// <param name="obj">Добавляемый объект</param>
        /// <returns>Возвращает true, если объект был добавлен, иначе false</returns>
        public bool TryEnqueue(T obj)
        {
            if (obj == null)
                return false;

            if (queue.Count < maxSize)
            {
                queue.Enqueue(obj);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Безопасное удаление элемента из очереди.
        /// </summary>
        /// <param name="obj">Возвращаемый объект</param>
        /// <returns>Возвращает true, если объект был удален, иначе false</returns>        
        public bool TryDequeue(out T obj)
        {
            if (queue.Count == 0)
            {
                obj = default(T);
                return false;
            }

            return queue.TryDequeue(out obj);
        }
    }
}
