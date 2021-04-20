using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesPE
{
    //Jay'llen
    //IGME106
    //Calculates the area which is used in other classes
    interface IArea
    {
        // Properties    
        double Area { get; }
        double Perimeter { get; }
        // Methods    
        // Is a coordinate from another object within the area of this object?
        bool ContainsPosition(IPosition position);
        // Is this object’s area larger than the area of another object?
        bool IsLargerThan(IArea areaObject);
    }
}
