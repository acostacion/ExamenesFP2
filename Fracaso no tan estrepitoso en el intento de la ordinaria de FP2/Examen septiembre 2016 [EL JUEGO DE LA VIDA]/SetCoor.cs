// Ariadna Alicia Ruiz Castillo
// Nieves Alonso Gilsanz
namespace Examen_septiembre_2016
{
    class SetCoor
    {
        Coor[] coors; // array con coordenadas
        int oc; // numero de eltos ocupados del array
        public SetCoor(int tam = 400)
        {
            coors = new Coor[tam];
            oc = 0;
        }

        // método privado. Busca elto en el array y devuelve su posición
        // -1 si no está
        private int SearchElem(Coor c)
        {
            int i = 0;
            while (i < oc && coors[i] != c) i++;
            if (i < oc) return i;
            else return -1;
        }


        // añadir elto al cto. si está no hace nada
        // si no está, añade en ultima posición
        // si no cabe -> exception
        // devuelve true si lo ha añadido, false si ya estaba
        public bool Add(Coor c)
        {
            int i = SearchElem(c);
            if (i < 0)
            { // no esta
                if (oc >= coors.Length) throw new Exception("Error SetCoor.Insert: overfull set");
                else
                {  // colocamos en última posición 
                    coors[oc] = c;
                    oc++;
                    return true;
                }
            }
            else return false;
        }

        // eliminación de elto en el cto
        // si está, lo reemplaza por el último
        // devuelve true si el elto estaba, false eoc
        public bool Remove(Coor c)
        {
            int i = SearchElem(c);
            if (i < 0) return false;
            else
            {
                coors[i] = coors[oc - 1];
                oc--;
                return true;
            }
        }


        // extrae un elto cualquiera del cto (no hay orden)
        public Coor PopElem()
        {
            if (oc == 0) throw new Exception("Error SetCoor.PopElem: empty set");
            else
            {
                oc--;
                return coors[oc];
            }
        }

        //Método auxiliar para coger una de las coordenadas del conjunto
        public Coor GetElement(int i)
        {
            if (i > coors.Length)
            {
                throw new Exception("Error SetCoor.GetElem: element out of bounds");
            }
            else
            {
                return coors[i];
            }
        }

        public bool Empty() { return oc == 0; }
        public int Size() { return oc; }

        public bool IsElementOf(Coor c)
        {
            return SearchElem(c) >= 0;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < oc; i++)
                s = s + coors[i].ToString() + "  ";
            return s;
        }

    }
}

