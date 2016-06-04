using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using Microsoft.Win32;
using System.Windows.Threading;

namespace MultiThreadFileCopy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        private string  inputFile;
        private string  outputFile;
        private int     bufferSize;
        private Classes.ReaderThread reader;
        private Classes.WriterThread writer;
        private Classes.ConcurrentNonSizebleQueue<short> buffer;

        private DispatcherTimer timer;
 
        public MainWindow()
        {
            InitializeComponent();
            reader = null;
            writer = null;
            buffer = null;
            inputFile = null;
            outputFile = null;
            bufferSize = -1;
            labelProgress.Visibility = progressReading.Visibility = Visibility.Hidden;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(500);
            timer.Tick += Timer_Tick;
            progressBuferSize.Minimum = 0;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progressBuferSize.Value = buffer.CurrentSize;
            labelBufferSizeChangeable.Content = buffer.CurrentSize;
        }

        //работа с кнопочками
        private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                inputFile = openFileDialog.FileName;
                textBlockInputFileName.Content = inputFile;
                buttonOpenFile.IsChecked = true;
            }
            else
                if (inputFile == null)
                    buttonOpenFile.IsChecked = false;
        }

        private void buttonSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                outputFile = saveFileDialog.FileName;
                textBlockOutputFileName.Content = outputFile;
                buttonSaveFile.IsChecked = true;
            }
            else
                if (outputFile == null)
                    buttonSaveFile.IsChecked = false;
        }

        private void buttonSetBufferSize_Click(object sender, RoutedEventArgs e)
        {
            int tmpBufferSize = 0;
            if (int.TryParse(textBoxBufferSize.Text, out tmpBufferSize))
            {
                bufferSize = tmpBufferSize;
                if (buffer != null)
                    buffer.MaxSize = bufferSize;
                buttonSetBufferSize.IsChecked = true;
                progressBuferSize.Maximum = bufferSize;
            }
            else
            {
                buttonSetBufferSize.IsChecked = false;
                MessageBox.Show("Некорректный размер буфера");
            }
        }
        

        //работа с ридером
        private void readerButtons(bool start, bool pause, bool stop)
        {
            buttonReaderStart.IsEnabled = start;
            buttonReaderPause.IsEnabled = pause;
            buttonReaderStop.IsEnabled = stop;
        }

        private void buttonReaderStart_Click(object sender, RoutedEventArgs e)
        {
            if (bufferSize == -1)
            {
                MessageBox.Show("Не задан максимальный размер буфера!");
                return;
            }
            if (inputFile == null)
            {
                MessageBox.Show("Не задан входной файл");
                return;
            }
            if (buffer == null)
                buffer = new Classes.ConcurrentNonSizebleQueue<short>(bufferSize);

            if (reader == null)
            {
                try {
                    reader = new Classes.ReaderThread(inputFile, ref buffer);
                    reader.OnReadingFinished += Reader_OnReadingFinished;
                    reader.OnPercentageUpped += Reader_OnReadingStateChanged;
                } catch(Exception)
                {
                    reader = null;
                    MessageBox.Show("Ошибка при открытии исходного файла");
                }
            }

            buttonOpenFile.IsEnabled = false;
            if (timer.IsEnabled == false)
                timer.IsEnabled = true;
            labelProgress.Visibility = progressReading.Visibility = Visibility.Visible;


            readerButtons(false, true, true);

            reader.Read();
        }

        private void Reader_OnReadingFinished(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(delegate
            {
                MessageBox.Show("Чтение закончено");
                buttonOpenFile.IsEnabled = true;
                buttonSaveFile.IsEnabled = true;
                progressReading.Value = 0;
                labelProgress.Visibility = progressReading.Visibility = Visibility.Hidden;

                readerButtons(true, false, false);
            }));
        }

        private void Reader_OnReadingStateChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(delegate
            {
                progressReading.Value = ((Classes.ReaderThread.ReaderArgs)e).Percent;
            }));
        }

        private void buttonReaderPause_Click(object sender, RoutedEventArgs e)
        {
            reader.Pause();
            readerButtons(true, false, true);
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            reader.Stop();
            readerButtons(true, false, false);
            reader = null;

            writer.Stop();
            writerButtons(true, false, false);
            writer = null;

            labelProgress.Visibility = progressReading.Visibility = Visibility.Hidden;
            buttonOpenFile.IsEnabled = true;
            buttonSaveFile.IsEnabled = true;

            inputFile = null;
            outputFile = null;
            buttonOpenFile.IsChecked = false;
            buttonSaveFile.IsChecked = false;

            textBlockInputFileName.Content = "";
            textBlockOutputFileName.Content = "";

            progressReading.Value = 0;


            timer.IsEnabled = false;
            progressBuferSize.Value = 0;
            labelBufferSizeChangeable.Content = "";
        }

        
        //работа с райтером
        private void writerButtons(bool start, bool pause, bool stop)
        {
            buttonWriterStart.IsEnabled = start;
            buttonWriterPause.IsEnabled = pause;
            buttonWriterStop.IsEnabled = stop;
        }

        private void buttonWriterStart_Click(object sender, RoutedEventArgs e)
        {
            if (bufferSize == -1)
            {
                MessageBox.Show("Не задан максимальный размер буфера!");
                return;
            }
            if (outputFile == null)
            {
                MessageBox.Show("Не задан выходной файл");
                return;
            }
            if (buffer == null)
                buffer = new Classes.ConcurrentNonSizebleQueue<short>(bufferSize);
            if (writer == null)
            {
                try {
                    writer = new Classes.WriterThread(outputFile, ref buffer);
                    writer.OnWritingFinished += Writer_OnWritingFinished;
                }
                catch (Exception)
                {
                    reader = null;
                    MessageBox.Show("Ошибка при открытии файла для сохраниения");
                }
            }

            buttonSaveFile.IsEnabled = false;

            if (timer.IsEnabled == false)
                timer.IsEnabled = true;

            buttonWriterStart.IsEnabled = false;
            buttonWriterStop.IsEnabled = true;
            buttonWriterPause.IsEnabled = true;

            writer.Write();
        }

        private void Writer_OnWritingFinished(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(delegate
            {
                MessageBox.Show("Запись закончена");
                buttonOpenFile.IsEnabled = true;
                buttonSaveFile.IsEnabled = true;

                writerButtons(true, false, false);

                reader.Stop();
                readerButtons(true, false, false);
                reader = null;

                writer.Stop();
                writerButtons(true, false, false);
                writer = null;

                labelProgress.Visibility = progressReading.Visibility = Visibility.Hidden;
                buttonOpenFile.IsEnabled = true;
                buttonSaveFile.IsEnabled = true;

                inputFile = null;
                outputFile = null;
                buttonOpenFile.IsChecked = false;
                buttonSaveFile.IsChecked = false;

                textBlockInputFileName.Content = "";
                textBlockOutputFileName.Content = "";

                progressReading.Value = 0;

                timer.IsEnabled = false;
                progressBuferSize.Value = 0;
                labelBufferSizeChangeable.Content = "";
            }));
        }

        private void buttonWriterPause_Click(object sender, RoutedEventArgs e)
        {
            writer.Pause();
            writerButtons(true, false, true);
        }
    }
}
