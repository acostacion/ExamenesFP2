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
                    if (i == fil && j == col) RenderColorPincel();
                    else RenderColorCuadricula(i, j);  
                }
                Console.WriteLine();
            }
        }

        #region Métodos Render
        // [NOTA MENTAL] Las variables globales no meterlas en métodos auxiliares.
        // Lo blanco no se mete y lo azul si.
        private void RenderColorPincel()
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
            Console.Write("ºº");
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

            for(int i = 0; i<cuadricula.GetLength(0); i++)
            {
                for(int j= 0; j< cuadricula.GetLength (1); j++)
                {
                    cuadricula[i, j] = traspuesta[i, j];
                }
            }
        }
    }
}
