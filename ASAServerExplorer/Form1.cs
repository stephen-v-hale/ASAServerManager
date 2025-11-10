using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        int reconnectAttemps = 1;
        int maxReconnectAttempts = 20;
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

        private void timer1_Tick( object sender, EventArgs e )
        {

            if ( disconnected )
            {
                timer3.Stop();
            }

            statusToolStripMenuItem.ForeColor = toolStripStatusLabel1.ForeColor;
            statusToolStripMenuItem.Text = toolStripStatusLabel1.Text;

            if ( currentRcon != null )
            {
                Ping ping = new Ping();
                try
                {
                    var result = ping.Send($"{currentAuth.Address}");

                    if ( result.Status == IPStatus.Success )
                    {
                        if ( disconnected )
                        {
                            Connect( currentAuth );
                        }
                    }
                }
                catch
                {
                    if ( reconnectAttemps != maxReconnectAttempts )
                    {

                        toolStripStatusLabel1.ForeColor = Color.Red;
                        if ( !disconnected )
                        {
                            toolStripStatusLabel1.Text = "Reconnecting.. " + reconnectAttemps;
                        }

                        if ( !HasNetworkConnection() )
                        {
                            toolStripStatusLabel1.Text = "No Network Connection";
                            reconnectAttemps = maxReconnectAttempts;

                            notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", "Disconnected. Reason: No Network Connection", ToolTipIcon.Warning );
                        }


                        if ( !HasInternetConnection() )
                        {
                            toolStripStatusLabel1.Text = "No Internet Connection";
                            reconnectAttemps = maxReconnectAttempts;

                            notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", "Disconnected. Reason: No Internet Connection", ToolTipIcon.Warning );
                        }

                        toolStripStatusLabel4.Text = "";
                        toolStripStatusLabel3.Text = "";
                        toolStripStatusLabel1.Image = Properties.Resources.cross;
                        reconnectAttemps += 1;
                        disconnected = true;
                        disconnectToolStripMenuItem_Click( sender, e );
                        timer3.Stop();
                    }
                    else
                    {
                        toolStripStatusLabel1.ForeColor = Color.Red;
                        toolStripStatusLabel1.Text = "Disconnected.";

                        toolStripStatusLabel1.Image = Properties.Resources.cross;
                        toolStripStatusLabel4.Text = "";
                        toolStripStatusLabel3.Text = "";
                        currentRcon = null;

                        disconnectToolStripMenuItem_Click( sender, e );
                        timer3.Stop();
                    }
                }
                panel3.Enabled = false;
            }
            else
            {
                panel3.Enabled = true;
            }
        }

        private async void Connect( Authentication authentication )
        {
            if ( !HasNetworkConnection() )
            {
                toolStripStatusLabel1.Text = "No Network Connection";
                reconnectAttemps = maxReconnectAttempts;

                notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", "Disconnected. Reason: No Network Connection", ToolTipIcon.Warning );

                return;
            }


            if ( !HasInternetConnection() )
            {
                toolStripStatusLabel1.Text = "No Internet Connection";
                reconnectAttemps = maxReconnectAttempts;

                notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", "Disconnected. Reason: No Internet Connection", ToolTipIcon.Warning );

                return;
            }


            if ( currentRcon == null )
                currentRcon = new Rcon( authentication );

            currentRcon.OnAvaliabilityCompleted += ( s, e ) =>
            {
                if ( e )
                {
                    SetLogText( "Server Reachable ✅" );
                }
            };
            currentRcon.OnError += ( sender, e ) =>
            {
                richTextBox1.Invoke( () =>
                {
                    SetErrorLogMessage( $"ERROR: {e.Message}" );
                    timer3.Stop();
                    timer4.Stop();
                } );
            };

            toolStripStatusLabel1.ForeColor = Color.Black;

            toolStripStatusLabel1.Text = "Connecting to " + authentication.Address;

            try
            {
                if ( await currentRcon.ConnectAsync() )
                {
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    toolStripStatusLabel1.Text = "Connected ✅";

                    if ( await currentRcon.AuthenticateAsync() )
                    {
                        toolStripStatusLabel1.ForeColor = Color.DarkGreen;
                        toolStripStatusLabel1.Text = "Authenticated";

                        toolStripStatusLabel1.Image = Properties.Resources.check;

                        disconnected = false;

                        playersToolStripMenuItem1.Enabled = true;
                        broadcastToolStripMenuItem.Enabled = true;

                        disconnectToolStripMenuItem1.Enabled = true;
                        notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", $"Connected & Authenticated to {authentication.Address}:{authentication.Port}", ToolTipIcon.Info );

                        if ( _currentItem.rtgl || _currentItem.rtpl )
                        {
                            timer3.Start();
                        }

                        if ( _currentItem.mb )
                        {
                            timer4.Start();
                        }
                        reconnectAttemps = 1;

                        if ( _currentItem.cheats )
                        {
                            SetLogText( "Enabling cheats" );
                            if ( await currentRcon.EnableCheats() )
                                SetLogText( "Cheats Enabled ✅" );
                        }
                    }
                    else
                    {
                        toolStripStatusLabel1.ForeColor = Color.Red;
                        toolStripStatusLabel1.Text = "Connected";

                        toolStripStatusLabel1.Image = Properties.Resources.cross;

                        disconnected = false;
                    }
                }
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "Unable to connect. Server may be down.";


                toolStripStatusLabel1.Image = Properties.Resources.cross;


                disconnected = false;
            }
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

                timer3_Tick( this, EventArgs.Empty );
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
            if ( currentRcon != null )
            {
                disconnectToolStripMenuItem.Enabled = true;
            }
            else
            {
                disconnectToolStripMenuItem.Enabled = false;
            }

            if ( currentRcon == null )
            {
                playersToolStripMenuItem.Enabled = false;
                toolsToolStripMenuItem.Enabled = false;

            }
            else
            {
                toolsToolStripMenuItem.Enabled = true;
                playersToolStripMenuItem.Enabled = true;
            }

            if ( disconnected )
            {
                button1.Enabled = false;
                textBox1.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                textBox1.Enabled = true;
                comboBox1.Enabled = true;
            }
        }

        private void disconnectToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( currentRcon != null && currentRcon.Disconnect() )
            {
                disconnected = true;
                currentRcon = null;


                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "Disconnected";
                toolStripStatusLabel1.Image = Properties.Resources.cross;
                toolStripStatusLabel4.Text = "";
                toolStripStatusLabel3.Text = "";

                playersToolStripMenuItem1.Enabled = false;
                broadcastToolStripMenuItem.Enabled = false;
                disconnectToolStripMenuItem1.Enabled = false;

                notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", $"Disconnected", ToolTipIcon.Info );
                timer3.Enabled = false;
                timer4.Enabled = false;
                panel8.Controls.Clear();
            }
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
                    SetErrorLogMessage( result.Replace( "\n", "" ) );
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

            if ( toolStripStatusLabel1.ForeColor == Color.Red )
            {
                SetErrorLogMessage( toolStripStatusLabel1.Text );
            }
            else
            {
                SetLogText( toolStripStatusLabel1.Text, toolStripStatusLabel1.ForeColor, Color.White );
            }
        }

        void SetErrorLogMessage( string message )
        {
            SetLogText( message, Color.White, Color.Red );
        }
        void SetLogText( string message, Color fontColor, Color background )
        {
            richTextBox1.SelectionColor = fontColor;
            richTextBox1.SelectionBackColor = background;
            richTextBox1.SelectedText = message + "\n";
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionBackColor = Color.White;
            richTextBox1.ScrollToCaret();
        }
        void SetLogText( string message ) => SetLogText( message, Color.Black, Color.White );

        private void Form1_FormClosed( object sender, FormClosedEventArgs e )
        {
            if ( exitApp )
                Process.GetCurrentProcess().Kill();

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

        int lastPlayerCount = 0;
        /// <summary>
        /// Called by timer. Compares last known player list with current one.
        /// Detects joins and leaves.
        /// </summary>
        private List<string> _lastPlayers = new List<string>();
        private List<ASAPlayer> joined1 = new List<ASAPlayer>();

        private List<string> _lastLog = new List<string>();
        private async void timer3_Tick( object sender, EventArgs e )
        {

            if ( currentRcon != null && currentRcon.IsConnected && !disconnected )
            {
                var players = await currentRcon.GetPlayerList();
                var currentPlayers = players
              .Where( p => p.ValidPlayer )
              .Select( p => p.Name )
              .ToList();

                if ( _currentItem.rtpl )
                {
                    panel8.Controls.Clear();

                    // detect joins
                    var joined = currentPlayers.Except(_lastPlayers).ToList();

                    // detect leaves
                    var left = _lastPlayers.Except(currentPlayers).ToList();


                    joined1.Clear();
                    // notify joins
                    foreach ( var name in joined )
                    {
                        notifyIcon1.ShowBalloonTip( 10, "Players", $"{name} has joined {ServerName}", ToolTipIcon.Info );

                        SetLogText( $"{name} has joined {ServerName}\n" );

                        foreach ( var player in players )
                            if ( player.Name == name || player.Name.Contains( name ) )
                                joined1.Add( player );
                    }

                    // notify leaves
                    foreach ( var name in left )
                    {
                        notifyIcon1.ShowBalloonTip( 10, "Players", $"{name} has left {ServerName}", ToolTipIcon.Info );
                        SetLogText( $"{name} has left {ServerName}\n" );
                    }
                    // save for next tick diff
                    _lastPlayers = currentPlayers;


                    foreach ( var i in players )
                    {
                        var playerItem = new PlayerUserControl(i, i.ValidPlayer);
                        playerItem.Dock = DockStyle.Top;
                        panel8.Controls.Add( playerItem );
                    }

                    toolStripStatusLabel4.Text = $"{currentPlayers.Count} Players Online";
                    toolStripStatusLabel3.Text = "|";
                }

                if ( _currentItem.rtgl )
                {
                    var currentLogs = await currentRcon.GetServerLog();
                    var logs = currentLogs.ToList().Except(_lastLog).ToList();
                    var previous = _lastLog.Except(currentLogs).ToList();

                    foreach ( var log in logs )
                    {
                        if ( !log.Contains( "Server received" ) && !string.IsNullOrWhiteSpace( log ) )
                            SetLogText( log );
                    }

                    _lastLog = currentLogs.ToList();
                }

                if ( _currentItem.mb )
                {
                    currentTime += 5;

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
            };

            textBox1.PlaceholderText = text;
        }

        private void panel3_Paint( object sender, PaintEventArgs e )
        {
        }

        private void menuStrip1_Paint( object sender, PaintEventArgs e )
        {

        }

        private void button2_Click( object sender, EventArgs e )
        {
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

        int currentTime = 0;
        int currentMessageIndex = 0;
        private async void timer4_Tick( object sender, EventArgs e )
        {

        }

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
            e.Graphics.FillRectangle( new LinearGradientBrush( e.ClipRectangle, Color.White, Color.WhiteSmoke, LinearGradientMode.Vertical ), e.ClipRectangle );
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