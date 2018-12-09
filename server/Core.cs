using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using IniHelper = ZUtility.ZIniParser;

namespace server
{
	internal static class Program
	{
		private static Encoding _encoding = Encoding.UTF8;
		private const int _BUFFER_SIZE = 1024;
		private static int _client_id = -1;

		// TODO: read from settings.ini
		private const string _CONFIG_PATH = ".\\config.ini";
		private const string _CONFIG_CONNECTION_SECTION = "Connection";

		private static int _backlog; // maximum length of the pending connections queue
		private static int _port;
		private static string _host;

		private static void Main()
		{
			if ( readConfig() )
			{
				Console.WriteLine( "Initializing..." );

				var server = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp ) { ReceiveTimeout = -1 };
				var endpoint = new IPEndPoint( IPAddress.Parse( _host ), _port );

				try
				{
					server.Bind( endpoint );
					server.Listen( _backlog );
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
			else
			{
				Console.WriteLine( $"Failed to load server config: {_CONFIG_PATH}" );
			}
		}

		private static void Process(
			Socket client,
			int    id )
		{
			var log_prefix = $"#{id}_[ {client.RemoteEndPoint} ]: ";

			Console.WriteLine( log_prefix + "connected" );

			while ( true )
			{
				var response = new byte[ _BUFFER_SIZE ];
				var received = client.Receive( response );

				if ( received == 0 )
				{
					Console.WriteLine( log_prefix + "closed connection" );

					return;
				}

				var resp_bytes_list = new List< byte >( response );
				resp_bytes_list.RemoveRange( received, _BUFFER_SIZE - received );

				var message = _encoding.GetString( resp_bytes_list.ToArray() );

				Console.WriteLine( log_prefix + message );

				if ( message.Contains( "/time" ) )
				{
					client.Send( _encoding.GetBytes( DateTime.Now.ToString( "h:mm:ss tt zz" ) ) );
				}
				else
				{
					client.Send( _encoding.GetBytes( message ) ); // simple echo
				}
			}
		}

		private static bool readConfig(
			string config_path = _CONFIG_PATH )
		{
			Console.WriteLine( $"Reading server config: {config_path} ..." );

			var success = true;

			success = int.TryParse( IniHelper.readValue( _CONFIG_CONNECTION_SECTION, "backlog", config_path ), out _backlog );
			success = success && int.TryParse( IniHelper.readValue( _CONFIG_CONNECTION_SECTION, "port", config_path ), out _port );

			_host = IniHelper.readValue( _CONFIG_CONNECTION_SECTION, "host", config_path );

			success = success && ( !string.IsNullOrWhiteSpace( _host ) );

			return success;
		}
	}
}