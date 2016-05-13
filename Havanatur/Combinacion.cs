using System;

namespace Havanatur
{
	public class Combinacion
	{
		public Combinacion (string nombre, string temporada, string tipoHab, string[][] descuentos)
		{
			Nombre = nombre;
			this.Temporada = temporada;
			TipoHabitacion = tipoHab;
			Descuentos = descuentos;
		}

		public string Nombre { get; private set;}
		public string Temporada { get; private set;}
		public string[][] Descuentos { get; private set;}
		public string TipoHabitacion { get; private set;}
	}
}

