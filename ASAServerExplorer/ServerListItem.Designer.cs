namespace ASAServerExplorer;

partial class ServerListItem
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
        label1 = new Label();
        textBox1 = new TextBox();
        label2 = new Label();
        label3 = new Label();
        textBox2 = new TextBox();
        label4 = new Label();
        textBox3 = new TextBox();
        panel1 = new Panel();
        button2 = new Button();
        button1 = new Button();
        panel2 = new Panel();
        textBox4 = new TextBox();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point( 12, 38 );
        label1.Name = "label1";
        label1.Size = new Size( 52, 15 );
        label1.TabIndex = 0;
        label1.Text = "Address:";
        // 
        // textBox1
        // 
        textBox1.Location = new Point( 70, 35 );
        textBox1.Name = "textBox1";
        textBox1.Size = new Size( 165, 23 );
        textBox1.TabIndex = 1;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point( 241, 38 );
        label2.Name = "label2";
        label2.Size = new Size( 32, 15 );
        label2.TabIndex = 2;
        label2.Text = "Port:";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point( 19, 12 );
        label3.Name = "label3";
        label3.Size = new Size( 45, 15 );
        label3.TabIndex = 4;
        label3.Text = "Name: ";
        // 
        // textBox2
        // 
        textBox2.Location = new Point( 70, 9 );
        textBox2.Name = "textBox2";
        textBox2.Size = new Size( 252, 23 );
        textBox2.TabIndex = 5;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point( 1, 64 );
        label4.Name = "label4";
        label4.Size = new Size( 63, 15 );
        label4.TabIndex = 6;
        label4.Text = "Password: ";
        // 
        // textBox3
        // 
        textBox3.Location = new Point( 70, 61 );
        textBox3.Name = "textBox3";
        textBox3.Size = new Size( 252, 23 );
        textBox3.TabIndex = 7;
        textBox3.UseSystemPasswordChar = true;
        // 
        // panel1
        // 
        panel1.BackColor = Color.WhiteSmoke;
        panel1.Controls.Add( button2 );
        panel1.Controls.Add( button1 );
        panel1.Controls.Add( panel2 );
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point( 0, 93 );
        panel1.Name = "panel1";
        panel1.Size = new Size( 334, 36 );
        panel1.TabIndex = 8;
        // 
        // button2
        // 
        button2.Location = new Point( 170, 5 );
        button2.Name = "button2";
        button2.Size = new Size( 75, 23 );
        button2.TabIndex = 1;
        button2.Text = "Add";
        button2.UseVisualStyleBackColor = true;
        button2.Click +=  button2_Click ;
        // 
        // button1
        // 
        button1.Location = new Point( 251, 5 );
        button1.Name = "button1";
        button1.Size = new Size( 75, 23 );
        button1.TabIndex = 1;
        button1.Text = "Cancel";
        button1.UseVisualStyleBackColor = true;
        button1.Click +=  button1_Click ;
        // 
        // panel2
        // 
        panel2.BackColor = SystemColors.ScrollBar;
        panel2.Dock = DockStyle.Top;
        panel2.Location = new Point( 0, 0 );
        panel2.Name = "panel2";
        panel2.Size = new Size( 334, 1 );
        panel2.TabIndex = 0;
        // 
        // textBox4
        // 
        textBox4.Location = new Point( 279, 35 );
        textBox4.Name = "textBox4";
        textBox4.Size = new Size( 43, 23 );
        textBox4.TabIndex = 9;
        // 
        // ServerListItem
        // 
        AutoScaleDimensions = new SizeF( 7F, 15F );
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size( 334, 129 );
        Controls.Add( textBox4 );
        Controls.Add( panel1 );
        Controls.Add( textBox3 );
        Controls.Add( label4 );
        Controls.Add( textBox2 );
        Controls.Add( label3 );
        Controls.Add( label2 );
        Controls.Add( textBox1 );
        Controls.Add( label1 );
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Text = "Add Server";
        panel1.ResumeLayout( false );
        ResumeLayout( false );
        PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox textBox1;
    private Label label2;
    private Label label3;
    private TextBox textBox2;
    private Label label4;
    private TextBox textBox3;
    private Panel panel1;
    private Button button2;
    private Button button1;
    private Panel panel2;
    private TextBox textBox4;
}