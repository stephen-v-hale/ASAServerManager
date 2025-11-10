using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ASARcon;

using Microsoft.VisualBasic.ApplicationServices;

namespace ASAServerExplorer;

public partial class ServerList : Form
{
    public struct _ServerListItem
    {
        public string Name;
        public string Address;
        public string Password;
        public string Port;
        public bool rtgl, rtpl, mb, cheats;
        public List<string> mbl;
        public int mblt;
    }
    public ServerList()
    {
        InitializeComponent();

        if ( Directory.Exists( $"{Environment.CurrentDirectory}\\Servers" ) )
        {
            foreach ( var file in Directory.GetFiles( Environment.CurrentDirectory + @"\\Servers" ) )
            {
                ServerIniParser init = new ServerIniParser(file);
                _ServerListItem list = new _ServerListItem()
                {
                    Name = init.Sections[0].Value,
                    Address = init.Sections[1].Value,
                    Password = init.Sections[2].Value,
                    Port = init.Sections[3].Value,
                    rtgl = init.Sections[4].Value == "1" ? true : false,
                    rtpl = init.Sections[5].Value == "1" ? true : false,
                    mb = init.Sections[6].Value == "1" ? true: false,
                    mbl =init.Sections[7].Value
    .Split('|')
    .Where(p => !string.IsNullOrWhiteSpace(p))
    .ToList(),
                    mblt = int.Parse(init.Sections[8].Value),
                    cheats = init.Sections[9].Value == "1"? true : false,
                };
                TreeNode node = new TreeNode(list.Name);
                node.SelectedImageIndex = 0;
                node.ImageIndex = 0;
                node.Tag = list;
                treeView1.Nodes[0].Nodes.Add( node );
            }
        }
    }

    private void timer1_Tick( object sender, EventArgs e )
    {
        if ( treeView1.Nodes.Count > 0 )
        {
            addServerToolStripMenuItem.Enabled = true;
        }

        if ( treeView1.SelectedNode != null )
        {
            removeToolStripMenuItem.Enabled = true;
        }
        else
        {
            removeToolStripMenuItem.Enabled = false;
        }

        if ( textChanged1 || textChanged2 || portChagned )
        {
            button1.Enabled = true;
        }
        else
        {
            button1.Enabled = false;
        }
    }

