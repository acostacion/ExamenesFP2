// Carmen Gómez Becerra
// Mesa de mi casa
using System;
using System.IO;
using Listas;

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

            Tablero t;
            

            // [NOTITA] Te has rayado un poco con el LeeArchivo pero no es complicado.
            Console.WriteLine("¿Preferirías leer de archivo o jugar con la plantilla base 1/2");
            if(1 == int.Parse(Console.ReadLine()))
            {
                string file = "ex.txt";
                // [IMPORTANTE] Cuando son por out copiar literalmente lo del método
                LeeNivel(file, out int[,] tb, out int[] pd);
                t = new Tablero (tb, pd); 
            }
            else
            {  
                t = new Tablero(tabEj, pendEj);
            }

            t.Render();
            // Siempre y cuando no se le haya dado a la q y no se haya terminado el juego...
            while (c != 'q' && !t.FinJuego())
            {
                c = LeeInput();
                t.Render();
                ProcesaInput(t, c);
                Thread.Sleep(200);
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

        static void ProcesaInput(Tablero tab, char c)
        {
            if(c == 'l' || c == 'r' || c == 'u' || c == 'd')
            {
                tab.MueveCursor(c);
            }
            else if (c == 's')
            {
                tab.QuitaNumero();
            }
            else if ((int)(c -'0') > 0 && (int)(c - '9') <= 9)
            {
                tab.PonNumero((int)(c - '0'));
            }
        }

        // [DESPISTE] es bastante probable que vayan por out.
        static void LeeNivel(string file, out int[,] tb, out int[] pd)
        {
            StreamReader sr = new StreamReader(file);

            string[] digs = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            tb = new int[int.Parse(digs[0]), int.Parse(digs[1])];

            for(int i = 0; i < tb.GetLength(0); i++)
            {
                digs = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < tb.GetLength(1); j++)
                {
                    tb[i,j] = int.Parse(digs[j]);
                }
            }

            // ¿Se sobreescriben los tres arrays? [DUDA]
            digs = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            pd = new int[digs.Length];

            for (int i = 0; i < digs.Length; i++)
            {
                pd[i] = int.Parse(digs[i]);
            }

            sr.Close();
        }


    }
}
