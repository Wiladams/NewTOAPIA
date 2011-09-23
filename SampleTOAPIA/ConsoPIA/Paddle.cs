using TOAPI.Kernel32;

namespace ConsoPIA
{
    public enum PaddleSide
    {
        Left,
        Right
    }

    public class Paddle
    {
        public SMALL_RECT fPlayArea;
        int fPaddleHeight;
        public COORD fPosition;
        public PaddleSide fSide;
        public Terminal fTerm;
        ConsoleTextColor fColor;

        public Paddle(SMALL_RECT playArea, short row, PaddleSide aSide, Terminal aTerm)
        {
            fPaddleHeight = 5;
            fPlayArea = playArea;
            fColor = ConsoleTextColor.White;
            fSide = aSide;
            fTerm = aTerm;
            
            short column;
            if (PaddleSide.Left == aSide)
                column = 0;
            else
                column = 79;

            fPosition = new COORD(column, row);
        }

        public bool Intersects(COORD aCoord)
        {
            bool hit = false;

            if ((aCoord.X == fPosition.X) &&
                (aCoord.Y >= fPosition.Y) &&
                (aCoord.Y <= fPosition.Y + fPaddleHeight))
                hit = true;

            return hit;
        }

        void DrawAt(short x, short y)
        {
            // Go to new position and write the ball
            for (int i = 0; i < fPaddleHeight; i++)
            {
                fTerm.GotoXY(x, (short)(y + i)); fTerm.Write("|");
            }
        }

        public void Draw()
        {
            fTerm.BackgroundColor = fColor;
            fTerm.ForegroundColor = fColor;

            DrawAt(fPosition.X, fPosition.Y);
        }

        public void MoveTo(short x, short y)
        {
            // Go to the old position and write a blank
            fTerm.BackgroundColor = ConsoleTextColor.Black;
            fTerm.ForegroundColor = ConsoleTextColor.Black;

            DrawAt(fPosition.X, fPosition.Y);

            // Set the new position and draw
            fPosition.X = x;
            fPosition.Y = y;
            Draw();
        }

        public void MoveBy(short cx, short cy)
        {
            short newX = (short)(fPosition.X + cx);
            short newY = (short)(fPosition.Y + cy);

            if (newY < fPlayArea.Top)
                newY = fPlayArea.Top;
            if (newY > fPlayArea.Bottom - 4)
                newY = (short)(fPlayArea.Bottom - 4);

            MoveTo(newX, newY);
        }
    }
}
