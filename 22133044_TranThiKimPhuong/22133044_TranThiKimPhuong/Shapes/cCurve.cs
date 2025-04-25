using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _22133044_TranThiKimPhuong.Shapes
{
    class cCurve : Shape
    {
        public int CurShapeWidth { get; set; }
        public Color CurColor { get; set; }
        public List<Point> LPoint { get; set; } = new();
        public bool BSolid { get; set; }
        public bool BDash { get; set; }
        public bool BDashDot { get; set; }
        public bool BDashDotDot { get; set; }
        public bool BDot { get; set; }
        public GraphicsPath Path { get; set; }
        public Point P1R { get; set; }
        public Point P2R { get; set; }
        public Point X { get; set; }
        public Point Y { get; set; }

        private int resizePoint = -1;

        private DashStyle GetDashStyle()
        {
            if (BSolid) return DashStyle.Solid;
            if (BDash) return DashStyle.Dash;
            if (BDashDot) return DashStyle.DashDot;
            if (BDashDotDot) return DashStyle.DashDotDot;
            if (BDot) return DashStyle.Dot;
            return DashStyle.Solid;
        }

        public override void Draw(Graphics gp)
        {
            using Pen p = new(CurColor, CurShapeWidth) { DashStyle = GetDashStyle() };
            gp.DrawCurve(p, LPoint.ToArray());
        }

        public override bool IsHit(Point e)
        {
            X = e;
            Path = new GraphicsPath();
            Path.AddCurve(LPoint.ToArray());
            using Pen pen = new(CurColor, CurShapeWidth + 2);
            return Path.IsOutlineVisible(e, pen);
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;

            for (int i = 0; i < LPoint.Count; i++)
                LPoint[i] = new Point(LPoint[i].X + dx, LPoint[i].Y + dy);

            P1R = new Point(P1R.X + dx, P1R.Y + dy);
            P2R = new Point(P2R.X + dx, P2R.Y + dy);
            X = Y;
        }

        public override void DrawSelectArea(Graphics gp)
        {
            using SolidBrush b = new(Color.BlueViolet);
            if (LPoint.Count == 0) return;

            P1R = P2R = LPoint[0];
            foreach (var pt in LPoint)
            {
                P1R = new Point(Math.Min(P1R.X, pt.X), Math.Min(P1R.Y, pt.Y));
                P2R = new Point(Math.Max(P2R.X, pt.X), Math.Max(P2R.Y, pt.Y));

                gp.FillEllipse(b,
                    pt.X - 7 - CurShapeWidth / 2,
                    pt.Y - 7 - CurShapeWidth / 2,
                    14 + CurShapeWidth,
                    14 + CurShapeWidth);
            }
        }

        public override bool CanResize(Point e)
        {
            for (int i = 0; i < LPoint.Count; i++)
            {
                var pt = LPoint[i];
                if (Math.Abs(pt.X - e.X) <= 5 && Math.Abs(pt.Y - e.Y) <= 5)
                {
                    resizePoint = i;
                    return true;
                }
            }
            resizePoint = -1;
            return false;
        }

        public override void Resize(Point e)
        {
            if (resizePoint != -1)
                LPoint[resizePoint] = e;
        }
    }
}
