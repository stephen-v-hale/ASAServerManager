using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Linq;

using ASARcon;

namespace ASAServerExplorer
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;

        Authentication currentAuth;
        internal static Rcon currentRcon;
        internal static bool disconnected = false;

        ServerList._ServerListItem _currentItem;


        public Form1()
        {
            InitializeComponent();

            disconnected = true;

            Instance = this;

            menuStrip1.Renderer = new WhiteSmokeRenderer();
        }



        private async void button1_Click( object sender, EventArgs e )
        {

        }

        string createPasswordText( string password )
        {
            string pw = "";
            for ( int i = 0; i < password.Length; i++ )
            {
                pw += "*";
            }
            return pw;
        }

        private void timer1_Tick( object sender, EventArgs e )
        {
            if ( !disconnected )
            {
                if ( currentRcon != null )
                {
                    toolStripStatusLabel3.Text = "|";
                    toolStripStatusLabel4.Text = $"{currentRcon.ConnectedPlayers.Count} Players Online";
                }
            }


            if (!string.IsNullOrWhiteSpace( richTextBox1.Text ) )
                toolStripMenuItem5.Enabled = true;
            else
                toolStripMenuItem5.Enabled = false;
        }

        private async void Connect( Authentication authentication )
        {
            if ( currentRcon != null )
            {
                currentRcon.Disconnect();
            }
            currentRcon = new Rcon( authentication );

            currentRcon.AllowLogRedirect = _currentItem.rtgl;

            currentRcon.OnPlayerJoined += ( s ) =>
            {
                executeIfInvoke( treeView1, () =>
                {
                    // try find existing
                    TreeNode existing = treeView1.Nodes
                                .Cast<TreeNode>()
                                .FirstOrDefault(n => n.Text == s.Name);

                    if ( existing != null )
                    {
                        // maybe update properties here if you want
                        existing.Tag = s;
                    }
                    else
                    {
                        // add new
                        TreeNode node = new TreeNode($"{s.Name}");
                        node.Nodes.AddRange( new[]
                        {
            new TreeNode($"ARK ID: {s.ID}"),
            new TreeNode($"Valid: Yes")
        } );
                        node.Tag = s;
                        treeView1.Nodes.Add( node );
                    }
                } );

                if ( _currentItem.rtpl )
                {
                    notifyIcon1.ShowBalloonTip( 10, "", $"{s.Name} has joined {ServerName}", ToolTipIcon.Info );
                    SetLogText( $"{s.Name} has joined {ServerName}" );
                }
            };

            currentRcon.OnPlayerLeave += ( s ) =>
            {
                executeIfInvoke( treeView1, () =>
                {
                    if ( treeView1.Nodes.Count == 0 )
                        return;

                    // find the node once
                    var node = treeView1.Nodes
                            .Cast<TreeNode>()
                            .FirstOrDefault(n => n.Text == s.Name);

                    if ( node == null )
                        return;

                    treeView1.Nodes.Remove( node );
                } );

                if ( _currentItem.rtpl )
                {
                    notifyIcon1.ShowBalloonTip( 10, "", $"{s.Name} has left {ServerName}", ToolTipIcon.Info );
                    SetLogText( $"{s.Name} has left {ServerName}" );
                }
            };
            currentRcon.OnNetworkAccessCheck += ( e ) =>
            {
                if ( e.HasNetworkAccess && !e.HasInternetAccess )
                {
                    SetErrorLogMessage( "Has network access, but no internet access" );
                    return;
                }
                else if ( !e.HasNetworkAccess && !e.HasInternetAccess )
                {
                    SetErrorLogMessage( "Has no network or internet connection " );
                    return;
                }
                else if ( e.HasNetworkAccess && e.HasInternetAccess )
                {
                    SetLogText( "Internet Access ✅" );
                }
            };
            currentRcon.OnAvaliabilityCompleted += ( e ) =>
            {
                if ( e )
                {
                    SetLogText( "Server Reachable ✅" );
                }
            };
            currentRcon.OnError += ( e ) =>
            {
                richTextBox1.Invoke( () =>
                {
                    SetErrorLogMessage( $"{e.Message}" );
                } );
            };
            currentRcon.OnConnecting += () =>
            {
                executeIfInvoke( statusStrip1, () =>
                {
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    toolStripStatusLabel1.Text = $"Connecting to {authentication.Address}:{authentication.Port} ({ServerName})";

                    SetLogText( $"Connecting to {authentication.Address}:{authentication.Port} ({ServerName})" );
                    panel3.Enabled = false;
                } );
            };

            currentRcon.OnConnected += async ( s ) =>
            {
                if ( s )
                {
                    executeIfInvoke( statusStrip1, () =>
                    {
                        toolStripStatusLabel1.ForeColor = Color.Black;
                        toolStripStatusLabel1.Text = "Connected ✅";

                        SetLogText( "Connected ✅" );
                    } );

                    await currentRcon.AuthenticateAsync();
                }
                else
                {
                    executeIfInvoke( statusStrip1, () =>
                    {
                        toolStripStatusLabel1.ForeColor = Color.Black;
                        toolStripStatusLabel1.Text = "Not connected, authentication ignored";
                    } );
                }
            };

            currentRcon.OnLogEntry += ( s ) =>
            {
                if ( _currentItem.rtgl )
                    SetLogText( $"{s.Value} [{s.Type}]" );
            };

            currentRcon.OnAuthenticated += async ( s ) =>
            {
                if ( s )
                {
                    executeIfInvoke( statusStrip1, async () =>
                    {
                        toolStripStatusLabel1.ForeColor = Color.DarkGreen;
                        toolStripStatusLabel1.Text = "Authenticated";
                        SetLogText( "Authenticated ✅" );
                        toolStripStatusLabel1.Image = Properties.Resources.check;

                        disconnected = false;

                        playersToolStripMenuItem1.Enabled = true;
                        broadcastToolStripMenuItem.Enabled = true;

                        disconnectToolStripMenuItem1.Enabled = true;
                        notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", $"Connected & Authenticated to {authentication.Address}:{authentication.Port}", ToolTipIcon.Info );

                        if ( _currentItem.cheats )
                        {
                            if ( await currentRcon.EnableCheats() )
                                SetLogText( "Cheats Enabled ✅" );
                        }

                        if ( panel5.InvokeRequired )
                            panel5.Invoke( () => panel5.Visible = true );
                        else
                            panel5.Visible = true;

                        if ( _currentItem.mb )
                        {
                            timer5.Enabled = true;
                        }
                    } );

                    disconnected = false;


                    executeIfInvoke( menuStrip1, () => playersToolStripMenuItem.Enabled = true );
                    executeIfInvoke( menuStrip1, () => playersToolStripMenuItem1.Enabled = true );
                    executeIfInvoke( menuStrip1, () => disconnectToolStripMenuItem.Enabled = true );
                    executeIfInvoke( menuStrip1, () => disconnectToolStripMenuItem1.Enabled = true );
                    executeIfInvoke( menuStrip1, () => toolsToolStripMenuItem.Enabled = true );

                }
                else
                {
                    executeIfInvoke( statusStrip1, async () =>
                    {
                        toolStripStatusLabel1.ForeColor = Color.Red;
                        toolStripStatusLabel1.Text = "Not authenticated";

                        toolStripStatusLabel1.Image = Properties.Resources.cross;

                        SetErrorLogMessage( "Not Authenticated." );

                        disconnected = true;

                        playersToolStripMenuItem1.Enabled = false;
                        broadcastToolStripMenuItem.Enabled = false;

                        disconnectToolStripMenuItem1.Enabled = false;
                        notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", $"Could not authenticate.", ToolTipIcon.Info );
                        timer5.Enabled = false;
                    } );

                    currentRcon.Disconnect();
                }
            };

            currentRcon.OnDisconnected += () =>
            {

                disconnected = true;

                executeIfInvoke( menuStrip1, () =>
                {
                    panel3.Enabled = true;
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "Disconnected";
                    toolStripStatusLabel1.Image = null;
                    toolStripStatusLabel4.Text = "";
                    toolStripStatusLabel3.Text = "";
                    SetErrorLogMessage( "Disconnected" );
                    playersToolStripMenuItem1.Enabled = false;
                    broadcastToolStripMenuItem.Enabled = false;
                    disconnectToolStripMenuItem1.Enabled = false;
                    timer5.Enabled = false;

                    toolStripStatusLabel3.Text = "";
                    toolStripStatusLabel4.Text = $"";
                } );

                executeIfInvoke( menuStrip1, () => disconnectToolStripMenuItem.Enabled = false );
                executeIfInvoke( menuStrip1, () => disconnectToolStripMenuItem1.Enabled = false );

                executeIfInvoke( menuStrip1, () => playersToolStripMenuItem.Enabled = false );
                executeIfInvoke( menuStrip1, () => playersToolStripMenuItem1.Enabled = false );

                executeIfInvoke( menuStrip1, () => toolsToolStripMenuItem.Enabled = false );

                notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", $"Disconnected", ToolTipIcon.Info );

                executeIfInvoke( treeView1, treeView1.Nodes.Clear );
                executeIfInvoke( panel5, () => { panel5.Visible = false; } );

                currentRcon = null;
            };

            await currentRcon.ConnectAsync();

        }

        // Check if there is a network connection
        public static bool HasNetworkConnection()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        // Check if there is an active internet connection
        public static bool HasInternetConnection()
        {
            try
            {
                using ( Ping ping = new Ping() )
                {
                    PingReply reply = ping.Send("8.8.8.8", 3000); // Ping Google DNS
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        private void closeToolStripMenuItem_Click( object sender, EventArgs e )
        {
            exitApp = true;
            Close();
        }

        public string ServerName = "";
        public List<string> MessagesToBroadcast = new List<string>();
        public bool MessageBroadCast = false;
        public int MessageTimeInterval = 0;
        private void listToolStripMenuItem_Click( object sender, EventArgs e )
        {
            ServerList list = new ServerList();
            list.OnConnect += async ( e, s, info ) =>
            {
                ServerName = s;
                currentAuth = e;
                _currentItem = info;
                MessagesToBroadcast.AddRange( info.mbl );
                MessagesToBroadcast = info.mbl;
                MessageTimeInterval = info.mblt;
                Connect( e );
            };
            list.ShowDialog();
        }

        private void Form1_Load( object sender, EventArgs e )
        {
            var items = Enum.GetNames(typeof(Command));
            comboBox1.Items.AddRange( items );
            comboBox1.SelectedIndex = 0;
        }

        private void timer2_Tick( object sender, EventArgs e )
        {
        }

        private void disconnectToolStripMenuItem_Click( object sender, EventArgs e )
        {
            currentRcon.Disconnect();
        }
        void executeIfInvoke( Control control, Action a )
        {
            if ( control.InvokeRequired )
                control.Invoke( a );
            else
                a();
        }
        private async void button1_Click_1( object sender, EventArgs e )
        {
            try
            {
                richTextBox1.ForeColor = Color.Black;
                var command = (Command)Enum.Parse(typeof(Command), comboBox1.SelectedItem.ToString());

                var result =await currentRcon.SendCommandAsync( command, default, textBox1.Text.Split( " " ) );

                if ( result.Contains( "Server received" ) )
                {
                }
                else
                {
                    SetLogText( result.Replace( "\n", "" ) );
                }
            }
            catch
            {
                SetErrorLogMessage( "Unable to send command" );
            }
        }

        private void playersToolStripMenuItem_Click( object sender, EventArgs e )
        {
            panel5.Visible = !panel5.Visible;
        }

        private void toolStripStatusLabel1_Click( object sender, EventArgs e )
        {

        }

        private void toolStripStatusLabel1_RightToLeftChanged( object sender, EventArgs e )
        {

        }

        private void toolStripStatusLabel1_TextChanged( object sender, EventArgs e )
        {

        }

        void SetErrorLogMessage( string message )
        {
            SetLogText( message, Color.White, Color.Red );
        }
        void SetLogText( string message, Color fontColor, Color background )
        {
            if ( richTextBox1.InvokeRequired )
            {
                richTextBox1.Invoke( () =>
                {
                    richTextBox1.SelectionColor = fontColor;
                    richTextBox1.SelectionBackColor = background;
                    richTextBox1.SelectedText = message + "\n";
                    richTextBox1.SelectionColor = Color.Black;
                    richTextBox1.SelectionBackColor = Color.White;
                    richTextBox1.ScrollToCaret();
                } );
            }
            else
            {
                richTextBox1.SelectionColor = fontColor;
                richTextBox1.SelectionBackColor = background;
                richTextBox1.SelectedText = message + "\n";
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.SelectionBackColor = Color.White;
                richTextBox1.ScrollToCaret();
            }
        }
        void SetLogText( string message ) => SetLogText( message, Color.Black, Color.White );

        private void Form1_FormClosed( object sender, FormClosedEventArgs e )
        {
            if ( exitApp )
            {
                if ( currentRcon != null )
                    currentRcon.Disconnect();

                Process.GetCurrentProcess().Kill();

            }

        }

        private void richTextBox1_TextChanged( object sender, EventArgs e )
        {
            richTextBox1.ScrollToCaret();
        }

        private async void setMessageOfTheDayToolStripMenuItem_Click( object sender, EventArgs e )
        {
            var item = InputBox.Show("The message", "SetMessageOfDay", "");

            if ( !string.IsNullOrWhiteSpace( item ) )
            {
                if ( await currentRcon.SetMessageOfTheDay( item ) )
                {
                    SetLogText( $"Message of the day set to {'"'}{item}{'"'}", Color.Black, Color.White );

                }
                else
                {
                    SetErrorLogMessage( "There was an issue with sending MessageOfTheDay" );
                }
            }
        }

        private async void timer3_Tick( object sender, EventArgs e )
        {
            await DoOtherStuff();
        }


        async Task DoOtherStuff()
        {
            if ( currentRcon == null ) return;
            if ( !currentRcon.IsConnected ) return;

            var players = await currentRcon.GetPlayerList();
            var currentPlayers = players
              .Where( p => p.ValidPlayer )
              .Select( p => p.Name )
              .ToList();

            currentTime += 5;
            if ( _currentItem.mb )
                if ( currentTime >= _currentItem.mblt )
                {
                    if ( players.Length > 0 )
                    {
                        if ( currentMessageIndex > MessagesToBroadcast.Count - 1 )
                            currentMessageIndex = 0;

                        foreach ( var p in players )
                        {
                            if ( p.ValidPlayer )
                            {
                                p.SendMessage( MessagesToBroadcast[currentMessageIndex] );
                            }
                        }
                    }

                    currentMessageIndex += 1;
                    currentTime = 0;
                }
        }


        private void notifyIcon1_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            this.Show();
        }

        private void Form1_FormClosing( object sender, FormClosingEventArgs e )
        {
            var messageResult = MessageBox.Show("Minimize into tray?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if ( messageResult == DialogResult.No )
            {
                exitApp = true;
            }
            else
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip( 500, "Close", "ASA Server Manager Hidden", ToolTipIcon.Info );
                e.Cancel = true;
                this.Hide();
            }
        }

        private void broadcastToolStripMenuItem_Click( object sender, EventArgs e )
        {
            var input = InputBox.Show("Broadcast", "Message");
            if ( !string.IsNullOrEmpty( input ) )
            {
                try
                {
                    currentRcon.SendCommandAsync( Command.Broadcast, default, new object[] { input } );
                    SetLogText( "Message Broadcasted" );
                    notifyIcon1.ShowBalloonTip( 10, "Broadcasted", "Message Broadcasted", ToolTipIcon.Info );
                }
                catch
                {
                    SetErrorLogMessage( "Error while broadcasting message." );
                    notifyIcon1.ShowBalloonTip( 10, "Broadcasted", "Error while broadcasting message", ToolTipIcon.Error );
                }
            }
        }
        bool exitApp = false;
        private void playersToolStripMenuItem1_Click( object sender, EventArgs e )
        {
        }

        private void showWindowToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            exitApp = true;

            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void serversListToolStripMenuItem_Click( object sender, EventArgs e )
        {
            ServerList list = new ServerList();
            list.OnConnect += async ( e, s, info ) =>
            {
                ServerName = s;
                currentAuth = e;
                _currentItem = info;
                MessagesToBroadcast.AddRange( info.mbl );
                MessagesToBroadcast = info.mbl;
                MessageTimeInterval = info.mblt;
                Connect( e );
            };
            list.ShowDialog();
        }

        private void disconnectToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            disconnectToolStripMenuItem_Click( sender, e );
        }

        private void notifyIcon1_BalloonTipClicked( object sender, EventArgs e )
        {
            this.Show();
        }

        private void comboBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            var text = comboBox1.SelectedItem switch
            {
                "Broadcast" => "<message>",
                "ServerChat" => "<message>",
                "ServerChatTo" => "<playerId> <message>",
                "ServerChatToPlayer" => "<playerName> <message>",
                "KickPlayer" => "<playerIdOrName>",
                "BanPlayer" => "<playerIdOrName>",
                "GetAllState" => "<id>",
                "DoRestartLevel"=> "Empty arguments",
                "GetChat"=> "<index>",
                "Raw" => "<command> <args>",
                _ => "NONE.",
            };

            textBox1.PlaceholderText = text;
        }

        private void panel3_Paint( object sender, PaintEventArgs e )
        {
            switch ( panel5.Visible )
            {
                case true:
                e.Graphics.DrawLine( new Pen( Color.FromKnownColor( KnownColor.ScrollBar ) ),
                new Point( panel5.Width, e.ClipRectangle.Y + e.ClipRectangle.Height - 1 ),
                new PointF( panel5.Width + e.ClipRectangle.Width - panel5.Width, e.ClipRectangle.Y + e.ClipRectangle.Height - 1 ) );
                break;
                case false:
                e.Graphics.DrawLine( new Pen( Color.FromKnownColor( KnownColor.ScrollBar ) ),
                    new Point( e.ClipRectangle.X, e.ClipRectangle.Y + e.ClipRectangle.Height - 1 ),
                    new PointF( e.ClipRectangle.X + e.ClipRectangle.Width, e.ClipRectangle.Y + e.ClipRectangle.Height - 1 ) );
                break;
            }
        }

        private void menuStrip1_Paint( object sender, PaintEventArgs e )
        {

        }

        private void button2_Click( object sender, EventArgs e )
        {
            if ( !ensureContent( textBox2, "Please enter the host address." ) ) return;
            if ( !ensureContent( textBox3, "Please enter the password" ) ) return;
            if ( !ensureContent( numericUpDown1, "Port must be greater than 0" ) ) return;


            Authentication ee = new Authentication(textBox2.Text, textBox3.Text, (int)numericUpDown1.Value);
            ServerName = "Quick Connect";
            currentAuth = ee;
            _currentItem = new ServerList._ServerListItem()
            {
                Address = ee.Address,
                Password = ee.Password,
                Port = ee.Port.ToString(),
                Name = "Quick Connect",
                rtgl = false,
                rtpl = true,
            };
            Connect( ee );

            timer3_Tick( this, EventArgs.Empty );
        }

        bool ensureContent( Control control, string message )
        {
            if ( control is TextBox )
                if ( string.IsNullOrEmpty( control.Text ) )
                {
                    SetLogText( message, Color.Black, Color.Yellow );
                    return false;
                }
                else
                    return true;

            if ( control is NumericUpDown )
                if ( ( ( NumericUpDown )control ).Value > 0 )
                {
                    SetLogText( message, Color.Black, Color.Yellow );
                    return false;
                }
                else
                    return true;

            return false;
        }
        int currentTime = 0;
        int currentMessageIndex = 0;

        private void button3_Click( object sender, EventArgs e )
        {

        }

        private void button3_Click_1( object sender, EventArgs e )
        {
            panel5.Visible = false;
        }

        private void Form1_Shown( object sender, EventArgs e )
        {
            notifyIcon1.Visible = false;
        }

        private void toolStripMenuItem5_Click( object sender, EventArgs e )
        {
            richTextBox1.Text = "";
        }
        private void toolStripMenuItem6_Click( object sender, EventArgs e )
        {
            currentRcon.ShutDown();
        }

        private void panel7_Paint( object sender, PaintEventArgs e )
        {

        }
        private async void timer5_Tick( object sender, EventArgs e )
        {
            await DoOtherStuff();
        }

        private void kickToolStripMenuItem_Click( object sender, EventArgs e )
        {
            var asaPlayer = (ASAPlayer)(treeView1.SelectedNode).Tag;

            if ( asaPlayer == null )
                return;


            asaPlayer.Kick();
            SetLogText( $"{asaPlayer.Name} has been kicked" );


            treeView1.Nodes.Remove( treeView1.SelectedNode );
        }

        private void banToolStripMenuItem_Click( object sender, EventArgs e )
        {
            var asaPlayer = (ASAPlayer)(treeView1.SelectedNode).Tag;

            if ( asaPlayer == null )
                return;


            asaPlayer.Ban();
            SetLogText( $"{asaPlayer.Name} has been banned" );


            treeView1.Nodes.Remove( treeView1.SelectedNode );
        }

        private void whitelistToolStripMenuItem_Click( object sender, EventArgs e )
        {
            var asaPlayer = (ASAPlayer)(treeView1.SelectedNode).Tag;

            if ( asaPlayer == null )
                return;


            asaPlayer.WhiteList();
            SetLogText( $"{asaPlayer.Name} has been white listed" );
        }

        private void blackListToolStripMenuItem_Click( object sender, EventArgs e )
        {
            var asaPlayer = (ASAPlayer)(treeView1.SelectedNode).Tag;

            if ( asaPlayer == null )
                return;


            asaPlayer.Blacklist();
            SetLogText( $"{asaPlayer.Name} has been black listed" );
        }

        private void treeView1_AfterSelect( object sender, TreeViewEventArgs e )
        {
            if ( treeView1.SelectedNode.Tag is ASAPlayer )
            {
                foreach ( var item in contextMenuStrip2.Items )
                    if ( item is ToolStripMenuItem )
                    {
                        ( ( ToolStripMenuItem )item ).Enabled = true;
                    }
            }
            else
            {
                foreach ( var item in contextMenuStrip2.Items )
                    if ( item is ToolStripMenuItem )
                    {
                        ( ( ToolStripMenuItem )item ).Enabled = false;
                    }
            }
        }

        private void messageToolStripMenuItem_Click( object sender, EventArgs e )
        {

            var asaPlayer = (ASAPlayer)(treeView1.SelectedNode).Tag;

            if ( asaPlayer == null )
                return;

            asaPlayer.SendMessage( InputBox.Show( "The message to send " + asaPlayer.Name, "Send user message" ) );

            SetLogText( "Message sent to " + asaPlayer.Name );
        }

        private void consoleToolStripMenuItem_Click( object sender, EventArgs e )
        {
        }

        private void panel5_VisibleChanged( object sender, EventArgs e )
        {
            panel3.Invalidate();
        }

        private void aboutToolStripMenuItem_Click_1( object sender, EventArgs e )
        {
            new About().ShowDialog();
        }

        private void preferencesToolStripMenuItem_Click( object sender, EventArgs e )
        {
        }
    }
}

