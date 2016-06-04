using System;
using System.IO;
using System.Threading;

namespace MultiThreadFileCopy.Classes
{
    class WriterThread
    {
        private string _fileName;
        private ConcurrentNonSizebleQueue<short> _buffer;
        private volatile bool _stopped;
        private volatile bool _paused;

        private Thread _worker;

        public event EventHandler OnWritingFinished; 

        public WriterThread(string fileName, ref ConcurrentNonSizebleQueue<short> buffer)
        {
            _fileName = fileName;
            _buffer = buffer;
            _stopped = false;
            _paused = false;
        }

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

        public void Stop()
        {
            _stopped = true;
        }

        public void Pause()
        {
            _paused = true;
        }
    }
}
