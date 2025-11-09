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
                            toolStripStatusLabel1.Text = "Disconnected. No Network Connection";
                            reconnectAttemps = maxReconnectAttempts;
                        }


                        if ( !HasInternetConnection() )
                        {
                            toolStripStatusLabel1.Text = "Disconnected. No Internet Connection";
                            reconnectAttemps = maxReconnectAttempts;
                        }


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

                        if ( !HasNetworkConnection() )
                        {
                            toolStripStatusLabel1.Text = "Disconnected. No Network Connection";
                        }


                        if ( !HasInternetConnection() )
                        {
                            toolStripStatusLabel1.Text = "Disconnected. No Internet Connection";
                        }
                        currentRcon = null;

                        disconnectToolStripMenuItem_Click( sender, e );
                    }
                }
            }
        }

        private async void Connect( Authentication authentication )
        {
            reconnectAttemps = 1;
            if ( currentRcon == null )
                currentRcon = new Rcon( authentication );

            toolStripStatusLabel1.ForeColor = Color.Black;

            toolStripStatusLabel1.Text = "Connecting to " + authentication.Address;

            try
            {
                await currentRcon.ConnectAsync();
                toolStripStatusLabel1.ForeColor = Color.Black;
                toolStripStatusLabel1.Text = "Connected.. Authenticating";

                if ( await currentRcon.AuthenticateAsync() )
                {
                    toolStripStatusLabel1.ForeColor = Color.DarkGreen;
                    toolStripStatusLabel1.Text = "Connected, and Authenticated.";

                    toolStripStatusLabel1.Image = Properties.Resources.check;

                    disconnected = false;
                }
                else
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "Connected, not authenticated.";

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
            if ( currentRcon.Disconnect() )
            {
                disconnected = true;
                currentRcon = null;


                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "Disconnected";
                toolStripStatusLabel1.Image = Properties.Resources.cross;
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
            if ( currentRcon != null )
            {
                currentRcon.Disconnect();
                currentRcon.Dispose();

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
    }
}