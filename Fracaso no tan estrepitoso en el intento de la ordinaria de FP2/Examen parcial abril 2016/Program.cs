using System;
using System.IO;

namespace sopa
{
    struct Par // representa posiciones y direcciones.
    {
        public int x, y;
    }

    struct Sopa // sopa de letras junto con sus dimensiones.
    {
        public int alto, ancho; // dimensiones.
        public string[] tab; // array de strings.
    }

    internal class Program
    {
        string file = "torturesoup.txt";

        public static void Main()
        {
            // sopa de letras del ejemplo.
            Sopa s;
            s.alto = 6;
            s.ancho = 9;
            s.tab = new string[] // sopa de letras del ejemplo.
            {
                "ABCDBARCO", "EKLMNOPQR", 
                "HTAVIONOR", "CGRTUITXB", 
                "OROHFOVAZ", "CMPPMEVAN" };

            Resuelve(s, s.tab);
            
            
        }

        string[] palabs = { "COCHE", "AVION", "BARCO", "MOTO", "PATINES" };

        // resuelve (s, pals);

        static Par[] Dirs()
        {
            Par[] dirs = new Par[8];

            int cont = 0;

            for(int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) ;
                    else
                    {
                        dirs[cont].x = j;
                        dirs[cont].y = i;
                        cont++;
                    }
                }
            }
            return dirs;
        }

        static bool CompruebaPosDir(Sopa s, string pal, Par pos, Par dir)
        {
            // [NOTA MENTAL] Observa este código, es la forma más pro
            // de hacerlo. Si es algo de contadores es posible que el return
            // vaya a ser así de pro. De verdad te lo digo, aprecia este
            // método. Léelo, siéntelo, hazte con el método.
            int cont = 0;

            for (int i = pos.y; i <= pal.Length; i += dir.y)
            {
                for (int j = pos.x; j <= pal.Length; j += dir.x)
                {
                    if(i >= 0 && j>= 0)
                    {
                        if (s.tab[i][j] == pal[cont] && cont < pal.Length)
                        {
                            cont++;
                        }
                    }
                    
                }
            }
            return cont == pal.Length;
        }

        static bool BuscaDir(Sopa s, string pal, Par pos, out Par dir)
        {
            Par[] dirs = Dirs();
            bool encontradaDir = false;

           int i = 0;
           while(i < dirs.Length && !encontradaDir)
           {  
                if(CompruebaPosDir(s, pal, pos, dirs[i])) encontradaDir=true;
           }

           dir = dirs[i];

           return encontradaDir;
        }

        static bool BuscaPal(Sopa s, string pal, out Par pos, out Par dir)
        {
            int i = 0;
            int j = 0;

            Par[] dirs = Dirs();

            bool encontradaPal = false;

            while (i <= s.alto && !encontradaPal)
            {
                while (j <= s.ancho && !encontradaPal)
                {
                    pos.x = j; pos.y = i;
                    encontradaPal = BuscaDir(s, pal, pos, out dirs[i]) ;
                    j++;
                }
                i++;
            }

            pos.x = j; pos.y = i;
            dir = dirs[i];

            return encontradaPal;
        }

        static void Resuelve(Sopa s, string[] pals)
        {
            bool terminado = false;
            bool palabraEncontrada = false;
            bool dirEncontrada = false;

            int i = 0;
            int j = 0;
            int k = 0;

            while(i < s.alto && !terminado)
            {
                while(j < s.ancho && !terminado)
                {
                    palabraEncontrada = false;

                    while(k < pals.Length || !palabraEncontrada)
                    {  
                        Par pos = new Par();
                        pos.x = j;
                        pos.y = i;

                        if (BuscaPal(s, pals[k], out pos, out Par dir))
                        {
                            Console.WriteLine($"Encontrada {pals[k]} en Posicion ({pos.x}, {pos.y})  direccion ({dir.x}, {dir.y})");
                            palabraEncontrada = true;
                        }
                        else if (i == s.alto && j == s.ancho && !palabraEncontrada)
                        {
                            Console.WriteLine($"No encontrada {pals[k]}");
                            palabraEncontrada = false;
                        }

                        terminado = k == pals.Length;

                        k++;
                    }
                    j++;
                }
                i++;
            }
            
        }
        
        static void LeeSopa(string file, Sopa s, string[] pals)
        {
            StreamReader sr = new StreamReader(file);

            string line = sr.ReadLine();
            s.alto = int.Parse(line);
            line = sr.ReadLine();
            s.ancho = int.Parse(line);

            for(int i = 0; i <= s.alto; i++)
            {
                // [NOTA MENTAL] Los bucles anidados se reinician solos, te estás rayando.
                for(int j = 0; j <= s.ancho; j++)
                {
                    s.tab[i] = sr.ReadLine();
                }
            }

            pals = new string[int.Parse(Console.ReadLine())];

            for(int i = 0; i < pals.Length; i++)
            {
                pals[i] = sr.ReadLine();
            }
        }
    }
}


