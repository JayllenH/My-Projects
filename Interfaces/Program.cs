using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesPE
{
    class Program
    {
        //IGME106
        //Jay'llen
        //Interfaces
        //set hard code values and calls other classes for information and calculations
        static void Main(string[] args)
        {
            //create circle/points
            Point point1 = new Point(5, 7);
            Point point2 = new Point(10, 10);
            Circle circle1 = new Circle(10, 10, 3, 28.27, 18.85);
            Circle circle2 = new Circle(0, 0, 5, 78.54, 31.42);
            //point info
            Console.WriteLine("Point 1: x " + point1.X + " y " + point1.Y);
            Console.WriteLine("Point 2: x " + point2.X + " y " + point2.Y);
            //circle info
            Console.WriteLine("Circle 1: center x " + circle1.X + " center y " + circle1.Y + ", radius " + circle1.Radius
                + ", area " + circle1.Area + ", perimeter " + circle1.Perimeter);
            Console.WriteLine("Circle 2: center x " + circle2.X + " center y " + circle2.Y + ", radius " + circle2.Radius
                + ", area " + circle2.Area + ", perimeter " + circle2.Perimeter);

            //move point 2 and circle 2 using moveTo
            Console.WriteLine("\nMoving Point 2 to (2,2)");
            point2.MoveTo(2, 2);
            Console.WriteLine("Moving Circle 2 by (-1,-1)\n");
            circle2.MoveBy(-1, -1);

            //reprint
            Console.WriteLine("Point 1: x " + point1.X + " y " + point1.Y);
            Console.WriteLine("Point 2: x " + point2.X + " y " + point2.Y);

            Console.WriteLine("Circle 1: center x " + circle1.X + " center y " + circle1.Y + ", radius " + circle1.Radius
               + ", area " + circle1.Area + ", perimeter " + circle1.Perimeter);
            Console.WriteLine("Circle 2: center x " + circle2.X + " center y " + circle2.Y + ", radius " + circle2.Radius
                + ", area " + circle2.Area + ", perimeter " + circle2.Perimeter);

            //distance
            Console.WriteLine("\nDistance Between Point 1 and Point 2: {0:F5}", point1.DistanceTo(point2));
            Console.WriteLine("Distance Between Point 1 and Circle 1: {0:F5}", point1.DistanceTo(circle1));
            Console.WriteLine("Distance Between Point 1 and Circle 2: " + point1.DistanceTo(circle2));
            Console.WriteLine("Distance Between Point 2 and Circle 1: {0:F4}", point2.DistanceTo(circle1));
            Console.WriteLine("Distance Between Point 2 and Circle 2: {0:F5}" ,point2.DistanceTo(circle2));

            //area
            if (circle2.IsLargerThan(circle1))
                Console.WriteLine("\nCircle 2's area (" + circle2.Area + ") is larger than Circle 1's area (" + circle1.Area + ")");
            else
                Console.WriteLine("\nCircle 1's area (" + circle1.Area + ") is larger than Circle 2's area (" + circle2.Area + ")");

            //Contains

            if (circle1.ContainsPosition(point1) == true)
                Console.WriteLine("\nDoes circle 1 contain point 1? Yes");
            else
                Console.WriteLine("\nDoes circle 1 contain point 1? No");

            if (circle1.ContainsPosition(point2) == true)
                Console.WriteLine("Does circle 1 contain point 2? Yes");
            else
                Console.WriteLine("Does circle 1 contain point 2? No");

            if (circle2.ContainsPosition(point1) == true)
                Console.WriteLine("Does circle 2 contain point 1? Yes");
            else
                Console.WriteLine("Does circle 2 contain point 1? No");

            if (circle2.ContainsPosition(point2) == true)
                Console.WriteLine("Does circle 2 contain point 2? Yes");
            else
                Console.WriteLine("Does circle 2 contain point 2? No");

            Console.ReadKey();
        }
    }
}
