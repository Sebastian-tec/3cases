using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseTwo;

namespace CaseTwo
{
    internal class Fodbold
    {
        public void Start(bool debug)
        {
            // Create new instances of our classes
            Program pg = new Program();
            OutPut op = new OutPut();
            Game ge = new Game();
            Cheer cr = new Cheer();
            Console.Clear();
            // Inform which case we are in
            op.PrintLine(debug, "[Fodbold]");
            // Standard variables
            bool parseint;
            int passes;
            string tast;

            do
            {
                // Ask how many passes
                op.PrintLine(debug, "\nAfleveringer: ");
                do
                {
                    // TryParse their input to an int and return a bool value
                    parseint = int.TryParse(Console.ReadLine(), out passes);

                } while (!parseint);
                // Make use of our function
                cr.Replies(debug, passes);
                ge.Quit(debug, out tast);
                pg.Fod = false;

            } while (pg.Fod);
        }
    }
    class Cheer
    {
        // Create a string-array with all the necessary replies
        public string[] cheering = {"High five", "Jubel!!!", "Huh!", "Shh", "Olé Olé Olé" };
        // Our reply function
        public void Replies(bool debug, int passes)
        {
            bool goal = false;
            OutPut op = new OutPut();
            // Ask if a goal has happened
            op.PrintLine(debug, "Skriv 'mål' hvis mål:");
            string text = Console.ReadLine().ToLower();
            // If a goal is true
            if (text == "mål")
            {
                goal = true;
            }
            // If goal is false
            if (!goal)
            {
                // Check if passes is below 1
                if (passes < 1)
                {
                    // Print the fourth(third) string in the array
                    op.PrintLine(debug, cheering[3]);
                }
                // Check if passes is above 10
                else if (passes > 10)
                {
                    // Print the first(zero) and second(first) string in the array
                    op.PrintLine(debug, cheering[0] + cheering[1]);
                }
                else
                {
                    // Create a string that will contain what we need to print
                    string print = "";
                    // Create a loop based on the number of passes
                    for (int i = 0; i < passes; i++)
                    {
                        // Add our arraystring to our string
                        print += cheering[3];
                    }
                    // Print the string
                    op.PrintLine(debug, print);
                }
            }
            else
            {
                // Print the goal string
                op.PrintLine(debug, cheering[cheering.Length - 1]);
            }
        }


    }
}
