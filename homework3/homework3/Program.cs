using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework3
{
    public abstract class Shape
    {
        public abstract double Area
        {
            get;           
        }
    }
    public class Rectangle:Shape
    {
        private double area;
        private double width;
        private double height;
        public Rectangle(double width,double height)
        {
            this.width = width;
            this.height = height;
            this.area = width * height;
        }
        public override double Area
        {
            get
            {
                return area;
            }
        }
    }
    class Square:Shape
    {
        private double side;
        private double area;
        public Square(double side)
        {
            this.side = side;
            this.area = side * side;
        }
        public override double Area
        {
            get
            {
                return area;
            }
        }
    }
    class Round:Shape
    {
        private double radius;
        private double area;
        public Round(double radius)
        {
            this.radius = radius;
            this.area = radius * radius*3.14;
        }
        public override double Area
        {
            get
            {
                return area;
            }
        }
    }
    class Triangle:Shape
    {
        private double side1;
        private double side2;
        private double side3;
        private double area;
        public Triangle(double side1,double side2,double side3)
        {
            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
            double p = (side1 + side2 + side3) / 2;
            double ss = p * (p - side1) * (p - side2) * (p - side3);
            this.area = System.Math.Sqrt(ss);
        }
        public override double Area
        {
            get
            {
                return area;
            }
        }
    }
    class Factory
    {
        public static Shape factoryMethod(int index,double num1,double num2 = 0.0,double num3 = 0.0)
        {
            Shape shape = null;
            switch(index)
            {
                case 0:shape = new Rectangle(num1, num2);break;
                case 1:shape = new Square(num1);break;
                case 2:shape = new Round(num1);break;
                case 3:shape = new Triangle(num1, num2, num3);break;
            }
            return shape;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Shape shape1 = Factory.factoryMethod(0, 3.5, 2.0);
            Shape shape2 = Factory.factoryMethod(1, 5.0);
            Shape shape3 = Factory.factoryMethod(2, 2.0);
            Shape shape4 = Factory.factoryMethod(3, 3.0, 4.0,5.0);
            Console.WriteLine($"所创长方形面积为:{shape1.Area}");
            Console.WriteLine($"所创正方形面积为:{shape2.Area}");
            Console.WriteLine($"所创圆形面积为:{shape3.Area}");
            Console.WriteLine($"所创三角形面积为:{shape4.Area}");
        }
    }
}
