using System;

namespace Examen_septiembre_2016
{
    internal class Program
    {
        const int fils = 20;
        const int cols = 20;

        static void Main(string[] args)
        {
            string file = "folagor.txt";

            // INICIALMENTE.
            //bool[,] tablero = LeeEntrada(file);
            bool[,] tablero = Inicializa(fils, cols);
            Dibuja(tablero);

            while (!Estable(tablero, Siguiente(tablero)))
            {
                // Primero lo dibuja y luego lo sobreescribes.
                Dibuja(Siguiente(tablero));
                tablero = Siguiente(tablero);
                Thread.Sleep(400);
            }
            
        }

        static bool[,] Inicializa(int fils, int cols)
        {
            // [NOTA MENTAL] Que el random no te engañe, es Next(0, 2) -> [0, 2).
            Random rnd = new Random();
            bool[,] bools = new bool[fils, cols];

            // TRUE célula viva.
            // FALSE casilla vacía.
            for (int i = 0; i< bools.GetLength(0); i++)
            {
                for(int j = 0; j< bools.GetLength(1); j++)
                {
                    // Si sale 0 -> false.
                    // Si sale 1 -> true.
                    if (rnd.Next(0, 2) == 0) bools[i, j] = false;
                    else bools[i, j] = true;
                }
            }

            return bools; 
        }

        static void Dibuja(bool[,] mat)
        {
            // [NOTA MENTAL] Es muy importante antes de escribir en una pizarra de borrarla. Antes de fregar hay que barrer.
            Console.Clear();
                
            // [NOTA MENTAL] SIEMPRE es doble. TE LO HE ADIVINAO falso JAIME PANOLI.
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    // Las células moradas y el fondo azul.
                    if (mat[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(":3");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write("  ");
                    }
                }
                // [NOTA MENTAL] Que no te engañen, los writeline tras cada linea leída.
                // En asia se lee pa abajo y en Jaime se lee de lado. No olvides el writeline.
                Console.WriteLine();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(Convierte(mat).ToString());

            Console.ResetColor();
        }

        static int Vecinos(bool[,] mat, int x, int y)
        {
            // Decir cuántas células hay alrededor de mat[x,y].
            int cuentaCells = 0;

            // Bordes.
            if(y == 0 && x != 0 && x != mat.GetLength(1) - 1) // arriba.
            {
                for (int i = y; i <= y + 1; i++)
                {
                    for (int j = x - 1; j <= x + 1; j++)
                    {
                        if (mat[i, j]) cuentaCells++;
                    }
                }
            } 
            else if (x == 0 && y!=0 && y != mat.GetLength(0)-1) // izquierda.
            {
                for (int i = y - 1; i <= y + 1; i++)
                {
                    for (int j = x; j <= x + 1; j++)
                    {
                        if (mat[i, j]) cuentaCells++;
                    }
                }
            }
            else if (y == mat.GetLength(0) - 1 && x!= 0 && x!=mat.GetLength(1)-1) // abajo.
            {
                for (int i = y - 1; i <= y; i++)
                {
                    for (int j = x - 1; j <= x + 1; j++)
                    {
                        if (mat[i, j]) cuentaCells++;
                    }
                }
            }
            else if (x == mat.GetLength(1) - 1 && y!= 0 && y!= mat.GetLength(0)-1) // derecha
            {
                for (int i = y - 1; i <= y + 1; i++)
                {
                    for (int j = x - 1; j <= x; j++)
                    {
                        if (mat[i, j]) cuentaCells++;
                    }
                }
            }
            // Esquinas.
            else if(x == 0 && y == 0) // Esquina arriba izquierda.
            {
                for (int i = y; i <= y + 1; i++)
                {
                    for (int j = x; j <= x + 1; j++)
                    {
                        if (mat[i, j]) cuentaCells++;
                    }
                }
            }
            else if(x==0 && y == mat.GetLength(0) - 1) // Esquina abajo izquierda.
            {
                for (int i = y - 1; i <= y; i++)
                {
                    for (int j = x; j <= x + 1; j++)
                    {
                        if (mat[i, j]) cuentaCells++;
                    }
                }
            }
            else if(x== mat.GetLength(1) -1 && y == 0) // Esquina derecha arriba.
            {
                for (int i = y; i <= y + 1; i++)
                {
                    for (int j = x - 1; j <= x; j++)
                    {
                        if (mat[i, j]) cuentaCells++;
                    }
                }
            }
            else if(x == mat.GetLength(1)-1 && y == mat.GetLength(0) - 1) // Esquina abajo derecha.
            {
                for (int i = y - 1; i <= y; i++)
                {
                    for (int j = x - 1; j <= x; j++)
                    {
                        if (mat[i, j]) cuentaCells++;
                    }
                }
            }
            // Caso formal.
            else
            {
                for (int i = y - 1; i <= y + 1; i++)
                {
                    for (int j = x - 1; j <= x + 1; j++)
                    {
                        if (mat[i, j]) cuentaCells++;
                    }
                }
            }

            // Le quitamos la cell de (X,Y).
            return cuentaCells - 1;
        }

