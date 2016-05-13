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

		public string Nombre { get; }
		public string Fecha { get; }
		public string TipoHab { get; }
		public string[][] PlanAlimentario { get; }

	}
}

