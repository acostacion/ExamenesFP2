using System;
using System.IO;
using System.Net.Security;


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

            Tablero tablero = new Tablero(tabEj, pendEj);
            tablero.Render();

            while (!tablero.FinJuego() || c == 'q')
            {
                ProcesaInput(tablero, ref c);
                tablero.Render();
            }
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

        static void ProcesaInput(Tablero tab, ref char c)
        {
            if(c == 'l' || c == 'r' || c== 'u' || c== 'd')
            {
                tab.MueveCursor(c);
            }
            else if (c == 's')
            {
                tab.QuitaNumero();
            }
            else if(c <= '1' && c >= '9') // O es sin las comillas?
            {
                tab.PonNumero((int)(c - '0'));
            }
        }

        static void LeeNivel(string file, int[,] tb, int[] pd)
        {
            file = "level/ex.txt";
            StreamReader f = new StreamReader(file);

            // FILAS Y COLUMNAS.
            string[] digs = f.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int fil = int.Parse(digs[0]);
            int col = int.Parse(digs[1]);

            tb = new int[fil, col];

            Array.Clear(digs, 0, digs.Length);

            // TB.
            for(int i = 0; i < tb.GetLength(0); i++)
            {
                digs = f.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < tb.GetLength(1); j++)
                {
                    tb[i, j] = int.Parse(digs[j]);
                }

                Array.Clear(digs,0,digs.Length);
            }

            // DIGS.
            digs = f.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i< digs.Length; i++)
            {
                pd[i] = int.Parse(digs[i]);
            }

        }

    }
}
