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


        public Tablero(int[,] tb, int[] pd)
        {
            tab = new int [tb.GetLength(0), tb.GetLength(1)];
            fijas = new bool[tb.GetLength(0), tb.GetLength(1)];
            tab = tb;

            for (int i = 0;  i < tb.GetLength(0); i++)
            {
                for(int j = 0; j < tb.GetLength(1); j++)
                {
                    if (tab[i,j] == 0) fijas[i, j] = false;
                    else fijas[i, j] = true;
                }
            }

            pend = new Lista();

            for(int i = 0; i < pd.Length; i++)
            {
                pend.InsertaPpio(pd[i]);
            }

            Console.SetCursorPosition(0, 0); 
        }

        public void Render()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            
            // Falta hacer lo de marcar el cursor.
            
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    switch (tab[i,j])
                    {
                        case -1:
                            Console.Write("  ");
                            break;

                        case 0:
                            Console.Write(" ·");
                            break;
                        
                        default:
                            Console.Write($" {tab[i,j]}");
                            break;
                    }
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Pends: {pend.ToString()}");

            Console.ResetColor();
              
        }

        public void MueveCursor(char c)
        {
            
            if(c == 'l' && fil >= 0)
            {
                fil--;
            }
            else if (c == 'u' && col >= 0)
            {
                col--;
            }
            else if (c == 'r' && fil < tab.GetLength(1))
            {
                fil++;
            }
            else if (c == 'd' && fil < tab.GetLength(0))
            {
                col++;
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
            
            int i = 0;
            while (i < pend.NumElems() && num != pend.C)
            

            if(viable && )
        }
    }
}        
        
