namespace ConsoPIA
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal term = new Terminal();

            TerminalTester tTester = new TerminalTester();
            tTester.Run(term);
            
            //Pong pongGame = new Pong(term);
            //pongGame.Run();
        }
    }
}
