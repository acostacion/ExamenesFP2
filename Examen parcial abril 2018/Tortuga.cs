using System;
using System.IO;

namespace Examen_parcial_abril_2018
{
    class Tortuga
    {
        enum Dir { Up, Right, Down, Left }; // orientaciones de la tortuga.
        enum Colores { Negro, Rojo, Verde, Azul}; // color del pincel.

        Colores[,] cuadricula; // cuadricula de dibujo.
        int fil, col; // posicion de la tortuga.
        Dir dir; // direccion en la que mira la tortuga.
        Colores pincel; // color del pincel de la tortuga.

        public Tortuga(int fils, int cols)
        {
            // Creamos la cuadrícula filsxcols y la llenamos de negro.
            cuadricula = new Colores[fils, cols];

            Limpia();
        }

        public void Render()
        {
            // [NOTA MENTAL] No te olvides del clear porque antes de fregar hay que barrer.
            Console.Clear();
            // [NOTA MENTAL] En el render no pongas set cursor position a no ser que
            // estés completamente segura.

            // [NOTA MENTAL] Si pasa de 30 líneas suele pedir submétodos.
            for(int i = 0; i< cuadricula.GetLength(0); i++)
            {
                for (int j = 0; j < cuadricula.GetLength(1); j++)
                {
                    // [NOTA MENTAL] Fíjate porque es una forma muy chula de 
                    // representar personajes.
                    // 1. Vemos si i, j es la posición del Player.
                    // 2. Si no, pinta el fondo.
                    if (i == fil && j == col) RenderCocoteLiso();
                    else RenderColorCuadricula(i, j);  
                }
                Console.WriteLine();
            }
            // [NOTA MENTAL] Si no no deja de pintar nunca.
            Console.ResetColor();
        }

        #region Métodos Render
        // [NOTA MENTAL] Las variables globales no meterlas en métodos auxiliares.
        // Lo blanco no se mete y lo azul si.
        private void RenderCocoteLiso()
        {
            switch (pincel)
            {
                case Colores.Negro:
                    Console.BackgroundColor = ConsoleColor.Black; break;
                case Colores.Rojo:
                    Console.BackgroundColor = ConsoleColor.Red; break;
                case Colores.Verde:
                    Console.BackgroundColor = ConsoleColor.Green; break;
                case Colores.Azul:
                    Console.BackgroundColor = ConsoleColor.Blue; break;
            }
            
            switch (dir)
            {
                case Dir.Up:
                    Console.Write("··"); break;
                case Dir.Right:
                    Console.Write(" :"); break;
                case Dir.Down:
                    Console.Write(".."); break;
                case Dir.Left:
                    Console.Write(": "); break;
            }
        }

        private void RenderColorCuadricula(int i, int j)
        {
            switch (cuadricula[i, j])
            {
                case Colores.Negro:
                    Console.BackgroundColor = ConsoleColor.Black; break;
                case Colores.Rojo:
                    Console.BackgroundColor = ConsoleColor.Red; break;
                case Colores.Verde:
                    Console.BackgroundColor = ConsoleColor.Green; break;
                case Colores.Azul:
                    Console.BackgroundColor = ConsoleColor.Blue; break;
            }

            Console.Write("  ");
        }
        #endregion

        private void Paso()
        {
            // Pone en la posición actual de la tortuga el color del pincel.
            cuadricula[fil, col] = pincel;

            // Aquí aplicamos la regla del tiburón.
            if (dir == Dir.Down && fil < cuadricula.GetLength(0) - 1) fil++;
            else if (dir == Dir.Left && col > 0) col--;
            else if (dir == Dir.Up && fil > 0) fil--;
            else if (dir == Dir.Right && col < cuadricula.GetLength(1) - 1) col++;
        }

        public void Avanza(int n)
        {
            for(int i = 0; i <= n; i++) Paso();
        }

        public void Gira()
        {
            if (dir == Dir.Down) dir = Dir.Left;
            else if (dir == Dir.Left) dir = Dir.Up;
            else if (dir == Dir.Up) dir = Dir.Right;
            else if (dir == Dir.Right) dir = Dir.Down;
        }

        public void Limpia()
        {
            for (int i = 0; i < cuadricula.GetLength(0); i++)
            {
                for (int j = 0; j < cuadricula.GetLength(1); j++)
                {
                    cuadricula[i, j] = Colores.Negro;
                }
            }

            // Tortuguita en posicion (0, 0), mirando hacia abajo y con el pincel negro.
            fil = 0; col = 0;
            dir = Dir.Down;
            pincel = Colores.Negro;
        }

        public void RotaColor()
        {
            if (pincel == Colores.Negro) pincel = Colores.Rojo;
            else if (pincel == Colores.Rojo) pincel = Colores.Verde;
            else if (pincel == Colores.Verde) pincel = Colores.Azul;
            else if (pincel == Colores.Azul) pincel = Colores.Negro;
        }

        // [NOTA MENTAL] Memoriza simetría.
        public void Simetria()
        {
            Colores[,] traspuesta = new Colores[cuadricula.GetLength(1), cuadricula.GetLength(0)];

            // Hacer la traspuesta.
            for(int i = 0; i < cuadricula.GetLength(0); i++)
            {
                for(int j = 0; j < cuadricula.GetLength(1); j++)
                {
                    traspuesta[j,i] = cuadricula[i,j];
                }
            }

            RedimensionaAarray(ref cuadricula, traspuesta.GetLength(0), traspuesta.GetLength(1));

            for(int i = 0; i<cuadricula.GetLength(0); i++)
            {
                for(int j= 0; j< cuadricula.GetLength (1); j++)
                {
                    cuadricula[i, j] = traspuesta[i, j];
                }
            }
        }

        #region Métodos Simetría
        private void RedimensionaAarray(ref Colores[,] mat, int fils, int cols)
        {
            // Creamos un array nuevo con la dimensión deseada.
            Colores[,] aux = new Colores[fils, cols];
            // Copia el contenido del viejo array al nuevo.
            Array.Copy(mat, aux, mat.Length);
            // [NOTA MENTAL] El array.copy es un metodo especial y dentro de SU mat.Length
            // va el gl(1) y el gl(0).
            mat = aux;
        }
        #endregion


    }


}