class WhiteSmokeRenderer : ToolStripProfessionalRenderer
{
    public WhiteSmokeRenderer() : base( new WhiteSmokeColors() ) { }

    private sealed class WhiteSmokeColors : ProfessionalColorTable
    {
        public override Color ToolStripGradientBegin => Color.WhiteSmoke;
        public override Color ToolStripGradientMiddle => Color.WhiteSmoke;
        public override Color ToolStripGradientEnd => Color.WhiteSmoke;

        // drop down menus also
        public override Color MenuStripGradientBegin => Color.WhiteSmoke;
        public override Color MenuStripGradientEnd => Color.WhiteSmoke;
    }

    protected override void OnRenderToolStripBackground( ToolStripRenderEventArgs e )
    {
        base.OnRenderToolStripBackground( e );
    }
}
internal class ModernMenuRenderer : ToolStripProfessionalRenderer
    {
        private static readonly Color BackgroundColor = Color.WhiteSmoke;
        private static readonly Color ItemHoverColor = Color.FromArgb(230, 230, 230);
        private static readonly Color ItemPressedColor = Color.FromArgb(210, 210, 210);
        private static readonly Color BorderColor = Color.FromArgb(180, 180, 180);
        private static readonly Color TextColor = Color.Black;
        private static readonly Color DisabledTextColor = Color.Gray;
        private static readonly Color SeparatorColor = Color.FromArgb(200, 200, 200);
        private static readonly Color DropShadowColor = Color.FromArgb(80, 0, 0, 0);

        public ModernMenuRenderer() : base(new ModernMenuColorTable())
        {
            RoundedEdges = false;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (var b = new SolidBrush(BackgroundColor))
                e.Graphics.FillRectangle(b, e.AffectedBounds);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);

            if (e.Item.Selected && !e.Item.Pressed)
            {
                using (var b = new SolidBrush(ItemHoverColor))
                    e.Graphics.FillRectangle(b, rect);
                using (var p = new Pen(BorderColor))
                    e.Graphics.DrawRectangle(p, 0, 0, rect.Width - 1, rect.Height - 1);
            }
            else if (e.Item.Pressed)
            {
                using (var b = new SolidBrush(ItemPressedColor))
                    e.Graphics.FillRectangle(b, rect);
                using (var p = new Pen(BorderColor))
                    e.Graphics.DrawRectangle(p, 0, 0, rect.Width - 1, rect.Height - 1);
            }
            else if (e.ToolStrip is MenuStrip)
            {
                // Top menu background
                using (var b = new SolidBrush(BackgroundColor))
                    e.Graphics.FillRectangle(b, rect);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Enabled ? TextColor : DisabledTextColor;
            e.TextFont = e.TextFont ?? SystemFonts.MenuFont;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(2, e.Item.ContentRectangle.Height / 2, e.Item.Width - 4, 1);
            using (var p = new Pen(SeparatorColor))
                e.Graphics.DrawLine(p, rect.Left, rect.Top, rect.Right, rect.Top);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            // Custom clean black arrow
            var arrowColor = e.Item.Enabled ? TextColor : DisabledTextColor;
            using (var b = new SolidBrush(arrowColor))
            {
                var size = new Size(5, 9);
                var loc = new Point(
                    e.ArrowRectangle.Left + (e.ArrowRectangle.Width - size.Width) / 2,
                    e.ArrowRectangle.Top + (e.ArrowRectangle.Height - size.Height) / 2);

                Point[] arrow = {
                    new Point(loc.X, loc.Y),
                    new Point(loc.X + size.Width, loc.Y + size.Height / 2),
                    new Point(loc.X, loc.Y + size.Height)
                };

                e.Graphics.FillPolygon(b, arrow);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using (var p = new Pen(BorderColor))
                e.Graphics.DrawRectangle(p, new Rectangle(Point.Empty, e.ToolStrip.Size - new Size(1, 1)));
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            OnRenderMenuItemBackground(e);
        }
    }

    internal class ModernMenuColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => Color.WhiteSmoke;
        public override Color MenuStripGradientBegin => Color.WhiteSmoke;
        public override Color MenuStripGradientEnd => Color.WhiteSmoke;
        public override Color MenuItemBorder => Color.FromArgb(180, 180, 180);
        public override Color MenuItemSelected => Color.FromArgb(230, 230, 230);
        public override Color MenuItemPressedGradientBegin => Color.FromArgb(210, 210, 210);
        public override Color MenuItemPressedGradientEnd => Color.FromArgb(210, 210, 210);
        public override Color ImageMarginGradientBegin => Color.WhiteSmoke;
        public override Color ImageMarginGradientEnd => Color.WhiteSmoke;
        public override Color ToolStripBorder => Color.FromArgb(180, 180, 180);
    }