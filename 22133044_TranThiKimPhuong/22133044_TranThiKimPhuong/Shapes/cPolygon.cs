using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _22133044_TranThiKimPhuong.Shapes
{
    class cPolygon : Shape
    {
        private Point x, y, p1R, p2R;
        private int pShapeWidth;
        private bool fillShape;
        private Color pLColor, pFColor;
        private DashStyle dashStyle = DashStyle.Solid;
        private List<Point> lPoint = new List<Point>();

        public List<Point> LPoint { get => lPoint; set => lPoint = value; }
        public int PShapeWidth { get => pShapeWidth; set => pShapeWidth = value; }
        public bool FillShape { get => fillShape; set => fillShape = value; }
        public Color PLColor { get => pLColor; set => pLColor = value; }
        public Color PFColor { get => pFColor; set => pFColor = value; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }
        public GraphicsPath Path { get; set; }

        public bool BSolid { set { if (value) dashStyle = DashStyle.Solid; } }
        public bool BDash { set { if (value) dashStyle = DashStyle.Dash; } }
        public bool BDashDot { set { if (value) dashStyle = DashStyle.DashDot; } }
        public bool BDashDotDot { set { if (value) dashStyle = DashStyle.DashDotDot; } }
        public bool BDot { set { if (value) dashStyle = DashStyle.Dot; } }

        public override void Draw(Graphics gp)
        {
            if (fillShape && lPoint.Count >= 3)
            {
                using (var brush = new SolidBrush(pFColor))
                    gp.FillPolygon(brush, lPoint.ToArray());
            }

            using (var pen = new Pen(pLColor, pShapeWidth) { DashStyle = dashStyle })
            {
                gp.DrawPolygon(pen, lPoint.ToArray());
            }
        }

        public override bool IsHit(Point e)
        {
            X = e;
            int minX = lPoint[0].X, minY = lPoint[0].Y;
            int maxX = lPoint[0].X, maxY = lPoint[0].Y;

            foreach (var pt in lPoint)
            {
                if (pt.X < minX) minX = pt.X;
                if (pt.Y < minY) minY = pt.Y;
                if (pt.X > maxX) maxX = pt.X;
                if (pt.Y > maxY) maxY = pt.Y;
            }

            p1R = new Point(minX, minY);
            p2R = new Point(maxX, maxY);

            Path = new GraphicsPath();
            Path.AddPolygon(lPoint.ToArray());

            if (!fillShape)
            {
                using (var pen = new Pen(pLColor, pShapeWidth + 2))
                    return Path.IsOutlineVisible(e, pen);
            }

            return Path.IsVisible(e);
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;

            for (int i = 0; i < lPoint.Count; i++)
                lPoint[i] = new Point(lPoint[i].X + dx, lPoint[i].Y + dy);

            p1R.Offset(dx, dy);
            p2R.Offset(dx, dy);
            X = Y;
        }

        public override void DrawSelectArea(Graphics gp)
        {
            using (var pen = new Pen(Color.Blue, 2) { DashStyle = DashStyle.Dash })
                gp.DrawRectangle(pen, p1R.X, p1R.Y, p2R.X - p1R.X, p2R.Y - p1R.Y);
        }

        public override bool CanResize(Point e)
        {
            return Math.Abs(e.X - p2R.X) <= 10 && Math.Abs(e.Y - p2R.Y) <= 10;
        }

        public override void Resize(Point e)
        {
        }
    }
}
