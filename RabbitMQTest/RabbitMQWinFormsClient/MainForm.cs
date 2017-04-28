using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQCommonLib;
using RabbitMQCommonLib.Client;

namespace RabbitMQWinFormsClient
{
    public partial class MainForm : Form
    {
        private string m_imgPath;

        private Queue<RabbitMQTask> m_tasks = new Queue<RabbitMQTask>();
        private Dictionary<Guid, UCTask> m_tasksPanels = new Dictionary<Guid, UCTask>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void ucStart1_OnCorrectlyInitialized(object sender, UCStart.UCStartEventArgs e)
        {
            m_imgPath = e.ImageFullPath;

            CreateNewTask();
        }

        private void CreateNewTask()
        {
            var task = new UCTask();
            task.Name = Guid.NewGuid().ToString();
            task.BackColor = Color.DarkRed;
            task.Margin = new Padding(20);
            task.Size = new Size(136, 136);
            task.Index = (m_tasksPanels.Count + 1).ToString();

            m_tasksPanels.Add(task.TaskId, task);

            task.OnAddClicked += Task_OnAddClicked;
            task.OnStartClicked += Task_OnStartClicked;

            flowLayoutPanel1.Controls.Add(task);
        }

        private void Task_OnAddClicked(object sender, EventArgs e)
        {
            var task = (UCTask)sender;
            task.BackColor = Color.OrangeRed;

            m_tasks.Enqueue(new RabbitMQTask(task.TaskType, task.TaskId));

            CreateNewTask();
        }

        private void Task_OnStartClicked(object sender, EventArgs e)
        {
            var task = (UCTask)sender;
            task.BackColor = Color.OrangeRed;

            m_tasks.Enqueue(new RabbitMQTask(task.TaskType, task.TaskId));

            var end = new UCEnd();
            end.Name = Guid.NewGuid().ToString();
            end.BackColor = Color.DarkRed;
            end.Margin = new Padding(20);
            end.Size = new Size(136, 136);
            
            flowLayoutPanel1.Controls.Add(end);

            Task.Factory.StartNew(() =>
            {
                var setup = Properties.Settings.Default;

                using (var client = new RabbitMQClient(setup.Host, setup.User, setup.Pass, setup.Port))
                {
                    var imgPath = m_imgPath;

                    var bmp = new Bitmap(imgPath);
                    var serializer = new BytesSerializer<Bitmap>();

                    client.OnTaskProcessed += Client_OnTaskProcessed;

                    var message = new RabbitMQMessage(serializer.ObjectToByteArray(bmp), m_tasks);

                    Console.WriteLine(" [x] Requesting {0}", imgPath);

                    var response = client.GetResponce(message, Guid.NewGuid(), setup.Timeout);

                    var result = serializer.ByteArrayToObject(response.Data);

                    if (end.InvokeRequired)
                        end.BeginInvoke(new Action(() =>
                        {
                            end.SetupImage(result);
                        }));

                    end.BackColor = Color.Green;
                }
            });     
        }

        private void Client_OnTaskProcessed(object sender, RabbitMQClient.RabbitMQClientEventArgs e)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() =>
               {
                   var task = m_tasks.Single(t => t.Id == e.CompletedTask.Id);
                   m_tasksPanels[task.Id].BackColor = Color.Green;
               }));
                Console.WriteLine(e.ToString());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Click on first square and set image\nThen, select filters and press \"Start\"", "Help", MessageBoxButtons.OK);
        }
    }

}
