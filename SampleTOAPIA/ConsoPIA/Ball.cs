using TOAPI.Kernel32;

namespace ConsoPIA
{
    public class Ball
    {
        COORD fPosition;
        Terminal fTerm;
        SMALL_RECT fBallArea;
        short xInc;
        short yInc;

        public Ball(short x, short y, SMALL_RECT ballArea, Terminal aTerm)
        {
            fPosition.X = x;
            fPosition.Y = y;
            xInc = 1;
            yInc = 1;
            fBallArea = ballArea;
            fTerm = aTerm;
        }

        public COORD Position
        {
            get { return fPosition; }
            set { fPosition = value; }
        }

        public short XIncrement
        {
            get { return xInc; }
            set { xInc = value; }
        }

        public short YIncrement
        {
            get { return yInc; }
            set { yInc = value; }
        }

        public COORD NextMove()
        {
            COORD nextPlace = fPosition;
            nextPlace.X += xInc;
            nextPlace.Y += yInc;

            if (nextPlace.X < fBallArea.Left)
            {
                nextPlace.X = fBallArea.Left;
                xInc = (short)-xInc;
            }
            if (nextPlace.X > fBallArea.Right)
            {
                nextPlace.X = fBallArea.Right;
                xInc = (short)-xInc;
            }

            // Now deal with the y position
            if (nextPlace.Y < fBallArea.Top)
            {
                nextPlace.Y = fBallArea.Top;
                yInc = (short)-yInc;
            }
            if (nextPlace.Y > fBallArea.Bottom)
            {
                nextPlace.Y = fBallArea.Bottom;
                yInc = (short)-yInc;
            }

            return nextPlace;
        }

        public void DrawAt(short x, short y)
        {
            // Go to new position and write the ball
            fTerm.BackgroundColor = ConsoleTextColor.White;
            fTerm.ForegroundColor = ConsoleTextColor.White;
            
            fTerm.GotoXY(x, y);
            fTerm.Write(" ");
        }

        public void Draw()
        {
            DrawAt(fPosition.X, fPosition.Y);
        }

        public void MoveTo(short x, short y)
        {
            // Go to the old position and write a blank
            fTerm.BackgroundColor = ConsoleTextColor.Black;
            fTerm.ForegroundColor = ConsoleTextColor.Black;
            
            fTerm.GotoXY(fPosition.X, fPosition.Y);
            fTerm.Write(" ");//erases old ball

            // Set the new position and draw
            fPosition.X = x;
            fPosition.Y = y;

            DrawAt(fPosition.X, fPosition.Y);
        }

        public void MoveBy(short cx, short cy)
        {
            short newX = (short)(fPosition.X + cx);
            short newY = (short)(fPosition.Y + cy);

            MoveTo(newX, newY);
        }

        public void Move()
        {
            COORD nextMove = NextMove();
            MoveTo(nextMove.X, nextMove.Y);
        }
    }
}
