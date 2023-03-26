namespace Services
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Logger logger = new Logger(new Message());
            logger.Text = "Hello";
            logger.Print();

        }
    }
}