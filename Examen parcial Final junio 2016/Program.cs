using System.ComponentModel;
using System.Diagnostics;

namespace Examen_parcial_Final_junio_2016
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (!QRValido(GeneraQRAleatorio()))
            {
                Console.WriteLine("Cargando...");
            }
            Dibuja(GeneraQRAleatorio());
            Console.SetCursorPosition(0,0);
            Dibuja(GeneraPatronFP());
            Console.SetCursorPosition(14,0);
            Dibuja(GeneraPatronFP());
            Console.SetCursorPosition(0,14);
            Dibuja(GeneraPatronFP());

            Console.SetCursorPosition(21, 21);
        }

        static bool[,] GeneraQRAleatorio()
        {
            // [NOTA MENTAL] Que el random no te engañe, es Next(0, 2) -> [0, 2).
            Random rnd = new Random();
            bool[,] bools = new bool[21, 21];

            // TRUE negro
            // FALSE blanco
            for (int i = 0; i < bools.GetLength(0); i++)
            {
                for (int j = 0; j < bools.GetLength(1); j++)
                {
                    // Si sale 0 -> false.
                    // Si sale 1 -> true.
                    if (rnd.Next(0, 2) == 0) bools[i, j] = false;
                    else bools[i, j] = true;
                }
            }

            return bools;
        }

        static void Dibuja(bool[,] qr)
        {
            for (int i = 0; i < qr.GetLength(0); i++)
            {
                for (int j = 0; j < qr.GetLength(1); j++)
                {
                    // [NOTA MENTAL] Para qué vas a hacer == true si qr[i, j] ya es true.
                    if (qr[i, j]) Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    else Console.BackgroundColor = ConsoleColor.DarkGray;

                    // [NOTA MENTAL] No te olvides de pintar.
                    Console.Write("  ");
                }
                // [NOTA MENTAL] Tampoco te olvides de saltar de línea.
                Console.WriteLine();
            }

            Console.ResetColor();
        }

        static bool[,] GeneraPatronFP()
        {
            // Genera una matriz 7x7.
            bool[,] cuadrado = new bool[7, 7];

            // [NOTA MENTAL] Fijarse muchísimo en i, j no vaya a ser que hayas confundido índices.
            for (int i = 0; i < cuadrado.GetLength(0); i++)
            {
                for (int j = 0; j < cuadrado.GetLength(1); j++)
                {
                    if ((i == 0 || i == cuadrado.GetLength(0) - 1) // Si el indice está en la linea de arriba o la de abajo...
                        || (j == 0 || j == cuadrado.GetLength(1) - 1) // O el indice está en la primera o última columna...
                        || (i >= 2 && i <= 4) && (j >= 2 && j <= 4)) // O se encuentra en el rango del cuadrao...
                    {
                        cuadrado[i, j] = true;
                    }
                    else cuadrado[i, j] = false;
                }
            }

            return cuadrado;
        }

        static bool[,] GeneraPatronJaime()
        {
            // [NOTA MENTAL] Fijarse muchísimo en i, j no vaya a ser que hayas confundido índices.

            // Genera una matriz 7x7.
            bool[,] cuadrado = new bool[7, 7];

            // Pinta el primer cuadrado negro.
            for (int i = 0; i < cuadrado.GetLength(0); i++)
            {
                for (int j = 0; j < cuadrado.GetLength(1); j++)
                {
                    cuadrado[i, j] = true;
                }
            }

            // Pinta el segundo cuadrado blanco.
            for (int i = 1; i < cuadrado.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < cuadrado.GetLength(1) - 1; j++)
                {
                    cuadrado[i, j] = false;
                }
            }

            // Pinta el tercer cuadrado negro.
            for (int i = 2; i < cuadrado.GetLength(0) - 2; i++)
            {
                for (int j = 2; j < cuadrado.GetLength(1) - 2; j++)
                {
                    cuadrado[i, j] = true;
                }
            }

            return cuadrado;
        }

        static bool EstaPatron(bool[,] pat, bool[,] qr, int x, int y)
        {
            bool hayPatron = true;

            int i = 0;
            int j = 0;

            while (i < pat.GetLength(0) && hayPatron)
            {
                while (j < pat.GetLength(1) && hayPatron)
                {
                    // [NOTA MENTAL] También comprobar siempre que no nos salimos de los bordes que estamos buscando.
                    if (qr[y,x] != pat[i, j] 
                        && (x >= 0 && y >= 0) 
                        && (x <= qr.GetLength(1) && y <= qr.GetLength(0)))
                    {
                        hayPatron = false;
                    }
                    x++;
                    j++;
                }
                y++;
                i++;
            }

            return hayPatron;
        }

        static bool QRValido(bool[,] qr)
        {
            // [NOTA MENTAL] Mejor esto que llamar 3 veces al método.
            bool[,] patron = GeneraPatronFP();

            int i = 0;
            int j = 0;
            bool hayBlanco = true;

            // franjas blancas.
            while (i <= qr.GetLength(0) && hayBlanco)
            {
                while (j <= qr.GetLength(1) && hayBlanco)
                {
                    if ((j >= 0 && j <= 8 && i == 8)
                        || (j >= 13 && j <= 21 && i == 8)
                        || (j >= 0 && j <= 8 && i == 13)
                        && qr[i,j])
                    {
                        hayBlanco = false;
                    }
                }
            }

            // patron.
            bool hayPatron = EstaPatron(qr, patron, 0, 0) &&
                             EstaPatron(qr, patron, 14, 0) &&
                             EstaPatron(qr, patron, 0, 14);

            return hayPatron && hayBlanco;
        }

        static void EscribeSalida(bool[,] qr, string file)
        {
            StreamWriter sw = new StreamWriter(file);

            for(int i = 0; i < qr.GetLength(0); i++)
            {
                for( int j = 0; j < qr.GetLength(1); j++)
                {
                    sw.WriteLine(qr[i,j]);
                }
            }
            sw.Close();
        }

        static bool[,] LeeEntrada(string file)
        {
            StreamReader sr = new StreamReader(file);

            bool[,] qr = new bool[21,21];

            for(int i = 0; i < File.ReadAllLines(file).Length; i++)
            {
                for(int j = 0; j < qr.GetLength(1); j++)
                {
                    qr[i, j] = bool.Parse(sr.ReadLine());
                }
            }

            sr.Close();

            return qr;
        }

        static SetCoor Convierte(bool[,] qr)
        {
            SetCoor setCoor = new SetCoor();

            for(int i = 0; i< qr.GetLength(0); i++)
            {
                for(int j = 0; j < qr.GetLength(1); j++)
                {
                    if (qr[i, j])
                    {
                        Coor c = new Coor();
                        c.X = j; c.Y = i;
                        setCoor.Add(c);
                    }
                }
            }

            return setCoor;
        }

        //static int CuentaRec(ListaEnlazada l)
        //{

        //}
    }
}
