namespace Examen_de_convocatoria_ordinaria._Curso_22_23
{
    internal class Program
    {
        const int NUM_DIGS = 4, // número de dígitos de las combinaciones
                  FILAS = 10;   // número máximo de intentos permitidos
        static Random rnd = new Random(); // para generar comb secreta
        public static void Main()
        {
            int[] secr = new int[NUM_DIGS]; // combinacion secreta
            int[] comb = new int[NUM_DIGS]; // intento del jugador
            int[,] tab = new int[FILAS, NUM_DIGS + 2]; // histórico de intentos del jugador
            int numJug = 0; // número de jugadas realizadas hasta el momento

            Console.Write("Desea leer de archivo (1) o generar una combinación nueva (2)");
            if(int.Parse(Console.ReadLine()) == 1)
            {
                Lee(secr, tab, ref numJug);
            }
            else
            {
                GeneraComb(secr);
            }

            bool adivinada = false;

            bool abortar = false;
            
            Console.Clear();    
            Render(secr, false, tab, numJug);

            while (!adivinada && numJug < FILAS && !abortar)
            {
                PideCombinacion(comb);
                if (comb[0] == 0)
                {
                    Console.Clear();
                    Console.Write("Desea abortar el juego (1) o continuar jugando (2)");
                    if (int.Parse(Console.ReadLine()) == 1) abortar = true;

                }
                EvaluaComb(secr, comb, out int mu, out int he);
                GuardaComb(tab, comb, mu, he, ref numJug);
                Console.Clear();
                Render(secr, false, tab, numJug);

                adivinada = mu == NUM_DIGS;
            }

            Console.Clear();

            Render(secr, true, tab, numJug);

            if (adivinada) Console.Write("¡Has ganado!");
            else if (abortar) { Salva(secr, tab, numJug); Console.Write("¡Hasta pronto!"); }
            else Console.Write("¡Has perdido!");
            Console.WriteLine("\n\n\n");

            
        }

        static void Render(int[] secr, bool muestraSecr, int[,] tab, int numJug)
        {
            // Combinación secreta (XXX...MH).
            // Si muestraSecr=true escribre los dígitos de la combinación secreta.
            // Mostrar los intentos.

            if (muestraSecr)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                for (int i = 0; i < NUM_DIGS; i++)
                {
                    Console.Write($"{secr[i]} ");
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                for (int i = 0; i < NUM_DIGS; i++)
                {
                    Console.Write("X ");
                }
            }
            Console.WriteLine("M H");

            Console.ResetColor();

            for(int i = 0; i < numJug; i++)
            {
                for(int j = 0; j < NUM_DIGS + 2; j++)
                {
                    Console.Write($"{tab[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        static bool Muerto(int[] secr, int[] comb, int pos)
        {
            return secr[pos] == comb[pos];
        }

        static bool Herido(int[] secr, int[] comb, int pos)
        {
            bool hayHerido = false;

            int i = 0;
            while(i < NUM_DIGS && !hayHerido)
            {
                hayHerido = (comb[pos] == secr[i]) && (pos != i);
                i++;
            }

            return hayHerido;
        }

        static void EvaluaComb(int[] secr, int[] comb, out int mu, out int he) // [NOTA MENTAL] Cuando no está creado fuera y te dice DEVUELVE AQUÍ, 99% de probabilidades de out.
        {
            mu = 0; he = 0;
            for(int i = 0; i <  NUM_DIGS; i++)
            {
                if (Muerto(secr, comb, i)) mu++;
                else if (Herido(secr, comb, i)) he++;
            }
        }

        static void PideCombinacion(int[] comb)
        {
            Console.Write($"Introduzca una combinación de {NUM_DIGS} dígitos: ");
            
            string acueste = Console.ReadLine();

            for(int i = 0; i < NUM_DIGS; i++)
            {
                comb[i] = int.Parse(acueste[i].ToString());
            }
        }

        static void GuardaComb(int[,] tab, int[] comb, int mu, int he, ref int numJug)
        {
            // Hemos de guardar una combinación en tab en la fila numJug.
            for (int i = 0; i < NUM_DIGS; i++)
            {
                tab[numJug, i] = comb[i];
            }
            // Guarda también muertos y heridos.
            tab[numJug, NUM_DIGS] = mu;
            tab[numJug, NUM_DIGS + 1] = he;
            // Aumenta numJug.
            numJug++;

        }

        static void GeneraComb(int[] combSecr)
        {
            int num = 0;

            for (int i = 0; i < NUM_DIGS; i++)
            {
                while (EstaYa(num, combSecr))
                {
                    num = rnd.Next(1, 10);
                }

                combSecr[i] = num;
            }
        }

        static bool EstaYa(int num, int[] combSecr)
        {
            bool esta = false;

            int i = 0;

            while (i < combSecr.Length && !esta)
            {
                if (combSecr[i] == num)
                {
                    esta = true;
                }
                i++;
            }

            return esta;
        }

        static void Salva(int[] secr, int[,] tab, int numJug)
        {
            string file = "partida.txt";

            StreamWriter sw = new StreamWriter(file);
            
            for(int i = 0; i < NUM_DIGS; i++)
            {
                sw.Write(secr[i]);
            }

            sw.WriteLine();

            for(int i = 0; i < numJug; i++)
            {
                for(int j = 0; j < NUM_DIGS + 2; j++)
                {
                    sw.Write(tab[i, j]);
                }
                sw.WriteLine();
            }

            sw.Close();

        }

        static void Lee(int[] secr, int[,] tab, ref int numJug)
        {
            string file = "partida.txt";

            StreamReader sr = new StreamReader(file);

            string secreto = sr.ReadLine();

            for(int i = 0; i < NUM_DIGS; i++)
            {
                secr[i] = int.Parse(secreto[i].ToString());
            }

            int k = 0;
            while (!sr.EndOfStream)
            {
                string avemaria = sr.ReadLine();

                for (int j = 0; j < NUM_DIGS; j++)
                {
                    tab[k, j] = int.Parse(avemaria[j].ToString());
                }
                k++;
            }
            numJug = k;

            sr.Close();
        }
    }
}
