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
        private bool debug;
        public bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }
        public bool Spil { get; set; }
        public bool Fod { get; set; }
        

        static void Main(string[] args)
        {
            bool valid = false;
            string tast = "", input = "", name, num, file = "", path = "";
            Program pg = new Program();
            Check ck = new Check();
            NameCheck nc = new NameCheck(); // nc is an abbr. for namecheck
            OutPut op = new OutPut(); // op is an abbr. for output
            FileHandlers fh = new FileHandlers(); // fh is an abbr. for FileHandlers
            Spil sl = new Spil();
            Fodbold fb = new Fodbold();
            Game gm = new Game();

            Console.WriteLine("[D] Debug | [S] Standard");
            do
            {
                op.KeyPress(out tast);
                op.ClearCurrentConsoleLine(pg.Debug);
            } while (tast != "d" && tast != "s");
            

            if (tast == "d")
            {
                pg.Debug = true;
            }
            else
            {
                pg.Debug = false;
            }

            Console.Clear();
            op.PrintLine(pg.Debug, "[L] Login | [O] Opret");

            do
            {
                do
                {
                    op.KeyPress(out tast);
                } while (tast != "l" && tast != "o" && tast != "h");

                if (tast == "o")
                {

                    op.PrintLine(pg.Debug, "Opret bruger");
                    do
                    {
                        do
                        {
                            do
                            {

                                op.PrintLine(pg.Debug, "Indtast venligst dit navn:");
                                name = Console.ReadLine();
                                nc.CheckChars(pg.Debug, name, out valid);

                                if (!valid)
                                {
                                    op.PrintLine(pg.Debug, "Dit brugernavn indeholder ugyldig(e) tegn");
                                }
                            } while (!valid);

                            op.Update(input, name);

                            do
                            {
                                op.PrintLine(pg.Debug, "Indtast venligst dit kodeord:");
                                num = Console.ReadLine();
                                nc.CheckNumber(pg.Debug, num, out valid);

                                if (!valid)
                                {
                                    op.PrintLine(pg.Debug, "Dit kodeord indeholder ugyldige(e) tegn");
                                }
                            } while (!valid);

                            op.Update(input, name);
                        } while (!valid);

                        if (name.ToLower() == num.ToLower())
                        {
                            valid = false;
                            op.PrintLine(pg.Debug, "Dit brugernavn og kodeord må ikke være det samme!");
                        }

                    } while (!valid);


                    op.PrintLine(pg.Debug, "Succes! Din bruger er oprettet!");


                    if (pg.Debug)
                    {
                        op.PrintLine(pg.Debug, "[O] Opret Fil  |  [S] Slet fil");
                        do
                        {
                            op.KeyPress(out tast);
                            op.ClearCurrentConsoleLine(pg.Debug);

                        } while (tast != "o" && tast != "s");

                        if (tast == "o")
                        {
                            fh.Create(pg.Debug, file, out path);
                        }
                        else if (tast == "s")
                        {
                            fh.Delete(pg.Debug, file);
                        }
                    }
                }
                else if (tast == "l")
                {
                    do
                    {
                        op.PrintLine(pg.Debug, "Intast brugernavn: ");
                        name = Console.ReadLine().Trim();

                        op.PrintLine(pg.Debug,  "Indtast kodeord: ");
                        num = Console.ReadLine();

                        if (name.ToLower() == num.ToLower())
                        {
                            valid = false;
                        }
                        string[] lines = File.ReadAllLines(file);

                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i].Contains((name + ";" + num)))
                            {
                                valid = true;
                            }
                        }
                    } while (!valid);
                }
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
                    if (sl.Counter > 0)
                    {
                        
                        op.PrintLine(pg.Debug, $"Du har været her {sl.Counter} gang(e)");

                        gm.Quit(pg.Debug, out tast);
                    }
                    op.PrintLine(pg.Debug, "[D] Dans | [F] Fodbold");
                    op.KeyPress(out tast);
                    //ck.Counter(sl.Counter); - Funktion virker ikke helt...
                    sl.Counter++;
                } while (tast != "d" && tast != "f");

                if (tast == "d")
                {
                    do
                    {
                        // Spil sl = new Spil();
                        sl.Run(pg.Debug);  
                    } while (pg.Spil);
                }
                else if (tast == "f")
                {
                    fb.Start(pg.Debug);
                } while (pg.Fod);

                Console.Clear();
            } while (!pg.Spil);
 

            Console.ReadKey();
        }
    }

    class FileHandlers
    { 
        public void Create(bool Debug, string file, out string path)
        {
            OutPut op = new OutPut();
            if (file == "")
            {
                op.PrintLine(Debug, "[Opret fil]");
                op.PrintLine(Debug, "Indtast et filnavn: ");
                file = Console.ReadLine().TrimEnd();
                if (file == "")
                {
                    file = "output.txt";
                }
            }

            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
            {
                File.Create(Path.Combine(Directory.GetCurrentDirectory(), file));

                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
                {
                    op.PrintLine(Debug, $"{file} er nu oprettet");
                }
            }

            path = Path.Combine(Directory.GetCurrentDirectory(), file);
        }

        public void Delete(bool Debug, string file)
        {
            OutPut op = new OutPut();
            if (file == "")
            {
                op.PrintLine(Debug, "[Slet fil]");
                op.PrintLine(Debug, "Indtast et filnavn");
            }

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
            {
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), file));

                if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
                {
                    op.PrintLine(Debug, $"{file} er nu slettet");
                }
            }
        }

        public void WriteText(bool Debug, string file, string input)
        {
            if (!File.Exists(file))
            {
                Create(Debug, file, out string tmp);
            }

            File.WriteAllText(file, input);
        }
    }
    class OutPut
    { 
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

        public void KeyPress(out string tast)
        {
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
        public void CheckChars(bool debug, string name, out bool valid)
        {
            OutPut op = new OutPut();
            valid = true;
            if (string.IsNullOrEmpty(name))
            {
                valid = false;
            }
            
            if (name.Length == 0)
            {
                valid = false;
            }
            
            if (string.IsNullOrWhiteSpace(name))
            {
                valid = false;
            }

            if (!name.All(Char.IsLetter))
            {
                valid = false;
            }

            if (!valid)
            {
                op.PrintLine(debug, "Invalid name!");
            }
        }

        public void CheckNumber(bool Debug, string num, out bool valid)
        {
            OutPut op = new OutPut();
            valid = true;

            if (num.Length < 12)
            {
                valid = false;
            }

            if (!num.Any(char.IsUpper) | num.Any(char.IsLower))
            {
                valid = false;
            }

            if (num.All(char.IsLetterOrDigit) | num.Any(char.IsNumber))
            {
                valid = false;
            }

            if (char.IsDigit(num[0]) | char.IsDigit(num[num.Length - 1]))
            {
                valid = false;
            }

            if (num.Any(char.IsWhiteSpace))
            {
                valid = false;
            }
        }
    }
}