using System;
using System.IO;
using Listas;

// https://www.kongregate.com/games/ejbarreto/puzlogic
// Eduardo Barreto

namespace puzlogic {
    class Tablero {
        int [,] tab; // matriz de números
        bool [,] fijas; // matriz de posiciones fijas
        Lista pend;  // lista de dígitos pendientes
        int fil,col; // posición del cursor (fila y columna)

        // FILAS Y COLUMNAS SIEMPRE CAMBIADAS EN EL CURSOR.

        public Tablero(int[,] tb, int[] pd) // [DONE].
        {
            // La matriz "fijas" será de las mismas dimensiones que tab.
            tab = new int [tb.GetLength(0), tb.GetLength(1)];
            fijas = new bool[tab.GetLength(0), tab.GetLength(1)];

            // Inicializamos tab con los valores de tb***.
            // Si hay hueco vacío (0) en tab -> true.
            // Resto de casos -> false.
            for (int i = 0;  i < tb.GetLength(0); i++)
            {
                for(int j = 0; j < tb.GetLength(1); j++)
                {
                    tab[i,j] = tb[i,j];
                    if (tab[i,j] == 0) fijas[i, j] = false;
                    else fijas[i, j] = true;
                }
            }

            // Creamos pend y le insertamos los números de pd.
            pend = new Lista();
            for(int i = 0; i < pd.Length; i++)
            {
                pend.InsertaFin(pd[i]);
            }

            // Cursor a (0, 0)***.
            fil = 0;
            col = 0;
        }


        public void Render()
        {
            Console.Clear();
        
            // Dibujamos el tablero desde SetCursorPosition(0, 0)***.
            Console.SetCursorPosition(0, 0);

            // Escritura de tablero.
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    // Color***.
                    if (fijas[i, j]) Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.Yellow;

                    switch (tab[i,j])
                    {
                        case -1:
                            Console.Write("  "); break;
                        case 0:
                            Console.Write(" ·"); break;
                        default:
                            Console.Write($" {tab[i,j]}"); break;
                    }
                }
                Console.WriteLine();
            }

            // Lista de pendientes.
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Pends: {pend.ToString()}");

            Console.ResetColor();

            // Sitúo el cursor en la posición correspondiente***.
            //Console.SetCursorPosition(col * 2 + 1, fil);
        }

        public void MueveCursor(char c)
        {
            // HA SIDO CAMBIADO ENTERO***
            // Hacer siempre representación de fil col en papel.
            // En el cursor está cambiado.
            if(c == 'l' && col > 0)
            {
                col--;
            }
            else if (c == 'u' && col > 0)
            {
                fil--;
            }
            else if (c == 'r' && col < tab.GetLength(0) - 1)
            {
                col++;
            }
            else if (c == 'd' && fil < tab.GetLength(1) - 1)
            {
                fil++;
            }
        }

        private bool NumViable(int num)
        {
            bool libre = false;

            int i = 0;
            while (num != tab[i, col] && i < tab.GetLength(0))
            {
                libre = true;
                i++;
            }
            i = 0;
            while (num != tab[fil, i] && i < tab.GetLength(1))
            {
                libre = true;
                i++;
            }

 
            if (tab[fil,col] == 0 && libre)
            {
                libre = true;
            }
            else
            {
                libre = false;
            }

            return libre;
        }

        public bool PonNumero (int num)
        {
            bool viable = NumViable(num);
            bool numPuesto = false;
            
            if(viable && pend.BuscaDato(num))
            {
                tab[fil, col] = num;
                pend.EliminaElto(num);
                numPuesto = true;
            }

            return numPuesto;
        }

        public bool QuitaNumero()
        {
            bool numQuitado = false;

            if (tab[fil,col] > 0 && fijas[fil,col] == false)
            {
                pend.InsertaFin(tab[fil,col]);
                tab[fil,col] = 0;
                numQuitado = true;
            }

            return numQuitado;
        }

        public bool FinJuego()
        {
            return pend.EsVacia();
        }
        
    }
}        
        
