using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesPE
{
    //point class
    //Jay'llen
    //deals with anything that invloves the point
    class Point : IPosition
    {
        //fields
        private double xPosition;
        private double yPosition;
        private double theDistance;
        //constructor
        public Point(double x, double Y)
        {
            xPosition = x;
            yPosition = Y;
        }
        //getters and setters
        public double X { get { return xPosition; } set { xPosition = X; } }
        public double Y { get { return yPosition; } set { yPosition = Y; } }
        //distance formula
        //returns distance calculations
        public double DistanceTo(IPosition position)
        {
            double newX = (position.X - xPosition);
            double newY = (position.Y - yPosition);
            theDistance = Math.Sqrt((newX * newX) + (newY * newY));

            return theDistance;
        }
        //moves to new point
        //sets values
        public void MoveTo(double x, double y)
        {
            xPosition = x;
            yPosition = y;
        }
        //move by numbers read in
        //sets values
        public void MoveBy(double xOffset, double yOffset)
        {
            xPosition += xOffset;
            yPosition += yOffset;
        }
    }
}
