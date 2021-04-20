using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesPE
{
    //Jay'llen
    //IGME106
    //Deals with all code about position of circles and points
    interface IPosition
    {
        // Properties
        double X { get; set; }
        // X–axis coordinate
        double Y { get; set; }
        // Y-axis coordinate    
        // Methods    
        // Distance to this coordinate from another coordinate
        double DistanceTo(IPosition position);
        // Moves the object to a new coordinate   
        // Example: MoveTo(500, 200); places the object at coordinate (500, 200)
        void MoveTo(double x, double y);
        // Increases or decreases the X and/or Y coordinate    
        // Example: MoveBy(-5, 6); would move 5 units negatively on X-axis     
        //   and 6 units positively on Y-axis.
        void MoveBy(double xOffset, double yOffset);
    }
}
