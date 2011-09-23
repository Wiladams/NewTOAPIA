using System;
using System.Threading;

//===================================================================
// Inspired by: Emilie Sutterlin, 10/10/97
//===================================================================

using TOAPI.Kernel32;

namespace ConsoPIA
{
    public class PongField
    {
        public SMALL_RECT fBallArea;
        public SMALL_RECT fScoreDisplayArea;
        public SMALL_RECT fMessageArea;

        public PongField()
        {
            fBallArea = new SMALL_RECT(0, 0, 79, 23);
            fScoreDisplayArea = new SMALL_RECT(0, 24, 79, 24);
            fMessageArea = new SMALL_RECT(10, 10, 69, 14);
        }

        public PongField(SMALL_RECT playarea, SMALL_RECT scorearea, SMALL_RECT messagearea)
        {
        }
    }

    public class Pong
    {
        const int MAX = 10;
        short lprow = 8, rprow = 8, col = 2, colinc = 1, rowinc = 1, row = 1;
        int scorr = 0, scorl = 0;
        bool fContinueGame;
        PongField fField;
        Ball fBall;
        Paddle fLeftPaddle;
        Paddle fRightPaddle;

        Terminal fTerm;

        public Pong(Terminal aTerm)
        {
            fContinueGame = true;
            fTerm = aTerm;

            fField = new PongField();
            fBall = new Ball(col, row, fField.fBallArea,aTerm);

            fLeftPaddle = new Paddle(fField.fBallArea, lprow, PaddleSide.Left, aTerm);
            fRightPaddle = new Paddle(fField.fBallArea, rprow, PaddleSide.Right, aTerm);
        
            // Make sure no scrolling occurs, so the buffer height
            // is the same as the window height, by default
            fTerm.BufferHeight = 25;
            //Console.SetBufferSize(80, 25);

            aTerm.Title = "Pong";
        }

        public void Run()
        {
            RunIntroScreen();

            // Draw the playing field
            DrawField();

            //Run the game loop
            // This should be changed to simply move the ball and
            // update the score.
            // Keyboard input should come in through interrupt
            while (fContinueGame)
            {
                BounceBall();
                MovePaddles();
            }
        }

        void RunIntroScreen()
        {
            fTerm.BackgroundColor = TOAPI.Kernel32.ConsoleTextColor.Black;
            fTerm.ForegroundColor = TOAPI.Kernel32.ConsoleTextColor.White;
            fTerm.Clear();

            fTerm.CursorVisible = false;

            //INTRODUCTION
            fTerm.WriteLine("WELCOME TO PONG\n");
            fTerm.WriteLine("This is a two person game.\n");
            fTerm.WriteLine("The object is to gain points by having your opponent miss hitting the ball with the paddle.");
            fTerm.WriteLine("The game ends when a player reaches 20 points.\n");
            fTerm.WriteLine("You can stop the game early anytime by pressing either the Enter or Escape key.");
            fTerm.WriteLine("The paddle on the left can be controlled with the arrow up and down keys.");
            fTerm.WriteLine("The paddle on the right can be controlled using the page up and down keys.");
            fTerm.WriteLine("Press any key to begin");

            Console.ReadKey(true);
            fTerm.Clear();
        }

        void DrawInitialPaddles()
        {
            fLeftPaddle.Draw();
            fRightPaddle.Draw();
        }
    
        void DrawField()
        {
            DrawInitialPaddles();
            DrawScores();
        }

        void DrawScores()
        {
            // Black lettering on a white background
            fTerm.BackgroundColor = ConsoleTextColor.White;
            fTerm.ForegroundColor = ConsoleTextColor.Black;
            for (short x = 0; x < 79; x++)
            {
                fTerm.GotoXY(x, 24);
                fTerm.Write(" ");
            }

            // Left Side Score
            fTerm.GotoXY(1, 24); Console.Write(scorr);

            // Right Score
            fTerm.GotoXY(74, 24); Console.Write(scorl);

            // Reset to standard colors
            fTerm.BackgroundColor = ConsoleTextColor.Black;
            fTerm.ForegroundColor = ConsoleTextColor.White;
        }

        void EvaluateScores()
        {
            string winner = null;

            if (scorl == 20)
                winner = "right";
            else if (scorr == 20)
                winner = "left";

            if (winner != null)
            {
                fTerm.BackgroundColor = ConsoleTextColor.Black;
                fTerm.ForegroundColor = ConsoleTextColor.White;
                fTerm.GotoXY(22, 10); Console.Write("The winner is the player on the ");
                fTerm.Write(winner);
                fTerm.CursorVisible = false;
            }
        }

        void GameOver()
        {
            fTerm.BackgroundColor = ConsoleTextColor.Black;
            fTerm.ForegroundColor = ConsoleTextColor.White;

            fTerm.GotoXY(10, 10);
            fTerm.Write("GAME OVER. Press any key to exit.");

            Console.ReadKey(true);
            fTerm.CursorVisible = false;
            fContinueGame = false;
        }

        void BounceBall()
        {
            while (!Console.KeyAvailable)
            {
                // Calculate where the ball will be next, based 
                // on its current trajectory.
                COORD nextLoc = fBall.NextMove();

                // See if this intersects one of the paddles
                Paddle paddleHit = null;
                if (fLeftPaddle.Intersects(nextLoc))
                    paddleHit = fLeftPaddle;
                else if (fRightPaddle.Intersects(nextLoc))
                    paddleHit = fRightPaddle;

                // If a paddle would be hit, then reverse the
                // ball's direction, and let it continue on it's
                // new path.
                if (null != paddleHit)
                {
                    fBall.XIncrement = (short)-fBall.XIncrement;
                    continue;
                }

                if (nextLoc.X <= fLeftPaddle.fPosition.X)
                {
                    // We're about to go off the left side
                    // so increase that score.
                    scorl += 1;
                    fBall.XIncrement = (short)-fBall.XIncrement;
                    DrawScores();
                }

                if (nextLoc.X >= fRightPaddle.fPosition.X)
                {
                    scorr += 1;
                    fBall.XIncrement = (short)-fBall.XIncrement;
                    DrawScores();
                }



                EvaluateScores();
                fBall.Move();


                Thread.Sleep(55);
            }
        }


        void MovePaddles()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            
            switch (keyInfo.Key)
            {
                // Either escape or Enter quite the game
                case ConsoleKey.Enter:
                case ConsoleKey.Escape:
                    GameOver();
                    break;
            
                // Move the left paddle up
                case ConsoleKey.W:
                    fLeftPaddle.MoveBy(0, -1);
                    break;

                case ConsoleKey.Z:
                    fLeftPaddle.MoveBy(0, 1);
                        break;

                // Move the right paddle up
                case ConsoleKey.UpArrow:
                        fRightPaddle.MoveBy(0, -1);
                        break;

                // Move the right paddle down
                case ConsoleKey.DownArrow:
                case ConsoleKey.PageDown:
                        fRightPaddle.MoveBy(0, 1);
                        break;
            }
        }
    }
}