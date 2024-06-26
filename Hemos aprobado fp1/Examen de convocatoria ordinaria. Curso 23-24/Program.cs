// Carmen Gómez Becerra
// TomellosA <3

using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

namespace Examen_de_convocatoria_ordinaria._Curso_23_24
{
    class Program
    {
        static Random rnd = new Random(); // generador de aleatorios (se usará al final)
        const int DESCUBIERTAS = 8; // número de cartas descubiertas en la mesa
        static void Main()
        {
            int[] mazo = new int[40], // array de cartas del mazo
            mesa = new int[DESCUBIERTAS]; // array de cartas de la mesa
            int prim; // posición de la primera carta aún no extraída del mazo
            //InicializaMazo(ref mazo, out prim); // método ya implementado que genera el mazo

            Console.Write("¿Desea cargar una partida anterior (1) o empezar nuevo juego (2)?");
            if(int.Parse(Console.ReadLine()) == 1)
            {
                Lee(mesa, mazo, out prim);
            }
            else
            {
                InicializaMazoAleatorio(mazo, out prim);
                InicializaMesa(mazo, ref prim, mesa);
            }

            Console.Clear();

            Render(mesa, prim);

            bool abortado = false;

            while (HayPar(mesa) && !abortado)
            {
                // [NOTA MENTAL] Cuando sea out no te rayes que se crea en la misma llamada al método.
                PidePosiciones(out int p1, out int p2);

                if (SacaValor(mesa[p1]) == SacaValor(mesa[p2]) && p1 != p2)
                {
                    ExtraeCarta(mesa, p1, mazo, ref prim);
                    ExtraeCarta(mesa, p2, mazo, ref prim);
                }
                else if (p1 == p2)
                {
                    Console.Write("¿Desea abortar el juego? Sí (1)/ No (2) ");
                    if(int.Parse(Console.ReadLine()) == 1) abortado = true;
                }

                Console.Clear();

                Render(mesa, prim);
            }
            Console.Clear();

            Console.WriteLine("¿Desea guardar el juego? Sí (1)/ No(2) ");
            if (int.Parse(Console.ReadLine()) == 1) { Salva(mesa, mazo, prim); Console.Write("¡Hasta luego!"); }
            else
            {
                Console.Clear();

                if (MesaVacia(mesa)) Console.WriteLine("¡Ganaste!");
                else Console.WriteLine("¡Perdiste!");
            }

            
        }

        // [URGENCIA MÉDICA] Si puedes quitar el ref de mazo de aquí, quítalo, investiga.
        static void InicializaMazo(ref int[] mazo, out int prim)
        {
            // [NOTA MENTAL] si pone new seria el tamaño y las dimensiones, si no pone na como aquí sería el valor.
            mazo = [ 0, 3, 26, 24, 7, 15, 36, 16, 0, 29, 35, 34, 1, 6, 39, 15, 34, 16, 10, 19, 1, 6, 5, 17, 6, 11, 9, 19, 36, 14, 34, 25, 10, 20, 18, 5, 4, 18, 12, 7 ];
            prim = 0;
        }

        static void ExtraeCarta(int[] mesa, int pos, int[] mazo, ref int prim) // [NOTA MENTAL] pasamos prim por ref porque se crea fuera y se modifica dentro y queremos que salga.
        {
            // Extrae de mazo la primera disponible mazo[prim].
            // Pone ese valor en mesa[pos].
            // Incrementa prim.
            // Si está vacío no altera prim y deja vacía la posición pos.

            if(prim <= 39)
            {
                mesa[pos] = mazo[prim];
                prim++;
            }
            else
            {
                // Mirar a ver si hay que dejarlo.
                mesa[pos] = -1;
            }
        }

        static void InicializaMesa(int[] mazo, ref int prim, int[] mesa) // [NOTA MENTAL] Creemos que sí es ref.
        {
            // [NOTA MENTAL] Mejor esto que el length porque <<noes eficiente>>.

            for(int i = 0; i < DESCUBIERTAS; i++)
            {
                ExtraeCarta(mesa, i, mazo, ref prim);
            }
        }

