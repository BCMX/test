using System;
using Mono.Data.Sqlite;

/// <summary>
///  Schema de la tabla
/// CREATE TABLE folios (fecha varchar(10) primary key, consecutivo integer);
/// https://sqlite.org/sqlite.html
/// http://www.codeproject.com/Articles/22165/Using-SQLite-in-your-C-Application
/// </summary>

namespace generarConsecutivo
{
	public class folios
	{
		//fecha de hoy, no se si sea mejor que se genere en base a la fecha de la venta...
		private static string fecha = System.DateTime.Now.ToShortDateString();
		
		//cadena de conexion a DB
		private static string dbSource = "Data Source=/Users/jfernandez/Desktop/generarConsecutivo/generarConsecutivo/folios.db3";
		
		public static int generarConsecutivo { get; private set; }
		
		//constructor
		static folios() 
		{ 
			try {
				//conexion a DB
				var connection = new SqliteConnection(dbSource);
				connection.Open();
				
				//crear query
				var query = connection.CreateCommand();
				query.CommandText = "select consecutivo from folios where fecha='" + fecha + "'";
				
				//inicia en el folio de la DB
				generarConsecutivo=Convert.ToInt16(query.ExecuteScalar());
				
				//si el valor es 0, entonces creamos un nuevo renglon para ese dia
				//esto del 0 no necesariamente funciona en VistaDB
				if(generarConsecutivo.Equals(0))
				{
					generarConsecutivo=1;
					query = connection.CreateCommand();
					query.CommandText = "insert into folios (fecha,consecutivo) values ('" + fecha + "'," + generarConsecutivo.ToString() + ")";
					query.ExecuteNonQuery();
				}
								
				//cerrar conexion a DB
				connection.Close();
				
			} catch(Exception ex) {
				//en caso de error en la base de datos empezamos en 1
				generarConsecutivo=1; 
			}
		}
		
		
		public static void actualizarConsecutivo()
		{
			//incrementamos
			generarConsecutivo++;
			
			//grabamos en DB
			try {			
				var connection = new SqliteConnection(dbSource);
				connection.Open();
				var query = connection.CreateCommand();
				query.CommandText = "update folios set consecutivo=" + generarConsecutivo.ToString() + " where fecha='" + fecha + "'";
				query.ExecuteNonQuery();
				connection.Close();
			} catch(Exception ex) {
				//error en la DB no se actualiza la base de datos, pero sigue funcionando el folio
				//en caso que se reinicie la compu el folio iniciaria en 1
				//sigue siendo util
			}
		}
	}
}
