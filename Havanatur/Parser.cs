using System;
using System.Collections.Generic;
using D = System.Diagnostics.Debug;

namespace Havanatur
{
	public class Parser
	{
		DocumentoHavanatur d;
		int n, total;

		#region Constantes del documento
		readonly string sTemp = "Temporada:", sSup = "Suplementos y descuentos:", 
		sDes = "Descuentos por combinaciones de edad:", sTar = "Tarifario:",
		sPHab = "Por habitación", sCP = "CP", sMAP = "MAP", sAP = "AP", sTI = "TI",
		sTHab = "Tipo de habitación:", sTp = "Tipo:", sNom = "Nombre:", sVal = "Valor:",
		sS = "Suplemento", sComb = "Combinación:", sClas = "Clasificación de edad",
		sV = "Valor", sPC = "%", sTHAB = "Tipo de Habitación:"; 
		#endregion

		public Parser (DocumentoHavanatur d)
		{
			D.Assert (d != null);
			this.d = d;
			n = -1;
			total = d.NFilas * d.NCols;
			Error = "";
		}

		#region Interfaz con el exterior(documento de entrada/mensajes al usuario)

		void FC (out int i, out int j)
		{
			D.Assert (0 <= n && n < total);
			i = n / d.NCols;
			j = n % d.NCols;
			D.Assert (i * d.NCols + j == n);
		}

		string ProxNoVacia ()
		{
			D.Assert (-1 <= n && n <= total && Error == "");
			var s = "";
			n++;
			D.Assert (0 <= n && n <= total && Error == "");
			while (s == "" && n != total) {				
				s = CeldaActual ();
				n++;
			}
			//{n = position of next non-empty cell in document d}
			D.Assert (0 <= n && n <= total && (s != "" || n == total) && Error == "");
			return s;
		}

		string CeldaActual ()
		{
			D.Assert (0 <= n && n < total, RE ("Celda fuera de rango"));
			int i, j;
			FC (out i, out j);
			return d.Value (i, j);
		}

		public string Error { get; private set; }

		string RE (string msg)
		{
			Error = msg;
			return msg;
		}

		string DM(){			
			return RE ("Documento malformado");
		}
		#endregion

		public Hotel RecHotel ()
		{
			Hotel r;
			D.Assert (-1 <= n && n < total, DM ());
			string c = "";
			while (c != sTar) {
				c = ProxNoVacia ();
				D.Assert (n != total, DM());
			}
			//{título, emisión, usuario y generales han sido saltados}
			var nombre = ProxNoVacia ();
			var ts = RecTemporadas ();
			var ss = RecSuplementos ();
			var cs = RecCombinaciones ();
			r = new Hotel (nombre, ts, ss, cs);

			return r;
		}

		#region Reconocimiento de temporada

		void ProxTemp ()
		{
			var c = CeldaActual ();
			while (n != total && c != sTemp && c != sSup && c != sDes) {
				D.Assert (n != total, DM());
				c = ProxNoVacia ();
			}
		}

		int ProxPlan ()
		{
			int a = 0;
			var c = CeldaActual ();
			while (c != sPHab) {
				D.Assert (n != total, DM());
				c = ProxNoVacia ();
				a++;
			}
			c = ProxNoVacia ();
			D.Assert (c == sCP || c == sMAP || c == sAP || c == sTI);
			return a;
		}

		Temporada[] RecTemporadas ()
		{
			D.Assert (0 <= n && n < total && Error == "");

			List<Temporada> ts = new List<Temporada> ();
			ProxTemp ();
			var c = CeldaActual ();		
			while (n != total && c != sSup && c != sDes) {
				D.Assert (c == sTemp, DM());
				var nombre = ProxNoVacia ();
				var fecha = ProxNoVacia ();
				c = ProxNoVacia ();
				D.Assert (c == sTHab);
				var thab = ProxNoVacia ();
				int a = ProxPlan ();
				List<string[]> pa = new List<string[]> ();
				while (c != sTHab && c != sTemp && c != sSup && c != sDes) {
					string[] pae = new string[a];
					for (int i = 0; i != a; i++) {
						pae [i] = CeldaActual ();
						n += 2;
					}
					pa.Add (pae);
				}
				var t = new Temporada (nombre, fecha, thab, pa.ToArray ());
				ts.Add (t);
				ProxTemp ();
			}
			return ts.ToArray ();
		}
		#endregion

		#region Reconocimiento de suplemento

		Suplemento RecSuplemento ()
		{
			var c = CeldaActual ();
			D.Assert (c == sTp);
			c = ProxNoVacia ();
			D.Assert (c == sS);
			c = ProxNoVacia ();
			D.Assert (c == sNom);
			var nombre = ProxNoVacia ();
			c = ProxNoVacia ();
			D.Assert (c == sVal);
			var valor = ProxNoVacia ();
			var r = new Suplemento (nombre, valor);
			return r;
		}

		void ProxSup ()
		{
			var c = CeldaActual ();
			while (n != total && c != sTp && c != sDes) {
				c = ProxNoVacia ();
			}
		}

		Suplemento[] RecSuplementos ()
		{
			var c = CeldaActual ();
			D.Assert (c == sSup || c == sDes);
			c = ProxNoVacia ();
			D.Assert (c == sTp);
			var ls = new List<Suplemento> ();
			while (n != total && c != sDes) {				
				D.Assert (n != total, DM());
				var s = RecSuplemento ();
				ls.Add (s);
				ProxSup ();
			}
			return ls.ToArray ();
		}

		#endregion

		Combinacion[] RecCombinaciones ()
		{
			var ls = new List<Combinacion> ();
			var c = CeldaActual ();
			D.Assert (n == total || c == sDes);
			c = ProxNoVacia ();
			while (n != total) {
				D.Assert (c == sComb);
				var nombre = ProxNoVacia ();
				c = ProxNoVacia ();
				D.Assert (c == sClas);
				c = ProxNoVacia ();
				D.Assert (c == sV);
				c = ProxNoVacia ();
				D.Assert (c == sPC);
				c = ProxNoVacia ();
				var temporada = "";
				var thab = "";
				if (c == sTemp) {
					c = ProxNoVacia ();
					temporada = c;
					c = ProxNoVacia ();
				}
				if (c == sTHAB) {
					c = ProxNoVacia ();
					thab = c;
					c = ProxNoVacia ();
				}
				var des = new List<string[]> ();
				while (n != total && c != sComb) {
					var fila = new string[2];
					fila [0] = c;
					c = ProxNoVacia ();
					fila [1] = c;
					c = ProxNoVacia ();
					des.Add (fila);
				}
				var comb = new Combinacion (nombre, temporada, thab, des.ToArray ());
				ls.Add (comb);
			}
			return ls.ToArray ();
		}
	}
}