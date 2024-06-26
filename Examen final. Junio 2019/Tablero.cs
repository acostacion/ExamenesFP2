// Carmen Gómez Becerra
// El comedor de Tomelloso.

using System;
using System.Drawing;
using System.Net;
using Listas;

namespace takuzu
{
    public class Tablero
    {
        int N; // lado de la cuadrícula. Es como si fuera el mismo GetLength.
        enum Casilla { Cero, Uno, Vacio }; // contenido de las casillas
        Casilla[,] mat; // cuadrícula de juego
        bool[,] fijos; // 0s y 1s fijos (iniciales)
        struct Coor { public int x, y; } // Coordenadas. x: fila; y: columna
        Coor pos; // posición del cursor

        

        public Tablero(int tam, string[] lineas)
        {
            // Inicializa N con el valor tam y pos con el valor(0, 0).
            N = tam;
            pos.x = 0; pos.y = 0;

            // Crea las matrices mat y fijos, y las inicializa con la cuadricula de "lineas".
            mat = new Casilla[N, N];
            fijos = new bool[N, N];

            // MAT: . -> vacío, 0 -> cero, 1 -> uno.
            // FIJOS: . -> false, 0 y 1 -> true.
            for (int i = 0; i < tam; i++)
            {
                for (int j = 0; j < tam; j++)
                {
                    if(lineas[i][j] == '.')
                    {
                        mat[i, j] = Casilla.Vacio;
                        fijos[i, j] = false;
                    }
                    else if (lineas[i][j] == '1')
                    {
                        mat[i, j] = Casilla.Uno;
                        fijos[i, j] = true;
                    }
                    else if (lineas[i][j] == '0')
                    {
                        mat[i, j] = Casilla.Cero;
                        fijos[i, j] = true;
                    }
                }
            }

        }

        public void Escribe()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            // Las casillas fijas se escriben en amarillo y las no fijas en verde.
            for (int i = 0; i < N; i++)
            {
                for(int j = 0; j < N; j++)
                {
                    if (fijos[i, j])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    // Dejar un espacio delante de cada carácter.
                    Console.Write(" ");
                    CasillaWriter(i, j);
                }
                Console.WriteLine();
            }

            
            // Colocar el cursor (el CursorPosition es (col, fil)).
            Console.SetCursorPosition(pos.x * 2, pos.y);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;

            CasillaWriter(pos.y, pos.x);

            Console.ResetColor();
        }

        private void CasillaWriter(int fil, int col)
        {
            if (mat[fil, col] == Casilla.Cero)
            {
                Console.Write("0");
            }
            else if (mat[fil, col] == Casilla.Uno)
            {
                Console.Write("1");
            }
            else if (mat[fil, col] == Casilla.Vacio)
            {
                Console.Write(".");
            }
        }

        public void ProcesaInput(char c)
        {
            // Actualiza el valor de pos controlando que no se salga de los límites de la cuadrícula.
            if (c == 'u' && pos.y > 0)
            {
                pos.y--;
            }
            else if (c == 'd' && pos.y < N - 1)
            {
                pos.y++;
            }
            else if (c == 'l' && pos.x > 0)
            {
                pos.x--;
            }
            else if (c == 'r' && pos.x < N - 1)
            {
                pos.x++;
            }

            // Coloca en mat[pos] el valor correspondiente, si dicha posición no es fija;
            else if (c == '0' && !fijos[pos.y, pos.x])
            {
                mat[pos.y, pos.x] = Casilla.Cero;
            }
            else if (c == '1' && !fijos[pos.y, pos.x])
            {
                mat[pos.y, pos.x] = Casilla.Uno;
            }
            else if (c == '.' && !fijos[pos.y, pos.x])
            {
                mat[pos.y, pos.x] = Casilla.Vacio;
            }
        }

        public bool EstaLleno()
        {
            // Comprueba si mat está llena de 0 o 1.
            bool estaLleno = true;

            for(int i = 0; i < N; i++)
            {
                for(int j  = 0; j < N; j++)
                {
                    if (mat[i, j] == Casilla.Vacio)
                    {
                        estaLleno = false;
                    }
                }
            }
            return estaLleno;
        }

        private void SacaFilCol(int i, out Casilla[] fil, out Casilla[] col)
        {
            fil = new Casilla[N];
            col = new Casilla[N];

            for(int j = 0; j < N; j++)
            {
                fil[j] = mat[j, i];
                col[j] = mat[i, j];
            }
        }

        private bool TresSeguidos(Casilla[] lin)
        {
            int cont = 0;

            int i = 1;
            while (i < N && cont < 3)
            {
                if (lin[i-1] == lin[i]) cont++;
                i++;
            }

            return cont == 3;
        }

        private bool IgCerosUnos(Casilla[] lin)
        {
            int cantCeros = 0;
            int cantUnos = 0;

            for(int i = 0; i < N; i++)
            {
                if (lin[i] == Casilla.Uno) cantUnos++;
                else if (lin[i] == Casilla.Cero) cantCeros++;
            }

            return cantCeros == cantUnos;
        }

        public void BuscaIncorrectas(ref Lista fils, ref Lista cols) // NO ME SALE EL BUSCAINCORRECTAS O ALGUNO DE LOS PRIVATES ANTERIORES.
        {
            // devuelve en las listas fils y cols los índices de las filas de mat que no verifican alguna de las reglas.
            for(int i = 0; i<N; i++)
            {
                SacaFilCol(i, out Casilla[] fil, out Casilla[] col);

                // 1. Cada fila y cada columna tienen que contener el mismo número de 0s y 1s.
                // 2. No pueden colocarse más de dos 0s ó 1s consecutivos en una fila ni en una columna.

                if(!IgCerosUnos(fil) || TresSeguidos(fil))
                {
                    fils.InsertaPri(i);
                }
                
                if (!IgCerosUnos(col) || TresSeguidos(col))
                {
                    cols.InsertaPri(i);
                }
            }
        }
    }
}