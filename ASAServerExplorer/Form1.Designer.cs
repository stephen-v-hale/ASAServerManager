namespace ASAServerExplorer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            timer1 = new System.Windows.Forms.Timer( components );
            menuStrip1 = new MenuStrip();
            serverToolStripMenuItem = new ToolStripMenuItem();
            listToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            setMessageOfTheDayToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            playersToolStripMenuItem = new ToolStripMenuItem();
            disconnectToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            panel2 = new Panel();
            textBox1 = new TextBox();
            button1 = new Button();
            comboBox1 = new ComboBox();
            timer2 = new System.Windows.Forms.Timer( components );
            richTextBox1 = new RichTextBox();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.WhiteSmoke;
            statusStrip1.Items.AddRange( new ToolStripItem[] { toolStripStatusLabel2, toolStripStatusLabel1 } );
            statusStrip1.Location = new Point( 0, 376 );
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size( 557, 22 );
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size( 0, 17 );
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size( 96, 17 );
            toolStripStatusLabel1.Text = "Connect Ready...";
            toolStripStatusLabel1.Click +=  toolStripStatusLabel1_Click ;
            toolStripStatusLabel1.RightToLeftChanged +=  toolStripStatusLabel1_RightToLeftChanged ;
            toolStripStatusLabel1.TextChanged +=  toolStripStatusLabel1_TextChanged ;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick +=  timer1_Tick ;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange( new ToolStripItem[] { serverToolStripMenuItem, playersToolStripMenuItem, disconnectToolStripMenuItem } );
            menuStrip1.Location = new Point( 0, 0 );
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Size = new Size( 557, 24 );
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // serverToolStripMenuItem
            // 
            serverToolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { listToolStripMenuItem, toolStripMenuItem1, toolsToolStripMenuItem, closeToolStripMenuItem } );
            serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            serverToolStripMenuItem.Size = new Size( 51, 20 );
            serverToolStripMenuItem.Text = "&Server";
            // 
            // listToolStripMenuItem
            // 
            listToolStripMenuItem.Image = Properties.Resources.servers;
            listToolStripMenuItem.Name = "listToolStripMenuItem";
            listToolStripMenuItem.ShortcutKeys =    Keys.Control  |  Keys.S ;
            listToolStripMenuItem.Size = new Size( 180, 22 );
            listToolStripMenuItem.Text = "List";
            listToolStripMenuItem.Click +=  listToolStripMenuItem_Click ;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size( 177, 6 );
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { setMessageOfTheDayToolStripMenuItem } );
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size( 180, 22 );
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // setMessageOfTheDayToolStripMenuItem
            // 
            setMessageOfTheDayToolStripMenuItem.Name = "setMessageOfTheDayToolStripMenuItem";
            setMessageOfTheDayToolStripMenuItem.Size = new Size( 189, 22 );
            setMessageOfTheDayToolStripMenuItem.Text = "SetMessageOfTheDay";
            setMessageOfTheDayToolStripMenuItem.Click +=  setMessageOfTheDayToolStripMenuItem_Click ;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size( 180, 22 );
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click +=  closeToolStripMenuItem_Click ;
            // 
            // playersToolStripMenuItem
            // 
            playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            playersToolStripMenuItem.Size = new Size( 56, 20 );
            playersToolStripMenuItem.Text = "Players";
            playersToolStripMenuItem.Click +=  playersToolStripMenuItem_Click ;
            // 
            // disconnectToolStripMenuItem
            // 
            disconnectToolStripMenuItem.Image = Properties.Resources.cross;
            disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            disconnectToolStripMenuItem.Size = new Size( 94, 20 );
            disconnectToolStripMenuItem.Text = "&Disconnect";
            disconnectToolStripMenuItem.Click +=  disconnectToolStripMenuItem_Click ;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add( panel2 );
            panel1.Controls.Add( textBox1 );
            panel1.Controls.Add( button1 );
            panel1.Controls.Add( comboBox1 );
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point( 0, 346 );
            panel1.Name = "panel1";
            panel1.Size = new Size( 557, 30 );
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ScrollBar;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point( 0, 0 );
            panel2.Name = "panel2";
            panel2.Size = new Size( 557, 1 );
            panel2.TabIndex = 9;
            // 
            // textBox1
            // 
            textBox1.Location = new Point( 145, 4 );
            textBox1.Name = "textBox1";
            textBox1.Size = new Size( 345, 23 );
            textBox1.TabIndex = 7;
            // 
            // button1
            // 
            button1.Location = new Point( 494, 2 );
            button1.Name = "button1";
            button1.Size = new Size( 58, 27 );
            button1.TabIndex = 8;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = true;
            button1.Click +=  button1_Click_1 ;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point( 3, 4 );
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size( 136, 23 );
            comboBox1.TabIndex = 7;
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Interval = 1;
            timer2.Tick +=  timer2_Tick ;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.White;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font( "Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point,   0 );
            richTextBox1.Location = new Point( 0, 24 );
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size( 557, 322 );
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            richTextBox1.TextChanged +=  richTextBox1_TextChanged ;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF( 7F, 15F );
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size( 557, 398 );
            Controls.Add( richTextBox1 );
            Controls.Add( panel1 );
            Controls.Add( statusStrip1 );
            Controls.Add( menuStrip1 );
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = ( Icon )resources.GetObject( "$this.Icon" );
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "ASA Server Manager";
            FormClosed +=  Form1_FormClosed ;
            Load +=  Form1_Load ;
            statusStrip1.ResumeLayout( false );
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout( false );
            menuStrip1.PerformLayout();
            panel1.ResumeLayout( false );
            panel1.PerformLayout();
            ResumeLayout( false );
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer timer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem serverToolStripMenuItem;
        private ToolStripMenuItem listToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem disconnectToolStripMenuItem;
        private Panel panel1;
        private TextBox textBox1;
        private Button button1;
        private ComboBox comboBox1;
        private System.Windows.Forms.Timer timer2;
        private RichTextBox richTextBox1;
        private Panel panel2;
        private ToolStripMenuItem playersToolStripMenuItem;
        public ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem setMessageOfTheDayToolStripMenuItem;
    }
}
