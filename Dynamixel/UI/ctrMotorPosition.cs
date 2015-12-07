using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Dynamixel.Driver;
using Dynamixel.Events;

namespace GripperControler.Dynamixel.UI
{
    public partial class ctrMotorPosition : UserControl
    {

        #region MEMBER
        private DynamixelData motor;
        private const byte radiusCorrection = 15;

        private float radius;
        private PointF center;
        private PointF goalPosition = new PointF();
        private PointF presentPosition = new PointF();
        private PointF pt0 = new PointF();
        private PointF pt150 = new PointF();
        private PointF pt300 = new PointF();

        private PointF ptCWAngleLimit = new PointF();
        private PointF ptCCWAngleLimit = new PointF();

        // the zero angle of each motor is at 240 deg
        private double angle0 = (double)240 * Math.PI / (double)180; 

        // -----------------------------
        // properties
        // -----------------------------

        private float _angle;
        [Description("Angle of the current position of the motor"), Category("Data")]
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; this.Invalidate(); }
        }
        #endregion


        #region INITIALIZATION
        public ctrMotorPosition()
        {
            InitializeComponent();
        }

        private void ctrMotorPosition_Load(object sender, EventArgs e)
        {
            ctrMotorPosition_Resize(sender, e);
            DynamixelEvents.Instance.OnMotorSelectedChange += MotorDataReceived;
            DynamixelEvents.Instance.OnMessageBusEvent += OnMessageBusEventReceived;
        }
        #endregion


        #region NEW MOTOR SELECTED MESSAGE (OBSERVER PATTERN)
        private void MotorDataReceived(object sender, DynamixelEvents.MotorSelectedChangeArgs e)
        {
            motor = e.motor;
            goalPosition.X = 0;
            presentPosition.X = 0;
            ptCWAngleLimit.X = 0;
            ptCCWAngleLimit.X = 0;
            this.Invalidate();
        }

        private void OnMessageBusEventReceived(object sender, DynamixelEvents.MessageBusArgs e)
        {
            switch (e.type)
            {
                case DynamixelEvents.MessageBusType.PRESENT_POSITION_CHANGE:
                    presentPosition = getPointFromDynamixelData((ushort)e.value);
                    this.Invalidate();
                    break;

                case DynamixelEvents.MessageBusType.GOAL_POSITION_CHANGE:
                    PointF pt = getPointFromDynamixelData((ushort)e.value);
                    changeGoalPosition((int)pt.X, (int)pt.Y);
                    break;

                case DynamixelEvents.MessageBusType.CW_ANGLE_LIMIT_CHANGE:
                    ptCWAngleLimit = getPointFromDynamixelData((ushort)e.value);
                    break;

                case DynamixelEvents.MessageBusType.CCW_ANGLE_LIMIT_CHANGE:
                    ptCCWAngleLimit = getPointFromDynamixelData((ushort)e.value);
                    break;
            }
        }
        #endregion


        #region CAN MESSAGE TREATMENT

        private void packetPositionDecoder(byte[] packet)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<byte[]>(packetPositionDecoder), packet);
            }
            else
            {
                presentPosition = getPointFromDynamixelData((ushort)((packet[5] << 8) | packet[4]));
                this.Invalidate();
            }
        }

        #endregion


        #region PAINTING MOTOR
        private void ctrMotorPosition_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen myPen = new Pen(Color.Black, 1);
            SolidBrush brush = new SolidBrush(Color.Blue);

            // circle of the motor
            drawCircle(e.Graphics, myPen, radius, center);

            // line that represent the center of the motor (150 degrees)
            e.Graphics.DrawLine(myPen, center, pt150);
            e.Graphics.DrawString("150°", this.Font, brush, pt150.X - 11, pt150.Y - 11);

            // line that represent the angle 0 of the motor
            e.Graphics.DrawLine(myPen, center, pt0);
            e.Graphics.DrawString("0°", this.Font, brush, pt0);

            // line that represent the angle 300 of the motor
            e.Graphics.DrawLine(myPen, center, pt300);
            e.Graphics.DrawString("300°", this.Font, brush, pt300.X-20, pt300.Y);

            myPen.Width = 2;

            // draw the angle limits
            myPen.Color = Color.Red;
            if (ptCWAngleLimit.X > 0) e.Graphics.DrawLine(myPen, center, ptCWAngleLimit);
            if (ptCCWAngleLimit.X > 0) e.Graphics.DrawLine(myPen, center, ptCCWAngleLimit);

            // draw the goal position
            myPen.Color = Color.Blue;
            if (goalPosition.X > 0)
                e.Graphics.DrawLine(myPen, center, goalPosition);

            // draw the current position of the motor
            myPen.Color = Color.Green;
            if (presentPosition.X > 0)
                e.Graphics.DrawLine(myPen, center, presentPosition);

            brush.Dispose();
            myPen.Dispose();
        }
        #endregion


        #region CHANGING MOTOR POSITION

        private void ctrMotorPosition_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            changeGoalPosition(e.Location.X, e.Location.Y);
            sendGoalPositionEvent(getMotorAngle(e.Location.X, e.Location.Y));
        }

        private void changeGoalPosition(int locationX, int locationY)
        {
            // find the new angle from the mouse click position
            double x = center.X - locationX;
            double y = center.Y - locationY;

            // calculate the angle
            double _angle = Math.Atan(y / x);
            if (x >= 0) _angle += Math.PI;

            goalPosition.X = center.X + radius * (float)Math.Cos(_angle);
            goalPosition.Y = center.Y + radius * (float)Math.Sin(_angle);

            this.Invalidate();
        }

        #endregion


        #region LAYOUT RESIZE HANDLER
        private void ctrMotorPosition_Resize(object sender, EventArgs e)
        {
            float aCW = (float)getRadianAngle(ptCWAngleLimit.X - center.X, ptCWAngleLimit.Y - center.Y);
            float aCCW = (float)getRadianAngle(ptCCWAngleLimit.X - center.X, ptCCWAngleLimit.Y - center.Y);
            float aCurPos = (float)getRadianAngle(presentPosition.X - center.X, presentPosition.Y - center.Y);
            float aGoal = (float)getRadianAngle(goalPosition.X - center.X, goalPosition.Y - center.Y);

            center = new PointF(this.Width / 2, this.Height / 2);
            if (this.Height < this.Width)
                radius = this.Height / 2 - radiusCorrection;
            else
                radius = this.Width / 2 - radiusCorrection;

            // point that represent the center of the motor (150 degrees)
            pt150 = calculateNewPointFromRadianAngle((double)(90.0 / 180.0 * Math.PI));

            // point that represent the angle 0 of the motor
            pt0 = calculateNewPointFromRadianAngle((double)(240.0 / 180.0 * Math.PI));

            // point that represent the angle 300 of the motor
            pt300 = calculateNewPointFromRadianAngle((double)(-60.0 / 180.0 * Math.PI));

            // point representing the angle limit
            ptCWAngleLimit = calculateNewPointFromRadianAngle(aCW);
            ptCCWAngleLimit = calculateNewPointFromRadianAngle(aCCW);

            // point representing the current position
            presentPosition = calculateNewPointFromRadianAngle(aCurPos);

            // point representing the goal position
            if (goalPosition.X > 0)
                goalPosition = calculateNewPointFromRadianAngle(aGoal);

            this.Invalidate();
        }
        #endregion


        #region HELPER

        private PointF calculateNewPointFromRadianAngle(double radian)
        {
            PointF pt = new PointF();
            pt.X = center.X - radius * (float)Math.Cos(radian);
            pt.Y = center.Y - radius * (float)Math.Sin(radian);
            return pt;
        }

        private void sendGoalPositionEvent(double angle)
        {
            DynamixelEvents.Instance.postMessageBusEvent(DynamixelEvents.MessageBusType.GOAL_POSITION_CHANGE, motor.getPositionFromAngle(angle));
        }

        private void drawCircle(Graphics graph, Pen pen, float radius, PointF center)
        {
            RectangleF rect = new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            graph.DrawEllipse(pen, rect);
        }


        private void fillCircle(Graphics graph, Brush brush, float radius, PointF center)
        {
            RectangleF rect = new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            graph.FillEllipse(brush, rect);
        }

        private PointF getPointFromDynamixelData(ushort data)
        {
            PointF pt = new PointF();
            double angle = motor.getAngleFromPosition(data);

            pt = rotatePoint(radius * (float)Math.Cos(angle), radius * (float)Math.Sin(angle), -angle0);
            pt.X = -pt.X + center.X;
            pt.Y += center.Y;

            return pt;
        }


        private double getMotorAngle(double x, double y)
        {
            // find the new angle from the clicker position
            x = x - center.X;
            y = y - center.Y;

            // calculate the angle
            double aXY = getRadianAngle(x, y);

            // projection of the point
            x = radius * Math.Cos(aXY);
            y = -radius * Math.Sin(aXY);

            // rotation
            PointF pt = rotatePoint(x, y, angle0);

            return getDegreeAngle(pt);
        }

        // return the angle in radian
        private double getRadianAngle(double x, double y)
        {
            // calculate the angle
            double aXY = Math.Atan(y / x);
            if (x >= 0) aXY += Math.PI;

            return aXY;
        }


        // return the angle in degree
        private double getDegreeAngle(PointF pt)
        {
            double angle = (float)(Math.Atan(pt.Y / pt.X) * 180 / Math.PI) + 180;
            if (pt.X >= 0) angle = (angle + 180) % 360;

            return angle;
        }

        private PointF rotatePoint(double x, double y, double angle) 
        {
            PointF pt = new PointF();

            // rotation
            pt.X = (float)(x * Math.Cos(angle) - y * Math.Sin(angle));
            pt.Y = (float)(x * Math.Sin(angle) + y * Math.Cos(angle));

            return pt;
        }

        #endregion

    }
}
