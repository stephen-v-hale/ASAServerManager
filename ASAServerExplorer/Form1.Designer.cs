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
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel4 = new ToolStripStatusLabel();
            toolStripStatusLabel5 = new ToolStripStatusLabel();
            timer1 = new System.Windows.Forms.Timer( components );
            menuStrip1 = new MenuStrip();
            serverToolStripMenuItem = new ToolStripMenuItem();
            listToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            setMessageOfTheDayToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
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
            timer3 = new System.Windows.Forms.Timer( components );
            notifyIcon1 = new NotifyIcon( components );
            contextMenuStrip1 = new ContextMenuStrip( components );
            showWindowToolStripMenuItem = new ToolStripMenuItem();
            serversListToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            statusToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            disconnectToolStripMenuItem1 = new ToolStripMenuItem();
            broadcastToolStripMenuItem = new ToolStripMenuItem();
            playersToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            panel3 = new Panel();
            button2 = new Button();
            numericUpDown1 = new NumericUpDown();
            label3 = new Label();
            textBox3 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label1 = new Label();
            panel4 = new Panel();
            timer4 = new System.Windows.Forms.Timer( components );
            panel5 = new Panel();
            panel8 = new Panel();
            panel13 = new Panel();
            panel7 = new Panel();
            button3 = new Button();
            label4 = new Label();
            panel6 = new Panel();
            panel9 = new Panel();
            panel10 = new Panel();
            panel11 = new Panel();
            panel12 = new Panel();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            panel3.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )numericUpDown1 ).BeginInit();
            panel5.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.WhiteSmoke;
            statusStrip1.Items.AddRange( new ToolStripItem[] { toolStripStatusLabel2, toolStripStatusLabel1, toolStripStatusLabel3, toolStripStatusLabel4, toolStripStatusLabel5 } );
            statusStrip1.Location = new Point( 0, 376 );
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size( 639, 22 );
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
            toolStripStatusLabel1.Size = new Size( 39, 17 );
            toolStripStatusLabel1.Text = "Ready";
            toolStripStatusLabel1.Click +=  toolStripStatusLabel1_Click ;
            toolStripStatusLabel1.RightToLeftChanged +=  toolStripStatusLabel1_RightToLeftChanged ;
            toolStripStatusLabel1.TextChanged +=  toolStripStatusLabel1_TextChanged ;
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size( 0, 17 );
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new Size( 0, 17 );
            // 
            // toolStripStatusLabel5
            // 
            toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            toolStripStatusLabel5.Size = new Size( 0, 17 );
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
            menuStrip1.Size = new Size( 639, 24 );
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.Paint +=  menuStrip1_Paint ;
            // 
            // serverToolStripMenuItem
            // 
            serverToolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { listToolStripMenuItem, toolStripMenuItem5, toolsToolStripMenuItem, toolStripMenuItem1, closeToolStripMenuItem } );
            serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            serverToolStripMenuItem.Size = new Size( 37, 20 );
            serverToolStripMenuItem.Text = "&File";
            // 
            // listToolStripMenuItem
            // 
            listToolStripMenuItem.AutoToolTip = true;
            listToolStripMenuItem.Image = Properties.Resources.servers;
            listToolStripMenuItem.Name = "listToolStripMenuItem";
            listToolStripMenuItem.ShortcutKeys =    Keys.Control  |  Keys.S ;
            listToolStripMenuItem.Size = new Size( 169, 22 );
            listToolStripMenuItem.Text = "ServersList";
            listToolStripMenuItem.ToolTipText = "Show the saved servers.";
            listToolStripMenuItem.Click +=  listToolStripMenuItem_Click ;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.AutoToolTip = true;
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.ShortcutKeys =    Keys.Control  |  Keys.C ;
            toolStripMenuItem5.Size = new Size( 169, 22 );
            toolStripMenuItem5.Text = "Clear";
            toolStripMenuItem5.ToolTipText = "Clear the text window";
            toolStripMenuItem5.Click +=  toolStripMenuItem5_Click ;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { setMessageOfTheDayToolStripMenuItem, toolStripMenuItem6 } );
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size( 169, 22 );
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // setMessageOfTheDayToolStripMenuItem
            // 
            setMessageOfTheDayToolStripMenuItem.AutoToolTip = true;
            setMessageOfTheDayToolStripMenuItem.Name = "setMessageOfTheDayToolStripMenuItem";
            setMessageOfTheDayToolStripMenuItem.Size = new Size( 189, 22 );
            setMessageOfTheDayToolStripMenuItem.Text = "SetMessageOfTheDay";
            setMessageOfTheDayToolStripMenuItem.ToolTipText = "Set the message of the day of the currently connected server.";
            setMessageOfTheDayToolStripMenuItem.Click +=  setMessageOfTheDayToolStripMenuItem_Click ;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size( 189, 22 );
            toolStripMenuItem6.Text = "Shutdown";
            toolStripMenuItem6.Click +=  toolStripMenuItem6_Click ;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size( 166, 6 );
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.AutoToolTip = true;
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size( 169, 22 );
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.ToolTipText = "Close this window";
            closeToolStripMenuItem.Click +=  closeToolStripMenuItem_Click ;
            // 
            // playersToolStripMenuItem
            // 
            playersToolStripMenuItem.AutoToolTip = true;
            playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            playersToolStripMenuItem.Size = new Size( 56, 20 );
            playersToolStripMenuItem.Text = "Players";
            playersToolStripMenuItem.ToolTipText = "Show the players.";
            playersToolStripMenuItem.Click +=  playersToolStripMenuItem_Click ;
            // 
            // disconnectToolStripMenuItem
            // 
            disconnectToolStripMenuItem.AutoToolTip = true;
            disconnectToolStripMenuItem.Image = Properties.Resources.cross;
            disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            disconnectToolStripMenuItem.Size = new Size( 94, 20 );
            disconnectToolStripMenuItem.Text = "&Disconnect";
            disconnectToolStripMenuItem.ToolTipText = "Disconnect from server.";
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
            panel1.Size = new Size( 639, 30 );
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ScrollBar;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point( 0, 0 );
            panel2.Name = "panel2";
            panel2.Size = new Size( 639, 1 );
            panel2.TabIndex = 9;
            // 
            // textBox1
            // 
            textBox1.Location = new Point( 145, 4 );
            textBox1.Name = "textBox1";
            textBox1.Size = new Size( 427, 23 );
            textBox1.TabIndex = 7;
            // 
            // button1
            // 
            button1.Location = new Point( 578, 2 );
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
            comboBox1.SelectedIndexChanged +=  comboBox1_SelectedIndexChanged ;
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
            richTextBox1.Location = new Point( 178, 58 );
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size( 456, 283 );
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            richTextBox1.TextChanged +=  richTextBox1_TextChanged ;
            // 
            // timer3
            // 
            timer3.Interval = 5000;
            timer3.Tick +=  timer3_Tick ;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = ( Icon )resources.GetObject( "notifyIcon1.Icon" );
            notifyIcon1.Text = "ASA Server Manager";
            notifyIcon1.BalloonTipClicked +=  notifyIcon1_BalloonTipClicked ;
            notifyIcon1.MouseDoubleClick +=  notifyIcon1_MouseDoubleClick ;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange( new ToolStripItem[] { showWindowToolStripMenuItem, serversListToolStripMenuItem, toolStripMenuItem4, statusToolStripMenuItem, toolStripMenuItem2, disconnectToolStripMenuItem1, broadcastToolStripMenuItem, playersToolStripMenuItem1, toolStripMenuItem3, exitToolStripMenuItem } );
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size( 151, 176 );
            // 
            // showWindowToolStripMenuItem
            // 
            showWindowToolStripMenuItem.Name = "showWindowToolStripMenuItem";
            showWindowToolStripMenuItem.Size = new Size( 150, 22 );
            showWindowToolStripMenuItem.Text = "Show Window";
            showWindowToolStripMenuItem.Click +=  showWindowToolStripMenuItem_Click ;
            // 
            // serversListToolStripMenuItem
            // 
            serversListToolStripMenuItem.Name = "serversListToolStripMenuItem";
            serversListToolStripMenuItem.Size = new Size( 150, 22 );
            serversListToolStripMenuItem.Text = "Servers";
            serversListToolStripMenuItem.Click +=  serversListToolStripMenuItem_Click ;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size( 147, 6 );
            // 
            // statusToolStripMenuItem
            // 
            statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            statusToolStripMenuItem.Size = new Size( 150, 22 );
            statusToolStripMenuItem.Text = "{Status}";
            statusToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size( 147, 6 );
            toolStripMenuItem2.Visible = false;
            // 
            // disconnectToolStripMenuItem1
            // 
            disconnectToolStripMenuItem1.Enabled = false;
            disconnectToolStripMenuItem1.Name = "disconnectToolStripMenuItem1";
            disconnectToolStripMenuItem1.Size = new Size( 150, 22 );
            disconnectToolStripMenuItem1.Text = "Disconnect";
            disconnectToolStripMenuItem1.Click +=  disconnectToolStripMenuItem1_Click ;
            // 
            // broadcastToolStripMenuItem
            // 
            broadcastToolStripMenuItem.Enabled = false;
            broadcastToolStripMenuItem.Name = "broadcastToolStripMenuItem";
            broadcastToolStripMenuItem.Size = new Size( 150, 22 );
            broadcastToolStripMenuItem.Text = "Broadcast";
            broadcastToolStripMenuItem.Click +=  broadcastToolStripMenuItem_Click ;
            // 
            // playersToolStripMenuItem1
            // 
            playersToolStripMenuItem1.Enabled = false;
            playersToolStripMenuItem1.Name = "playersToolStripMenuItem1";
            playersToolStripMenuItem1.Size = new Size( 150, 22 );
            playersToolStripMenuItem1.Text = "Players";
            playersToolStripMenuItem1.Click +=  playersToolStripMenuItem1_Click ;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size( 147, 6 );
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size( 150, 22 );
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click +=  exitToolStripMenuItem_Click ;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.Controls.Add( button2 );
            panel3.Controls.Add( numericUpDown1 );
            panel3.Controls.Add( label3 );
            panel3.Controls.Add( textBox3 );
            panel3.Controls.Add( label2 );
            panel3.Controls.Add( textBox2 );
            panel3.Controls.Add( label1 );
            panel3.Controls.Add( panel4 );
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point( 0, 24 );
            panel3.Name = "panel3";
            panel3.Size = new Size( 639, 29 );
            panel3.TabIndex = 8;
            panel3.Paint +=  panel3_Paint ;
            // 
            // button2
            // 
            button2.Anchor =    AnchorStyles.Top  |  AnchorStyles.Right ;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point( 533, 2 );
            button2.Name = "button2";
            button2.Size = new Size( 96, 23 );
            button2.TabIndex = 7;
            button2.Text = "Quick Connect";
            button2.UseVisualStyleBackColor = true;
            button2.Click +=  button2_Click ;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Anchor =     AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            numericUpDown1.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown1.Location = new Point( 482, 2 );
            numericUpDown1.Maximum = new decimal( new int[] { 999999, 0, 0, 0 } );
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size( 46, 23 );
            numericUpDown1.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point( 444, 6 );
            label3.Name = "label3";
            label3.Size = new Size( 32, 15 );
            label3.TabIndex = 5;
            label3.Text = "Port:";
            // 
            // textBox3
            // 
            textBox3.Anchor =     AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Location = new Point( 309, 2 );
            textBox3.Name = "textBox3";
            textBox3.Size = new Size( 129, 23 );
            textBox3.TabIndex = 4;
            textBox3.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point( 243, 6 );
            label2.Name = "label2";
            label2.Size = new Size( 60, 15 );
            label2.TabIndex = 3;
            label2.Text = "Password:";
            // 
            // textBox2
            // 
            textBox2.Anchor =     AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point( 54, 3 );
            textBox2.Name = "textBox2";
            textBox2.Size = new Size( 183, 23 );
            textBox2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point( 10, 7 );
            label1.Name = "label1";
            label1.Size = new Size( 38, 15 );
            label1.TabIndex = 1;
            label1.Text = "Host: ";
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ScrollBar;
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point( 0, 28 );
            panel4.Name = "panel4";
            panel4.Size = new Size( 639, 1 );
            panel4.TabIndex = 0;
            // 
            // timer4
            // 
            timer4.Interval = 1000;
            timer4.Tick +=  timer4_Tick ;
            // 
            // panel5
            // 
            panel5.BackColor = Color.WhiteSmoke;
            panel5.Controls.Add( panel8 );
            panel5.Controls.Add( panel13 );
            panel5.Controls.Add( panel7 );
            panel5.Controls.Add( panel6 );
            panel5.Dock = DockStyle.Left;
            panel5.Location = new Point( 0, 53 );
            panel5.Name = "panel5";
            panel5.Size = new Size( 173, 293 );
            panel5.TabIndex = 9;
            panel5.Visible = false;
            // 
            // panel8
            // 
            panel8.BackColor = Color.White;
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point( 0, 37 );
            panel8.Name = "panel8";
            panel8.Size = new Size( 172, 256 );
            panel8.TabIndex = 3;
            // 
            // panel13
            // 
            panel13.BackColor = SystemColors.ScrollBar;
            panel13.Dock = DockStyle.Top;
            panel13.Location = new Point( 0, 36 );
            panel13.Name = "panel13";
            panel13.Size = new Size( 172, 1 );
            panel13.TabIndex = 2;
            // 
            // panel7
            // 
            panel7.Controls.Add( button3 );
            panel7.Controls.Add( label4 );
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point( 0, 0 );
            panel7.Name = "panel7";
            panel7.Size = new Size( 172, 36 );
            panel7.TabIndex = 2;
            panel7.Paint +=  panel7_Paint ;
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font( "Webdings", 9F, FontStyle.Regular, GraphicsUnit.Point,   2 );
            button3.Location = new Point( 141, 5 );
            button3.Name = "button3";
            button3.Size = new Size( 28, 27 );
            button3.TabIndex = 1;
            button3.Text = "r";
            button3.UseVisualStyleBackColor = false;
            button3.Click +=  button3_Click_1 ;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font( "Segoe UI", 14F );
            label4.Location = new Point( 5, 4 );
            label4.Name = "label4";
            label4.Size = new Size( 72, 25 );
            label4.TabIndex = 0;
            label4.Text = "Players";
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.ScrollBar;
            panel6.Dock = DockStyle.Right;
            panel6.Location = new Point( 172, 0 );
            panel6.Name = "panel6";
            panel6.Size = new Size( 1, 293 );
            panel6.TabIndex = 1;
            // 
            // panel9
            // 
            panel9.Dock = DockStyle.Left;
            panel9.Location = new Point( 173, 53 );
            panel9.Name = "panel9";
            panel9.Size = new Size( 5, 293 );
            panel9.TabIndex = 10;
            // 
            // panel10
            // 
            panel10.Dock = DockStyle.Top;
            panel10.Location = new Point( 178, 53 );
            panel10.Name = "panel10";
            panel10.Size = new Size( 461, 5 );
            panel10.TabIndex = 11;
            // 
            // panel11
            // 
            panel11.Dock = DockStyle.Bottom;
            panel11.Location = new Point( 178, 341 );
            panel11.Name = "panel11";
            panel11.Size = new Size( 461, 5 );
            panel11.TabIndex = 12;
            // 
            // panel12
            // 
            panel12.Dock = DockStyle.Right;
            panel12.Location = new Point( 634, 58 );
            panel12.Name = "panel12";
            panel12.Size = new Size( 5, 283 );
            panel12.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF( 7F, 15F );
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size( 639, 398 );
            Controls.Add( richTextBox1 );
            Controls.Add( panel12 );
            Controls.Add( panel11 );
            Controls.Add( panel10 );
            Controls.Add( panel9 );
            Controls.Add( panel5 );
            Controls.Add( panel3 );
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
            FormClosing +=  Form1_FormClosing ;
            FormClosed +=  Form1_FormClosed ;
            Load +=  Form1_Load ;
            Shown +=  Form1_Shown ;
            statusStrip1.ResumeLayout( false );
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout( false );
            menuStrip1.PerformLayout();
            panel1.ResumeLayout( false );
            panel1.PerformLayout();
            contextMenuStrip1.ResumeLayout( false );
            panel3.ResumeLayout( false );
            panel3.PerformLayout();
            ( ( System.ComponentModel.ISupportInitialize )numericUpDown1 ).EndInit();
            panel5.ResumeLayout( false );
            panel7.ResumeLayout( false );
            panel7.PerformLayout();
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
        private System.Windows.Forms.Timer timer3;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem statusToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem broadcastToolStripMenuItem;
        private ToolStripMenuItem playersToolStripMenuItem1;
        private ToolStripMenuItem showWindowToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem serversListToolStripMenuItem;
        private ToolStripMenuItem disconnectToolStripMenuItem1;
        public NotifyIcon notifyIcon1;
        private Panel panel3;
        private Panel panel4;
        private Label label2;
        private TextBox textBox2;
        private Label label1;
        private Button button2;
        private NumericUpDown numericUpDown1;
        private Label label3;
        private TextBox textBox3;
        private System.Windows.Forms.Timer timer4;
        private Panel panel5;
        private Label label4;
        private Panel panel6;
        private Panel panel7;
        private Button button3;
        private Panel panel8;
        private Panel panel9;
        private Panel panel10;
        private Panel panel11;
        private Panel panel12;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private Panel panel13;
    }
}
