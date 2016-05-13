using System;

namespace Havanatur
{
	public class Temporada
	{
		public Temporada (string nombre, string fecha, string tipoHab, string[][] planAlimentario)
		{
			Nombre = nombre;
			Fecha = fecha;
			TipoHab = tipoHab;
			PlanAlimentario = planAlimentario;
		}

		public string Nombre { get; private set;}
		public string Fecha { get; private set;}
		public string TipoHab { get; private set;}
		public string[][] PlanAlimentario { get; private set;}

	}
}

