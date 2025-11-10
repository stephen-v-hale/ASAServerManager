namespace ASAServerExplorer;

partial class PlayerUserControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        button1 = new Button();
        button2 = new Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font( "Segoe UI", 10F );
        label1.Location = new Point( 9, 4 );
        label1.Name = "label1";
        label1.Size = new Size( 53, 19 );
        label1.TabIndex = 0;
        label1.Text = "{Name}";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.ForeColor = Color.Gray;
        label2.Location = new Point( 11, 32 );
        label2.Name = "label2";
        label2.Size = new Size( 49, 15 );
        label2.TabIndex = 1;
        label2.Text = "ASA ID: ";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.ForeColor = Color.Gray;
        label3.Location = new Point( 61, 32 );
        label3.Name = "label3";
        label3.Size = new Size( 25, 15 );
        label3.TabIndex = 2;
        label3.Text = "{id}";
        // 
        // button1
        // 
        button1.Anchor =    AnchorStyles.Top  |  AnchorStyles.Right ;
        button1.Location = new Point( 88, 51 );
        button1.Name = "button1";
        button1.Size = new Size( 75, 23 );
        button1.TabIndex = 3;
        button1.Text = "Kick";
        button1.UseVisualStyleBackColor = true;
        button1.Click +=  button1_Click ;
        // 
        // button2
        // 
        button2.Anchor =    AnchorStyles.Top  |  AnchorStyles.Right ;
        button2.Location = new Point( 7, 51 );
        button2.Name = "button2";
        button2.Size = new Size( 75, 23 );
        button2.TabIndex = 4;
        button2.Text = "Ban";
        button2.UseVisualStyleBackColor = true;
        button2.Click +=  button2_Click ;
        // 
        // PlayerUserControl
        // 
        AutoScaleDimensions = new SizeF( 7F, 15F );
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add( button2 );
        Controls.Add( button1 );
        Controls.Add( label3 );
        Controls.Add( label2 );
        Controls.Add( label1 );
        Name = "PlayerUserControl";
        Size = new Size( 164, 79 );
        ResumeLayout( false );
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
    private Label label3;
    private Button button1;
    private Button button2;
}
