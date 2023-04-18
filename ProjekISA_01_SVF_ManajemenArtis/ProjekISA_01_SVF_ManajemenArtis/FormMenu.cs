﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ManajemenArtis_Lib;

namespace ProjekISA_01_SVF_ManajemenArtis
{
    public partial class FormMenu : Form
    {
        public Artis artis;
        public Manager manager;

        public FormMenu()
        {
            InitializeComponent();
        }

        private void FormMenu_Load_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;

            if(artis != null)
            {
                labelNama.Text = artis.Nama;
            }
            else
            {
                labelNama.Text = manager.Nama;
            }
            
        }

        private void CekAccessControl()
        {
            if (manager != null)
            {
                if (manager.Title == jabatan.manager)
                {
                    homeToolStripMenuItem.Visible = true;
                    
                }
                else if (manager.Title == jabatan.superAdmin)
                {
                    homeToolStripMenuItem.Visible = true;
                }
            }
            else
            {
                //kalau login sebagai artis
                homeToolStripMenuItem.Visible = true;
            }
        }
    }
} 
