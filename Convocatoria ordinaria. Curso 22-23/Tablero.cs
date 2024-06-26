// Carmen Gómez Becerra
// Autoescuela Faustino. Tomelloso.

using System;
using System.Data;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using Listas;

// https://www.kongregate.com/games/ejbarreto/puzlogic
// Eduardo Barreto

namespace puzlogic {
    class Tablero {
        int [,] tab; // matriz de números.
        bool [,] fijas; // matriz de posiciones fijas.
        Lista pend;  // lista de dígitos pendientes.
        int fil,col; // posición del cursor (fila y columna).

        /* En tab:
           -1: casilla muerta.
           0: hueco vacío que el jugador puede rellenar.
           1..9: representa el propio valor.*/

        // métodos...

        public Tablero(int[,] tb, int[] pd)
        {
            // Crea las matrices tab y fijas con las dimensiones de tb.
            tab = new int[tb.GetLength(0), tb.GetLength(1)]; 
            fijas = new bool[tb.GetLength(0), tb.GetLength(1)];

            // Inicializa tab con los valores de tb.
            for(int i = 0; i < tb.GetLength(0); i++)
            {
                for(int j = 0; j < tb.GetLength(1); j++)
                {
                    tab[i, j] = tb[i, j];

                    // Fijas: false si tab -> hueco; true en otro caso.
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

            // Crea la lista pend e inserta los números dados en pd.
            pend = new Lista();
            for(int i = 0; i < pd.Length; i++)
            {
                pend.InsertaFin(pd[i]);
            }

            // Situa la posición del cursor en(0, 0).
            fil = 0; col = 0;
        }

        public void Render()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            // Las casillas separadas por un blanco en horizontal + el cursor colocado.
            for(int i = 0; i < tab.GetLength(0); i++)
            {
                for(int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" ");
                    if (tab[i, j] == -1)
                    {
                        Console.Write(" ");
                    }
                    else if (tab[i, j] == 0)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        if (!fijas[i, j])
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }

                        Console.Write($"{tab[i, j]}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Pends: {pend.ToString()}");

            Console.SetCursorPosition(col * 2 + 1, fil);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            EscribeTab(fil, col);
            Console.SetCursorPosition(0, tab.GetLength(0) + 2);
            Console.ResetColor();
        }

        private void EscribeTab(int fila, int colum) // HAY Q MIRAR COMO PODRÍA HACERSE ESTO EN LA PRIMERA PARTE DEL RENDER.
        {
            if (tab[fila, colum] == 0)
            {
                Console.Write(".");
            }
            else if (tab[fila, colum] == -1)
            {
                Console.Write(" ");
            }
            else
            {
                Console.Write($"{tab[fila, colum]}");
            }
        }

        public void MueveCursor(char c)
        {
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
            // Comprueba si num puede colocarse en tab[fil, col].
            // si hay un hueco libre y además ni fil, ni col contienen ya n.
            bool viable = true;
            int i = 0;
            while(i < tab.GetLength(0))
            {
                if (tab[fil, i] == num || tab[i, col] == num)
                {
                    viable = false;
                }
            }
            return viable && tab[fil, col] == 0;
        }

        public bool PonNumero(int num)
        {
            // Pone num en tab[fil, col] si es viable y aparece en pend.
            // En ese caso, elimina el dígito de pend y devuelve true; en otro caso devuelve false.
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
            // si tab[fil,col] tiene un dígito >0 y !fijo, lo elimina y lo devuelve a pend.En ese caso devuelve true; en otro caso false.

            bool quitado = false;
            if (tab[fil, col] > 0 && !fijas[fil, col])
            {
                pend.InsertaFin(tab[fil, col]);
                tab[fil, col] = 0;
                quitado = true;
            }
            return quitado;
        }

    }
}        
        
