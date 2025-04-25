using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _22133044_TranThiKimPhuong.Shapes
{
    class cCircle : Shape
    {
        private Point p1, p2, cLocation, x, y, p1R, p2R;
        private int cDiameter;
        private Color cLColor, cFColor;
        private float cShapeWidth;
        private bool fillShape, isDrawn, bSolid, bDash, bDashDot, bDashDotDot, bDot;
        public GraphicsPath Path { get; set; }

        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
        public Color CLColor { get => cLColor; set => cLColor = value; }
        public Color CFColor { get => cFColor; set => cFColor = value; }
        public float CShapeWidth { get => cShapeWidth; set => cShapeWidth = value; }
        public bool FillShape { get => fillShape; set => fillShape = value; }
        public bool BSolid { get => bSolid; set => bSolid = value; }
        public bool BDash { get => bDash; set => bDash = value; }
        public bool BDashDot { get => bDashDot; set => bDashDot = value; }
        public bool BDashDotDot { get => bDashDotDot; set => bDashDotDot = value; }
        public bool BDot { get => bDot; set => bDot = value; }
        public bool IsDrawn { get => isDrawn; set => isDrawn = value; }
        public Point CLocation { get => cLocation; set => cLocation = value; }
        public int CDiameter { get => cDiameter; set => cDiameter = value; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }

        private void SetCircle()
        {
            cDiameter = Math.Min(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
            cLocation = new Point(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y)
            );
        }

        private Pen CreatePen()
        {
            var pen = new Pen(cLColor, cShapeWidth);
            if (bDash) pen.DashStyle = DashStyle.Dash;
            else if (bDashDot) pen.DashStyle = DashStyle.DashDot;
            else if (bDashDotDot) pen.DashStyle = DashStyle.DashDotDot;
            else if (bDot) pen.DashStyle = DashStyle.Dot;
            else if (bSolid) pen.DashStyle = DashStyle.Solid;
            return pen;
        }

        public override void Draw(Graphics gp)
        {
            if (!isDrawn) SetCircle();

            if (fillShape)
                gp.FillEllipse(new SolidBrush(cFColor), cLocation.X, cLocation.Y, cDiameter, cDiameter);

            using (var pen = CreatePen())
                gp.DrawEllipse(pen, cLocation.X, cLocation.Y, cDiameter, cDiameter);
        }

        public override bool IsHit(Point e)
        {
            P1R = cLocation;
            P2R = new Point(cLocation.X + cDiameter, cLocation.Y + cDiameter);
            X = e;

            Path = new GraphicsPath();
            Path.AddEllipse(cLocation.X, cLocation.Y, cDiameter, cDiameter);

            using (var pen = new Pen(cLColor, cShapeWidth + 2))
                return fillShape ? Path.IsVisible(e) : Path.IsOutlineVisible(e, pen);
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;
            cLocation.Offset(dx, dy);
            p1R.Offset(dx, dy);
            p2R.Offset(dx, dy);
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
            if (e.X > cLocation.X && e.Y > cLocation.Y)
            {
                cDiameter = Math.Min(Math.Abs(cLocation.X - e.X), Math.Abs(cLocation.Y - e.Y));
                P2R = new Point(cLocation.X + cDiameter, cLocation.Y + cDiameter);
            }
        }
    }
}
