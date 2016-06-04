using System;
using System.Threading;
using System.IO;

namespace MultiThreadFileCopy.Classes
{
    public class ReaderThread
    {
        private string _filename;
        private ConcurrentNonSizebleQueue<short> _buffer;
        private volatile bool _stopped;
        private volatile bool _paused;

        private Thread _worker;

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
        public event EventHandler OnPercentageUpped;
        public event EventHandler OnReadingFinished;

        public ReaderThread(string filename, ref Classes.ConcurrentNonSizebleQueue<short> buffer)
        {
            _filename = filename;
            _buffer = buffer;
            _stopped = false;
            _paused = false;
            _worker = null;
        }

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

        public void Stop()
        {
            _stopped = true;
            _worker = null;
        }

        public void Pause()
        {
            _paused = true;
        }
    }
}
