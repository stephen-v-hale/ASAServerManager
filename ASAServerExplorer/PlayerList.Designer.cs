namespace ASAServerExplorer;

partial class PlayerList
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
        if ( disposing && ( components != null ) )
        {
            components.Dispose();
        }
        base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerList));
        listView1 = new ListView();
        columnHeader1 = new ColumnHeader();
        columnHeader2 = new ColumnHeader();
        columnHeader3 = new ColumnHeader();
        contextMenuStrip1 = new ContextMenuStrip( components );
        toolsToolStripMenuItem = new ToolStripMenuItem();
        sendMessageToolStripMenuItem = new ToolStripMenuItem();
        kickToolStripMenuItem = new ToolStripMenuItem();
        banToolStripMenuItem = new ToolStripMenuItem();
        timer1 = new System.Windows.Forms.Timer( components );
        timer2 = new System.Windows.Forms.Timer( components );
        contextMenuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // listView1
        // 
        listView1.BorderStyle = BorderStyle.None;
        listView1.Columns.AddRange( new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 } );
        listView1.ContextMenuStrip = contextMenuStrip1;
        listView1.Dock = DockStyle.Fill;
        listView1.FullRowSelect = true;
        listView1.Location = new Point( 0, 0 );
        listView1.Name = "listView1";
        listView1.Size = new Size( 425, 268 );
        listView1.TabIndex = 0;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = View.Details;
        // 
        // columnHeader1
        // 
        columnHeader1.Text = "#";
        columnHeader1.Width = 30;
        // 
        // columnHeader2
        // 
        columnHeader2.Text = "Name";
        columnHeader2.Width = 100;
        // 
        // columnHeader3
        // 
        columnHeader3.Text = "ID";
        columnHeader3.Width = 100;
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Items.AddRange( new ToolStripItem[] { toolsToolStripMenuItem, kickToolStripMenuItem, banToolStripMenuItem } );
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new Size( 103, 70 );
        // 
        // toolsToolStripMenuItem
        // 
        toolsToolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { sendMessageToolStripMenuItem } );
        toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
        toolsToolStripMenuItem.Size = new Size( 102, 22 );
        toolsToolStripMenuItem.Text = "Tools";
        // 
        // sendMessageToolStripMenuItem
        // 
        sendMessageToolStripMenuItem.Name = "sendMessageToolStripMenuItem";
        sendMessageToolStripMenuItem.Size = new Size( 149, 22 );
        sendMessageToolStripMenuItem.Text = "Send Message";
        sendMessageToolStripMenuItem.Click +=  sendMessageToolStripMenuItem_Click ;
        // 
        // kickToolStripMenuItem
        // 
        kickToolStripMenuItem.Name = "kickToolStripMenuItem";
        kickToolStripMenuItem.Size = new Size( 102, 22 );
        kickToolStripMenuItem.Text = "Kick";
        kickToolStripMenuItem.Click +=  kickToolStripMenuItem_Click ;
        // 
        // banToolStripMenuItem
        // 
        banToolStripMenuItem.Name = "banToolStripMenuItem";
        banToolStripMenuItem.Size = new Size( 102, 22 );
        banToolStripMenuItem.Text = "Ban";
        banToolStripMenuItem.Click +=  banToolStripMenuItem_Click ;
        // 
        // timer1
        // 
        timer1.Enabled = true;
        timer1.Interval = 1;
        timer1.Tick +=  timer1_Tick ;
        // 
        // timer2
        // 
        timer2.Enabled = true;
        timer2.Interval = 5000;
        timer2.Tick +=  timer2_Tick ;
        // 
        // PlayerList
        // 
        AutoScaleDimensions = new SizeF( 7F, 15F );
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size( 425, 268 );
        Controls.Add( listView1 );
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = ( Icon )resources.GetObject( "$this.Icon" );
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PlayerList";
        Text = "PlayerList";
        Load +=  PlayerList_Load ;
        contextMenuStrip1.ResumeLayout( false );
        ResumeLayout( false );
    }

    #endregion

    private ListView listView1;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem kickToolStripMenuItem;
    private ToolStripMenuItem banToolStripMenuItem;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Timer timer2;
    private ToolStripMenuItem toolsToolStripMenuItem;
    private ToolStripMenuItem sendMessageToolStripMenuItem;
}