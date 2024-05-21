namespace Examen_parcial_abril_2018
{
    internal class MainClass
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

        public static void Main() // método principal.
        {
            int fils = 9;
            int cols = 12;
            Tortuga tortuga = new Tortuga(fils, cols);
            tortuga.Render();

            char c = LeeInput();

            while (c != 'q')
            {
                ProcesaInput(tortuga, ref c);
                tortuga.Render();
                Thread.Sleep(100);
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

        static void ProcesaInput(Tortuga t, ref char c)
        {
            c = LeeInput();

            if (c == 'c') t.RotaColor();
            else if (c == 'g') t.Gira();
            else if (c == 'l') t.Limpia();
            //else if (c == 's') t.Simetria();
            else if (c == 'a') t.Avanza(1); // Ir modificando esto según los pasos que quieres que de.
        }
    }
}
