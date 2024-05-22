using System;
using System.IO;

namespace Examen_parcial_abril_2018
{
    internal class Tortuga
    {
        enum Dir { Up, Right, Down, Left }; // orientaciones de la tortuga.
        enum Colores { Negro, Rojo, Verde, Azul }; // color del pincel.

        Colores[,] cuadricula; // cuadricula de dibujo.
        int fil, col; // posicion de la tortuga.
        Dir dir; // direccion en la que mira la tortuga.
        Colores pincel; // color del pincel de la tortuga.

        public Tortuga(int fils, int cols)
        {
            // Cuadrícula tamaño fils x cols;
            cuadricula = new Colores[fils, cols];

            // Todas las posiciones de la cuadrícula de color Negro.
            for (int i = 0; i < cuadricula.GetLength(0); i++)
            {
                for (int j = 0; j < cuadricula.GetLength(1); j++)
                {
                    cuadricula[i, j] = Colores.Negro;
                }
            }

            // Posición inicial de la tortuga en (0, 0);
            fil = 0;
            col = 0;

            // Inicialmente la dirección es down.
            dir = Dir.Down;

            // Inicialmente pincel de color negro.
            pincel = Colores.Negro;
        }

        public void Render()
        {
            Console.Clear();
            // Dibuja el estado de la cuadrícula con los colores
            // Se ocupan dos caracteres en pantalla.
            for (int i = 0; i < cuadricula.GetLength(0); i++)
            {
                for (int j = 0; j < cuadricula.GetLength(1); j++)
                {
                    switch (cuadricula[i, j])
                    {
                        case Colores.Negro:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("  ");
                            break;

                        case Colores.Rojo:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("  ");
                            break;
                        case Colores.Verde:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write("  ");
                            break;
                        case Colores.Azul:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("  ");
                            break;
                    }
                }
                Console.WriteLine();
            }

            // Guarda la posición actual del cursor.
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            // Dibuja la tortuga en su posición con el color del pincel.
            Console.SetCursorPosition(col * 2, fil);
            switch (pincel)
            {
                case Colores.Negro:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("ºº");
                    break;

                case Colores.Rojo:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("ºº");
                    break;
                case Colores.Verde:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("ºº");
                    break;
                case Colores.Azul:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("ºº");
                    break;
            }

            // Restablece la posición del cursor al final de la cuadrícula.
            Console.SetCursorPosition(currentLeft, currentTop);
            Console.ResetColor();
        }

        private void Paso()
        {
            // Pinta el cuadrado en el que se encuentra la tortuga del color actual del pincel.
            cuadricula[fil, col] = pincel;

            // La tortuga avanza un paso de acuerdo a su orientación, siempre que no se salga de la cuadrícula.
            // LO HAS HECHO MAL. RECUERDA. PARA EL 0 (> o <) Y PARA EL GETLENGTH (> o <) PERO CON -1.
            if (dir == Dir.Up && fil > 0) fil--;
            else if (dir == Dir.Down && fil < cuadricula.GetLength(0) -1) fil++;
            else if (dir == Dir.Left && col > 0) col--;
            else if (dir == Dir.Right && col < cuadricula.GetLength(1) -1) col++;
        }

        public void Avanza(int n)
        {
            // La tortuga da n pasos.
            for (int i = 0; i < n; i++) Paso();
        }

        public void Gira()
        {
            // Camba la orientación de la tortuga, girando 90º en el sentido horario.
            switch (dir)
            {
                case Dir.Up:
                    dir = Dir.Right; break;
                case Dir.Right:
                    dir = Dir.Down; break;
                case Dir.Down:
                    dir = Dir.Left; break;
                case Dir.Left:
                    dir = Dir.Up; break;
            }
        }

        public void Limpia()
        {
            // Limpia la pantalla.
            for (int i = 0; i < cuadricula.GetLength(0); i++)
            {
                for (int j = 0; j < cuadricula.GetLength(1); j++)
                {
                    cuadricula[i, j] = Colores.Negro;
                }
            }

            // Sitúa la tortuga en (0, 0), mirando hacia abajo y con el pincel negro.
            fil = 0;
            col = 0;
            dir = Dir.Down;
            pincel = Colores.Negro;
        }

        public void RotaColor()
        {
            // Cambia cíclicamente el color del pincel.
            switch (pincel)
            {
                case Colores.Negro:
                    pincel = Colores.Rojo; break;
                case Colores.Rojo:
                    pincel = Colores.Verde; break;
                case Colores.Verde:
                    pincel = Colores.Azul; break;
                case Colores.Azul:
                    pincel = Colores.Negro; break;
            }
        }

        public void Simetria() // TODO.
        {
            Colores[,] aux = new Colores[cuadricula.GetLength(0), cuadricula.GetLength(1)];

            for(int i = cuadricula.GetLength(0) - 1; i >= 0; i--)
            {
                for(int j = cuadricula.GetLength(1) - 1; j >= 0; j--)
                {
                    aux[i, j] = cuadricula[i, j];
                }
            }
            

        }
    }
}