        // [NOTA MENTAL] RENDER LO MÁS EFICIENTE POSIBLE.
        static void Render(int[] mesa, int prim)
        {
            // [NOTA MENTAL] el clear, fuera.

            Console.Write("Posiciones: ");

            for(int i = 0; i < DESCUBIERTAS; i++)
            {
                Console.Write($"{i} ");
            }

            Console.WriteLine();

            Console.Write("Cartas:     ");

            for(int i = 0; i < DESCUBIERTAS; i++)
            {
                // [NOTA MENTAL] guardar en variables los métodos que devuelven.

                int palo = SacaPalo(mesa[i]);
                int valor = SacaValor(mesa[i]);

                if (mesa[i] != -1)
                {
                    if (palo == 0) // OROS
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (palo == 1) // COPAS
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (palo == 2) // ESPADAS.
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (palo == 3) // BASTOS.
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write($"{valor} ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("· ");
                }
                
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.ResetColor();

            // [NOTA MENTAL] Olvídate siempre que puedas del getlength.
            Console.WriteLine($"Quedan {40 - prim} cartas en el mazo");
        }

        static int SacaValor(int carta)
        {
            int valor;

            if (carta == 0) { valor = 1; }
            else { valor = carta % 10; }

            return valor;
        }

        static int SacaPalo(int carta)
        {
            return carta / 10;
        }

        static void PidePosiciones(out int p1, out int p2) // [NOTA MENTAL] va por out porque se crean dentro y se sacan luego.
        {
            p1 = -1;
            p2 = -1;

            Console.Write("\nDeme una primera posición: ");
            while (p1 < 0 || p1 > DESCUBIERTAS)
            {
                
                p1 = int.Parse(Console.ReadLine());
            }

            Console.Write("\nDeme una segunda posición: ");
            while (p2 < 0 || p2 > DESCUBIERTAS)
            {
                
                p2 = int.Parse(Console.ReadLine());
            }
        }

        static bool HayPar(int[] mesa)
        {
            bool hayPar = false;

            // lo de i != j es pa no evaluar la misma carta.J EMPEIZA EN 1 PARA QUE NO COINCIDA LA PRIMERA.

            int i = 0;
            int j = 1;
            while(i < DESCUBIERTAS && !hayPar)
            {
                while (j < DESCUBIERTAS && !hayPar)
                {
                    hayPar = (SacaValor(mesa[i]) == SacaValor(mesa[j]));
                    j++;
                }
                j = 1;
                i++;
            }

            return hayPar;
        }

        static void Salva(int[] mesa, int[] mazo, int prim)
        {
            string file = "solitario.txt";

            StreamWriter sw = new StreamWriter(file);

            for(int i = 0; i < DESCUBIERTAS; i++)
            {
                sw.Write($"{mesa[i]} ");
            }

            sw.WriteLine();

            for(int i = 0; i < mazo.Length; i++)
            {
                sw.Write($"{mazo[i]} ");
            }

            sw.WriteLine();

            sw.Write(prim);

            sw.Close(); 
        }

        static void Lee(int[] mesa, int[] mazo, out int prim)
        {
            string file = "solitario.txt";

            StreamReader sr = new StreamReader(file);

            string[] mesaNums = (sr.ReadLine()).Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i < DESCUBIERTAS; i++)
            {
                mesa[i] = int.Parse(mesaNums[i]);
            }

            string[] mazoNums = (sr.ReadLine()).Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < mazo.Length; i++)
            {
                mazo[i] = int.Parse(mazoNums[i]);
            }

            prim = int.Parse(sr.ReadLine());

            sr.Close();
        }

        static void InicializaMazoAleatorio(int[] mazo, out int prim)
        {
            prim = 0; 

            for(int i = 0; i < mazo.Length; i++)
            {
                mazo[i] = i;
            }

            for(int i = 0; i < mazo.Length; i++)
            {
                mazo[i] = rnd.Next(mazo[i], mazo.Length);
            }
        }

        static bool MesaVacia(int[] mesa)
        {
            bool estaVacia = true;

            int i = 0;

            while(i < DESCUBIERTAS && estaVacia)
            {
                if (mesa[i] != -1) estaVacia = false;
            }

            return estaVacia;
        }
    }
}

