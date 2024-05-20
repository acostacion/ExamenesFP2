﻿using System;
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
            if (dir == Dir.Up && fil >= 0) fil--;
            else if (dir == Dir.Down && fil <= cuadricula.GetLength(0)) fil++;
            else if (dir == Dir.Left && col >= 0) col--;
            else if (dir == Dir.Right && col <= cuadricula.GetLength(1)) col++;
        }

        public void Avanza(int n)
        {

        }
    }
}