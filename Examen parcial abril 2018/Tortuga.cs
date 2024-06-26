using System;
using System.IO;
using static System.Net.WebRequestMethods;

namespace Examen_parcial_abril_2018
{
    internal class Tortuga
    {
        enum Dir { Up, Right, Down, Left } // Orientaciones de la tortuga.
        enum Colores { Negro, Rojo, Verde, Azul } // Color del pincel.

        Colores[,] cuadricula; // Cuadrícula del dibujo.
        int fil, col; // Posición de la tortuga.
        Dir dir; // Dirección en la que mira la tortuga.
        Colores pincel; // Color del pincel de la tortuga.

        public Tortuga(int fils, int cols)
        {
            // Genera una cuadrícula de tamaño filsxcols.
            cuadricula = new Colores[fils, cols];

            Limpia();
        }

        public void Render()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            // Dibuja con dos caracteres en pantalla los colores del fondo.
            for(int i = 0; i < cuadricula.GetLength(0); i++)
            {
                for(int j = 0; j < cuadricula.GetLength(1); j++)
                {
                    if(cuadricula[i, j] == Colores.Negro)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (cuadricula[i, j] == Colores.Rojo)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else if (cuadricula[i, j] == Colores.Verde)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else if (cuadricula[i, j] == Colores.Azul)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    Console.Write("  ");
                }
                Console.WriteLine();
            }

            // Dibuja a la tortuga en la posición (fil, col) con "ºº" y el fondo del pincel.
            // [NOTA MENTAL] El cursor va cambiado (col, fil).
            Console.SetCursorPosition(col * 2, fil);
            if (pincel == Colores.Negro)
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (pincel == Colores.Rojo)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else if (pincel == Colores.Verde)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (pincel == Colores.Azul)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            Console.Write("ºº");

            Console.ResetColor();
        }

        private void Paso()
        {
            // Pinta el cuadrado actual de la tortuga del color del pincel.
            cuadricula[fil, col] = pincel;

            // La tortuga avanza un paso en su dirección sin salirse de la cuadrícula.
            if(dir == Dir.Left && col > 0)
            {
                col--;
            }
            else if(dir == Dir.Right && col < cuadricula.GetLength(1) - 1)
            {
                col++;
            }
            else if(dir == Dir.Up && fil > 0)
            {
                fil--;
            }
            else if(dir == Dir.Down && fil < cuadricula.GetLength(0) - 1)
            {
                fil++;
            }
        }

        public void Avanza(int n)
        {
            for(int i = 0; i < n; i++)
            {
                Paso();
            }
        }

        public void Gira()
        {
            if (dir == Dir.Left)
            {
                dir = Dir.Up;
            }
            else if (dir == Dir.Right)
            {
                dir = Dir.Down;
            }
            else if (dir == Dir.Up)
            {
                dir = Dir.Right;
            }
            else if (dir == Dir.Down)
            {
                dir = Dir.Left;
            }
        }

        public void Limpia()
        {
            // Todas las posiciones de la cuadrícula son de color negro.
            for (int i = 0; i < cuadricula.GetLength(0); i++)
            {
                for (int j = 0; j < cuadricula.GetLength(1); j++)
                {
                    cuadricula[i, j] = Colores.Negro;
                }
            }

            // La tortuga está en la posición (0, 0), con orientación Down y con pincel negro.
            fil = col = 0;
            dir = Dir.Down;
            pincel = Colores.Negro;
        }

        public void RotaColor()
        {
            if(pincel == Colores.Negro)
            {
                pincel = Colores.Rojo;
            }
            else if(pincel == Colores.Rojo)
            {
                pincel = Colores.Verde;
            }
            else if (pincel == Colores.Verde)
            {
                pincel = Colores.Azul;
            }
            else if (pincel == Colores.Azul)
            {
                pincel = Colores.Negro;
            }
        }

        public void Simetria()
        {
            int fils = cuadricula.GetLength(0);
            int cols = cuadricula.GetLength(1);
            Colores[,] aux = new Colores[cols, fils];

            for (int i = 0; i < fils; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    aux[j,i] = cuadricula[i, j];
                }
            }

            RedimensionaAarray(ref cuadricula, cols, fils);

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < fils; j++)
                {
                    cuadricula[i,j] = aux[i,j];
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
