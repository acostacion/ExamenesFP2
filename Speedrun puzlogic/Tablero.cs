// Carmen Gómez Becerra
// Mesa de mi casa
using System;
using System.Data;
using System.Drawing;
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

        // -1: casilla muerta.
        // 0: hueco vacío que el jugador puede rellenar.
        // 1..9: representa el propio valor.

        public Tablero(int[,] tb, int[] pd)
        {
            tab = new int[tb.GetLength(0), tb.GetLength(1)];
            fijas = new bool[tb.GetLength(0), tb.GetLength(1)];

            // [DESPISTE] Has creado tab y fijas pero no los has inicializado.
            for(int i = 0; i < tb.GetLength(0); i++)
            {
                for(int j = 0; j < tb.GetLength(1); j++)
                {
                    tab[i,j] = tb[i,j];
                    if (tab[i, j] == 0)
                    {
                        fijas[i, j] = false;
                    }
                    else
                    {
                        fijas[i, j] = true;
                    }
                }
            }

            pend = new Lista();

            for(int i = 0; i < pd.Length; i++)
            {
                pend.InsertaFin(pd[i]);
            }

            fil = 0; col = 0;
        }

        public void Render()
        {
            Console.Clear();

            for(int i = 0; i < tab.GetLength(0); i++)
            {
                for(int j = 0; j < tab.GetLength (1); j++)
                {
                    // [DESPISTE] Hay que separar por casos.

                    // CASO 1: COLOR DEL CURSOR.
                    if(i == fil && j == col)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                    // CASO 3: ESCRIBIR.
                    if (tab[i, j] > 0)
                    {
                        // CASO 2: COLOR DE LOS NÚMEROS.
                        if (fijas[i, j])
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }

                        Console.Write($" {tab[i, j]}");
                    }
                    else if (tab[i, j] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" ·");
                    }
                    else if (tab[i, j] == -1)
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Pends: {pend.ToString()}");

            Console.ResetColor();
        } // Revisatelo para q no te raye.

        public void MueveCursor(char c)
        {
            // [DESPISTE] Hay que saludar.
            if(c == 'l' && col > 0)
            {
                col--;
            }
            else if (c == 'r' && col < tab.GetLength(1) - 1)
            {
                col++;
            }
            else if (c == 'u' && fil > 0)
            {
                fil--;
            }
            else if (c == 'd' && fil < tab.GetLength(0) - 1)
            {
                fil++;
            }
        }

        private bool NumViable(int num)
        {
            // Si hay un hueco libre (0) y ni fil ni col tienen num.

            // QUE NO SE TE OLVIDE BAJO NINGUN CONCEPTO AUMENTAR EL INDICE EN LOS WHILES.
            bool numIgual = false;

            int i = 0;

            while(i < tab.GetLength(0) && !numIgual)
            {
                numIgual = tab[i, col] == num;
                i++;
            }

            i = 0;
            
            while (i < tab.GetLength(1) && !numIgual)
            {
                numIgual = tab[fil, i] == num;
                i++;
            }

            return tab[fil, col] == 0 && !numIgual;
        }

        public bool PonNumero(int num)
        {
            // Pone num en tab(fil, col) si es viable y aparece en pend.
            // Si lo pone, elimina el digito de pend y devuelve true.
            bool puesto = false;

            if (NumViable(num) && pend.BuscaDato(num))
            {
                tab[fil, col] = num;
                pend.EliminaElto(num);
                puesto = true;
            }

            return puesto;
        }

        public bool QuitaNumero()
        {
            // Si tab[fil,col] tienen un numero >0 y no es fijo (fijas = false)...
            // Lo elimina y lo devuelve a pend. Devolvería true.
            bool quitado = false;

            // [DESPISTE] En vez de poner fijas == false, muchas veces mola más poner !fijas

            if (tab[fil,col] > 0 && !fijas[fil,col])
            {
                pend.InsertaFin(tab[fil, col]);
                tab[fil, col] = 0;
                quitado = true;
            }

            return quitado;
        }

        public bool FinJuego()
        {
            return pend.EsVacia();
        }

        public Lista PosiblesRec(Lista returnList, int i)
        {
            // Devuelve una lista con posibles valores dentro de [1,9]
            // Estos valores podrían colocarse en fil,col.
            // Están en la lista de pendientes y son viables.

            if(i <= 9 && i >= 1 && pend.BuscaDato(i))
            {
                if (NumViable(i))
                {
                    returnList.InsertaFin(i);
                    PosiblesRec(returnList, i++);
                }
                else
                {
                    PosiblesRec(returnList, i++);
                }
            }

            return returnList;
        }
    }
}        
        
