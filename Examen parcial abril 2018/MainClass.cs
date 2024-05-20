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
            int fils = 5;
            int cols = 5;
            Tortuga tortuga = new Tortuga(fils, cols);
            
            tortuga.Render();
        }
    }
}
