using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;


namespace CaseTwo
{
    public class Program
    {
        //public bool check { get; set; }
        // Create a public bool for whether or not the program is ran in debug mdoe
        private bool debug;
        public bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }
        // Create public bools for our games, they do not work as i thought...
        public bool Spil { get; set; }
        public bool Fod { get; set; }
        
        // Main function
        static void Main(string[] args)
        {
            // Create variables that will be used later in the program
            bool valid = false;
            string tast = "", name, num, file = "", path = "";
            string[] input = {""};
            // Create new instances to make use of our classes
            Program pg = new Program();
            Check ck = new Check();
            NameCheck nc = new NameCheck(); // nc is an abbr. for namecheck
            OutPut op = new OutPut(); // op is an abbr. for output
            FileHandlers fh = new FileHandlers(); // fh is an abbr. for FileHandlers
            Spil sl = new Spil();
            Fodbold fb = new Fodbold();
            Game gm = new Game();
            // Ask if the program should be in debug or standard mode
            Console.WriteLine("[D] Debug | [S] Standard");
            // Loop to get the right input 
            do
            {
                // Use our keypress method to return their choice
                op.KeyPress(out tast);
                // Remove their keypress from the screen for prettier console
                op.ClearCurrentConsoleLine(pg.Debug);
            } while (tast != "d" && tast != "s");
            
            // Check which key they pressed
            if (tast == "d")
            {
                // Set our public bool to true
                pg.Debug = true;
            }
            else
            {
                // Set the bool to false
                pg.Debug = false;
            }
            // Clear the console window
            Console.Clear();
            // Give the user two new options to either login or create a user
            op.PrintLine(pg.Debug, "[L] Login | [O] Opret");

            do
            {
                // Loop to check their input
                do
                {
                    op.KeyPress(out tast);
                } while (tast != "l" && tast != "o" && tast != "h");
                // Check if their keypress is 'o'
                if (tast == "o")
                {
                    // Inform about their choice
                    op.PrintLine(pg.Debug, "Opret bruger");
                    do
                    {
                        do
                        {
                            do
                            {
                                // Ask about their name
                                op.PrintLine(pg.Debug, "Indtast venligst dit navn:");
                                // Declare name to their input
                                name = Console.ReadLine();
                                // their if their input is letters and return a bool
                                nc.CheckChars(pg.Debug, name, out valid);
                                // if their input isnt valid
                                if (!valid)
                                {
                                    // Inform the user that their input is invalid
                                    op.PrintLine(pg.Debug, "Dit brugernavn indeholder ugyldig(e) tegn");
                                }
                            } while (!valid);
                            // Save their name to our string-array
                            input[0] = name;
                            // Make a space between the questions for format
                            Console.WriteLine();
                            // Loop to check if the bool is true
                            do
                            {
                                // Ask about their password
                                op.PrintLine(pg.Debug, "Indtast venligst dit kodeord:");
                                // Set our string to their input
                                num = Console.ReadLine();
                                // Make sure their input is valid 
                                nc.CheckPassword(pg.Debug, num, out valid);
                                // If it isnt valid
                                if (!valid)
                                {
                                    // Inform that they need to type it again
                                    op.PrintLine(pg.Debug, "Dit kodeord indeholder ugyldige(e) tegn");
                                }
                            } while (!valid);
                            input[1] = num;

                        } while (!valid);
                        
                        // Check if the username and password is the same
                        if (name.ToLower() == num.ToLower())
                        {
                            valid = false;
                            op.PrintLine(pg.Debug, "Dit brugernavn og kodeord må ikke være det samme!");
                        }

                    } while (!valid);



                    // Inform about they can choose to create a file or delete a file
                    op.PrintLine(pg.Debug, "[O] Opret Fil  |  [S] Slet fil");
                    // Make sure that their input is what we expect with a loop
                    do
                    {
                        // Get their keypress
                        op.KeyPress(out tast);
                        // Remove the char from the console
                        op.ClearCurrentConsoleLine(pg.Debug);
                    } while (tast != "o" && tast != "s");

                    // If their keypress is 'o'
                    if (tast == "o")
                    {
                        // Use our Create function
                        fh.Create(pg.Debug, file, out path);
                    }
                    else if (tast == "s")
                    {
                        // Use our Delete function
                        fh.Delete(pg.Debug, file);
                    }

                    // Write our array to the file
                    fh.WriteText(pg.Debug, path, input);
                    // Inform that the user is successfully created
                    op.PrintLine(pg.Debug, "Succes! Din bruger er oprettet!");

                }
                else if (tast == "l")
                {
                    do
                    {
                        // Get their username
                        op.PrintLine(pg.Debug, "Intast brugernavn: ");
                        name = Console.ReadLine().Trim();

                        // Get their password
                        op.PrintLine(pg.Debug,  "Indtast kodeord: ");
                        num = Console.ReadLine();

                        // If the name and password is the same, set valid to false
                        if (name.ToLower() == num.ToLower())
                        {
                            valid = false;
                        }
                        // Else do
                        else
                        {
                            // Create an array that has all lines of the file
                            string[] lines = File.ReadAllLines(file);
                            // Create a loop based on the length of the loop
                            for (int i = 0; i < lines.Length; i++)
                            {
                                // If one of the lines has their username and password
                                if (lines[i].Contains((name + ";" + num)))
                                {
                                    // Set valid to true
                                    valid = true;
                                }
                            }
                        }
                        
                    } while (!valid);
                }
                // Else if their input is 'h'
                // This is just a shortcut for easier debugging
                else if (tast == "h")
                {
                    op.ClearCurrentConsoleLine(pg.Debug);
                    op.PrintLine(pg.Debug, "Genvej!");
                    valid = true;
                }
            } while (!valid);

            do
            {
                do
                {
                    // This method doesn't work properly, but the idea was to just create a int counting how many times they been here
                    if (sl.Counter > 0)
                    {
                        // Inform them how many times they been here
                        op.PrintLine(pg.Debug, $"Du har været her {sl.Counter} gang(e)");
                        // Ask if they want to quit the program
                        gm.Quit(pg.Debug, out tast);
                    }
                    // Tell 'em which options they have
                    op.PrintLine(pg.Debug, "[D] Dans | [F] Fodbold");
                    // Get their choice
                    op.KeyPress(out tast);
                    //sl.Counter = ck.Counter(sl.Counter); - Funktion virker ikke helt...
                    // Plus our counter
                    sl.Counter++; 
                } while (tast != "d" && tast != "f");
                // If their choice is 'd'
                if (tast == "d")
                {
                    do
                    {
                        
                        // Spil sl = new Spil();
                        // Run our run function
                        sl.Run(pg.Debug);  
                    } while (pg.Spil);
                }
                // Else if it is 'f'
                else if (tast == "f")
                {
                    // Start our Start function
                    fb.Start(pg.Debug);
                } while (pg.Fod);

                Console.Clear();
            } while (!pg.Spil);
 

            Console.ReadKey();
        }
    }

    class FileHandlers
    { 
        // A function to create a file
        public void Create(bool Debug, string file, out string path)
        {
            // Create a new instance of 'OutPut'
            OutPut op = new OutPut();
            // Check if the filename is nothing
            if (file == "")
            {
                // Inform about what we are going to do
                op.PrintLine(Debug, "[Opret fil]");
                // Tell the client to write a filename
                op.PrintLine(Debug, "Indtast et filnavn: ");
                // Trim the end in case of a whitespace
                file = Console.ReadLine().TrimEnd();
                // If they didnt type anything and file is still nothing
                if (file == "")
                {
                    // Declare file name
                    file = "output.txt";
                }
            }
            // Check if the path of this program + filename exists
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
            {
                // Create the file
                File.Create(Path.Combine(Directory.GetCurrentDirectory(), file));
                // Check if the file exists
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
                {
                    // Inform that the file is created
                    op.PrintLine(Debug, $"{file} er nu oprettet");
                }
            }
            // Return the path of the file
            path = Path.Combine(Directory.GetCurrentDirectory(), file);
        }

        // Delete file function
        public void Delete(bool Debug, string file)
        {
            OutPut op = new OutPut();
            if (file == "")
            {
                op.PrintLine(Debug, "[Slet fil]");
                op.PrintLine(Debug, "Indtast et filnavn");
            }
            // Check if the file exists
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
            {
                // Delete the file
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), file));
                // Check if the file is still there
                if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
                {
                    // Inform that we deleted the file
                    op.PrintLine(Debug, $"{file} er nu slettet");
                }
            }
        }
        // Write text to a file
        public void WriteText(bool Debug, string file, string[] input)
        {
            // Check if the file exists
            if (!File.Exists(file))
            {
                // Use our create function
                Create(Debug, file, out string tmp);
            }
            // Write the text ot the file
            input[0] = input[0] + ";";
            input[1] = input[1] + Environment.NewLine;
            File.WriteAllLines(file, input);
        }
    }
    class OutPut
    { 
        /*
        public void Update(string output, string input)
        {
            if (output == "")
            {
                output = input.TrimEnd() + ";" + Environment.NewLine;
            }
            else
            {
                output += input.TrimEnd() + ";" + Environment.NewLine;
            }
        }
        */
        // Function to get their keypress accurately
        public void KeyPress(out string tast)
        {
            // Define tast to be their keypress
            tast = Convert.ToString(Console.ReadKey().KeyChar).ToLower();
        }

        public void ClearCurrentConsoleLine(bool debug)
        {
            /* A void found on stackoverflow to clear the current line
             * This method won't do much if the program is ran in debug mode
            */

            OutPut op = new OutPut();
            
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            op.PrintLine(debug, new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        // Method to switch between Console and System writeLine
        public void PrintLine(bool Debug, string print)
        {
            OutPut op = new OutPut();
            if (Debug)
            {
                System.Diagnostics.Debug.WriteLine(print);
            }
            else
            {
               Console.Write($"{print} ");
            }
        }
    }

    class NameCheck
    {
        // Function to check if their input is letters
        public void CheckChars(bool debug, string name, out bool valid)
        {
            // Make a new instance of our class 
            OutPut op = new OutPut();
            // Set our bool to true
            valid = true;
            // If their input contains nothing
            if (string.IsNullOrEmpty(name))
            {
                // Set our bool to false
                valid = false;
            }
            
            // Check the length of their name
            if (name.Length == 0)
            {
                valid = false;
            }
            
            // Check if their name contains whitespaces
            if (string.IsNullOrWhiteSpace(name))
            {
                valid = false;
            }
            // If their name isn't letters only
            if (!name.All(Char.IsLetter))
            {
                valid = false;
            }
            // If the bool is false
            if (!valid)
            {
                // Inform them their name is invalid
                op.PrintLine(debug, "Ugyldigt navn!");
            }
        }

        // Function to check password 
        public void CheckPassword(bool Debug, string pw, out bool valid)
        {
            OutPut op = new OutPut();
            valid = true;
            
            // If their password is below 12
            if (pw.Length < 12)
            {
                // Set our bool to false
                valid = false;
            }

            // If the does not contains any upper- and lowercase
            if (!pw.Any(char.IsUpper) | pw.Any(char.IsLower))
            {
                valid = false;
            }
            // Check if their input is either letters or digits/numbers
            if (pw.All(char.IsLetterOrDigit) | pw.Any(char.IsNumber))
            {
                valid = false;
            }
            // Check if the first or last charachter is a digit
            if (char.IsDigit(pw[0]) | char.IsDigit(pw[pw.Length - 1]))
            {
                valid = false;
            }
            // If the password contains whitespaces
            if (pw.Any(char.IsWhiteSpace))
            {
                valid = false;
            }
        }
    }
}