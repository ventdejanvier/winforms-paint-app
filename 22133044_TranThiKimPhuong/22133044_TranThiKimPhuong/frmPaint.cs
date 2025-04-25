using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _22133044_TranThiKimPhuong.Shapes;

namespace _22133044_TranThiKimPhuong
{
    public partial class frmPaint : Form
    {
        List<cLine> lLine = new List<cLine>();
        List<cRectangle> lRectangle = new List<cRectangle>();
        List<cCircle> lCircle = new List<cCircle>();
        List<cEllipse> lEllipse = new List<cEllipse>();
        List<cPolygon> lPolygon = new List<cPolygon>();
        List<cSquare> lSquare = new List<cSquare>();
        List<cCurve> lCurve = new List<cCurve>();
        List<Point> lPoint = new List<Point>();

        List<Shape> lDrawn = new List<Shape>();
        List<int> lSelected = new List<int>();

        int sResize;
        Graphics gp;
        Pen myPen;
        Color myColor, myFColor;
        bool isClicked = false, isResizing = false, isMoving = false, multiSelect = false;
        bool bCircle = false, bCurve = false, bElipse = false, bLine = true, bPolygon = false, bRectangle = false, bSquare = false, bSelect = false;
        bool isFill = false;
        bool bSolid = true, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        private void btnGroup_Click(object sender, EventArgs e)
        {
            if (lSelected.Count > 1)
            {
                cGroup group = new cGroup();
                for (int i = 0; i < lSelected.Count; i++)
                {
                    group.Shapes.Add(lDrawn[lSelected[i]]);
                }
                List<int> temp = lSelected;
                temp.Sort();
                for (int i = temp.Count - 1; i >= 0; i--)
                {
                    lDrawn.RemoveAt(lSelected[i]);
                }
                lSelected.Clear();
                lDrawn.Add(group);
                lSelected.Add(lDrawn.Count - 1);
                panel1.Invalidate();
            }
        }

        private void btnUngroup_Click(object sender, EventArgs e)
        {
            int stopPos = lSelected.Count;
            for (int i = 0; i < stopPos; i++)
            {
                if (lDrawn[lSelected[i]] is cGroup)
                {
                    foreach (Shape shape in ((cGroup)lDrawn[lSelected[i]]).Shapes)
                    {
                        lDrawn.Add(shape);
                        lSelected.Add(lDrawn.Count - 1);
                    }
                    lDrawn.RemoveAt(lSelected[i]);
                    lSelected.RemoveAt(i);
                    for (int j = 0; j < lSelected.Count; j++)
                    {
                        if (lSelected[j] > i)
                            lSelected[j]--;
                    }
                }
            }
            panel1.Invalidate();
        }

        private void btnFColor_Click(object sender, EventArgs e)
        {
            ColorDialog tempDialog = new ColorDialog();
            tempDialog.ShowDialog();
            btnFillColor.BackColor = tempDialog.Color;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            lSelected.Sort();
            lSelected.Reverse();

            foreach (int index in lSelected)
            {
                if (index >= 0 && index < lDrawn.Count)
                    lDrawn.RemoveAt(index);
            }

            lSelected.Clear();
            panel1.Invalidate();

            if (lDrawn.Count == 0)
            {
                btnSelect_Click(sender, e);
            }

        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            multiSelect = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                multiSelect = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            TurnOffBools();
            if (bSelect == false)
            {
                panel1.Cursor = Cursors.Default;
                bSelect = true;
                btnDelete.Enabled = true;
                btnGroup.Enabled = true;
                btnUngroup.Enabled = true;
                boxDash.Enabled = false;
                boxFill.Enabled = false;
                barWidth.Enabled = false;
                boxShape.Enabled = false;
                btnLineColor.Enabled = false;
                btnFillColor.Enabled = false;
                btnSelect.BackColor = Color.LightSteelBlue;
            }
            else
            {
                panel1.Cursor = Cursors.Cross;
                btnSelect.ForeColor = Color.Black;
                bSelect = false;
                boxDash.Enabled = true;
                boxFill.Enabled = true;
                btnDelete.Enabled = false;
                btnGroup.Enabled = false;
                btnUngroup.Enabled = false;
                barWidth.Enabled = true;
                boxShape.Enabled = true;
                btnLineColor.Enabled = true;
                if (boxFill.SelectedItem.ToString() == "Fill Shape")
                    btnFillColor.Enabled = true;
                else
                    btnFillColor.Enabled = false;
                switch (boxShape.SelectedItem.ToString())
                {
                    case "Circle":
                        bCircle = true;
                        break;
                    case "Curve":
                        bCurve = true;
                        break;
                    case "Elipse":
                        bElipse = true;
                        break;
                    case "Line":
                        bLine = true;
                        break;
                    case "Polygon":
                        bPolygon = true;
                        break;
                    case "Rectangle":
                        bRectangle = true;
                        break;
                    case "Square":
                        bSquare = true;
                        break;
                }
                lSelected.Clear();
                panel1.Invalidate();
                isClicked = false;
            }
        }

