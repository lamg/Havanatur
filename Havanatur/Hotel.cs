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

		public string Nombre{ get; }
		public Temporada[] Temporadas{ get; }
		public Suplemento[] Suplementos{ get; }
		public Combinacion[] Combinaciones { get; }
	}
}

