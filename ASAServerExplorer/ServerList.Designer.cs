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
        panel2 = new Panel();
        groupBox2 = new GroupBox();
        button5 = new Button();
        button4 = new Button();
        listBox1 = new ListBox();
        numericUpDown2 = new NumericUpDown();
        label1 = new Label();
        groupBox1 = new GroupBox();
        checkBox3 = new CheckBox();
        checkBox5 = new CheckBox();
        textBox1 = new TextBox();
        checkBox2 = new CheckBox();
        label2 = new Label();
        checkBox1 = new CheckBox();
        numericUpDown1 = new NumericUpDown();
        button3 = new Button();
        label3 = new Label();
        button2 = new Button();
        textBox2 = new TextBox();
        button1 = new Button();
        timer1 = new System.Windows.Forms.Timer( components );
        timer2 = new System.Windows.Forms.Timer( components );
        contextMenuStrip1.SuspendLayout();
        panel1.SuspendLayout();
        groupBox2.SuspendLayout();
        ( ( System.ComponentModel.ISupportInitialize )numericUpDown2 ).BeginInit();
        groupBox1.SuspendLayout();
        ( ( System.ComponentModel.ISupportInitialize )numericUpDown1 ).BeginInit();
        SuspendLayout();
        // 
        // treeView1
        // 
        treeView1.BorderStyle = BorderStyle.None;
        treeView1.ContextMenuStrip = contextMenuStrip1;
        treeView1.Dock = DockStyle.Left;
        treeView1.ImageIndex = 0;
        treeView1.ImageList = imageList1;
        treeView1.Location = new Point( 0, 0 );
        treeView1.Name = "treeView1";
        treeNode1.ImageIndex = 1;
        treeNode1.Name = "Node0";
        treeNode1.SelectedImageIndex = 1;
        treeNode1.Text = "Servers";
        treeView1.Nodes.AddRange( new TreeNode[] { treeNode1 } );
        treeView1.SelectedImageIndex = 0;
        treeView1.Size = new Size( 204, 201 );
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
        panel1.BackColor = Color.WhiteSmoke;
        panel1.Controls.Add( panel2 );
        panel1.Controls.Add( groupBox2 );
        panel1.Controls.Add( groupBox1 );
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point( 204, 0 );
        panel1.Name = "panel1";
        panel1.Size = new Size( 297, 201 );
        panel1.TabIndex = 1;
        panel1.Visible = false;
        // 
        // panel2
        // 
        panel2.BackColor = SystemColors.ScrollBar;
        panel2.Dock = DockStyle.Left;
        panel2.Location = new Point( 0, 0 );
        panel2.Name = "panel2";
        panel2.Size = new Size( 1, 201 );
        panel2.TabIndex = 14;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add( button5 );
        groupBox2.Controls.Add( button4 );
        groupBox2.Controls.Add( listBox1 );
        groupBox2.Controls.Add( numericUpDown2 );
        groupBox2.Controls.Add( label1 );
        groupBox2.Location = new Point( 3, 199 );
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size( 286, 187 );
        groupBox2.TabIndex = 13;
        groupBox2.TabStop = false;
        groupBox2.Text = "Message broading settings";
        // 
        // button5
        // 
        button5.Location = new Point( 202, 158 );
        button5.Name = "button5";
        button5.Size = new Size( 75, 23 );
        button5.TabIndex = 63;
        button5.Text = "Remove";
        button5.UseVisualStyleBackColor = true;
        button5.Click +=  button5_Click ;
        // 
        // button4
        // 
        button4.Location = new Point( 121, 158 );
        button4.Name = "button4";
        button4.Size = new Size( 75, 23 );
        button4.TabIndex = 62;
        button4.Text = "Add";
        button4.UseVisualStyleBackColor = true;
        button4.Click +=  button4_Click ;
        // 
        // listBox1
        // 
        listBox1.FormattingEnabled = true;
        listBox1.Location = new Point( 10, 44 );
        listBox1.Name = "listBox1";
        listBox1.Size = new Size( 268, 109 );
        listBox1.TabIndex = 61;
        listBox1.SelectedIndexChanged +=  listBox1_SelectedIndexChanged ;
        // 
        // numericUpDown2
        // 
        numericUpDown2.Location = new Point( 149, 19 );
        numericUpDown2.Maximum = new decimal( new int[] { 60, 0, 0, 0 } );
        numericUpDown2.Name = "numericUpDown2";
        numericUpDown2.Size = new Size( 128, 23 );
        numericUpDown2.TabIndex = 60;
        numericUpDown2.ValueChanged +=  numericUpDown2_ValueChanged ;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point( 10, 22 );
        label1.Name = "label1";
        label1.Size = new Size( 134, 15 );
        label1.TabIndex = 0;
        label1.Text = "Time Interval (Seconds):";
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add( checkBox3 );
        groupBox1.Controls.Add( checkBox5 );
        groupBox1.Controls.Add( textBox1 );
        groupBox1.Controls.Add( checkBox2 );
        groupBox1.Controls.Add( label2 );
        groupBox1.Controls.Add( checkBox1 );
        groupBox1.Controls.Add( numericUpDown1 );
        groupBox1.Controls.Add( button3 );
        groupBox1.Controls.Add( label3 );
        groupBox1.Controls.Add( button2 );
        groupBox1.Controls.Add( textBox2 );
        groupBox1.Controls.Add( button1 );
        groupBox1.Location = new Point( 6, 3 );
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size( 283, 196 );
        groupBox1.TabIndex = 12;
        groupBox1.TabStop = false;
        groupBox1.Text = "groupBox1";
        // 
        // checkBox3
        // 
        checkBox3.AutoSize = true;
        checkBox3.Location = new Point( 65, 138 );
        checkBox3.Name = "checkBox3";
        checkBox3.Size = new Size( 98, 19 );
        checkBox3.TabIndex = 13;
        checkBox3.Text = "Enable cheats";
        checkBox3.UseVisualStyleBackColor = true;
        checkBox3.CheckedChanged +=  checkBox3_CheckedChanged ;
        // 
        // checkBox5
        // 
        checkBox5.AutoSize = true;
        checkBox5.Location = new Point( 65, 117 );
        checkBox5.Name = "checkBox5";
        checkBox5.Size = new Size( 202, 19 );
        checkBox5.TabIndex = 12;
        checkBox5.Text = "Message broadcaster (may crash)";
        checkBox5.UseVisualStyleBackColor = true;
        checkBox5.CheckedChanged +=  checkBox5_CheckedChanged ;
        // 
        // textBox1
        // 
        textBox1.Location = new Point( 65, 22 );
        textBox1.Name = "textBox1";
        textBox1.Size = new Size( 167, 23 );
        textBox1.TabIndex = 3;
        textBox1.TextChanged +=  textBox1_TextChanged ;
        // 
        // checkBox2
        // 
        checkBox2.AutoSize = true;
        checkBox2.Location = new Point( 65, 97 );
        checkBox2.Name = "checkBox2";
        checkBox2.Size = new Size( 204, 19 );
        checkBox2.TabIndex = 11;
        checkBox2.Text = "Show realtime player connectivity";
        checkBox2.UseVisualStyleBackColor = true;
        checkBox2.CheckedChanged +=  checkBox2_CheckedChanged ;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point( 10, 26 );
        label2.Name = "label2";
        label2.Size = new Size( 52, 15 );
        label2.TabIndex = 2;
        label2.Text = "Address:";
        // 
        // checkBox1
        // 
        checkBox1.AutoSize = true;
        checkBox1.Location = new Point( 65, 79 );
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new Size( 154, 19 );
        checkBox1.TabIndex = 10;
        checkBox1.Text = "Show realtime game log";
        checkBox1.UseVisualStyleBackColor = true;
        checkBox1.CheckedChanged +=  checkBox1_CheckedChanged ;
        // 
        // numericUpDown1
        // 
        numericUpDown1.Location = new Point( 238, 22 );
        numericUpDown1.Maximum = new decimal( new int[] { 9999999, 0, 0, 0 } );
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new Size( 40, 23 );
        numericUpDown1.TabIndex = 4;
        numericUpDown1.ValueChanged +=  numericUpDown1_ValueChanged ;
        // 
        // button3
        // 
        button3.Location = new Point( 154, 161 );
        button3.Name = "button3";
        button3.Size = new Size( 55, 25 );
        button3.TabIndex = 9;
        button3.Text = "Delete";
        button3.UseVisualStyleBackColor = true;
        button3.Click +=  button3_Click ;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point( 2, 54 );
        label3.Name = "label3";
        label3.Size = new Size( 60, 15 );
        label3.TabIndex = 5;
        label3.Text = "Password:";
        // 
        // button2
        // 
        button2.Location = new Point( 65, 161 );
        button2.Name = "button2";
        button2.Size = new Size( 83, 25 );
        button2.TabIndex = 8;
        button2.Text = "Connect";
        button2.UseVisualStyleBackColor = true;
        button2.Click +=  button2_Click ;
        // 
        // textBox2
        // 
        textBox2.Location = new Point( 65, 50 );
        textBox2.Name = "textBox2";
        textBox2.Size = new Size( 213, 23 );
        textBox2.TabIndex = 6;
        textBox2.UseSystemPasswordChar = true;
        textBox2.TextChanged +=  textBox2_TextChanged ;
        // 
        // button1
        // 
        button1.Enabled = false;
        button1.Location = new Point( 215, 161 );
        button1.Name = "button1";
        button1.Size = new Size( 63, 25 );
        button1.TabIndex = 7;
        button1.Text = "Save";
        button1.UseVisualStyleBackColor = true;
        button1.Click +=  button1_Click ;
        // 
        // timer1
        // 
        timer1.Interval = 5;
        timer1.Tick +=  timer1_Tick ;
        // 
        // timer2
        // 
        timer2.Interval = 1000;
        // 
        // ServerList
        // 
        AutoScaleDimensions = new SizeF( 7F, 15F );
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size( 501, 201 );
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
        groupBox2.ResumeLayout( false );
        groupBox2.PerformLayout();
        ( ( System.ComponentModel.ISupportInitialize )numericUpDown2 ).EndInit();
        groupBox1.ResumeLayout( false );
        groupBox1.PerformLayout();
        ( ( System.ComponentModel.ISupportInitialize )numericUpDown1 ).EndInit();
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
    private Button button1;
    private TextBox textBox2;
    private Label label3;
    private NumericUpDown numericUpDown1;
    private TextBox textBox1;
    private Button button2;
    private ImageList imageList1;
    private Button button3;
    private CheckBox checkBox2;
    private CheckBox checkBox1;
    private GroupBox groupBox2;
    private GroupBox groupBox1;
    private CheckBox checkBox5;
    private Button button5;
    private Button button4;
    private ListBox listBox1;
    private NumericUpDown numericUpDown2;
    private Label label1;
    private System.Windows.Forms.Timer timer2;
    private CheckBox checkBox3;
    private Panel panel2;
}