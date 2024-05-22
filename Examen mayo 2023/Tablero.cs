using System;
using System.IO;
using Listas;

// https://www.kongregate.com/games/ejbarreto/puzlogic
// Eduardo Barreto

namespace puzlogic {
    class Tablero {
        int [,] tab; // matriz de números.
        bool [,] fijas; // matriz de posiciones fijas.
        Lista pend;  // lista de dígitos pendientes.
        int fil,col; // posición del cursor (fila y columna).

        // [NOTA MENTAL]
        // GETLENGTH 0 -> FILAS
        // GETLENGTH 1 -> COLUMNAS

        public Tablero(int[,] tb, int[] pd)
        {
            // Crea tab y fijas con dimensiones de tb.
            tab = new int[tb.GetLength(0), tb.GetLength(1)];
            fijas = new bool[tab.GetLength(0), tab.GetLength(1)];

            // Inicializa tab con los valores de tb.
            for(int i = 0; i < tb.GetLength(0); i++)
            {
                for(int j = 0; j < tb.GetLength(1); j++)
                {
                    tab[i, j] = tb[i, j];

                    // Si es 0, en fijas es false.
                    if (tab[i, j] == 0 ) fijas[i,j] = false;
                }
            }

            // Crea pend.
            pend = new Lista();

            // Le inserta los números de pd.
            for(int i = 0; i < pd.Length; i++) pend.InsertaFin(pd[i]); // [NOTA MENTAL] Es mejor insertafin porque vas creando un nuevo nodo al final.

            // Sitúa el cursor en (0, 0).
            fil = 0; col = 0;
        }

        public void Render()
        {
            // [NOTA MENTAL] Antes de fregar hay que barrer.
            Console.Clear();

            // [NOTA MENTAL] Tengan ustedes cuidado con el Cursor del dibujante porque funciona tal que así: CursorPosition(columna, fila);. Que no te engañen.
            // [NOTA MENTAL] El cursor suele ir al principio de las lineas de código. SUELE. NO SIEMPRE.
            // [NOTA MENTAL] A la hora de representar ten cuidao porque las casillas son dobles (dobles o lo que surja).
            // [CUIDAO] si falla se cambia.
            Console.SetCursorPosition(col * 2 , fil);

            // [NOTA MENTAL] almost cada render suele tener dos casillas "**" pero fíjate bien, puede que no.
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for(int j = 0; j < tab.GetLength(1); j++)
                {
                    switch (tab[i, j])
                    {
                        case -1:
                            Console.Write("  "); break;
                        case 0:
                            Console.Write(" ·"); break;
                        default:
                            Console.Write($" {tab[i, j]}"); break;
                            //Console.Write(" " + tab[i, j]);
                    }
                }
                // [NOTA MENTAL] ¡ojo! no olviden, tras cada línea, se baja a la siguiente. Si no, sale la carretera de la Ossa de Montiel.
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\nPends: {pend.ToString()}");

            Console.ResetColor();
        }

        public void MueveCursor(char c)
        {
            // [NOTA MENTAL] Cuando es 0 (>o<). Cuando es GetLength (>o<)-1. Saludo. Tiburón.
            if (c == 'l' && col > 0) col--;
            else if (c == 'r' && col < tab.GetLength(1) - 1) col++;
            else if (c == 'd' && fil < tab.GetLength(0) - 1) fil++;
            else if (c == 'u' && fil > 0) fil--;    
        }

        private bool NumViable(int num)
        {
            bool viable = true;

            int i = 0;
            while (i < tab.GetLength(0) && viable)
            {
                if (tab[i, col] == num) viable = false;
                i++;
            }
            i = 0;
            while (i < tab.GetLength(1) && viable)
            {
                if (tab[fil, i] == num) viable = false;
                i++;
            }

            return tab[fil, col] == 0 && viable;
        }

        public bool PonNumero(int num)
        {
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
            bool quitado = false;

            if (tab[fil, col] > 0 && fijas[fil, col] == false)
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
    }
}        
        
