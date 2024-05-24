using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Examen_parcial_abril_2018
{
    internal class Program
    {
        enum NomInstr { Avanza, Gira, Color, Limpia, Invierte}

        struct Instruccion
        {
            public NomInstr nom; // nombre instruccion.
            public int param; // parametro/argumento.
        }

        struct Programa
        {
            public Instruccion[] ins; // vector de instrucciones.
            public int cont; // contador.
        }
     
        static void Main()
        {
            string file = "cocotesmooth.txt";
            string cua = "cua.txt";

            // [NOTA MENTAL] La misma tortuga vale para los tres, panoli.
            // lee el enunciado. Abre tu mente que no te raye la tortuga.
            Tortuga t = new Tortuga(9, 12);

            Console.WriteLine("¿Qué prefieres leer de archivo o jugar normal? 1/2");
            if(int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("¿Qué prefieres 'cocotesmooth' o 'cuadrado'? 1/2");
                if(int.Parse(Console.ReadLine()) == 1)
                {
                    LeePrograma(file, out Programa p);
                    // [NOTA MENTAL] El bucle principal de abajo es cuando estás
                    // en modo sandbox porque el Ejecuta lo que hace es, ejecutarlo.
                    // y tu lo observas, no juegas.
                    EjecutaPrograma(t, p);
                    
                }
                else
                {
                    Cuadrado(5, cua);
                    LeePrograma(cua, out Programa p);
                    EjecutaPrograma(t, p);


                }
            }
            else
            {
                
                t.Render();
                char c = LeeInput();

                // [NOTA MENTAL] El input va siempre dentro y fuera.
                while (c != 'q')
                {
                    // [NOTA MENTAL] Se renderiza antes de que se procesa.
                    // Al hacer stop motion pintas, y luego siguiente frame.
                    // Para después volverlo a pintar, para luego otro frame...
                    t.Render();
                    c = LeeInput();
                    ProcesaInput(t, c);
                    // [NOTA MENTAL] Si tu programa parpadea a mogollon y se ve
                    // como un mojón, aplica thread.sleep que es la solución.
                    Thread.Sleep(200);
                }
            }  
        }

        static char LeeInput()
        {
            char d = ' ';
            if (Console.KeyAvailable)
            {
                string tecla = Console.ReadKey(true).Key.ToString();
                switch (tecla)
                {
                    case "A": d = 'a'; break; // avanza.
                    case "C": d = 'c'; break; // color.
                    case "G": d = 'g'; break; // gira.
                    case "L": d = 'l'; break; // limpia.
                    case "S": d = 's'; break; // simétrico.
                    case "Q": d = 'q'; break; // exit.
                }
                while (Console.KeyAvailable)
                    Console.ReadKey().Key.ToString();
            }
            return d;
        }

        static void ProcesaInput(Tortuga t, char c)
        {
            if (c == 'c') t.RotaColor();
            else if (c == 'g') t.Gira();
            else if (c == 'l') t.Limpia();
            else if (c == 's') t.Simetria();
            else if (c == 'a') t.Avanza(0); // Ir modificando esto según los pasos que quieres que de.
        }

        static void LeePrograma(string file, out Programa p)
        {
            // Crea un programa con hueco para 100 instrucciones.
            p = new Programa();
            p.ins = new Instruccion[100];

            if (File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);

                for (int i = 0; i < File.ReadAllLines(file).Length; i++)
                {
                    string[] items = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    if (items[0] == "avanza") p.ins[i].nom = NomInstr.Avanza;
                    else if (items[0] == "gira") p.ins[i].nom = NomInstr.Gira;
                    else if (items[0] == "color") p.ins[i].nom = NomInstr.Color;
                    else if (items[0] == "limpia") p.ins[i].nom = NomInstr.Limpia;
                    else if (items[0] == "invierte") p.ins[i].nom = NomInstr.Invierte;

                    if (items.Length > 1)
                    {
                        p.ins[i].param = int.Parse(items[1]);
                        p.cont = p.cont + int.Parse(items[1]);
                    }
                    else p.cont++;
                }

                sr.Close();
            }
            else
            {
                throw new Exception("¡No existe el programa!");
            }
        }

        static void EjecutaPrograma(Tortuga t, Programa p)
        {
            for(int i = 0; i <= p.cont; i++)
            {
                if (p.ins[i].nom == NomInstr.Avanza) t.Avanza(p.ins[i].param);
                else if (p.ins[i].nom == NomInstr.Gira) t.Gira();
                else if (p.ins[i].nom == NomInstr.Color) t.RotaColor();
                else if (p.ins[i].nom == NomInstr.Limpia) t.Limpia();
                else if (p.ins[i].nom == NomInstr.Invierte) t.Simetria();
                t.Render();
            }
        }

        static void Cuadrado(int n, string file)
        {
            StreamWriter sr = new StreamWriter(file);

            sr.WriteLine("limpia");
            sr.WriteLine("color");
            for(int i = 0; i < 3; i++) sr.WriteLine("gira");
            for(int i = 0; i < 3; i++)
            {
                sr.WriteLine($"avanza {n}");
                sr.WriteLine("gira");
            }
            sr.WriteLine($"avanza {n - 1}");

            sr.Close();
        }
    }
}