        static bool[,] Siguiente(bool[,] mat)
        {
            // Hace las tres reglas y devuelve una nueva matriz ACTUALIZADA.

            // Si tiene 2 o 3 alrededor sobrevive y se queda.
            // Si tiene 4 o + alrededor muere por superpoblación.
            // Si tiene 1 o - alrededor muere por falta.
            // ***Si hay una vacía y alrededor hay 3 amigos, nace en la vacía.

            bool[,] siguiente = new bool[mat.GetLength(0), mat.GetLength(1)];   

            for(int i = 0; i<mat.GetLength(0); i++)
            {
                for(int j = 0; j<mat.GetLength(1); j++)
                {
                    // Si hay célula...
                    if (mat[i, j])
                    {
                        // Si tiene 2 o 3 alrededor...
                        if (Vecinos(mat, j, i) == 2 || Vecinos(mat, j, i) == 3)
                        {
                            // Sobrevivie y se queda.
                            siguiente[i, j] = true;
                        }
                        // Si tiene 4 o + alrededor || 1 o -...
                        else if (Vecinos(mat, j, i) >= 4 || Vecinos(mat, j, i) <= 1)
                        {
                            // Muere.
                            siguiente[i, j] = false;
                        }
                    }
                    // Si no hay célula...
                    else
                    {
                        // Si hay 3 alrededor de un hueco vacío...
                        if (Vecinos(mat, j, i) == 3)
                        {
                            // Nace la vida.
                            siguiente[i, j] = true;
                        }
                    }
                }
            }

            return siguiente;

        }

        static bool Estable(bool[,] mat1, bool[,] mat2)
        {
            bool estabilidad = true;

            // [NOTA MENTAL] En el momento en el que haya que hacer una comprobacion
            // booleana liviana suele o soliere ser un while ya que
            // a livianosidad encontrada no mires mas, que no hay más.

            // [NOTA MENTAL] Son por cojones de tamaños iguales ya que comparten
            // dimensión de tablero. Es la misma pero con diferentes colores.
            // Son la misma mierda pero con diferente olor.

            // [NOTA MENTAL] Date cuenta que no puede hacerse estabilidad = false inicialmench
            // PORQUE:
            // SI HAY UNA UNIDAD DE FALSE EN UN MAR DE ESTABILIDAD SE NOS JODE EL TINGLADO.
            int i = 0; int j = 0;

            while (i < mat1.GetLength(0) && estabilidad)
            {
                while(j < mat1.GetLength(1) && estabilidad)
                {
                    // Si son distintos deja de ser estable.
                    if (mat1[i, j] != mat2[i, j]) estabilidad = false;
                    j++;
                }
                i++;
            }

            return estabilidad;
        }

        static bool[,] LeeEntrada(string file)
        {
            StreamReader sr = new StreamReader(file);

            string[] size = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            bool[,] matrix = new bool[int.Parse(size[0]), int.Parse(size[1])];

            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                // [NOTA MENTAL] Se lee la línea y luego se rellena.
                string[] tab = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (int.Parse(tab[j]) == 1) matrix[i, j] = true;
                    else matrix[i, j] = false;
                }
            }

            // [NOTA MENTAL] Nunca te olvides de poner el Close pa cerrar flujo.
            sr.Close();

            return matrix;
        }

        static SetCoor Convierte(bool[,] matrix) 
        {
            SetCoor coors = new SetCoor();
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for ( int j = 0; j < matrix.GetLength(1); j++)
                {
                    Coor c = new Coor();
                    c.SetX(i+1);
                    c.SetY(j+1);
                    if (matrix[i, j]) coors.Add(c);
                }
            }

            return coors;
        }

        static int CuentaRec(SetCoor l)
        {

        }

    }
}
