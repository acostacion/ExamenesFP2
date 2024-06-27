// Carmen Gómez Becerra
// Autoescuela Faustino. Tomelloso.

using System;
using System.IO;

namespace puzlogic
{
    class Program
    {
        static void Main()
        {
            string file = "ex.txt";
            Tablero t;
            Console.Write("¿Desea usar con la plantilla (1) o cargar archivo (2)? ");
            if(int.Parse(Console.ReadLine()) == 1)
            {
                // ejemplo del enunciado
                // tablero    
                int[,] tabEj = new int[5, 5]
                      {{ 0,-1, 0,-1, 5},
                   {-1, 3,-1, 0,-1},
                   { 6,-1,-1,-1, 0},
                   {-1, 0,-1, 6,-1},
                   { 5,-1, 4,-1, 0}};
                // pendientes
                int[] pendEj = new int[6] { 4, 5, 6, 4, 5, 6 };
                t = new Tablero(tabEj, pendEj);
            }
            else
            {
                LeeNivel(file, out int[,] tb, out int[] pd);
                t = new Tablero(tb, pd);
            }

            t.Render();

            char c = ' ';

            while(c != 'q' && !t.FinJuego())
            {
                c = LeeInput();

                if(c != ' ')
                {
                    ProcesaInput(t, c);
                    t.Render();
                }
            }

            // completar...

        }

        static void ProcesaInput(Tablero tab, char c)
        {
            if (c == 'l' || c == 'r' || c == 'u' || c == 'd')
            {
                tab.MueveCursor(c);
            }
            else if(c == 's')
            {
                tab.QuitaNumero();
            }
            else // Parte rayante (he mirado esto en la anterior práctica porque me he rayado).
            {
                int val = (int)(c - '0');
                if (val <= 9 || val >= 1) tab.PonNumero(val);
            }
        }

        static void LeeNivel(string file, out int[,] tb, out int[] pd)
        {
            // Lee de file un tablero y un array de pends.

            StreamReader sr = new StreamReader(file);
            string[] filCols = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int fils = int.Parse(filCols[0]);
            int cols = int.Parse(filCols[1]);

            tb = new int[fils, cols];
            
            for(int i = 0; i < fils; i++)
            {
                string[] digs = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < cols; j++)
                {
                    tb[i, j] = int.Parse(digs[j]);
                }
            }

            string[] pends = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            pd = new int[pends.Length];

            for(int i = 0; i < pends.Length; i++)
            {
                pd[i] = int.Parse(pends[i]);
            }


            sr.Close();
        }

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

    }
}
