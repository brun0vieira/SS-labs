using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class Prompt : Form
    {
        public Prompt()
        {
            InitializeComponent();
        }

        public Prompt(string _title)
        {
            InitializeComponent();

            this.Text = _title;

        }


    }
}