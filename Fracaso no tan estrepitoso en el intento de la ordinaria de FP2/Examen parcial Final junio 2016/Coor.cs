using System.Diagnostics;
using System.Text;

namespace Examen_parcial_Final_junio_2016
{
    class Coor{
        // atributos
        int _x, _y; // componentes x y de la coordenada

        /*
        Dos constructoras Coor
            - Una con argumentos de inicialización
            - Otra sin argumentos: inicializa a 0 los valores x y de la coordenada
        C# no confunde nombres porque internamente las nombra como Coor_int_int y Coor respectivamente
        nombre interno 
        */
        //public Coor(int x, int y){ // nombre interno Coor_int_int
        //    _x = x;
        //    _y = y;
        //}

        // 
        //public Coor(){ // nombre interno Coor
        //    _x = _y = 0;
        //}


        /*
        Una constructora que engloba a las otras dos: 
            - inicializaciones por defecto
            - con parámetro nombrados
        */
        public Coor(int x=0, int y=0){ 
            _x = x;
            _y = y;
        }
        /*
        Puede llamarse de múltiples modos, con parámetros posicionales
            - Coor()    devuelve (0,0)
            - Coor(1,2) devuelve (0,0)
            - Coor(7)   devuelve (7,0)   

        Con parámetros nombrados:            
            - Coor(x:1, y:2)  devuelve (1,2)    
            - Coor(y:2)       devuelve (0,2)             
            - Coor(y:2, x:1)  devuelve (1,2)
        */



        // Métodos getters: devuelven valor de x e y
        public int GetX(){ return _x; }
        public int GetY(){ return _y; }
        
        // métodos setters: cambian valores x e y 
        public void SetX(int val){ _x = val;}
        public void SetY(int val){ _y = val;}

        /*
            Propiedades: Una formá más cómoda de acceder a los atributos
                - Sintaxis especial para definir getters y setters
                - que permiten el acceso con .X, como si fuese un atributo público
            En el set, "value" es un parámetro implícito con el valor a asignar
        */

        public int X{ // es habitual darle el mismo nombre que al atributo _x, pero comenznado con mayúscula X
            get => _x;  // devuelve el valor de _x
            set => _x = value; // cambia el valor de x
        }
        /* Si c es una coordenada, ahora podemos hacer 
            - int a = c.X;
            - c.X = 7;
        */

        // idem para la Y
        public int Y{   
            get => _y;  
            set => _y = value; 
        }


        /* 
        Método para sumar un vector v a la coordenada: en este caso se modifica la coordenada actual
        
        Utilizamos el mismo tipo Coor de manera dual: como punto y como vector en el plano
        Distintas formas de acceder al valor de los atributos:    
            - this representa el objeto actual 
                - En la mayoría de los casos general puede obviase esta cualificación: aquí this.x es lo mismo que .x
            - Para el vector v, podemos acceder a su componente x como 
                - v.X (como propiedad, definada arriba)
                - v._x directamente porque v es de la misma clase Coor: dentro de la clase se puede acceder a los atributos
                  de todos los objetos de esa misma clase.
        */
        public void Translate(Coor v){
            this._x += v._x; // mezclando formas de acceso a modo de ejemplo 
            _y = _y + v.Y;  
        }

        // suma de coordenadas (como si fuesen vectores)
        // El resultado se devuelve en una nueva coordenada
        public static Coor operator+(Coor c1, Coor c2){
            return new Coor(c1._x+c2._x ,c1._y+c2._y);
        }


        // método para escrbir una coordenada en pantalla en formato "(3,4)"
        //public void EscribeCoor(){
        //    Console.Write($"({_x},{_y})");
        //}


        // "sobreescritura" del método ToString para poder escribir coordenadas directamente con Console.Write(c);
        // este método construye la cadena que queremos imprimir, que será el valor de retorno
        public override string ToString(){
            return $"({_x},{_y})";
        }


        // método para leer coordenada de teclado en el formato "(23,34)", con posibles blancos entre medias
        // Este método lee sobre el propio objeto (this)
        public void LeeCoor(){
            string s = Console.ReadLine();
            ParseCoor(s,out _x, out _y);
        }

        // método auxiliar, privado, para parsear la cadena y obtener la coordenada
        private void ParseCoor(string s, out int vx, out int vy){
            int ini,fin;
            (ini,fin) = (s.IndexOf("("),s.IndexOf(")"));
            s = s.Substring(ini+1,fin-ini-2);
            string [] nums = s.Split(",",StringSplitOptions.RemoveEmptyEntries);
            vx = int.Parse(nums[0]);
            vy = int.Parse(nums[1]);
        }


        // Es más habitual y práctico leer un string y parsearlo para crear una nueva coordenada y devolverla
        // Entonces ya no operamos sobre this!!
        // Método estático (miembro de la clase) para parsear y crear nuevas coordenadas
        // Funciona como el int.Parse(), double.Parse(), etc: Coor.Parse()
        public static Coor Parse(string s){
            int ini,fin; // buscamos índices de "(" y de ")"
            (ini,fin) = (s.IndexOf("("),s.IndexOf(")"));

            // nos quedamos con la subcadena entre medias de ambos
            s = s.Substring(ini+1,fin-ini-1);

            // dividimos con la ","
            string [] nums = s.Split(",",StringSplitOptions.RemoveEmptyEntries);

            // parseamos los dos enteros y construimos coordenada
            return new Coor(int.Parse(nums[0]), int.Parse(nums[1]));


        }



    }


}