using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesPE
{
    //Jay'llen
    //Deals with anything that invloves the circle
    class Circle : IPosition, IArea
    {
        //fields
        private double xPosition;
        private double cArea;
        private double cPer;
        private double yPosition;
        private double theDistance;
        private double cRadii;
        
        //parameterized constructor
        public Circle(double x, double y, double radius, double area, double perimeter)
        {
            xPosition = x;
            yPosition = y;
            cArea = area;
            cPer = perimeter;
            cRadii = radius;
        }
        //getters and setters
        public double X { get { return xPosition; } set { xPosition = X; } }
        public double Y { get { return yPosition; } set { yPosition = Y; } }
        public double Area { get { return cArea; } }
        public double Perimeter { get { return cPer; } }
        public double Radius { get { return cRadii; } }

        //moves to new position
        //sets new values
        public void MoveTo(double x, double y)
        {
            xPosition = x;
            yPosition = y;
        }
        //moves by the offsets read in
        //sets new values
        public void MoveBy(double xOffset, double yOffset)
        {
            xPosition += xOffset;
            yPosition += yOffset;
        }
        //checks if point is with circle using distance foumula to compare with radius
        //either returns true or false
        public bool ContainsPosition(IPosition position)
        {
            double newX = (position.X - xPosition);
            double newY = (position.Y - yPosition);
            theDistance = Math.Sqrt((newX * newX) + (newY * newY));

            if (theDistance < cRadii)
                return true;
            else
                return false;
        }
        //comparing the two circle's area
        //returns true or false
        public bool IsLargerThan(IArea areaObject)
        {
            if (areaObject.Area < cArea)
                return true;
            else
                return false;
        }
        //distance calculation
        //returns the distance that was calculated
        public double DistanceTo(IPosition position)
        {
            double newX = (position.X - xPosition);
            double newY = (position.Y - yPosition);
            theDistance = Math.Sqrt((newX * newX) + (newY * newY));
            return theDistance;
        }
    }
}
