using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _22133044_TranThiKimPhuong.Shapes
{
    class cEllipse : Shape
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Point ELocation { get; set; }
        public Color ELColor { get; set; }
        public Color EFColor { get; set; }
        public float EShapeWidth { get; set; }
        public int EHeight { get; set; }
        public int EWidth { get; set; }
        public bool FillShape { get; set; }
        public bool IsDrawn { get; set; } = false;
        public GraphicsPath Path { get; set; }
        public Point P1R { get; set; }
        public Point P2R { get; set; }
        public Point X { get; set; }
        public Point Y { get; set; }

        private bool bSolid, bDash, bDashDot, bDashDotDot, bDot;

        public bool BSolid { get => bSolid; set => bSolid = value; }
        public bool BDash { get => bDash; set => bDash = value; }
        public bool BDashDot { get => bDashDot; set => bDashDot = value; }
        public bool BDashDotDot { get => bDashDotDot; set => bDashDotDot = value; }
        public bool BDot { get => bDot; set => bDot = value; }

        private DashStyle GetDashStyle()
        {
            if (BSolid) return DashStyle.Solid;
            if (BDash) return DashStyle.Dash;
            if (BDashDot) return DashStyle.DashDot;
            if (BDashDotDot) return DashStyle.DashDotDot;
            if (BDot) return DashStyle.Dot;
            return DashStyle.Solid;
        }

        private void SetEllipse()
        {
            EWidth = Math.Abs(P1.X - P2.X);
            EHeight = Math.Abs(P1.Y - P2.Y);
            ELocation = new Point(Math.Min(P1.X, P2.X), Math.Min(P1.Y, P2.Y));
        }

        public override void Draw(Graphics gp)
        {
            if (!IsDrawn)
                SetEllipse();

            if (FillShape)
                using (var brush = new SolidBrush(EFColor))
                    gp.FillEllipse(brush, ELocation.X, ELocation.Y, EWidth, EHeight);

            using var pen = new Pen(ELColor, EShapeWidth) { DashStyle = GetDashStyle() };
            gp.DrawEllipse(pen, ELocation.X, ELocation.Y, EWidth, EHeight);
        }

        public override bool IsHit(Point e)
        {
            X = e;
            P1R = ELocation;
            P2R = new Point(ELocation.X + EWidth, ELocation.Y + EHeight);
            Path = new GraphicsPath();
            Path.AddEllipse(ELocation.X, ELocation.Y, EWidth, EHeight);
            using var pen = new Pen(ELColor, EShapeWidth + 2);
            return FillShape ? Path.IsVisible(e) : Path.IsOutlineVisible(e, pen);
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;
            ELocation = new Point(ELocation.X + dx, ELocation.Y + dy);
            P1R = new Point(P1R.X + dx, P1R.Y + dy);
            P2R = new Point(P2R.X + dx, P2R.Y + dy);
            X = Y;
        }

        public override void DrawSelectArea(Graphics gp)
        {
            using var pen = new Pen(Color.Blue, 2) { DashStyle = DashStyle.Dash };
            gp.DrawRectangle(pen, P1R.X, P1R.Y, P2R.X - P1R.X, P2R.Y - P1R.Y);
        }

        public override bool CanResize(Point e)
        {
            return Math.Abs(e.X - P2R.X) <= 5 && Math.Abs(e.Y - P2R.Y) <= 5;
        }

        public override void Resize(Point e)
        {
            if (e.X > ELocation.X && e.Y > ELocation.Y)
            {
                P1 = ELocation;
                P2 = e;
                SetEllipse();
                P2R = new Point(ELocation.X + EWidth, ELocation.Y + EHeight);
            }
        }
    }
}
