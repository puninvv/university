using Dota2CommonLib.Heroes;
using Dota2CommonLib.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2ParserWinforms
{
    public partial class MainForm : Form
    {
        private Dictionary<Hero, UCDota2Element> m_heroes = new Dictionary<Hero, UCDota2Element>();
        private Dictionary<Item, UCDota2Element> m_items = new Dictionary<Item, UCDota2Element>();

        public MainForm()
        {
            InitializeComponent();

            this.tabPage1.Text = "Heroes";
            this.tabPage2.Text = "Items";

            this.Cursor = Cursors.WaitCursor;

            Task.Factory.StartNew(() =>
            {
                try
                {
                    LoadHeroes();
                    LoadItems();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }

                if (InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.Cursor = Cursors.Arrow;
                    }));
                }
                else
                    this.Cursor = Cursors.Arrow;
            });
        }

        private void LoadHeroes()
        {
            var heroes = new HeroesLoader().LoadHeroes(Properties.Settings.Default.API_KEY);
            foreach (var hero in heroes)
            {
                var tmpControl = new UCDota2Element();
                tmpControl.Name = Guid.NewGuid().ToString();

                tmpControl.Icon = hero.Picture;
                tmpControl.Caption = hero.LocalizedName;

                if (InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.flowLayoutPanel1.Controls.Add(tmpControl);
                    }));
                }
                else
                    this.flowLayoutPanel1.Controls.Add(tmpControl);

                m_heroes.Add(hero, tmpControl);
            }
        }

        private void LoadItems()
        {
            var items = new ItemsLoader().LoadItems(Properties.Settings.Default.API_KEY);
            foreach (var item in items)
            {
                var tmpControl = new UCDota2Element();
                tmpControl.Name = Guid.NewGuid().ToString();

                tmpControl.Icon = item.Picture;
                tmpControl.Caption = item.LocalizedName;

                if (InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.flowLayoutPanel2.Controls.Add(tmpControl);
                    }));
                }
                else
                    this.flowLayoutPanel2.Controls.Add(tmpControl);

                m_items.Add(item, tmpControl);
            }
        }
    }
}
