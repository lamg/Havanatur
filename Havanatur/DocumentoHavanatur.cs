using System;

namespace Havanatur
{
	public interface DocumentoHavanatur
	{
		/// <summary>
		/// Retorna el valor de la celda especificada como cadena,
		/// debe retornar la cadena vacía si la celda está vacía.
		/// Además la cadena no debe tener espacios al principio o
		/// al final. El rango de los índices es [0, NCols*NFilas).
		/// </summary>
		/// <param name="i">Número de la fila</param>
		/// <param name="j">Número de la columna</param>
		string Value(int i, int j);
		int NCols{ get;}
		int NFilas{ get;}
	}
}

