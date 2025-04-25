using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using _22133044_TranThiKimPhuong.Shapes;

namespace _22133044_TranThiKimPhuong
{
    class cGroup : Shape
    {
        private bool SetRecArea = false;
        private List<Shape> shapes = new List<Shape>();
        private Point p1R, p2R, x, y;

        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }
        internal List<Shape> Shapes { get => shapes; set => shapes = value; }

        public override bool CanResize(Point e) => false;

        public override void Draw(Graphics gp)
        {
            foreach (var shape in Shapes)
                shape.Draw(gp);
        }

        public override void DrawSelectArea(Graphics gp)
        {
            foreach (var shape in Shapes)
                UpdateRectangleBounds(shape);

            using (Pen p = new Pen(Color.Blue, 2) { DashStyle = DashStyle.Dash })
            {
                gp.DrawRectangle(p, P1R.X, P1R.Y, P2R.X - P1R.X, P2R.Y - P1R.Y);
            }
        }

        private void UpdateRectangleBounds(Shape shape)
        {
            var shapeP1 = GetP1R(shape);
            var shapeP2 = GetP2R(shape);

            if (!SetRecArea)
            {
                P1R = shapeP1;
                P2R = shapeP2;
                SetRecArea = true;
                return;
            }

            P1R = new Point(Math.Min(P1R.X, shapeP1.X), Math.Min(P1R.Y, shapeP1.Y));
            P2R = new Point(Math.Max(P2R.X, shapeP2.X), Math.Max(P2R.Y, shapeP2.Y));
        }

        private Point GetP1R(Shape s) => (Point)s.GetType().GetProperty("P1R")?.GetValue(s);
        private Point GetP2R(Shape s) => (Point)s.GetType().GetProperty("P2R")?.GetValue(s);

        public override bool IsHit(Point e)
        {
            foreach (var shape in Shapes)
            {
                var prop = shape.GetType().GetProperty("X");
                if (prop != null) prop.SetValue(shape, e);
            }

            return Shapes.Any(shape => shape.IsHit(e));
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;
            P1R = new Point(P1R.X + dx, P1R.Y + dy);
            P2R = new Point(P2R.X + dx, P2R.Y + dy);

            if (X != Y)
            {
                foreach (var shape in Shapes)
                    shape.Move(e);

                SetRecArea = false;
                X = Y;
            }
        }

        public override void Resize(Point e) { }

        public GraphicsPath[] GraphicsPaths
        {
            get
            {
                var paths = new GraphicsPath[Shapes.Count];
                for (int i = 0; i < Shapes.Count; i++)
                {
                    var path = new GraphicsPath();
                    var shape = Shapes[i];
                    switch (shape)
                    {
                        case cLine line:
                            path.AddLine(line.P1, line.P2);
                            break;
                        case cEllipse ellipse:
                            path.AddEllipse(ellipse.ELocation.X, ellipse.ELocation.Y, ellipse.EWidth, ellipse.EHeight);
                            break;
                        case cCircle circle:
                            path.AddEllipse(circle.CLocation.X, circle.CLocation.Y, circle.CDiameter, circle.CDiameter);
                            break;
                        case cRectangle rect:
                            path.AddRectangle(new Rectangle(rect.RLocation.X, rect.RLocation.Y, rect.RWidth, rect.RHeight));
                            break;
                        case cSquare square:
                            path.AddRectangle(new Rectangle(square.SLocation.X, square.SLocation.Y, square.SWidth, square.SWidth));
                            break;
                        case cCurve curve:
                            path.AddCurve(curve.LPoint.ToArray());
                            break;
                        case cPolygon polygon:
                            path.AddPolygon(polygon.LPoint.ToArray());
                            break;
                        case cGroup group:
                            foreach (var subPath in group.GraphicsPaths)
                                path.AddPath(subPath, false);
                            break;
                    }
                    paths[i] = path;
                }
                return paths;
            }
        }
    }
}

