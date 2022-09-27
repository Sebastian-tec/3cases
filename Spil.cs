using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CaseTwo
{
    internal class Spil
    {
        public int counter { get; set; }
        public void Run()
        {
            Program pg = new Program(); // Main class from 'program.cs'
            Game ge = new Game();
            do
            {
                string spil = "", navn, tast;
                Console.Clear();
                OutPut op = new OutPut();
                check ck = new check();
                op.PrintLine(pg.debug, "[Spil]");

                op.PrintLine(pg.debug, "\n[C] CSGO  |  [L] LOL");
                do
                {
                    op.KeyPress(out tast);

                } while (tast != "c" && tast != "l");

                if (tast == "c")
                {
                    spil = "csgo";
                }
                else if (tast == "l")
                {
                    spil = "lol";
                }

                do
                {
                    op.PrintLine(pg.debug, "\nNavn:");
                    navn = Console.ReadLine();
                } while (!ck.charvalid(navn));


                op.PrintLine(pg.debug, $"Spil: {spil} | Navn: {navn}");

                ge.Quit(out tast);

                pg.spil = false;
                
            } while (pg.spil);
        }
    }
    class check
    { 

        public bool charvalid(string input)
        {
            if (input.Any(char.IsLetter))
            {
                return true;
            }

            return false;
        }

        public int counter(int input)
        {
            return input++;
        }


    }

    class Game
    { 
        public void Quit(out string tast)
        {
            OutPut op = new OutPut();
            Program pg = new Program();
            op.PrintLine(pg.debug, "\n\nVil du lukke programmet? [Y/N]");
            op.KeyPress(out tast);
            
            if (tast == "y")
            {
                Environment.Exit(0);
            }
            else
            {
                return;
            }

        }
    }
}