    private void button1_Click( object sender, EventArgs e )
    {
        ServerIniParser parser = new ServerIniParser("");
        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "Name",
            Value = groupBox1.Text,
        } );
        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "Address",
            Value = textBox1.Text,
        } );
        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "Password",
            Value = textBox2.Text,
        } );
        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "Port",
            Value = numericUpDown1.Value.ToString(),
        } );

        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "rtgl",
            Value = checkBox1.Checked ? "1" : "0"
        } );

        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "rtpl",
            Value = checkBox2.Checked ? "1" : "0"
        } );

        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "mb",
            Value = checkBox5.Checked ? "1" : "0"
        } );
        string items = "";
        foreach ( var item in listBox1.Items )
            items += $"{item}|";
        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "mbl",
            Value = string.IsNullOrWhiteSpace( items ) ? "|" : items,
        } );

        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "mblt",
            Value = numericUpDown2.Value.ToString(),
        } );

        parser.Sections.Add( new ServerIniParser.ServerIniSection()
        {
            Owner = "ASAServer",
            Name = "cheats",
            Value = checkBox3.Checked ? "1" : "0",
        } );




        _ServerListItem list = new _ServerListItem()
        {
            Name = groupBox1.Text,
            Address = textBox1.Text,
            Password = textBox2.Text,
            Port = numericUpDown1.Value.ToString(),
            rtgl = checkBox1.Checked,
            rtpl = checkBox2.Checked,
            mb = checkBox5.Checked,
            mbl = items.Split("|").ToList(),
            cheats = checkBox3.Checked,
        };
        if ( !Directory.Exists( Environment.CurrentDirectory + @"\Servers" ) ) Directory.CreateDirectory( Environment.CurrentDirectory + @"\Servers" );
        textBox1.Text = list.Address;
        textBox2.Text = list.Password;
        numericUpDown1.Value = int.Parse( list.Port );
        groupBox1.Text = list.Name;

        parser.SaveToFile( Environment.CurrentDirectory + @"\Servers\" + groupBox1.Text );

        treeView1.SelectedNode.Tag = list;

        textChanged1 = false;
        textChanged2 = false;
        portChagned = false;
    }

    private void addServerToolStripMenuItem_Click( object sender, EventArgs e )
    {
        var input = new ServerListItem();
        if ( input.ShowDialog() == DialogResult.OK )
        {
            ServerIniParser parser = new ServerIniParser("");
            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "Name",
                Value = input.Name,
            } );
            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "Address",
                Value = input.Address,
            } );
            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "Password",
                Value = input.Password,
            } );
            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "Port",
                Value = input.Port.ToString(),
            } );

            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "rtgl",
                Value = input.ShowRealTimeLog ? "1" : "0",
            } );
            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "rtpl",
                Value = input.ShowRealTimePlayerLogging ? "1" : "0",
            } );
            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "mb",
                Value = input.MessageBroadcaster ? "1" : "0",
            } );
            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "mbl",
                Value = "|"
            } );

            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "mblt",
                Value = "0"
            } );

            parser.Sections.Add( new ServerIniParser.ServerIniSection()
            {
                Owner = "ASAServer",
                Name = "cheats",
                Value = "0"
            } );

            if ( !Directory.Exists( Environment.CurrentDirectory + @"\Servers" ) ) Directory.CreateDirectory( Environment.CurrentDirectory + @"\Servers" );

            parser.SaveToFile( Environment.CurrentDirectory + @"\Servers\" + input.Name );
        }

        treeView1.Nodes[0].Nodes.Clear();

        if ( Directory.Exists( $"{Environment.CurrentDirectory}\\Servers" ) )
        {
            foreach ( var file in Directory.GetFiles( Environment.CurrentDirectory + @"\\Servers" ) )
            {
                ServerIniParser init = new ServerIniParser(file);
                _ServerListItem list = new _ServerListItem()
                {
                    Name = init.Sections[0].Value,
                    Address = init.Sections[1].Value,
                    Password = init.Sections[2].Value,
                    Port = init.Sections[3].Value,
                    rtgl = init.Sections[4].Value == "1" ? true : false,
                    rtpl = init.Sections[5].Value == "1" ? true : false,
                };
                TreeNode node = new TreeNode(list.Name);
                node.SelectedImageIndex = 0;
                node.ImageIndex = 0;
                node.Tag = list;
                treeView1.Nodes[0].Nodes.Add( node );
            }
        }
    }

