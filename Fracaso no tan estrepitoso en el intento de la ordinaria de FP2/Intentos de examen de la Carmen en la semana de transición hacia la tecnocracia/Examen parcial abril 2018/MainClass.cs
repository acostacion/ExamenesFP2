using System.Drawing;

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

            string file = "comandos.txt";

            

            while (c != 'q')
            {
                ProcesaInput(tortuga, ref c);
                tortuga.Render();
                Thread.Sleep(100);

                //¡¡¡OJO!!! FALTA POR METERLE LA IMPLEMENTACION DEL N DE AVANZA (LeeInput, ProcesaInput, LeePrograma, comandos.txt).
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

        static void LeePrograma(string file, Programa p)
        {
            // Creamos un programa con capacidad para 100 instrucciones.
            p = new Programa();
            p.ins = new Instruccion[100];

            // En el caso de no existir dicho archivo se devolverá un programa vacío (HACER LO DEL CONTENIDO INCORRECTO).
            if (File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);

                // Procesamiento de las líneas.
                for (int i = 0; i < File.ReadAllLines(file).Count(); i++)
                {
                    switch (sr.ReadLine())
                    {
                        case "avanza":
                            p.ins[i].nom = NomInstr.Avanza; break;
                        case "gira":
                            p.ins[i].nom = NomInstr.Gira; break;
                        case "color":
                            p.ins[i].nom = NomInstr.Color; break;
                        case "limpia":
                            p.ins[i].nom = NomInstr.Limpia; break;
                        case "invierte":
                            p.ins[i].nom = NomInstr.Invierte; break;
                    }
                }

                sr.Close();
            }   
        }

        static void EjecutaPrograma(Tortuga t, Programa p)
        {
            char c = LeeInput();
            LeePrograma
            ProcesaInput(t, ref c);
            t.Render();
        }


    }
}
