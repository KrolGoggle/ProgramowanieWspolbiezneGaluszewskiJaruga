using System;

namespace projekt1
{
    public class Prog
    {
        static void Main(string[] args) {
            Prog prog1 = new Prog();
            String text = prog1.hello();
            Console.WriteLine(text);
        }

        public String hello()
        {
            String hi = "Hello World!";
            return hi;
        }
    }
}