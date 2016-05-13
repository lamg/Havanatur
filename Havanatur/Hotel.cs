using System;

namespace Havanatur
{
	public class Hotel
	{
		public Hotel (string nombre, Temporada[] ts, Suplemento[] ss, Combinacion[] cs)
		{
			Nombre = nombre;
			Temporadas = ts;
			Suplementos = ss;
			Combinaciones = cs;
		}

		public string Nombre{ get; private set;}
		public Temporada[] Temporadas{ get; private set;}
		public Suplemento[] Suplementos{ get; private set;}
		public Combinacion[] Combinaciones { get; private set;}
	}
}

