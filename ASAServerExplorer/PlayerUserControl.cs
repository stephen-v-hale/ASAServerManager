using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ASARcon;

namespace ASAServerExplorer;

public partial class PlayerUserControl : UserControl
{
    public ASAPlayer Player => player;
    public event EventHandler<ASAPlayer> OnKick;
    public event EventHandler<ASAPlayer> OnBan;
    ASAPlayer player;
    public PlayerUserControl( ASAPlayer player, bool buttonsEnabled )
    {
        InitializeComponent();
        label1.Text = player.Name;
        label3.Text = player.ID;

        this.player = player;
        button1.Enabled = buttonsEnabled;
        button2.Enabled = buttonsEnabled;

        if(buttonsEnabled)
        {
            Height = 79;
        }
        else
        {
            Height = 30;
        }
    }

    private void button2_Click( object sender, EventArgs e )
    {
        OnBan?.Invoke( this, player );
        player.Ban();
    }

    private void button1_Click( object sender, EventArgs e )
    {
        OnKick?.Invoke( this, player );
        player.Kick();
    }
}
