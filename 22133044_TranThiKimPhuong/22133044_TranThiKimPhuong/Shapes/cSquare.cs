using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace _22133044_TranThiKimPhuong.Shapes
{
    class cSquare : Shape
    {
        private Point p1, p2, x, y, p1R, p2R, sLocation;
        private int sWidth, sShapeWidth;
        private Color sLColor, sFColor;
        private bool fillShape, isDrawn;
        private bool bSolid, bDash, bDashDot, bDashDotDot, bDot;

        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Point SLocation { get => sLocation; set => sLocation = value; }
        public int SWidth { get => sWidth; set => sWidth = value; }
        public int SShapeWidth { get => sShapeWidth; set => sShapeWidth = value; }
        public Color SLColor { get => sLColor; set => sLColor = value; }
        public Color SFColor { get => sFColor; set => sFColor = value; }
        public bool FillShape { get => fillShape; set => fillShape = value; }
        public bool IsDrawn { get => isDrawn; set => isDrawn = value; }
        public bool BSolid { get => bSolid; set => bSolid = value; }
        public bool BDash { get => bDash; set => bDash = value; }
        public bool BDashDot { get => bDashDot; set => bDashDot = value; }
        public bool BDashDotDot { get => bDashDotDot; set => bDashDotDot = value; }
        public bool BDot { get => bDot; set => bDot = value; }
        public GraphicsPath Path { get; set; }

        public void SetSquare()
        {
            sWidth = Math.Min(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
            sLocation = new Point(p1.X, p1.Y);

            if (p1.X > p2.X) sLocation.X = p1.X - sWidth;
            if (p1.Y > p2.Y) sLocation.Y = p1.Y - sWidth;
        }

        public override void Draw(Graphics gp)
        {
            if (!isDrawn) SetSquare();

            if (fillShape)
                gp.FillRectangle(new SolidBrush(sFColor), sLocation.X, sLocation.Y, sWidth, sWidth);

            using var p = new Pen(sLColor, sShapeWidth)
            {
                DashStyle = GetDashStyle()
            };

            gp.DrawRectangle(p, sLocation.X, sLocation.Y, sWidth, sWidth);
        }

        private DashStyle GetDashStyle()
        {
            if (bDash) return DashStyle.Dash;
            if (bDashDot) return DashStyle.DashDot;
            if (bDashDotDot) return DashStyle.DashDotDot;
            if (bDot) return DashStyle.Dot;
            return DashStyle.Solid;
        }

        public override bool IsHit(Point e)
        {
            X = e;
            P1R = new Point(sLocation.X - sShapeWidth / 2, sLocation.Y - sShapeWidth / 2);
            P2R = new Point(sLocation.X + sWidth + sShapeWidth / 2, sLocation.Y + sWidth + sShapeWidth / 2);

            Path = new GraphicsPath();
            Path.AddRectangle(new Rectangle(sLocation.X, sLocation.Y, sWidth, sWidth));

            using var pen = new Pen(sLColor, sShapeWidth + 2);
            return fillShape ? Path.IsVisible(e) : Path.IsOutlineVisible(e, pen);
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;

            sLocation.Offset(dx, dy);
            p1 = sLocation;
            P1R.Offset(dx, dy);
            P2R.Offset(dx, dy);
            X = Y;
        }

        public override void DrawSelectArea(Graphics gp)
        {
            using var p = new Pen(Color.Blue, 2) { DashStyle = DashStyle.Dash };
            gp.DrawRectangle(p, P1R.X, P1R.Y, P2R.X - P1R.X, P2R.Y - P1R.Y);
        }

        public override bool CanResize(Point e) =>
            Math.Abs(e.X - P2R.X) <= 5 && Math.Abs(e.Y - P2R.Y) <= 5;

        public override void Resize(Point e)
        {
            if (e.X > sLocation.X && e.Y > sLocation.Y)
            {
                p2 = e;
                SetSquare();
                P2R = new Point(sLocation.X + sWidth + sShapeWidth / 2, sLocation.Y + sWidth + sShapeWidth / 2);
            }
        }
    }
}
