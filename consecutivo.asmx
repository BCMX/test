<%@ WebService Language="C#" Class="generarConsecutivo.consecutivo" %>

using System;
using System.Web.Services;

namespace generarConsecutivo
{
  internal class consecutivo
	{
		private int folio;
		      
		[WebMethod]
        	public int generarFolio()
        	{
 			folio = folios.generarConsecutivo;
 			folios.actualizarConsecutivo();
        		return folio;
        	}
        
        	///metodo para revisar si el server esta en linea (true esta en linea)
        	[WebMethod]
        	public bool enLinea()
        	{
        		return true;
        	}
	}
}
