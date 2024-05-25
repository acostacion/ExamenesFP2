using System.Diagnostics;

namespace Examen_parcial_Final_junio_2016
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
        }

        //static bool[,] GeneraQRAleatorio()


        static void Dibuja(bool[,] qr)
        {
            for (int i = 0; i < qr.GetLength(0); i++)
            {
                for (int j = 0; j < qr.GetLength(1); j++)
                {
                    // [NOTA MENTAL] Para qué vas a hacer == true si qr[i, j] ya es true.
                    if (qr[i, j]) Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    else Console.BackgroundColor = ConsoleColor.Black;

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

        static bool qrValido(bool[,] qr)
        {
            // [NOTA MENTAL] Mejor esto que llamar 3 veces al método.
            bool[,] patron = GeneraPatronFP();

            int i = -1;
            int j = -1;
            bool hayBlanco = true;

            

            while (i <= patron.GetLength(0) && hayBlanco)
            {
                while (j <= patron.GetLength(1) && hayBlanco)
                {
                    if(i == -1 || i == patron.GetLength(0) // Si 
                        || j == -1 || j == patron.GetLength(1) 
                        && qr[i,j] == true)
                    {
                        hayBlanco = false;
                    }
                }
            }

            return EstaPatron(qr, patron, 0, 0) &&
                   EstaPatron(qr, patron, 14, 0) &&
                   EstaPatron(qr, patron, 0, 14);
            
        }
    }
}
