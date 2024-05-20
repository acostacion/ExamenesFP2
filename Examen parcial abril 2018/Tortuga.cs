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

        }
    }
}
