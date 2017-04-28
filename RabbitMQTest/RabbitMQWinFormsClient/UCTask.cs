using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQCommonLib;

namespace RabbitMQWinFormsClient
{
    public partial class UCTask : UserControl
    {
        public event EventHandler OnAddClicked;
        public event EventHandler OnStartClicked;

        public void ChangeBackGround(Color _color)
        {
            this.BackColor = _color;
        }

        public string Index
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }
        public class UCTaskEventArgs : EventArgs
        {
            public RabbitMQTaskType TaskType
            {
                get;
                private set;
            }
            public Guid TaskId
            {
                get;
                private set;
            }

            public UCTaskEventArgs(RabbitMQTaskType _taskType, Guid _taskId)
            {
                TaskType = _taskType;
                TaskId = _taskId;
            }
        }

        public UCTask()
        {
            InitializeComponent();

            foreach (var item in Enum.GetValues(typeof(RabbitMQTaskType)))
                comboBox1.Items.Add((RabbitMQTaskType)item);

            comboBox1.SelectedIndex = 1;

            TaskId = Guid.NewGuid();
        }

        public Guid TaskId
        {
            get;
            private set;
        }

        public RabbitMQTaskType TaskType
        {
            get;
            private set;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OnAddClicked != null)
                OnAddClicked(this, new EventArgs());
            DisableAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (OnStartClicked != null)
                OnStartClicked(this, new EventArgs());
            DisableAll();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            TaskType = (RabbitMQTaskType)comboBox1.SelectedItem;
        }

        private void DisableAll()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            comboBox1.Enabled = false;
        }
    }
}
