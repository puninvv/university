using System;
using System.Threading;
using System.IO;

namespace MultiThreadFileCopy.Classes
{
    /// <summary>
    /// Класс, предназначенный для чтения из файла и записи в буфер
    /// </summary>
    public sealed class ReaderThread
    {
        /// <summary>
        /// Имя файла, откуда будут считываться данные
        /// </summary>
        private string _filename;

        /// <summary>
        /// Буфер
        /// </summary>
        private ConcurrentNonSizebleQueue<short> _buffer;

        /// <summary>
        /// Флаг для остановки ридера
        /// </summary>
        private volatile bool _stopped;


        /// <summary>
        /// Флаг для постановки ридера на паузу
        /// </summary>
        private volatile bool _paused;


        /// <summary>
        /// Поток, который управляет чтением из файла и записью в буфер
        /// </summary>
        private Thread _worker;


        /// <summary>
        /// Содержит в себе процент, показывающий "прочитанность" файла
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class ReaderArgs: EventArgs
        {
            public double Percent
            {
                get;
            }

            public ReaderArgs(double percent)
            {
                Percent = percent;
            }
        }

        /// <summary>
        /// Вызывается, когда процент прочтения файла изменился более, чем на единицу
        /// </summary>
        public event EventHandler OnPercentageUpped;

        /// <summary>
        /// Вызывается, когда чтение файла закончено
        /// </summary>
        public event EventHandler OnReadingFinished;


        /// <summary>
        /// Конструктор для <see cref="ReaderThread"/>
        /// </summary>
        /// <param name="filename">Имя файла, откуда будут браться данные.</param>
        /// <param name="buffer">Буфер, куда они будут передаваться.</param>
        public ReaderThread(string filename, ref Classes.ConcurrentNonSizebleQueue<short> buffer)
        {
            _filename = filename;
            _buffer = buffer;
            _stopped = false;
            _paused = false;
            _worker = null;
        }


        /// <summary>
        /// Процедура, которая будет выполняться в потоке.
        /// Работает так: пока флаги остановки и паузы не подняты - можно пытаться записать в буфер
        /// Как только все данные будут записаны - в буфер пойдет число -1
        /// Если флаг _stopped активен - прекращает работу
        /// Если флаг _paused активен - уходит в бесконечный цикл, проверяя на каждой итерации состояние флага _stopped
        /// 
        /// Выходит из "спячки" если флаг _paused опустить
        /// </summary>
        private void read()
        {
            using (BinaryReader binaryReader = new BinaryReader(File.Open(_filename, FileMode.Open)))
            {
                int oldPercent = 0;

                bool sended = false;

                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    Byte symb = binaryReader.ReadByte();

                    do
                    {
                        sended = false;

                        while (!_stopped && !_paused)
                        {
                            sended = _buffer.TryEnqueue(symb);
                            if (sended)
                                break;
                        }

                        if (_stopped)
                            return;

                        while (_paused)
                            if (_stopped)
                                return;

                    } while (!sended);

                    sended = false;

                    if (OnPercentageUpped != null && oldPercent < (int)(100.0 * binaryReader.BaseStream.Position / binaryReader.BaseStream.Length)-1)
                    {
                        oldPercent = (int)(100.0 * binaryReader.BaseStream.Position / binaryReader.BaseStream.Length);
                        OnPercentageUpped(this, new ReaderArgs(oldPercent));
                    }
                }

                do
                {
                    sended = false;

                    while (!_stopped && !_paused)
                    {
                        sended = _buffer.TryEnqueue(-1);
                        if (sended)
                            break;
                    }

                    if (_stopped)
                        return;

                    while (_paused)
                        if (_stopped)
                            return;

                } while (!sended);
             
            }
            
            if (OnReadingFinished != null)
                OnReadingFinished(this, null);
        }


        /// <summary>
        /// Опускает флаги, запускает поток чтения, если ещё не был запущен
        /// </summary>
        public void Read()
        {
            _paused = false;
            _stopped = false;

            if (_worker == null)
            {
                _worker = new Thread(read);
                _worker.Start();
            }
        }


        /// <summary>
        /// Останавливает поток
        /// </summary>
        public void Stop()
        {
            _stopped = true;
            _worker = null;
        }


        /// <summary>
        /// Ставит поток на паузу
        /// </summary>
        public void Pause()
        {
            _paused = true;
        }
    }
}
