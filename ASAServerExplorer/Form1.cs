using System.Net.NetworkInformation;
using System.Windows.Forms.Design;

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


        public Form1()
        {
            InitializeComponent();

            disconnected = true;

            Instance = this;
        }



        private async void button1_Click( object sender, EventArgs e )
        {

        }

        private void timer1_Tick( object sender, EventArgs e )
        {
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
                    }
                }
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

            toolStripStatusLabel1.ForeColor = Color.Black;

            toolStripStatusLabel1.Text = "Connecting to " + authentication.Address;

            try
            {
                await currentRcon.ConnectAsync();
                toolStripStatusLabel1.ForeColor = Color.Black;
                toolStripStatusLabel1.Text = "Connected";

                if ( await currentRcon.AuthenticateAsync() )
                {
                    toolStripStatusLabel1.ForeColor = Color.DarkGreen;
                    toolStripStatusLabel1.Text = "Authenticated.";

                    toolStripStatusLabel1.Image = Properties.Resources.check;

                    disconnected = false;

                    playersToolStripMenuItem1.Enabled = true;
                    broadcastToolStripMenuItem.Enabled = true;

                    disconnectToolStripMenuItem1.Enabled = true;
                    notifyIcon1.ShowBalloonTip( 10, "ASA Server Manager", $"Connected & Authenticated to {authentication.Address}:{authentication.Port}", ToolTipIcon.Info );

                    reconnectAttemps = 1;
                }
                else
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "Connected";

                    toolStripStatusLabel1.Image = Properties.Resources.cross;

                    disconnected = false;
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
            Close();
        }
        public string ServerName = "";
        private void listToolStripMenuItem_Click( object sender, EventArgs e )
        {
            ServerList list = new ServerList();
            list.OnConnect += async ( e, s ) =>
            {
                ServerName = s;
                currentAuth = e;

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
            }
        }

        private async void button1_Click_1( object sender, EventArgs e )
        {
            try
            {
                richTextBox1.ForeColor = Color.Black;
                var command = (Command)Enum.Parse(typeof(Command), comboBox1.SelectedItem.ToString());

                var result =await currentRcon.SendCommandAsync( command, default, textBox1.Text.Split( " " ) );

                richTextBox1.AppendText( result );
            }
            catch
            {
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectedText = "Unable to send command\n";
            }
        }

        private void playersToolStripMenuItem_Click( object sender, EventArgs e )
        {
            PlayerList list = new PlayerList(currentRcon);
            list.ShowDialog();
        }

        private void toolStripStatusLabel1_Click( object sender, EventArgs e )
        {

        }

        private void toolStripStatusLabel1_RightToLeftChanged( object sender, EventArgs e )
        {

        }

        private void toolStripStatusLabel1_TextChanged( object sender, EventArgs e )
        {
            richTextBox1.SelectionColor = toolStripStatusLabel1.ForeColor;
            richTextBox1.SelectedText = toolStripStatusLabel1.Text + "\n";
        }

        private void Form1_FormClosed( object sender, FormClosedEventArgs e )
        {

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
                    richTextBox1.SelectionColor = Color.Black;
                    richTextBox1.SelectedText = "MessageOfTheDay Set To " + item;

                }
                else
                {
                    richTextBox1.SelectionColor = Color.Red;
                    richTextBox1.SelectedText = "There was an issue sending this command.";
                }
            }
        }

        int lastPlayerCount = 0;
        /// <summary>
        /// Called by timer. Compares last known player list with current one.
        /// Detects joins and leaves.
        /// </summary>
        private List<string> _lastPlayers = new List<string>();

        private async void timer3_Tick( object sender, EventArgs e )
        {

            if ( currentRcon == null )
                return;

            // get latest players
            var players = await currentRcon.GetPlayerList();
            var currentPlayers = players
               .Where(p => p.ValidPlayer)
               .Select(p => p.Name)
               .ToList();

            // detect joins
            var joined = currentPlayers.Except(_lastPlayers).ToList();

            // detect leaves
            var left = _lastPlayers.Except(currentPlayers).ToList();

            // notify joins
            foreach ( var name in joined )
            {
                notifyIcon1.ShowBalloonTip( 10, "Players", $"{name} has joined {ServerName}", ToolTipIcon.Info );
                richTextBox1.AppendText( $"{name} has joined {ServerName}\n" );
            }

            // notify leaves
            foreach ( var name in left )
            {
                notifyIcon1.ShowBalloonTip( 10, "Players", $"{name} has left {ServerName}", ToolTipIcon.Info );
                richTextBox1.AppendText( $"{name} has left {ServerName}\n" );
            }

            // update status bar
            toolStripStatusLabel4.Text = $"{currentPlayers.Count} Players Online";
            toolStripStatusLabel3.Text = "|";

            // save for next tick diff
            _lastPlayers = currentPlayers;
        }

        async Task<bool> StillConnected(string playerName)
        {
            var players = await currentRcon.GetPlayerList();

            foreach ( var player in players )
                if ( player.Name.Contains(playerName) )
                    return true;

            return false;
        }
        private void notifyIcon1_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            this.Show();
        }

        private void Form1_FormClosing( object sender, FormClosingEventArgs e )
        {
            if ( exitApp )
            {
                this.Close();
            }
            else
            {
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
                    richTextBox1.SelectionColor = Color.Black;
                    richTextBox1.SelectedText = "Message Broadcasted";
                    notifyIcon1.ShowBalloonTip( 10, "Broadcasted", "Message Broadcasted", ToolTipIcon.Info );
                }
                catch
                {
                    richTextBox1.SelectionColor = Color.Red;
                    richTextBox1.SelectedText = "Error while broadcasting message";
                    notifyIcon1.ShowBalloonTip( 10, "Broadcasted", "Error while broadcasting message", ToolTipIcon.Error );
                }
            }
        }
        bool exitApp = false;
        private void playersToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            PlayerList list = new PlayerList(currentRcon);
            list.ShowDialog();
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
            list.OnConnect += async ( e, s ) =>
            {
                ServerName = s;
                currentAuth = e;

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
    }
}