        public frmPaint()
        {
            InitializeComponent();
            boxShape.SelectedItem = "Line";
            boxFill.SelectedItem = "No Fill Shape";
            boxDash.SelectedItem = "Solid";

            myColor = btnLineColor.BackColor;
            myFColor = btnFillColor.BackColor;
            myPen = new Pen(myColor, barWidth.Value);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gp = panel1.CreateGraphics();
            gp.Clear(Color.White);
            lLine.Clear();
            lRectangle.Clear();
            lCircle.Clear();
            lEllipse.Clear();
            lPolygon.Clear();
            lSquare.Clear();
            lCurve.Clear();
            lDrawn.Clear();
            isClicked = false;
            bSelect = true;
            btnSelect_Click(sender, e);
        }

        private void btnLColor_Click(object sender, EventArgs e)
        {
            ColorDialog tempDialog = new ColorDialog();
            tempDialog.ShowDialog();
            btnLineColor.BackColor = tempDialog.Color;
        }

        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (bCircle == false && bCurve == false && bElipse == false && bLine == false
                && bPolygon == false && bRectangle == false && bSquare == false && bSelect == false)
                {
                    MessageBox.Show("Vui lòng chọn hình bạn muốn vẽ!");
                    return;
                }
                if (bLine == true)
                {
                    cLine curLine = new cLine();
                    curLine.P1 = e.Location;
                    curLine.P2 = e.Location;
                    curLine.LWidth = barWidth.Value;
                    curLine.LColor = btnLineColor.BackColor;
                    if (bDash == true)
                    {
                        curLine.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curLine.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curLine.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curLine.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curLine.BSolid = true;
                    }
                    lLine.Add(curLine);
                    isClicked = true;
                }
                if (bRectangle == true)
                {
                    cRectangle curRec = new cRectangle();
                    curRec.P1 = e.Location;
                    curRec.P2 = e.Location;
                    curRec.RShapeWidth = barWidth.Value;
                    curRec.RLColor = btnLineColor.BackColor;
                    curRec.RFColor = btnFillColor.BackColor;
                    curRec.FillShape = isFill;
                    if (bDash == true)
                    {
                        curRec.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curRec.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curRec.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curRec.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curRec.BSolid = true;
                    }
                    lRectangle.Add(curRec);
                    isClicked = true;
                }
                if (bCircle == true)
                {
                    cCircle curCir = new cCircle();
                    curCir.P1 = e.Location;
                    curCir.P2 = e.Location;
                    curCir.CShapeWidth = barWidth.Value;
                    curCir.CLColor = btnLineColor.BackColor;
                    curCir.CFColor = btnFillColor.BackColor;
                    curCir.FillShape = isFill;
                    if (bDash == true)
                    {
                        curCir.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curCir.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curCir.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curCir.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curCir.BSolid = true;
                    }
                    lCircle.Add(curCir);
                    isClicked = true;
                }
                if (bElipse == true)
                {
                    cEllipse curEllip = new cEllipse();
                    curEllip.P1 = e.Location;
                    curEllip.P2 = e.Location;
                    curEllip.EShapeWidth = barWidth.Value;
                    curEllip.ELColor = btnLineColor.BackColor;
                    curEllip.EFColor = btnFillColor.BackColor;
                    curEllip.FillShape = isFill;
                    if (bDash == true)
                    {
                        curEllip.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curEllip.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curEllip.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curEllip.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curEllip.BSolid = true;
                    }
                    lEllipse.Add(curEllip);
                    isClicked = true;
                }
                if (bSquare == true)
                {
                    cSquare curSquare = new cSquare();
                    curSquare.P1 = e.Location;
                    curSquare.P2 = e.Location;
                    curSquare.SShapeWidth = barWidth.Value;
                    curSquare.SLColor = btnLineColor.BackColor;
                    curSquare.SFColor = btnFillColor.BackColor;
                    curSquare.FillShape = isFill;
                    if (bDash == true)
                    {
                        curSquare.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curSquare.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curSquare.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curSquare.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curSquare.BSolid = true;
                    }
                    lSquare.Add(curSquare);
                    isClicked = true;
                }
                if (bPolygon == true)
                {
                    if (isClicked == true)
                    {
                        lPolygon[lPolygon.Count - 1].LPoint[lPolygon[lPolygon.Count - 1].LPoint.Count - 1] = e.Location;
                        lPolygon[lPolygon.Count - 1].LPoint.Add(e.Location);
                        panel1.Invalidate();
                        return;
                    }
                    cPolygon curPol = new cPolygon();
                    curPol.PShapeWidth = barWidth.Value;
                    curPol.PLColor = btnLineColor.BackColor;
                    curPol.PFColor = btnFillColor.BackColor;
                    curPol.FillShape = isFill;
                    curPol.LPoint.Add(e.Location);
                    curPol.LPoint.Add(e.Location);
                    if (bDash == true)
                    {
                        curPol.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curPol.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curPol.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curPol.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curPol.BSolid = true;
                    }
                    lPolygon.Add(curPol);
                    isClicked = true;
                }

