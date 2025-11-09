namespace ASAServerExplorer;

partial class ServerList
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
        TreeNode treeNode1 = new TreeNode("Servers", 1, 1);
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerList));
        treeView1 = new TreeView();
        contextMenuStrip1 = new ContextMenuStrip( components );
        addServerToolStripMenuItem = new ToolStripMenuItem();
        removeToolStripMenuItem = new ToolStripMenuItem();
        imageList1 = new ImageList( components );
        panel1 = new Panel();
        button3 = new Button();
        button2 = new Button();
        button1 = new Button();
        textBox2 = new TextBox();
        label3 = new Label();
        numericUpDown1 = new NumericUpDown();
        textBox1 = new TextBox();
        label2 = new Label();
        panel2 = new Panel();
        panel3 = new Panel();
        label1 = new Label();
        timer1 = new System.Windows.Forms.Timer( components );
        contextMenuStrip1.SuspendLayout();
        panel1.SuspendLayout();
        ( ( System.ComponentModel.ISupportInitialize )numericUpDown1 ).BeginInit();
        panel2.SuspendLayout();
        SuspendLayout();
        // 
        // treeView1
        // 
        treeView1.ContextMenuStrip = contextMenuStrip1;
        treeView1.ImageIndex = 0;
        treeView1.ImageList = imageList1;
        treeView1.Location = new Point( 12, 12 );
        treeView1.Name = "treeView1";
        treeNode1.ImageIndex = 1;
        treeNode1.Name = "Node0";
        treeNode1.SelectedImageIndex = 1;
        treeNode1.Text = "Servers";
        treeView1.Nodes.AddRange( new TreeNode[] { treeNode1 } );
        treeView1.SelectedImageIndex = 0;
        treeView1.Size = new Size( 204, 426 );
        treeView1.TabIndex = 0;
        treeView1.AfterSelect +=  treeView1_AfterSelect ;
        treeView1.NodeMouseDoubleClick +=  treeView1_NodeMouseDoubleClick ;
        treeView1.DoubleClick +=  treeView1_DoubleClick ;
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Items.AddRange( new ToolStripItem[] { addServerToolStripMenuItem, removeToolStripMenuItem } );
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new Size( 132, 48 );
        contextMenuStrip1.Opening +=  contextMenuStrip1_Opening ;
        // 
        // addServerToolStripMenuItem
        // 
        addServerToolStripMenuItem.Name = "addServerToolStripMenuItem";
        addServerToolStripMenuItem.Size = new Size( 131, 22 );
        addServerToolStripMenuItem.Text = "&Add Server";
        addServerToolStripMenuItem.Click +=  addServerToolStripMenuItem_Click ;
        // 
        // removeToolStripMenuItem
        // 
        removeToolStripMenuItem.Name = "removeToolStripMenuItem";
        removeToolStripMenuItem.Size = new Size( 131, 22 );
        removeToolStripMenuItem.Text = "&Remove";
        removeToolStripMenuItem.Click +=  removeToolStripMenuItem_Click ;
        // 
        // imageList1
        // 
        imageList1.ColorDepth = ColorDepth.Depth32Bit;
        imageList1.ImageStream = ( ImageListStreamer )resources.GetObject( "imageList1.ImageStream" );
        imageList1.TransparentColor = Color.Transparent;
        imageList1.Images.SetKeyName( 0, "server-connection.png" );
        imageList1.Images.SetKeyName( 1, "servers.png" );
        // 
        // panel1
        // 
        panel1.BackColor = Color.White;
        panel1.Controls.Add( button3 );
        panel1.Controls.Add( button2 );
        panel1.Controls.Add( button1 );
        panel1.Controls.Add( textBox2 );
        panel1.Controls.Add( label3 );
        panel1.Controls.Add( numericUpDown1 );
        panel1.Controls.Add( textBox1 );
        panel1.Controls.Add( label2 );
        panel1.Controls.Add( panel2 );
        panel1.Location = new Point( 222, 11 );
        panel1.Name = "panel1";
        panel1.Size = new Size( 290, 426 );
        panel1.TabIndex = 1;
        panel1.Visible = false;
        // 
        // button3
        // 
        button3.Location = new Point( 157, 108 );
        button3.Name = "button3";
        button3.Size = new Size( 55, 25 );
        button3.TabIndex = 9;
        button3.Text = "Delete";
        button3.UseVisualStyleBackColor = true;
        button3.Click +=  button3_Click ;
        // 
        // button2
        // 
        button2.Location = new Point( 68, 108 );
        button2.Name = "button2";
        button2.Size = new Size( 83, 25 );
        button2.TabIndex = 8;
        button2.Text = "Connect";
        button2.UseVisualStyleBackColor = true;
        button2.Click +=  button2_Click ;
        // 
        // button1
        // 
        button1.Enabled = false;
        button1.Location = new Point( 218, 108 );
        button1.Name = "button1";
        button1.Size = new Size( 63, 25 );
        button1.TabIndex = 7;
        button1.Text = "Save";
        button1.UseVisualStyleBackColor = true;
        button1.Click +=  button1_Click ;
        // 
        // textBox2
        // 
        textBox2.Location = new Point( 68, 79 );
        textBox2.Name = "textBox2";
        textBox2.Size = new Size( 213, 23 );
        textBox2.TabIndex = 6;
        textBox2.UseSystemPasswordChar = true;
        textBox2.TextChanged +=  textBox2_TextChanged ;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point( 5, 83 );
        label3.Name = "label3";
        label3.Size = new Size( 60, 15 );
        label3.TabIndex = 5;
        label3.Text = "Password:";
        // 
        // numericUpDown1
        // 
        numericUpDown1.Location = new Point( 241, 51 );
        numericUpDown1.Maximum = new decimal( new int[] { 9999999, 0, 0, 0 } );
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new Size( 40, 23 );
        numericUpDown1.TabIndex = 4;
        numericUpDown1.ValueChanged +=  numericUpDown1_ValueChanged ;
        // 
        // textBox1
        // 
        textBox1.Location = new Point( 68, 51 );
        textBox1.Name = "textBox1";
        textBox1.Size = new Size( 167, 23 );
        textBox1.TabIndex = 3;
        textBox1.TextChanged +=  textBox1_TextChanged ;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point( 13, 55 );
        label2.Name = "label2";
        label2.Size = new Size( 52, 15 );
        label2.TabIndex = 2;
        label2.Text = "Address:";
        // 
        // panel2
        // 
        panel2.BackColor = Color.White;
        panel2.Controls.Add( panel3 );
        panel2.Controls.Add( label1 );
        panel2.Location = new Point( 3, 3 );
        panel2.Name = "panel2";
        panel2.Size = new Size( 284, 45 );
        panel2.TabIndex = 1;
        // 
        // panel3
        // 
        panel3.BackColor = Color.Black;
        panel3.Location = new Point( 0, 34 );
        panel3.Name = "panel3";
        panel3.Size = new Size( 284, 2 );
        panel3.TabIndex = 9;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font( "Yu Gothic UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point,   0 );
        label1.ForeColor = Color.Black;
        label1.Location = new Point( -3, -11 );
        label1.Name = "label1";
        label1.Size = new Size( 269, 37 );
        label1.TabIndex = 0;
        label1.Text = "{ArkServerListName}";
        // 
        // timer1
        // 
        timer1.Enabled = true;
        timer1.Interval = 5;
        timer1.Tick +=  timer1_Tick ;
        // 
        // ServerList
        // 
        AutoScaleDimensions = new SizeF( 7F, 15F );
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size( 554, 450 );
        Controls.Add( panel1 );
        Controls.Add( treeView1 );
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = ( Icon )resources.GetObject( "$this.Icon" );
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ServerList";
        Text = "ServerList";
        contextMenuStrip1.ResumeLayout( false );
        panel1.ResumeLayout( false );
        panel1.PerformLayout();
        ( ( System.ComponentModel.ISupportInitialize )numericUpDown1 ).EndInit();
        panel2.ResumeLayout( false );
        panel2.PerformLayout();
        ResumeLayout( false );
    }

    #endregion

    private TreeView treeView1;
    private Panel panel1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem addServerToolStripMenuItem;
    private ToolStripMenuItem removeToolStripMenuItem;
    private System.Windows.Forms.Timer timer1;
    private Label label2;
    private Panel panel2;
    private Label label1;
    private Button button1;
    private TextBox textBox2;
    private Label label3;
    private NumericUpDown numericUpDown1;
    private TextBox textBox1;
    private Button button2;
    private Panel panel3;
    private ImageList imageList1;
    private Button button3;
}