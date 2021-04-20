using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    //Jay'llen H
    //IGME 106
    //The list that's linked
    class LinkedList <T>
    {
        private int count; private CustomLinkedNode<T> head, tail; //fields
        private int GetCount { get { return count; } } //getter
        public LinkedList()
        {
            head = null; tail = null; count = 0;
        }

        public void Add(T item) //adds to list and sets the head and tail values
        {
            if (count == 0)
            {
                head = new CustomLinkedNode<T>(item); tail = head;
            }
            else
            {
                tail.Next = new CustomLinkedNode<T>(item); tail = tail.Next;
            }

              count++; //add to count
        }

        public void Clear()
        {
            head = null; tail = null; count = 0;
        }
        public T GetDataIndex(int index) //retrives data at certain index
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Invalid index !");

            CustomLinkedNode<T> node = head;
            for (int i = 0; i < index; i++)  { node = node.Next; } //loop
                    
            return node.Data;
        }
        //removing at an index
        public void Remove(int index)
        {
            CustomLinkedNode<T> current = head;
     
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("\nError: Invalid index!");

            if (index == 0)//remove head
            {
                Console.WriteLine("Removed " + head.Data);
                head = head.Next;
                if (count == 1)
                    tail = null;
            }
            else if(index == count-1)//remove tail
            {
                CustomLinkedNode<T> node = head;
                for (int i = 0; i < index; i++) { node = node.Next; }
                tail = node;  
                tail.Next = null;
                Console.WriteLine("Removed " + tail.Data);
            }
            else
            {
                Console.WriteLine("Removed " + current.Next.Data);
                current.Next = current.Next.Next;
            }

            count--;
        }
    }
}