#nullable disable
    private void treeView1_DoubleClick( object sender, EventArgs e )
    {

    }

    private void treeView1_NodeMouseDoubleClick( object sender, TreeNodeMouseClickEventArgs e )
    {

    }

    private void treeView1_AfterSelect( object sender, TreeViewEventArgs e )
    {
        if ( treeView1.SelectedNode.Tag is _ServerListItem )
        {
            var info = (_ServerListItem)treeView1.SelectedNode.Tag;
            textBox1.Text = info.Address;
            textBox2.Text = info.Password;
            numericUpDown1.Value = int.Parse( info.Port );
            groupBox1.Text = info.Name;
            checkBox1.Checked = info.rtgl;
            checkBox2.Checked = info.rtpl;
            Width = 541;
            panel1.Visible = true;
            numericUpDown2.Value = info.mblt;
            listBox1.Items.AddRange( info.mbl.ToArray() );
            checkBox5.Checked = info.mb;
            checkBox3.Checked = info.cheats;
            if ( Form1.currentRcon != null )
            {
                if ( info.Address == Form1.currentRcon.Authentication.Address )
                {
                    button2.Text = "Disconnect";
                }
                else
                {
                    button2.Text = "Connect";
                }
            }
            textChanged1 = false;
            textChanged2 = false;
            portChagned = false;
            timer1.Start();
        }

        else
        {
            Width = 243;
            panel1.Visible = false;

            timer1.Stop();
        }
    }

    private void removeToolStripMenuItem_Click( object sender, EventArgs e )
    {
        File.Delete( $"{Environment.CurrentDirectory}\\Servers\\{treeView1.SelectedNode.Text}" );
        treeView1.Nodes.Remove( treeView1.SelectedNode );
    }

    public delegate void ConnectClicked( Authentication authenticate, string serverName, _ServerListItem info );

    public event ConnectClicked OnConnect;

    private void button2_Click( object sender, EventArgs e )
    {
        if ( button2.Text == "Disconnect" )
        {
            Form1.currentRcon.Disconnect();
            Form1.currentRcon.Dispose();
            Form1.currentRcon = null;

            Form1.disconnected = true;

            Form1.Instance.toolStripStatusLabel1.ForeColor = Color.Red;
            Form1.Instance.toolStripStatusLabel1.Text = "Disconnected";
            Form1.Instance.toolStripStatusLabel1.Image = Properties.Resources.cross;
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            var info = (_ServerListItem)treeView1.SelectedNode.Tag;

            OnConnect?.Invoke( new Authentication( info.Address, info.Password, int.Parse( info.Port ) ), groupBox1.Text, info );

            DialogResult = DialogResult.OK;
            Close();
        }
    }

    bool textChanged1 = false, textChanged2= false, portChagned= false;
    private void textBox2_TextChanged( object sender, EventArgs e )
    {
        textChanged1 = true;
    }

    private void textBox1_TextChanged( object sender, EventArgs e )
    {
        textChanged2 = true;
    }

    private void numericUpDown1_ValueChanged( object sender, EventArgs e )
    {
        portChagned = true;
    }

    private void contextMenuStrip1_Opening( object sender, CancelEventArgs e )
    {

    }

    private void button3_Click( object sender, EventArgs e )
    {
        File.Delete( $"{Environment.CurrentDirectory}\\Servers\\{treeView1.SelectedNode.Text}" );
        treeView1.Nodes.Remove( treeView1.SelectedNode );
    }

    private void checkBox5_CheckedChanged( object sender, EventArgs e )
    {
        if ( checkBox5.Checked )
        {
            Height = 442;
            timer2.Start();
        }
        else
        {
            Height = 250;
            timer2.Stop();
        }

        textChanged1 = true;
    }

    private void button4_Click( object sender, EventArgs e )
    {
        listBox1.Items.Add( InputBox.Show( "Message to show", "Message" ) );
        textChanged1 = true;
    }

    private void button5_Click( object sender, EventArgs e )
    {
        listBox1.Items.Remove( listBox1.SelectedItem );
        textChanged1 = true;
    }

    private void numericUpDown2_ValueChanged( object sender, EventArgs e )
    {
        textChanged1 = true;
    }

    private void listBox1_SelectedIndexChanged( object sender, EventArgs e )
    {
        textChanged1 = true;
    }

    private void checkBox1_CheckedChanged( object sender, EventArgs e )
    {
        textChanged1 = true;
    }

    private void checkBox2_CheckedChanged( object sender, EventArgs e )
    {
        textChanged1 = true;
    }

    private void checkBox3_CheckedChanged( object sender, EventArgs e )
    {
        textChanged1 = true;
    }
}

public class ServerIniParser
{
    public class ServerIniSection
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public List<ServerIniSection> Sections { get; set; }

    public ServerIniParser(string fileName)
    {
        Sections = new List<ServerIniSection>();

        if ( !string.IsNullOrWhiteSpace( fileName ) )
        {
            if(File.Exists(fileName))
            {
                var lines = File.ReadAllLines(fileName);
                foreach(var line in lines)
                {
                    string currentHeader = "";

                    if ( line.StartsWith( "[" ) && line.EndsWith( "]" ) )
                        currentHeader = line.Replace( "[", "" ).Replace( "]", "" );

                    if(line.Contains("="))
                    {
                        Sections.Add( new ServerIniSection()
                        {
                            Owner = currentHeader,
                            Name = line.Split( "=" )[0].Trim(),
                            Value = line.Split( "=" )[1].Trim(),
                        } );
                    }
                }
            }
        }

        return;
    }

    public void SaveToFile(string fileName)
    {
        if ( string.IsNullOrEmpty( fileName ) ) throw new ArgumentNullException(nameof(fileName));

        StreamWriter writer = new StreamWriter(fileName);
        var lastHeader = "";

        foreach(var section in Sections)
        {
            if ( lastHeader != section.Owner ) { 
                lastHeader = section.Owner;

                writer.WriteLine( $"[{lastHeader}]" );
            }

            writer.WriteLine( $"{section.Name}={section.Value}" );
        }
        writer.Close();
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