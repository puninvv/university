using System;
using System.IO;
using System.Threading;

namespace MultiThreadFileCopy.Classes
{

    /// <summary>
    /// Класс, предназначенный для чтения из буфера и записи в файл
    /// </summary>
    public sealed class WriterThread
    {

        /// <summary>
        /// Имя файла, куда будут записываться данные
        /// </summary>
        private string _fileName;

        /// <summary>
        /// Буфер с исходными данными
        /// </summary>
        private ConcurrentNonSizebleQueue<short> _buffer;

        /// <summary>
        /// Флаг остановки
        /// </summary>
        private volatile bool _stopped;

        /// <summary>
        /// Флаг паузы
        /// </summary>
        private volatile bool _paused;


        /// <summary>
        /// Поток, который управляет чтением из буфера и записью в файл
        /// </summary>
        private Thread _worker;


        /// <summary>
        /// Возникает, когда поток заканчивает записывать
        /// </summary>
        public event EventHandler OnWritingFinished;


        /// <summary>
        /// Конструктор для <see cref="WriterThread"/>
        /// </summary>
        /// <param name="fileName">Имя файла, куда будут записываться данные</param>
        /// <param name="buffer">Буфер, откуда они будут браться</param>
        public WriterThread(string fileName, ref ConcurrentNonSizebleQueue<short> buffer)
        {
            _fileName = fileName;
            _buffer = buffer;
            _stopped = false;
            _paused = false;
        }


        /// <summary>
        /// Процедура, которая будет выполняться в потоке
        /// Работает так: пока не подняты флаги - пытается прочитать из буфера и записать в файл
        /// Если поднят флаг _stopped - закрывает файл записи, выхоит из потока
        /// Если поднят флаг _paused - уходит в бесконечный цикл, проверяя на каждой итерации состояние флага _stopped
        /// 
        /// Выходит из "спячки" если флаг _paused опустить
        /// </summary>
        private void write()
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(_fileName, FileMode.Create)))
            {
                short symb = 0;

                do
                {

                    bool recieved = false;
                    do
                    {
                        while (!_stopped && !_paused)
                        {
                            recieved = _buffer.TryDequeue(out symb);
                            if (recieved)
                                break;
                        }

                        if (_stopped)
                            return;

                        while (_paused)
                            if (_stopped)
                                return;

                    } while (!recieved);

                    binaryWriter.Write((byte)symb);
                } while (symb != -1);
            }

            if (OnWritingFinished != null)
                OnWritingFinished(this, null);
        }


        /// <summary>
        /// Опускает флаги, запускает поток записи, если ещё не запущен
        /// </summary>
        public void Write()
        {
            _stopped = false;
            _paused = false;
            if (_worker == null)
            {
                _worker = new Thread(write);
                _worker.Start();
            }
        }


        /// <summary>
        /// Останавливает поток
        /// </summary>
        public void Stop()
        {
            _stopped = true;
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
