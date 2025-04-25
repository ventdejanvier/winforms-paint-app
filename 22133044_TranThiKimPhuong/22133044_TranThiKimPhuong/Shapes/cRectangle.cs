using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _22133044_TranThiKimPhuong.Shapes
{
    class cRectangle : Shape
    {
        private Point p1, p2, rLocation, x, y, p1R, p2R;
        private int rWidth, rHeight, rShapeWidth;
        private Color rLColor, rFColor;
        private bool fillShape, isDrawn = false;
        private DashStyle dashStyle = DashStyle.Solid;

        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
        public Color RLColor { get => rLColor; set => rLColor = value; }
        public Color RFColor { get => rFColor; set => rFColor = value; }
        public int RShapeWidth { get => rShapeWidth; set => rShapeWidth = value; }
        public bool FillShape { get => fillShape; set => fillShape = value; }
        public bool IsDrawn { get => isDrawn; set => isDrawn = value; }
        public Point RLocation { get => rLocation; set => rLocation = value; }
        public int RWidth { get => rWidth; set => rWidth = value; }
        public int RHeight { get => rHeight; set => rHeight = value; }
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

        public void SetRec()
        {
            int left = Math.Min(p1.X, p2.X);
            int top = Math.Min(p1.Y, p2.Y);
            rWidth = Math.Abs(p1.X - p2.X);
            rHeight = Math.Abs(p1.Y - p2.Y);
            rLocation = new Point(left, top);
        }

        public override void Draw(Graphics gp)
        {
            if (!isDrawn)
                SetRec();

            if (fillShape)
            {
                using (var brush = new SolidBrush(rFColor))
                    gp.FillRectangle(brush, rLocation.X, rLocation.Y, rWidth, rHeight);
            }

            using (var pen = new Pen(rLColor, rShapeWidth) { DashStyle = dashStyle })
                gp.DrawRectangle(pen, rLocation.X, rLocation.Y, rWidth, rHeight);
        }

        public override bool IsHit(Point e)
        {
            X = e;
            P1R = new Point(rLocation.X - rShapeWidth / 2, rLocation.Y - rShapeWidth / 2);
            P2R = new Point(rLocation.X + rWidth + rShapeWidth / 2, rLocation.Y + rHeight + rShapeWidth / 2);

            Path = new GraphicsPath();
            Path.AddRectangle(new Rectangle(rLocation.X, rLocation.Y, rWidth, rHeight));

            if (!fillShape)
            {
                using (var pen = new Pen(rLColor, rShapeWidth + 2))
                    return Path.IsOutlineVisible(e, pen);
            }

            return Path.IsVisible(e);
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;
            rLocation.Offset(dx, dy);
            p1 = rLocation;
            P1R.Offset(dx, dy);
            P2R.Offset(dx, dy);
            X = Y;
        }

        public override void DrawSelectArea(Graphics gp)
        {
            using (var pen = new Pen(Color.Blue, 2) { DashStyle = DashStyle.Dash })
                gp.DrawRectangle(pen, P1R.X, P1R.Y, P2R.X - P1R.X, P2R.Y - P1R.Y);
        }

        public override bool CanResize(Point e)
        {
            return Math.Abs(e.X - P2R.X) <= 5 && Math.Abs(e.Y - P2R.Y) <= 5;
        }

        public override void Resize(Point e)
        {
            if (e.X > rLocation.X && e.Y > rLocation.Y)
            {
                p2 = e;
                SetRec();
                P2R = new Point(rLocation.X + rWidth + rShapeWidth / 2,
                                rLocation.Y + rHeight + rShapeWidth / 2);
            }
        }
    }
}
