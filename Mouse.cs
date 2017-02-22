using System.Runtime.InteropServices;

namespace MHP3rdController
{
    static class Mouse
    {
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public Point(System.Drawing.Point pt) : this(pt.X, pt.Y) { }

            public static implicit operator System.Drawing.Point(Point p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator Point(System.Drawing.Point p)
            {
                return new Point(p.X, p.Y);
            }

            public static Point operator + (Point p1, Point p2)
            {
                return new Point(p1.X + p2.X, p1.Y + p2.Y);
            }
            
            public static Point operator - (Point p1, Point p2)
            {
                return new Point(p1.X - p2.X, p1.Y - p2.Y);
            }
        }
        
        public static Point Position
        {
            get
            {
                Point pos;
                GetCursorPos(out pos);
                return pos;
            }
            set
            {
                SetCursorPos(value.X, value.Y);
            }
        }
    }
}