                if (bCurve == true)
                {
                    if (isClicked == true)
                    {
                        lCurve[lCurve.Count - 1].LPoint[lCurve[lCurve.Count - 1].LPoint.Count - 1] = e.Location;
                        lCurve[lCurve.Count - 1].LPoint.Add(e.Location);
                        panel1.Invalidate();
                        return;
                    }
                    cCurve curCurve = new cCurve();
                    curCurve.CurShapeWidth = barWidth.Value;
                    curCurve.CurColor = btnLineColor.BackColor;
                    curCurve.LPoint.Add(e.Location);
                    curCurve.LPoint.Add(e.Location);
                    if (bDash == true)
                    {
                        curCurve.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curCurve.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curCurve.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curCurve.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curCurve.BSolid = true;
                    }
                    lCurve.Add(curCurve);
                    isClicked = true;
                }
                if (bSelect == true)
                {
                    for (int i = lDrawn.Count - 1; i >= 0; i--)
                    {
                        if (lDrawn[i].IsHit(e.Location))
                        {
                            if (multiSelect == false)
                            {
                                lSelected.Clear();
                            }

                            lSelected.Add(i);
                            for (int m = 0; i < lSelected.Count - 1; i++)
                            {
                                if (lSelected[m] == i)
                                {
                                    lSelected.Remove(lSelected.Last());
                                    break;
                                }
                            }
                            if (multiSelect == false)
                                isMoving = true;
                            panel1.Invalidate();
                            return;
                        }
                    }
                    lSelected.Clear();
                    panel1.Invalidate();
                    isMoving = false;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (bPolygon == true)
                {
                    if (isClicked == true)
                    {
                        lPolygon[lPolygon.Count - 1].LPoint[lPolygon[lPolygon.Count - 1].LPoint.Count - 1] = e.Location;
                        lDrawn.Add(lPolygon[lPolygon.Count - 1]);
                        lPolygon.Clear();
                        isClicked = false;
                    }
                }
                if (bCurve == true)
                {
                    if (isClicked == true)
                    {
                        lCurve[lCurve.Count - 1].LPoint[lCurve[lCurve.Count - 1].LPoint.Count - 1] = e.Location;
                        lDrawn.Add(lCurve[lCurve.Count - 1]);
                        lCurve.Clear();
                        isClicked = false;
                    }
                }
                if (bSelect == true)
                {
                    if (panel1.Cursor == Cursors.SizeAll)
                    {
                        lSelected.Clear();
                        lSelected.Add(sResize);
                        isResizing = true;
                        panel1.Invalidate();
                        return;
                    }
                }
            }
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (bLine == true)
            {
                if (isClicked == true)
                {
                    lLine[lLine.Count - 1].P2 = e.Location;
                    panel1.Invalidate();
                }
            }
            if (bRectangle == true)
            {
                if (isClicked == true)
                {
                    lRectangle[lRectangle.Count - 1].P2 = e.Location;
                    panel1.Invalidate();
                }
            }
            if (bCircle == true)
            {
                if (isClicked == true)
                {
                    lCircle[lCircle.Count - 1].P2 = e.Location;
                    panel1.Invalidate();
                }
            }
            if (bElipse == true)
            {
                if (isClicked == true)
                {
                    lEllipse[lEllipse.Count - 1].P2 = e.Location;
                    panel1.Invalidate();
                }
            }
            if (bSquare == true)
            {
                if (isClicked == true)
                {
                    lSquare[lSquare.Count - 1].P2 = e.Location;
                    panel1.Invalidate();
                }
            }
            if (bPolygon == true)
            {
                if (isClicked == true)
                {
                    lPolygon[lPolygon.Count - 1].LPoint[lPolygon[lPolygon.Count - 1].LPoint.Count - 1] = e.Location;
                    panel1.Invalidate();
                }
            }
            if (bCurve == true)
            {
                if (isClicked == true)
                {
                    lCurve[lCurve.Count - 1].LPoint[lCurve[lCurve.Count - 1].LPoint.Count - 1] = e.Location;
                    panel1.Invalidate();
                }
            }
            if (bSelect == true)
            {
                panel1.Cursor = Cursors.Arrow;
                if (isMoving == true)
                {
                    for (int i = 0; i < lSelected.Count; i++)
                        lDrawn[lSelected[i]].Move(e.Location);
                    panel1.Invalidate();
                }
                if (isResizing == true)
                {
                    lDrawn[lSelected[0]].Resize(e.Location);
                    panel1.Invalidate();
                }
                for (int i = 0; i < lSelected.Count; i++)
                {
                    if (lDrawn[lSelected[i]].CanResize(e.Location))
                    {
                        sResize = lSelected[i];
                        panel1.Cursor = Cursors.SizeAll;
                        return;
                    }
                }
                for (int i = 0; i < lDrawn.Count; i++)
                {
                    if (lDrawn[i].IsHit(e.Location))
                        panel1.Cursor = Cursors.Hand;
                }

            }
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (bLine == true)
            {
                if (isClicked == true)
                {
                    lLine[lLine.Count - 1].P2 = e.Location;
                    lDrawn.Add(lLine[lLine.Count - 1]);
                    lLine.Clear();
                    panel1.Invalidate();
                    isClicked = false;
                }
            }
            if (bRectangle == true)
            {
                if (isClicked == true)
                {
                    lRectangle[lRectangle.Count - 1].P2 = e.Location;
                    lRectangle[lRectangle.Count - 1].IsDrawn = true;
                    lDrawn.Add(lRectangle[lRectangle.Count - 1]);
                    lRectangle.Clear();
                    panel1.Invalidate();
                    isClicked = false;
                }
            }
            if (bCircle == true)
            {
                if (isClicked == true)
                {
                    lCircle[lCircle.Count - 1].P2 = e.Location;
                    lCircle[lCircle.Count - 1].IsDrawn = true;
                    lDrawn.Add(lCircle[lCircle.Count - 1]);
                    lCircle.Clear();
                    panel1.Invalidate();
                    isClicked = false;
                }
            }
            if (bElipse == true)
            {
                if (isClicked == true)
                {
                    lEllipse[lEllipse.Count - 1].P2 = e.Location;
                    lEllipse[lEllipse.Count - 1].IsDrawn = true;
                    lDrawn.Add(lEllipse[lEllipse.Count - 1]);
                    lEllipse.Clear();
                    panel1.Invalidate();
                    isClicked = false;
                }
            }
            if (bSquare == true)
            {
                if (isClicked == true)
                {
                    lSquare[lSquare.Count - 1].P2 = e.Location;
                    lSquare[lSquare.Count - 1].IsDrawn = true;
                    lDrawn.Add(lSquare[lSquare.Count - 1]);
                    lSquare.Clear();
                    panel1.Invalidate();
                    isClicked = false;
                }
            }
            if (bSelect == true)
            {
                if (isMoving == true)
                {
                    for (int i = 0; i < lSelected.Count; i++)
                        lDrawn[lSelected[i]].Move(e.Location);
                    panel1.Invalidate();
                    isMoving = false;
                }
                if (isResizing == true)
                {
                    lDrawn[lSelected[0]].Resize(e.Location);
                    panel1.Invalidate();
                    isResizing = false;
                }
            }
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            gp = e.Graphics;
            if (bSelect == false)
            {
                if (lLine.Count > 0)
                {
                    for (int i = 0; i < lLine.Count; i++)
                    {
                        lLine[i].Draw(gp);
                    }
                }
                if (lRectangle.Count > 0)
                {
                    for (int i = 0; i < lRectangle.Count; i++)
                    {
                        lRectangle[i].Draw(gp);
                    }
                }
                if (lCircle.Count > 0)
                {
                    for (int i = 0; i < lCircle.Count; i++)
                    {
                        lCircle[i].Draw(gp);
                    }
                }
                if (lEllipse.Count > 0)
                {
                    for (int i = 0; i < lEllipse.Count; i++)
                    {
                        lEllipse[i].Draw(gp);
                    }
                }
                if (lSquare.Count > 0)
                {
                    for (int i = 0; i < lSquare.Count; i++)
                    {
                        lSquare[i].Draw(gp);
                    }
                }
                if (lPolygon.Count > 0)
                {
                    for (int i = 0; i < lPolygon.Count; i++)
                    {
                        lPolygon[i].Draw(gp);
                    }
                }

                if (lCurve.Count > 0)
                {
                    for (int i = 0; i < lCurve.Count; i++)
                    {
                        lCurve[i].Draw(gp);
                    }
                }
                for (int i = 0; i < lDrawn.Count; i++)
                {
                    lDrawn[i].Draw(gp);
                }
            }
            else
            {
                for (int i = 0; i < lDrawn.Count; i++)
                {
                    lDrawn[i].Draw(gp);
                }
                for (int j = 0; j < lSelected.Count; j++)
                {
                    lDrawn[lSelected[j]].DrawSelectArea(gp);
                }
            }
        }

