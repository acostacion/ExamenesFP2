namespace Examen_Parcial_Febrero_2017
{
    internal class Program
    {
        public static void Main()
        {
            const int MAX_FALLOS = 10; // número máximo de fallos permitidos.
            string pal; // palabra a adivinar.
            pal = "CELESTINA";// inicialización de la palabra.
            int fallos = 0; // inicialmente fallos a 0;

            bool[] descubiertas = new bool[pal.Length]; ;
            // inicializa el vector de descubiertas 
            InicializaDescubiertas(descubiertas);

            

            // bucle principal del juego.

            while (!PalabraAcertada(descubiertas) && fallos < MAX_FALLOS)
            {
                char c = LeeLetra();

                if (c != ' ')
                {
                    DescubreLetras(pal, descubiertas, c, out bool acierto);
                    if(!acierto) fallos++;
                    Muestra(pal, descubiertas, fallos);
                    
                }
            }
        }

        static void InicializaDescubiertas(bool[] descubiertas)
        {
            for(int i = 0; i < descubiertas.Length; i++)
            {
                descubiertas[i] = false;
            }
        }

        static void Muestra(string pal, bool[] descubiertas, int fallos)
        {
            for(int i = 0; i < pal.Length; i++)
            {
                if (descubiertas[i])
                {
                    Console.Write(pal[i]);
                }
                else
                {
                    Console.Write("-");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Fallos: {fallos}");
        }

        static bool PalabraAcertada(bool[] descubiertas)
        {
            bool cierta = true;

            int i = 0;
            while (i < descubiertas.Length && cierta)
            {
                if (!descubiertas[i])
                {
                    cierta = false;
                }
                i++;
            }

            return cierta;
        }

        static char LeeLetra()
        {
            Console.Write("Deme usted una letra, por favor: ");
            char letra = char.ToUpper(char.Parse(Console.ReadLine()));
            return letra;
        }

        static void DescubreLetras(string pal, bool[] descubiertas, char let, out bool acierto)
        {
            acierto = false;

            for(int i = 0; i < descubiertas.Length; i++)
            {
                if (pal[i] == let) descubiertas[i] = true;
            }

            // Pone a true las posiciones del vector descubiertas que contienen la letra let en la palabra pal.
            int j = 0;
            while(j < descubiertas.Length && !acierto)
            {
                if (pal[j] == let)  acierto = true;
                j++;
            }
        }
    }
}

