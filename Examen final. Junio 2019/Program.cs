// Carmen Gómez Becerra
// El comedor de Tomelloso.

using System;
using System.IO;
using Listas;

namespace takuzu
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] ex = {".1.0",
                            "..0.",
                            ".0..",
                            "11.0",
                            };

            Tablero tab = new Tablero(4, ex);

            Console.CursorVisible = false;
            tab.Escribe();
            // bucle ppal, etc
            char c = ' ';
            while(c != 'q')
            {
                c = LeeInput();
                if(c != ' ')
                {
                   tab.ProcesaInput(c);
                   tab.Escribe();
                }
            }
            Console.SetCursorPosition(0, ex.Length);
            Lista fils = new Lista();
            Lista cols = new Lista();
            tab.BuscaIncorrectas(ref fils, ref cols);
            Console.WriteLine($"Filas erróneas: {fils.SacaLista()}");
            Console.WriteLine($"Columnas erróneas: {cols.SacaLista()}");
        }


        // método pedido
        public static void Lee(string file, int tam, string[] lineas)
        {
            // Lee del archivo file una cuadrícula.
            StreamReader sr = new StreamReader(file);

            // Devuelve el tamaño en tam y las filas en array lineas.
            // Se lanzará una excepción si el archivo no existe o tiene un formato incorrecto.
            sr.Close();
        }



        static char LeeInput()
        {
            char d = ' ';
            while (d == ' ')
            {
                if (Console.KeyAvailable)
                {
                    string tecla = Console.ReadKey().Key.ToString();
                    switch (tecla)
                    {
                        case "LeftArrow": d = 'l'; break;  // izquierda
                        case "UpArrow": d = 'u'; break;  // arriba
                        case "RightArrow": d = 'r'; break;  // derecha
                        case "DownArrow": d = 'd'; break;  // abajo
                        case "D0": d = '0'; break;  // dígito 0
                        case "D1": d = '1'; break;  // dígito 1                
                        case "Spacebar": d = '.'; break;  // casilla vacia                    
                        case "Escape": d = 'q'; break;  // terminar
                        default: d = ' '; break;
                    }
                }
            }
            return d;
        }
    }
}