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
            Program pg = new Program();
            OutPut op = new OutPut();
            Game ge = new Game();
            Cheer cr = new Cheer();
            Console.Clear();
            op.PrintLine(debug, "[Fodbold]");
            bool parseint;
            int passes;
            string tast;

            do
            {
                op.PrintLine(debug, "\nAfleveringer: ");
                do
                {
                    parseint = int.TryParse(Console.ReadLine(), out passes);

                } while (!parseint);

                cr.Replies(debug, passes);
                ge.Quit(debug, out tast);
                pg.Fod = false;

            } while (pg.Fod);
        }
    }
    class Cheer
    {
        public string[] cheering = {"High five", "Jubel!!!", "Huh!", "Shh", "Olé Olé Olé" };
        
        public void Replies(bool debug, int passes)
        {
            bool goal = false;
            OutPut op = new OutPut();

            op.PrintLine(debug, "Skriv 'mål' hvis mål:");
            string text = Console.ReadLine().ToLower();

            if (text == "mål")
            {
                goal = true;
            }
            if (!goal)
            {
                if (passes < 1)
                {
                    op.PrintLine(debug, cheering[3]);
                }
                else if (passes > 10)
                {

                    op.PrintLine(debug, cheering[0] + cheering[1]);
                }
                else
                {
                    string print = "";
                    for (int i = 0; i < passes; i++)
                    {
                        print += cheering[3];
                    }

                    op.PrintLine(debug, print);
                }
            }
            else
            {
                op.PrintLine(debug, cheering[cheering.Length - 1]);
            }
        }


    }
}
