using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        //Jay'llen H.
        //IGME106
        //Linked List and nodes
        static void Main(string[] args)
        {
            LinkedList<string> theList = new LinkedList<string>(); //linked list

            string item; //field

            Console.WriteLine("Linked list is created. Please add 5 items to the Inventory!\n");

            for(int i=0; i<5;i++) //loop to get items
            {
                Console.Write("Enter an item: "); item = Console.ReadLine(); theList.Add(item);
            }

            Console.WriteLine("\nThe list has 5 items. The items are listed below\n");

            for(int i=0; i <5; i++) //loop to print items
            {
                Console.WriteLine("  - " + theList.GetDataIndex(i));
            }

            Console.WriteLine(); //blank line

            try { Console.WriteLine("\nRemoving item at invalid index: 99"); theList.Remove(99); } //try catch 
            catch (IndexOutOfRangeException) { Console.WriteLine("Error: Invalid Index!"); } //throws errors

            try { Console.WriteLine("\nRemoving item at index: 4"); theList.Remove(4); }
            catch (IndexOutOfRangeException) { Console.WriteLine("Error: Invalid Index!"); }

            try { Console.WriteLine("\nRemoving item at index: 0"); theList.Remove(0); }
            catch (IndexOutOfRangeException) { Console.WriteLine("Error: Invalid Index!"); }

            try { Console.WriteLine("\nRemoving item at index: 1"); theList.Remove(1); }
            catch (IndexOutOfRangeException) { Console.WriteLine("Error: Invalid Index!"); }

            Console.WriteLine("\nThe list has 2 items. The items are listed below"); 
            for (int i = 0; i < 2; i++) //loop to print items
            {
                Console.WriteLine("  - " + theList.GetDataIndex(i));
            }

            Console.ReadKey(); //keeps console open

        }
    }
}
