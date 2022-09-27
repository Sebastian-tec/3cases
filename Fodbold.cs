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
        public void Start()
        {
            Program pg = new Program();
            OutPut op = new OutPut();
            Game ge = new Game();
            Cheer cr = new Cheer();
            Console.Clear();
            op.PrintLine(pg.debug, "[Fodbold]");
            bool parseint;
            int passes;
            string tast;

            do
            {
                op.PrintLine(pg.debug, "\nAfleveringer: ");
                do
                {
                    parseint = int.TryParse(Console.ReadLine(), out passes);

                } while (!parseint);

                cr.Replies(passes);
                ge.Quit(out tast);
                pg.Fod = false;

            } while (pg.Fod);
        }
    }
    class Cheer
    {
        public string[] cheering = { "Spiller du baglæns?", "Solokører...", "High five", "Jubel!!!", "Huh!", "Shh", "Olé Olé Olé" };
        
        public void Replies(int passes)
        {
            bool goal;
            OutPut op = new OutPut();
            Program pg = new Program();

            if (passes < 0)
            {
                op.PrintLine(pg.debug, $"{cheering[0]}");
            }
            
            if (passes == 0)
            {
                op.PrintLine(pg.debug, $"{cheering[1]}");
            }
            
            if (passes >= 1 && passes <= 3)
            {
                for (int i = 0; i < passes; i++)
                {
                    op.PrintLine(pg.debug, cheering[4]);
                }
            }
            else if (passes > 3 && passes <= 5)
            {
                op.PrintLine(pg.debug, cheering[2] + cheering[4]);
            }
            else if (passes > 5 && passes <= 7)
            {
                op.PrintLine(pg.debug, cheering[4] + cheering[3]);
            }
            else if (passes > 7 && passes <= 10)
            {
                op.PrintLine(pg.debug, cheering[3] + cheering[5]);
            }
            else if (passes > 10)
            {
                op.PrintLine(pg.debug, cheering[2] + cheering[3]);
            }

            Random rand = new Random();
            int mål = rand.Next(0, 1);

            if (mål == 0)
            {
                goal = false;
            }
            else
            {
                goal = true;
            }

            if (goal)
            {
                op.PrintLine(pg.debug, cheering[cheering.Length - 1]);
            }
        }


    }
}
