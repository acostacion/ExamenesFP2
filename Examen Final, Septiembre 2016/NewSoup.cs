
// Tomelloso.

using System.Runtime.InteropServices;
using ConsoleApp1;

namespace Examen_Final__Septiembre_2016
{
    internal class NewSoup
    {
        static void Main()
        {
            int fil = 30;
            int col = 30;
            bool[,] tab = new bool[fil, col];

            string file = "jordiwild.txt";

            //tab = LeeEntrada(file);

            tab = Inicializa(fil, col);

            Console.Clear();
            Dibuja(tab);

            bool fin = false;

            while(!fin)
            {
                Console.Clear();
                Dibuja(tab);

                bool[,] aux = Siguiente(tab);

                if(Estable(tab, aux))
                {
                    fin = true;
                }

                for (int i = 0; i < tab.GetLength(0); i++)
                {
                    for(int j = 0; j <  tab.GetLength(1); j++)
                    {
            
                        tab[i,j] = aux[i, j];
                    }
                }
            }
        }

        static bool[,] Inicializa(int fils, int cols)
        {
            // True (viva), False (vacía).

            Random rnd = new Random();
            bool[,] tab = new bool[fils, cols];

            for (int i = 0; i < fils; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Lo pone a true.
                    if (rnd.Next(0, 2) == 1) tab[i, j] = true;
                    else tab[i, j] = false;
                }
            }
            return tab;
        }

        static void Dibuja(bool[,] mat)
        {
            // Cuadrados -> células.

            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }

        static int Vecinos(bool[,] mat, int x, int y)
        {
            /* Regla mnemotécnica TIBURONES V.2

            // x-1, y-1 | x,y-1 | x+1, y-1
            // x-1, y   |  x,y  | x+1, y
            // x-1, y+1 | x,y+1 | x+1, y+1

            // Regla mnemotécnica:
            // 1. Separar en casos: bordes y esquinas (hacerte esquema de arriba).
            // 2. Hay tantos casos como adyacentes (alrededores a comprobar) haya.
            // 3. Hay dos formas de que sea: a > 0 y a < mat.GetLegth() - 1 (Como los tiburones, vaya).
            // 4. Si a > 0 -> mat[a-1]
            // 5. Si a < mat.GetLegth() - 1 -> mat[a+1]
            // 6. Si a no se comprueba, en mat no cambia -> mat[a].
            // 7. Siempre con &&.
            // Ej.: <<else if (x > 0 && y < mat.GetLength(0) - 1 && mat[x - 1, y + 1])>>. Cero y tiburón.
            // Ej.: <<else if (y > 0 && mat[x, y - 1])>>. Nada y cero.

            */

            int count = 0;

            // Verificar las celdas vecinas, teniendo en cuenta los bordes de la matriz

            if (x > 0 && y > 0 && mat[x - 1, y - 1]) // Esquina superior izquierda.
            {
                count++;  
            }  
            if (x > 0 && mat[x - 1, y]) // Borde izquierdo.
            {
                count++;  
            }
            if (x > 0 && y < mat.GetLength(0) - 1 && mat[x - 1, y + 1]) // Esquina inferior izquierda.
            {
                count++;  
            }
            if (y > 0 && mat[x, y - 1]) // Borde superior.
            {
                count++;  
            } 
            if (y < mat.GetLength(0) - 1 && mat[x, y + 1]) // Borde abajo.
            {
                count++;  
            }
            if (x < mat.GetLength(1) - 1 && y > 0 && mat[x + 1, y - 1]) // Esquina superior derecha.
            {
                count++;  
            }   
            if (x < mat.GetLength(1) - 1 && mat[x + 1, y]) // Borde derecho.
            {
                count++;  
            }  
            if (x < mat.GetLength(1) - 1 && y < mat.GetLength(0) - 1 && mat[x + 1, y + 1]) // Esquina inferior derecha.
            {
                count++;  
            }

            return count;

        }

        static bool[,] Siguiente(bool[,] mat)
        {
            // Supervivencia: si una célula tiene 2 o 3 vecinas -> SOBREVIVE.
            // Fallecimiento: si una célula tiene >=4 vecinas -> MUERE.
            // Fallecimiento: si una célula tiene <2 vecinas -> MUERE.
            // Nacimiento: si vacío tiene ==3 vecinas -> NACE.


            bool[,] tab = new bool[mat.GetLength(0), mat.GetLength(1)];
            int v = 0;


            for (int i = 0; i <  mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    v = Vecinos(mat, i, j);

                    if (mat[i, j])
                    {
                        if (v == 2 || v == 3)
                        {
                            tab[i, j] = true;
                        }
                        else if (v >= 4)
                        {
                            tab[i, j] = false;
                        }
                        else if (v < 2)
                        {
                            tab[i, j] = false;
                        }
                    }
                    else
                    {
                        if (v == 3)
                        {
                            tab[i, j] = true;
                        }
                    }
                }
            }
            return tab;
        }

        static bool Estable(bool[,] mat1, bool[,] mat2)
        {
            int i = 0; int j = 0;

            bool sonIguales = true;

            while(i < mat1.GetLength(0) && sonIguales)
            {
                j = 0;
                while (j < mat1.GetLength(1) && sonIguales)
                {
                    if (mat1[i,j] != mat2[i, j])
                    {
                        sonIguales = false;
                    }
                    j++;
                }
                i++;
            }

            return sonIguales;
        }

        static bool[,] LeeEntrada(string file)
        {
            StreamReader sr = new StreamReader(file);

            int fil, col;
            fil = int.Parse(sr.ReadLine());
            col = int.Parse(sr.ReadLine());

            bool[,] mat = new bool[fil, col];

            string linea;

            for(int i = 0; i < fil; i++)
            {
                linea = sr.ReadLine();

                for(int j = 0; j < col; j++)
                {
                    if (linea[j] == '1')
                    {
                        mat[i, j] = true;
                    }
                    else
                    {
                        mat[i, j] = false;
                    }
                }
            }
            sr.Close();

            return mat;
        }

        static SetCoor Convierte(bool[,] mat)
        {
            SetCoor setCoor = new SetCoor();

            for(int i = 0; i <  mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j])
                    {
                        setCoor.Add(new Coor(i, j));
                    }
                }
            }
            return setCoor;
        }
    }
}
