namespace Examen_parcial_abril_2018
{
    internal class MainClass
    {
        enum NomInstr { Avanza, Gira, Color, Limpia, Invierte}
        struct Instruccion
        {
            public NomInstr nom; // Nombre instrucción.
            public int param; // Parámetro/ argumento.
        }
        struct Programa
        {
            public Instruccion[] ins; // Vector de instrucciones.
            public int cont; // Contador.
        }
        public static void Main() // Método principal.c
        {
            // [NOTA MENTAL] 12 ->  9 para abajo.
            Tortuga t = new Tortuga(9, 12);

            Console.Write("¿Qué desea? 1) Sandbox 2) Archivo 3) Cuadrado");
            int opcion = int.Parse(Console.ReadLine());
            if (opcion == 1)
            {
                t.Render();

                char c = ' ';

                while (c != 'q')
                {
                    c = LeeInput();

                    if (c != ' ')
                    {
                        ProcesaInput(t, c);
                        t.Render();
                    }
                }

                Console.SetCursorPosition(0, 9);
            }
            else if(opcion == 2)
            {
             
                Console.Clear();
                string file = "logo.txt";
                LeePrograma(file, out Programa p);
                EjecutaPrograma(t, p);
                Console.SetCursorPosition(0, 9);
            }
            else
            {
                Console.Clear();
                Cuadrado(5, out string file);
                LeePrograma(file, out Programa p);
                EjecutaPrograma(t, p);
                Console.SetCursorPosition(0, 9);
            }

            /* Cuando son más de dos opciones, esto no se puede hacer porque se cuelga el programa
             ya que solo funciona el console readline para el primero...
            if (int.Parse(Console.ReadLine()) == 1)
            {
                // OPCION 1.
            }
            else if (int.Parse(Console.ReadLine()) == 2)
            {

                // OPCION 2.
            }
            else
            {
                // OPCION 3.
            }*/
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
            else if (c == 'a') t.Avanza(1); // Ir modificando esto según los pasos que quieres que de.
        }

        static void LeePrograma(string file, out Programa p)
        {

            /*if (File.Exists(file))
            {
                try
                { 
                }
                catch
                {
                    Console.WriteLine("El programa no está en el formato correcto. Se ha devuelto un programa vacío.");
                }
            }
            else
            {
                throw new Exception("El programa no existe. Se ha devuelto un programa vacío.");
            }*/

            // Crea el programa p. CON TAMAÑO PARA 100 INSTRUCCIONES (SE TE HABIA OLVIDADO).
            p = new Programa();
            p.ins = new Instruccion[100];
            // Lee las instrucciones de file.
            // Si no existe dicho archivo o tiene contenido incorrecto se devolverá un programa vacío.
            StreamReader sr = new StreamReader(file);
            p.cont = 0;
            while (!sr.EndOfStream)
            {
                string[] items = (sr.ReadLine()).Split(' ');
                
                if (items[0] == "avanza")
                {
                    p.ins[p.cont].nom = NomInstr.Avanza;
                    p.ins[p.cont].param = int.Parse(items[1]);
                }
                else if (items[0] == "gira")
                {
                    p.ins[p.cont].nom = NomInstr.Gira;
                }
                else if (items[0] == "color")
                {
                    p.ins[p.cont].nom = NomInstr.Color;
                }
                else if (items[0] == "limpia")
                {
                    p.ins[p.cont].nom = NomInstr.Limpia;
                }
                else if (items[0] == "invierte")
                {
                    p.ins[p.cont].nom = NomInstr.Invierte;
                }
                p.cont++;

            }
            sr.Close();
        }

        static void EjecutaPrograma(Tortuga t, Programa p)
        { 
            // La tortuga ejecuta la secuencia de instrucciones del programa.
            for(int i = 0; i < p.cont; i++)
            {
                if (p.ins[i].nom == NomInstr.Gira)
                {
                    t.Gira();
                }
                else if (p.ins[i].nom == NomInstr.Invierte)
                {
                    t.Simetria();
                }
                else if (p.ins[i].nom == NomInstr.Color)
                {
                    t.RotaColor();
                }
                else if (p.ins[i].nom == NomInstr.Avanza)
                {
                    t.Avanza(p.ins[i].param);
                }
                else if (p.ins[i].nom == NomInstr.Limpia)
                {
                    t.Limpia();
                }

                // Hace un render de lo ejecutado.
                t.Render();
            }
        }

        static void Cuadrado(int n, out string file)
        {
            file = "cua.txt";

            StreamWriter sw = new StreamWriter(file);

            sw.WriteLine("color");
            sw.WriteLine("gira");
            sw.WriteLine("gira");

            for(int i = 0; i < 4; i++)
            {
                sw.WriteLine("gira");
                sw.WriteLine($"avanza {n}");
            }

            sw.Close();
        }
    }
}
