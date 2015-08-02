using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Kursach
{
    class myVertex
    {
        private double x;  
        public double X
        {
            get { return x;}
        }
        
        
        private double y;
        public double Y
        {
            get { return y; }
        }


        public myVertex(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public myVertex(myVertex vertex)
        {
            this.x = vertex.x;
            this.y = vertex.y;
        }
       
        
        public bool Equals(myVertex point)
        {
            return (Math.Abs(x - point.x) < 0.001 && Math.Abs(y - point.y) < 0.001);
        }
        public void Paint(Graphics graphics, Pen pen)
        {
            graphics.DrawEllipse(pen, (int)x - 2, (int)y - 2, 4, 4);
        }
        public Point ToPoint() 
        {
            return new Point((int)x, (int)y);
        }


        public double DistanceTo(myVertex vertex)
        {
            return Math.Sqrt((vertex.x - x) * (vertex.x - x) + (vertex.y - y) * (vertex.y - y));
        }
        public double CountAngleBetween(myVertex first, myVertex third) 
        {
            return (new myVector(first, this).getAngleTo(new myVector(this, third))) *180/Math.PI;
        }
    }
}
