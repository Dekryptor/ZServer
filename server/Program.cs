using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server
{
	internal static class Program
	{
		private static Encoding _encoding = Encoding.UTF8;
		private const int _BUFFER_SIZE = 1024;
		private static int _client_id = -1;

		private static void Main()
		{
			Console.WriteLine( "Initializing..." );

			const int backlog = -1;
			const int port = 2222;

			var server = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp ) { ReceiveTimeout = -1 };
			var endpoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), port );

			try
			{
				server.Bind( endpoint );
				server.Listen( backlog );
			}
			catch ( Exception )
			{
				Console.WriteLine( "Listening failed!" );
				Console.ReadKey();

				return;
			}

			Console.WriteLine( $"Started listening @ {endpoint}" );

			while ( true )
			{
				var client = server.Accept();

				new System.Threading.Thread( () =>
											 {
												 try
												 {
													 Process( client, ++_client_id );
												 }
												 catch ( Exception ex )
												 {
													 Console.WriteLine( $"Client connection processing error: {ex.Message}. Clients left: {_client_id}" );
												 }
											 } ).Start();
			}
		}

		private static void Process( Socket client, int id )
		{
			Console.WriteLine( $"#{id}_[ {client.RemoteEndPoint} ]: connected" );

			while ( true )
			{
				var response = new byte[ _BUFFER_SIZE ];
				var received = client.Receive( response );

				if ( received == 0 )
				{
					Console.WriteLine( $"#{id}_[ {client.RemoteEndPoint} ]: closed connection" );

					return;
				}

				var resp_bytes_list = new List< byte >( response );
				resp_bytes_list.RemoveRange( received, _BUFFER_SIZE - received );

				var message = _encoding.GetString( resp_bytes_list.ToArray() );

				Console.WriteLine( $"#{id}_[ {client.RemoteEndPoint} ]: {message}" );

				if ( message.Contains( "lesha sasat" ) )
				{
					client.Send( _encoding.GetBytes( "orda SoSaT'" ) );
				}
				else
				{
					client.Send( _encoding.GetBytes( message ) ); // simple echo
				}
			}
		}
	}
}