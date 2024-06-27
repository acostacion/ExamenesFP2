// Nombre y apellidos
// Laboratorio, Puesto

using System;
using System.IO;
using Listas;


namespace takuzu
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "takuzu6x6.txt";
            Tablero tab;
            Console.WriteLine("¿Prefieres archivo (1) o tablero de ejemplo (2)?");
            if(int.Parse(Console.ReadLine()) == 1)
            {
                Lee(file, out int tam, out string[] lineas);
                tab = new Tablero(tam, lineas);
            }
            else
            {
                string[] ex = {".1.0",
                            "..0.",
                            ".0..",
                            "11.0",
                            };

                tab = new Tablero(4, ex);
            }

            tab.Escribe();

            char c = ' ';
            // Comprobar hasta que te salga.
            while (c != 'q' && tab.EstaLleno())
            {
                c = LeeInput();

                if(c != ' ')
                {
                    tab.ProcesaInput(c);
                    tab.Escribe();
                }
            }

            tab.BuscaIncorrectas(out Lista fils, out Lista cols);
            Console.WriteLine($"Filas incorrectas: {fils.SacaLista()}");
            Console.WriteLine($"Columnas incorrectas: {cols.SacaLista()}");

            // bucle ppal, etc
        }


        // método pedido
        public static void Lee(string file, out int tam, out string[] lineas)
        {
            // INCORRECTO
            // 1. NOT cuadrada. *
            // 2. NOT 0, 1, ".". *
            // 3. Con una o más líneas vacías al final. 
            // 4. Que no exista el archivo. *

            if (File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);

                // Para que se pueda hacerse out.
                string primeraLin = sr.ReadLine();
                tam = primeraLin.Length;
                lineas = new string[tam];
                lineas[0] = primeraLin;

                for (int i = 1; i < tam; i++)
                {
                    lineas[i] = sr.ReadLine();

                    if (lineas[i].Length != tam)
                    {
                        throw new Exception("ERROR: El tablero no es cuadrado.");
                    }
                }

                for (int i = 0; i < tam; i++)
                {
                    for (int j = 0; j < tam; j++)
                    {
                        if (lineas[i][j] != '1' && lineas[i][j] != '0' && lineas[i][j] != '.')
                        {
                            // Excepción contenido incorrecto.
                            throw new Exception("ERROR: Contenido incorrecto.");
                        }
                    }
                }

                bool blanco = true;
                while (!sr.EndOfStream && blanco)
                {
                    blanco = sr.Read() != ' ';
                }

                if (!blanco)
                {
                    // Excepción de que no hay huecos en blanco debajo y de que hay más contenido y no es cuadrada.
                    throw new Exception("ERROR: No es un tablero cuadrado y no hay huecos blancos debajo del tablero.");
                }
               

                sr.Close();
            }
            else
            {
                throw new Exception("ERROR: No existe el archivo.");
            } 
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
