// Carmen Gómez Becerra
// El comedor de Tomelloso.

using System;

namespace Listas
{
    public class Lista
    {
        class Nodo
        {
            public int dato;
            public Nodo sig; // enlace al siguiente nodo
                             // constructoras
            public Nodo(int e) { dato = e; sig = null; }
            public Nodo(int e, Nodo n) { dato = e; sig = n; }
        }
        // atributos de la lista enlazada: referencia al primero y al último
        Nodo pri;

        // constructora de listas
        public Lista() { pri = null; }


        // version recursiva de SacaLista
        // public string SacaListaRec(){ }


        public bool EsVacia() { return pri == null; }

        public void InsertaPri(int x)
        {
            Nodo aux = new Nodo(x);
            aux.sig = pri;
            pri = aux;
        }

        public void EliminaPri()
        {
            if (pri == null) throw new Exception("EliminaPri");
            else pri = pri.sig;
        }

        public int DamePri()
        {
            if (pri == null) throw new Exception("DamePri");
            else return pri.dato;
        }

        public string SacaLista()
        {
            Nodo aux = pri;
            string sal = "";
            while (aux != null)
            {
                sal += aux.dato.ToString() + " ";
                aux = aux.sig;
            }
            return sal;
        }

    }
}
