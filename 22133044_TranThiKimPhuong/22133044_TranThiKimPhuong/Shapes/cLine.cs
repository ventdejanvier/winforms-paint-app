using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace _22133044_TranThiKimPhuong.Shapes
{
    class cLine : Shape
    {
        private Point p1, p2, p1R, p2R, x, y;
        private int lWidth;
        private Color lColor;
        private int resizePoint = -1;
        private DashStyle dashStyle = DashStyle.Solid;

        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }
        public int LWidth { get => lWidth; set => lWidth = value; }
        public Color LColor { get => lColor; set => lColor = value; }

        public bool BSolid { set { if (value) dashStyle = DashStyle.Solid; } }
        public bool BDash { set { if (value) dashStyle = DashStyle.Dash; } }
        public bool BDashDot { set { if (value) dashStyle = DashStyle.DashDot; } }
        public bool BDashDotDot { set { if (value) dashStyle = DashStyle.DashDotDot; } }
        public bool BDot { set { if (value) dashStyle = DashStyle.Dot; } }

        public GraphicsPath Path { get; set; }

        public override void Draw(Graphics gp)
        {
            using (var pen = new Pen(lColor, lWidth) { DashStyle = dashStyle })
            {
                gp.DrawLine(pen, p1, p2);
            }
        }

        public override bool IsHit(Point e)
        {
            X = e;
            Path = new GraphicsPath();
            Path.AddLine(p1, p2);
            using (var pen = new Pen(lColor, lWidth + 2))
            {
                return Path.IsOutlineVisible(e, pen);
            }
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;

            p1.Offset(dx, dy);
            p2.Offset(dx, dy);
            p1R.Offset(dx, dy);
            p2R.Offset(dx, dy);

            X = Y;
        }

        public override void DrawSelectArea(Graphics gp)
        {
            p1R = new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            p2R = new Point(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y));

            using (var brush = new SolidBrush(Color.BlueViolet))
            {
                int size = 14 + lWidth;
                gp.FillEllipse(brush, p1.X - 7 - lWidth / 2, p1.Y - 7 - lWidth / 2, size, size);
                gp.FillEllipse(brush, p2.X - 7 - lWidth / 2, p2.Y - 7 - lWidth / 2, size, size);
            }
        }

        public override bool CanResize(Point e)
        {
            if (IsNearPoint(e, p2)) { resizePoint = 2; return true; }
            if (IsNearPoint(e, p1)) { resizePoint = 1; return true; }

            resizePoint = -1;
            return false;
        }

        private bool IsNearPoint(Point a, Point b)
            => Math.Abs(a.X - b.X) <= 5 && Math.Abs(a.Y - b.Y) <= 5;

        public override void Resize(Point e)
        {
            if (resizePoint == 1) p1 = e;
            else if (resizePoint == 2) p2 = e;
        }
    }
}
