using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace client
{
	public partial class FormMain : Form
	{
		private AsyncClient _client;

		public FormMain()
		{
			InitializeComponent();

			Shown += ( sender, args ) => init();
		}

		private void init()
		{
#region [ AsyncClient ]

			_client = new AsyncClient();

			_client.StatusUpdated += ClientOnStatusUpdated;

			_client.ReceivedMessage += ClientOnReceivedMessage;

			_client.Connected += ClientOnConnected;

			_client.start( 2222 );

			tss_status_label.BackColor = Color.DarkOrange;

#endregion

			tb_chat_message.KeyPress += handleChatMessageKeyPress;
		}

#region [ Client Event ]

		private void clientUpdateStatus( string message )
		{
			tss_status.Text = message;
		}

		private void ClientOnStatusUpdated( object source, ClientEventArgs args )
		{
			var message = args.message;

			if ( ss_main.InvokeRequired )
			{
				BeginInvoke( new Action( () => clientUpdateStatus( message ) ) );
			}
			else
			{
				clientUpdateStatus( message );
			}
		}

		private void clientUpdateConnectionStatus()
		{
			tss_status_label.BackColor = Color.LimeGreen;
		}

		private void ClientOnConnected( object source, ClientEventArgs args )
		{
			if ( ss_main.InvokeRequired )
			{
				BeginInvoke( new Action( clientUpdateConnectionStatus ) );
			}
			else
			{
				clientUpdateConnectionStatus();
			}
		}

		private void clientReceivedMessage( string message )
		{
			tb_chat_history.AppendText( $@"[ Server ]: {message + Environment.NewLine}" );
		}

		private void ClientOnReceivedMessage( object source, ClientEventArgs args )
		{
			var message = args.message;

			if ( tb_chat_history.InvokeRequired )
			{
				BeginInvoke( new Action( () => clientReceivedMessage( message ) ) );
			}
			else
			{
				clientReceivedMessage( message );
			}

			_client?.receive();
		}

#endregion

#region [ Form Event ]

		private void handleChatMessageKeyPress( object sender, KeyPressEventArgs e )
		{
			if ( e.KeyChar == 13 ) // Enter
			{
				sendMessage( tb_chat_message.Text, true );
			}
		}

#endregion

		// DEBUG
		private void timer1_Tick( object sender, EventArgs e )
		{
			sendMessage( Process.GetCurrentProcess().Id.ToString() );
		}

		private void sendMessage( string message = "", bool manual = false )
		{
			if ( !string.IsNullOrWhiteSpace( message ) )
			{
				_client?.receive();

				_client?.send( message );

				tb_chat_history.AppendText( $@"[ Me ]: {message + Environment.NewLine}" );

				if ( manual )
					tb_chat_message.Clear();
			}
		}
	}
}