        private void boxFill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxFill.SelectedItem.ToString() == "Fill Shape")
            {
                btnFillColor.Enabled = true;
                boxShape.Items.Remove("Curve");
                boxShape.Items.Remove("Line");
                bCurve = false;
                bLine = false;
                isFill = true;
            }
            if (boxFill.SelectedItem.ToString() == "No Fill Shape")
            {
                btnFillColor.Enabled = false;
                if (!(boxShape.FindStringExact("Line") >= 0))
                    boxShape.Items.Add("Line");
                if (!(boxShape.FindStringExact("Curve") >= 0))
                    boxShape.Items.Add("Curve");
                isFill = false;
            }
        }

        private void boxShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            TurnOffBools();
            switch (boxShape.SelectedItem.ToString())
            {
                case "Circle":
                    bCircle = true;
                    break;
                case "Curve":
                    bCurve = true;
                    break;
                case "Elipse":
                    bElipse = true;
                    break;
                case "Line":
                    bLine = true;
                    break;
                case "Polygon":
                    bPolygon = true;
                    break;
                case "Rectangle":
                    bRectangle = true;
                    break;
                case "Square":
                    bSquare = true;
                    break;
            }
            isClicked = false;
        }

        private void boxDash_SelectedIndexChanged(object sender, EventArgs e)
        {
            bSolid = false;
            bDash = false;
            bDashDot = false;
            bDashDotDot = false;
            bDot = false;
            switch (boxDash.SelectedItem.ToString())
            {
                case "Solid":
                    bSolid = true;
                    break;
                case "Dash":
                    bDash = true;
                    break;
                case "Dash Dot":
                    bDashDot = true;
                    break;
                case "Dash Dot Dot":
                    bDashDotDot = true;
                    break;
                case "Dot":
                    bDot = true;
                    break;
            }
        }

        private void TurnOffBools()
        {
            bCircle = false;
            bCurve = false;
            bElipse = false;
            bLine = false;
            bPolygon = false;
            bRectangle = false;
            bSquare = false;
        }

        private void Paint_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {  
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             
                if (result == DialogResult.Yes)
                {
                    Application.Exit();  
                } 

        }
    }
    public class MyPanel : Panel
    {
        public MyPanel() : base()
        {
            this.DoubleBuffered = true;
        }
    }


}
