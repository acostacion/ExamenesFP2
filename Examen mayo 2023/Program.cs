using System;
using System.IO;


namespace puzlogic
{
    class Program
    {
        static void Main()
        {       
            // ejemplo del enunciado
            // tablero    
            int [,] tabEj = new int[5,5]
                  {{ 0,-1, 0,-1, 5},
                   {-1, 3,-1, 0,-1},
                   { 6,-1,-1,-1, 0},
                   {-1, 0,-1, 6,-1},
                   { 5,-1, 4,-1, 0}};
            // pendientes
            int [] pendEj =  new int[6] {4,5,6,4,5,6};

            char c = LeeInput();
            string file = "ex.txt";

            Tablero t = new Tablero();

            Console.Write("¡Hola!, ¿desea plantilla o archivo? 1/2");
            if(int.Parse(Console.ReadLine()) == 1)
            {
                t = new Tablero(tabEj, pendEj);
            }
            else if (int.Parse(Console.ReadLine()) == 2)
            {
                LeeNivel(file, out int[,] tb, out int[] pb);
            }

            t.Render();

            while (!t.FinJuego() || c == 'q')
            {
                ProcesaInput(t, c);
                t.Render();
            }
            
        }

        // [NOTA MENTAL] Los del program ponerlos siempre static.

		static char LeeInput(){		    			
			char d=' ';	
            if (Console.KeyAvailable) {			
    			string tecla = Console.ReadKey (true).Key.ToString ();
    			switch (tecla) {
    				case "LeftArrow":  d = 'l'; break;
    				case "UpArrow":    d = 'u'; break;
    				case "RightArrow": d = 'r';	break;
    				case "DownArrow":  d = 'd';	break;
                    case "Spacebar":                    // borrar num
                    case "S":          d = 's'; break; 
    				case "Escape":                      // salir
    				case "Q":          d = 'q'; break;
                    case "P":          d = 'p'; break;
                    // lectura de dígito
        			default:			
        				if (tecla.Length==2 && tecla[0]=='D' && tecla[1]>='0' && tecla[1]<='9') d=tecla[1];
        				else d = ' ';
        				break;
    			}		
                while (Console.KeyAvailable) 
    				Console.ReadKey ().Key.ToString ();
            }
			return d;
		}

        static void ProcesaInput(Tablero tab, char c)
        {
            if (c == 'r' || c == 'l' || c == 'u' || c == 'd') tab.MueveCursor(c);
            else if (c == 's') tab.QuitaNumero();
            // Parte rayante.
            else 
            {
                int val = (int)(c - '0');
                if (val <= 9 || val >= 1) tab.PonNumero(val);
            }
        }

        static void LeeNivel(string file, out int[,] tb, out int[] pd)
        {
            if(File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);
                string[] filcol = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                tb = new int[int.Parse(filcol[0]), int.Parse(filcol[1])];

                // Cosa turbia.
                for(int i = 0; i < tb.GetLength(0); i++)
                {
                    // Lee la línea.
                    sr.ReadLine();
                    // Trocea la línea. ¿Se sobreescribe?
                    string[] tablero = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < tb.GetLength(1); j++)
                    {
                        // Mete de una en una la línea.
                        tb[i,j] = int.Parse(tablero[j]);
                    }

                    //for (int j = 0; j < tablero.Length; j++) tablero[j] = null;
                }

                sr.ReadLine();
                string[] pendientes = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for(int i = 0; i < pendientes.Length; i++) pd[i] = int.Parse(pendientes[i]);

                sr.Close();
            }
        }

    }
}
