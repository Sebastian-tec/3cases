using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CaseTwo
{
    internal class Spil
    {  
        // Create a public int
        public int Counter { get; set; }
        public void Run(bool debug)
        {
            // Create instances of our classes
            Program pg = new Program(); // Main class from 'program.cs'
            Game ge = new Game();

            do
            {
                // Create variables that will be used later on
                string dansernavn1, dansernavn2, tast;
                int danser1point, danser2point;
                bool valid = false;
                // Clear the console
                Console.Clear();

                OutPut op = new OutPut();
                Check ck = new Check();
                // Inform which case we are in
                op.PrintLine(debug, "[Dansekonkurrence]");
                // Loop to check their input is true/valid
                do
                {
                    // Get the first dancers name
                    op.PrintLine(debug, "\n\nDanser 1 navn:");
                    dansernavn1 = Console.ReadLine();
                    // Get the second dancers name
                    op.PrintLine(debug, "Danser 2 navn:");
                    dansernavn2 = Console.ReadLine();
                } while (!ck.CharValid(dansernavn1) || !ck.CharValid(dansernavn2));

                do
                {
                    // Get the first dancers points
                    op.PrintLine(debug, "\nDanser 1 point:");
                    // Make sure their input is a int and return it to our danser1point variabe¨l
                    ck.IntValid(out valid, Console.ReadLine(), out danser1point);

                    op.PrintLine(debug, "\nDanser 2 point:");
                    ck.IntValid(out valid, Console.ReadLine(), out danser2point);
                } while (!valid);

                // Create three new instances with our variables as paramters
                Dansere danser1 = new Dansere(dansernavn1, danser1point);
                Dansere danser2 = new Dansere(dansernavn2, danser2point);
                Dansere DanserInfo = danser1 + danser2;
                // Inform about the total information we gotten
                op.PrintLine(debug, $"\nDansernavne: {DanserInfo.navn}\nDanser point: {danser1point} | {danser2point}\nTotale point: {DanserInfo.point}");
                


                // Ask if they want to quit the program
                ge.Quit(debug, out tast);

                pg.Spil = false;
                
            } while (pg.Spil);
        }
    }
    class Check
    { 
        // A bool to check if their input is valid (The method is just to show i can do it multiple ways)
        public bool CharValid(string input)
        {
            // If their input is letters only
            if (input.All(char.IsLetter))
            {
                // Return our bool as true
                return true;
            }
            // Else return it as false
            return false;
        }

        // A counter method to return + 1
        public int Counter(int input)
        {
            return input++;
        }

        // A bool to check if their input is valid
        public bool IntValid(out bool valid, string input, out int points)
        {
            // TryParse their input to a int, which returns a bool
            valid = int.TryParse(input, out points);
            // If our valid is true
            if (valid)
            {
                return true;
            }
            return false;
        }
    }

    class Game
    { 
        // Create a quit function 
        public void Quit(bool debug, out string tast)
        {
            OutPut op = new OutPut();
            // Ask if they want to close the program
            op.PrintLine(debug, "\n\nVil du lukke programmet? [Y/N]");
            op.KeyPress(out tast);
            
            // If their answer is 'y'
            if (tast == "y")
            {
                // Close the program
                Environment.Exit(0);
            }
            else
            {
                // Return from the function
                return;
            }
        }
    }
    internal class Dansere
    {
        // Create variables that is only available in this assembly
        internal string navn;
        internal int point;

        // Create an function based of our class
        internal Dansere(string navn, int point)
        {
            // Set this functions varibles to our internal variables
            this.navn = navn;
            this.point = point;
        }

        // 
        public static Dansere operator + (Dansere first, Dansere last)
        {
            string navn = $"{first.navn} | {last.navn}";
            int points = first.point + last.point;
            Dansere total = new Dansere(navn, points);
            return total;
        }
    }
}
