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

            Tablero t = new Tablero(tabEj, pendEj);
            t.Render();

            char c = ' ';

            while(c != 'q')
            {
                c = LeeInput();

                if(c != ' ')
                {
                    t.MueveCursor(c);
                    t.Render();
                }
            }

            // completar...

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
