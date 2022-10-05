using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CaseTwo
{
    internal class Spil
    {  
        public int Counter { get; set; }
        public void Run(bool debug)
        {
            Program pg = new Program(); // Main class from 'program.cs'
            Game ge = new Game();
            do
            {
                string dansernavn1, dansernavn2, tast;
                int danser1point, danser2point;
                bool valid = false;
                Console.Clear();
                OutPut op = new OutPut();
                Check ck = new Check();
                op.PrintLine(debug, "[Dansekonkurrence]");

                do
                {
                    op.PrintLine(debug, "\n\nDanser 1 navn:");
                    dansernavn1 = Console.ReadLine();
                    op.PrintLine(debug, "Danser 2 navn:");
                    dansernavn2 = Console.ReadLine();
                } while (!ck.CharValid(dansernavn1) || !ck.CharValid(dansernavn2));

                do
                {
                    op.PrintLine(debug, "\nDanser 1 point:");
                    ck.IntValid(out valid, Console.ReadLine(), out danser1point);
                    op.PrintLine(debug, "\nDanser 2 point:");
                    ck.IntValid(out valid, Console.ReadLine(), out danser2point);
                } while (!valid);


                Dansere danser1 = new Dansere(dansernavn1, danser1point);
                Dansere danser2 = new Dansere(dansernavn2, danser2point);
                Dansere DanserInfo = danser1 + danser2;
                op.PrintLine(debug, $"\nDansernavne: {DanserInfo.navn}\nDanser point: {danser1point} | {danser2point}\nTotale point: {DanserInfo.point}");
                



                ge.Quit(debug, out tast);

                pg.Spil = false;
                
            } while (pg.Spil);
        }
    }
    class Check
    { 

        public bool CharValid(string input)
        {
            if (input.All(char.IsLetter))
            {
                return true;
            }

            return false;
        }

        public int Counter(int input)
        {
            return input++;
        }

        public bool IntValid(out bool valid, string input, out int points)
        {
            valid = int.TryParse(input, out points);
            if (valid)
            {
                return true;
            }
            return false;
        }
    }

    class Game
    { 
        public void Quit(bool debug, out string tast)
        {
            OutPut op = new OutPut();
            op.PrintLine(debug, "\n\nVil du lukke programmet? [Y/N]");
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
    internal class Dansere
    {
        internal string navn;
        internal int point;

        internal Dansere(string navn, int point)
        {
            this.navn = navn;
            this.point = point;
        }

        public static Dansere operator + (Dansere first, Dansere last)
        {
            string navn = $"{first.navn} | {last.navn}";
            int points = first.point + last.point;
            Dansere total = new Dansere(navn, points);
            return total;
        }
    }
}
