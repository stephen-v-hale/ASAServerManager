using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ASARcon;

namespace ASAServerExplorer;

public partial class ServerList : Form
{
    public struct _ServerListItem
    {
        public string Name;
        public string Address;
        public string Password;
        public string Port;

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
            Value = label1.Text,
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
        _ServerListItem list = new _ServerListItem()
        {
            Name = label1.Text,
            Address = textBox1.Text,
            Password = textBox2.Text,
            Port = numericUpDown1.Value.ToString(),
        };
        if ( !Directory.Exists( Environment.CurrentDirectory + @"\Servers" ) ) Directory.CreateDirectory( Environment.CurrentDirectory + @"\Servers" );
        textBox1.Text = list.Address;
        textBox2.Text = list.Password;
        numericUpDown1.Value = int.Parse( list.Port );
        label1.Text = list.Name;

        parser.SaveToFile( Environment.CurrentDirectory + @"\Servers\" + label1.Text );

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
            label1.Text = info.Name;

            Width = 541;
            panel1.Visible = true;

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
        }

        else
        {
            Width = 243;
            panel1.Visible = false;
        }
    }

    private void removeToolStripMenuItem_Click( object sender, EventArgs e )
    {
        File.Delete( $"{Environment.CurrentDirectory}\\Servers\\{treeView1.SelectedNode.Text}" );
        treeView1.Nodes.Remove( treeView1.SelectedNode );
    }

    public delegate void ConnectClicked( Authentication authenticate, string serverName );

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
            if ( Form1.currentRcon != null )
            {
                Form1.currentRcon.Disconnect();
                Form1.currentRcon.Dispose();
                Form1.currentRcon = null;

                Form1.disconnected = true;

                Form1.Instance.toolStripStatusLabel1.ForeColor = Color.Black;
                Form1.Instance.toolStripStatusLabel1.Text = "Disconnected from " + Form1.currentRcon.Authentication.Address;
                Form1.Instance.toolStripStatusLabel1.Image = Properties.Resources.cross;
            }
            var info = (_ServerListItem)treeView1.SelectedNode.Tag;

            OnConnect?.Invoke( new Authentication( info.Address, info.Password, int.Parse( info.Port ) ), label1.Text );

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