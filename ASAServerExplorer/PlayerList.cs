using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ASARcon;

namespace ASAServerExplorer;

public partial class PlayerList : Form
{
    Rcon rcon;
    public PlayerList( Rcon rcon )
    {
        InitializeComponent();

        if ( rcon != null && rcon.IsConnected )
        {
            this.rcon = rcon;

            timer2_Tick( this, null );
        }

    }

    private void timer1_Tick( object sender, EventArgs e )
    {
        try
        {
            if ( ( ( ASAPlayer )listView1.Items[0].Tag ).ValidPlayer )
            {
                banToolStripMenuItem.Enabled = true;
                kickToolStripMenuItem.Enabled = true;
                sendMessageToolStripMenuItem.Enabled = true;
            }
            else
            {
                banToolStripMenuItem.Enabled = false;
                kickToolStripMenuItem.Enabled = false;
                sendMessageToolStripMenuItem.Enabled = false;
            }
        }
        catch
        {
            banToolStripMenuItem.Enabled = false;
            kickToolStripMenuItem.Enabled = false;
            sendMessageToolStripMenuItem.Enabled = false;
        }
    }

    private void PlayerList_Load( object sender, EventArgs e )
    {

    }

    private async void timer2_Tick( object sender, EventArgs e )
    {
        listView1.Items.Clear();
        foreach ( var player in await rcon.GetPlayerList() )
        {
            ListViewItem item = new ListViewItem(player.Index.ToString());
            item.SubItems.AddRange( new ListViewItem.ListViewSubItem[]
            {
                    new ListViewItem.ListViewSubItem(item, player.Name),
                    new ListViewItem.ListViewSubItem(item, player.ID),
            } );
            item.Tag = player;
            listView1.Items.Add( item );
        }
    }

    private void kickToolStripMenuItem_Click( object sender, EventArgs e )
    {
        if ( listView1.SelectedItems.Count > 0 )
        {
            var player = (ASAPlayer)listView1.SelectedItems[0].Tag;

            if ( player != null )
            {
                try
                {
                    rcon.SendCommandAsync( Command.KickPlayer, default, new object[1] { player.ID } );
                    listView1.Items.Remove( listView1.SelectedItems[0] );
                }
                catch
                {
                    MessageBox.Show( "Unable to kick player" );
                }
            }
        }
    }

    private void banToolStripMenuItem_Click( object sender, EventArgs e )
    {
        if ( listView1.SelectedItems.Count > 0 )
        {
            var player = (ASAPlayer)listView1.SelectedItems[0].Tag;

            if ( player != null )
            {
                try
                {
                    rcon.SendCommandAsync( Command.BanPlayer, default, new object[1] { player.ID } );
                    listView1.Items.Remove( listView1.SelectedItems[0] );

                }
                catch
                {
                    MessageBox.Show( "Unable to ban player" );
                }
            }
        }
    }

    private async void sendMessageToolStripMenuItem_Click( object sender, EventArgs e )
    {
        var item = (ASAPlayer)listView1.SelectedItems[0].Tag;
        var box = InputBox.Show( $"The message to send to {item.Name}", "SendMessage", "" );

        try
        {
            await rcon.SendMessageToPlayer( box, item );

            MessageBox.Show( "Message sent to " + item.Name );
        }
        catch
        {
            MessageBox.Show( "Error", "Unable to send message", MessageBoxButtons.OK, MessageBoxIcon.Error );
        }
    }
}

public static class InputBox
{
    /// <summary>
    /// Shows an input dialog and returns the user input.
    /// </summary>
    /// <param name="prompt">The message to display.</param>
    /// <param name="title">The dialog title.</param>
    /// <param name="defaultValue">The default text in the input box.</param>
    /// <returns>User input as a string. Empty string if cancelled.</returns>
    public static string Show( string prompt, string title = "Input", string defaultValue = "" )
    {
        Form inputForm = new Form()
        {
            Width = 400,
            Height = 150,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = title,
            StartPosition = FormStartPosition.CenterScreen,
            MinimizeBox = false,
            MaximizeBox = false
        };

        Label textLabel = new Label() { Left = 10, Top = 20, Width = 360, Text = prompt };
        TextBox inputBox = new TextBox() { Left = 10, Top = 50, Width = 360, Text = defaultValue };

        Button okButton = new Button() { Text = "OK", Left = 200, Width = 80, Top = 80, DialogResult = DialogResult.OK };
        Button cancelButton = new Button() { Text = "Cancel", Left = 290, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };

        okButton.Click += ( sender, e ) => { inputForm.Close(); };
        cancelButton.Click += ( sender, e ) => { inputForm.Close(); };

        inputForm.Controls.Add( textLabel );
        inputForm.Controls.Add( inputBox );
        inputForm.Controls.Add( okButton );
        inputForm.Controls.Add( cancelButton );

        inputForm.AcceptButton = okButton;
        inputForm.CancelButton = cancelButton;

        DialogResult result = inputForm.ShowDialog();

        return result == DialogResult.OK ? inputBox.Text : "";
    }
}