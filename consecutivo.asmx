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
        
        [WebMethod]
        public bool enLinea()
        {
        	return true;
        }
	}
}
