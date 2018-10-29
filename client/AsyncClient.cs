using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client
{
	public class ClientStateObject
	{
#region [ Data ]

		public Socket socket { get; set; }

		public const int BUFFER_SIZE = 1024;

		public byte[] buffer;

		public StringBuilder sb { get; }

#endregion

		public void appendBuffer( string data ) => sb.Append( data );

		public bool isBufferNotEmpty() => ( sb.Length > 0 );

		public ClientStateObject()
		{
			socket = null;

			buffer = new byte[ BUFFER_SIZE ];

			sb = new StringBuilder();
		}
	}

	// TODO: reconnect to server: 5 times with 2 second timeout (10 seconds in total)

	public class AsyncClient
	{
		public event ClientEventHandler StatusUpdated, ReceivedMessage, Connected;

		private string _response;

		private Socket _client;

		private Encoding _message_encoding;

		public AsyncClient( Encoding encoding = null )
		{
			_response = string.Empty;

			if ( encoding == null )
				encoding = Encoding.UTF8;

			_message_encoding = encoding;
		}

		public void start( int port )
		{
			try
			{
				var remote_ep = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), port );

				_client = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp ) { ReceiveTimeout = -1 };

				raiseClientEvent( StatusUpdated, $"Connecting to {remote_ep}..." );

				connect( remote_ep, _client );
			}
			catch ( Exception e )
			{
				printException( e );
			}
		}

		private void connect( EndPoint remote_ep, Socket socket )
		{
			socket.BeginConnect( remote_ep, connectCallback, socket );
		}

		public void send( string message )
		{
			try
			{
				var data = _message_encoding.GetBytes( message );

				_client.BeginSend( data, 0, data.Length, 0, sendCallback, _client );
			}
			catch ( Exception e )
			{
				printException( e );
			}
		}

		public void receive()
		{
			try
			{
				var state = new ClientStateObject { socket = _client };

				_client.BeginReceive( state.buffer, 0, ClientStateObject.BUFFER_SIZE, 0, receiveCallback, state );
			}
			catch ( Exception e )
			{
				printException( e );
			}
		}

#region [ Callback ]

		private void connectCallback( IAsyncResult async_result )
		{
			try
			{
				if ( async_result.AsyncState is Socket client )
				{
					client.EndConnect( async_result );

					raiseClientEvent( StatusUpdated, $"connected to {client.RemoteEndPoint}" );

					raiseClientEvent( Connected );
				}
			}
			catch ( Exception e )
			{
				printException( e );
			}
		}

		private void sendCallback( IAsyncResult async_result )
		{
			try
			{
				if ( async_result.AsyncState is Socket client )
				{
					var bytes_sent = client.EndSend( async_result );

					raiseClientEvent( StatusUpdated, $"Send {bytes_sent} bytes to server" );
				}
			}
			catch ( Exception e )
			{
				printException( e );
			}
		}

		private void receiveCallback( IAsyncResult async_result )
		{
			try
			{
				if ( async_result.AsyncState is ClientStateObject state )
				{
					var client = state.socket;
					var bytes_read = client.EndReceive( async_result );

					if ( bytes_read > 0 )
					{
						state.appendBuffer( _message_encoding.GetString( state.buffer, 0, bytes_read ) );

						if ( bytes_read == ClientStateObject.BUFFER_SIZE )
							client.BeginReceive( state.buffer, 0, ClientStateObject.BUFFER_SIZE, 0, receiveCallback, state );
					}

					if ( state.isBufferNotEmpty() )
					{
						_response = state.sb.ToString();

						raiseClientEvent( ReceivedMessage, _response );
					}
				}
			}
			catch ( Exception e )
			{
				printException( e );
			}
		}

#endregion


#region [ Utility ]

		private void raiseClientEvent( ClientEventHandler handler, string message = "" )
		{
			if ( handler != null )
				handler( this, new ClientEventArgs( message ) );
			else
			{
				if ( string.IsNullOrWhiteSpace( message ) )
					message = "connected";

				Console.WriteLine( $@"[ {GetType().Name} ]: '{message}'" );
			}
		}

		private void printException( Exception e ) => Console.WriteLine( $"E:\t[ {GetType().Name} ]: got exception: {e}" );

#endregion
	}

#region [ ClientEvent ]

	public delegate void ClientEventHandler( object source, ClientEventArgs args );

	public class ClientEventArgs : EventArgs
	{
		public string message { get; }

		public ClientEventArgs( string message )
		{
			this.message = message;
		}
	}

#endregion
}