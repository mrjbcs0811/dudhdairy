using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dairy
{
    public partial class Master : Form
    {
     
        public Master()
        {
            InitializeComponent();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void mnSabhasadNondani_Click(object sender, EventArgs e)
        {
            BuissnessPartner objsabhasad = new BuissnessPartner();
            objsabhasad.Show();
            objsabhasad.MdiParent = this;

        }

        private void mnKapat_Click(object sender, EventArgs e)
        {
            KapatEntry objKapat = new KapatEntry();
            objKapat.Show();
            objKapat.MdiParent = this;

        }

        private void mnDudhSankalan_Click(object sender, EventArgs e)
        {
            DudhSankalan objDudhSankalan = new DudhSankalan();
            objDudhSankalan.Show();
            objDudhSankalan.MdiParent = this;

        }

        private void mnDudhVikri_Click(object sender, EventArgs e)
        {
            Sale objSale = new Sale();
            objSale.Show();
            objSale.MdiParent = this;

        }

        private void mnKapatichePrakar_Click(object sender, EventArgs e)
        {
            KapatMaster objKapatMaster = new KapatMaster();
            objKapatMaster.Show();
            objKapatMaster.MdiParent = this;

        }

        private void mnBandKara_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void गवचयदToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GaonMaster objgaon = new GaonMaster();
            objgaon.Show();
            objgaon.MdiParent = this;

        }
    }
}
