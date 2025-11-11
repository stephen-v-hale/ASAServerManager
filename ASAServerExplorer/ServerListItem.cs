using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ASAServerExplorer;

public partial class ServerListItem : Form
{
    public new string Name => textBox2.Text;
    public string Address => textBox1.Text;
    public string Password => textBox3.Text;
    public int Port => int.Parse( textBox4.Text );

    public bool ShowRealTimeLog => checkBox1.Checked;
    public bool ShowRealTimePlayerLogging => checkBox2.Checked;

    public bool MessageBroadcaster => checkBox5.Checked;
    public ServerListItem()
    {
        InitializeComponent();
    }

    private void button1_Click( object sender, EventArgs e )
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void button2_Click( object sender, EventArgs e )
    {
        DialogResult = DialogResult.OK;
        Close();
    }
}
