using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ASAServerExplorer;

public partial class About : Form
{
    public About()
    {
        InitializeComponent();
    }

    private void pictureBox1_Click( object sender, EventArgs e )
    {

    }

    private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
    {
        Process.Start( new ProcessStartInfo()
        {
            FileName = "https://github.com/stephen-v-hale/ASAServerManager",
            UseShellExecute = true
        } );
    }

    private void button1_Click( object sender, EventArgs e )
    {
        Close();
    }
}
