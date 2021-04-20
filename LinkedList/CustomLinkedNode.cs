using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class CustomLinkedNode<T>
    {
        //Jayllen H
        //IGME106
        //Custom Node
        private T data, end; private CustomLinkedNode<T> next;//fields
        //the getters and setters
        public T Data { get { return data; } set { data = value; } }
        public T GetEnd { get { return end; } set { end = value; } }
        public CustomLinkedNode<T> Next { get { return next; } set { next = value; } }

        public CustomLinkedNode(T theData) //constructor
        {
            data = theData;
            next = null;
        }
      
    }
}
