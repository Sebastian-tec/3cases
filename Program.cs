using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;


namespace CaseTwo
{
    internal class Program
    {
        //public bool check { get; set; }

        public bool debug { get; set; }
        public bool spil { get; set; }
        public bool Fod { get; set; }
        

        static void Main(string[] args)
        {
            bool valid = false;
            string tast = "", input = "", name, num, file = "", path = "";
            Program pg = new Program();
            check ck = new check();
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
                op.ClearCurrentConsoleLine(pg.debug);
            } while (tast != "d" && tast != "s");
            

            if (tast == "d")
            {
                pg.debug = true;
            }
            else
            {
                pg.debug = false;
            }
            Console.Clear();

            op.PrintLine(pg.debug, "[L] Login | [O] Opret");

            do
            {
                do
                {
                    op.KeyPress(out tast);
                } while (tast != "l" && tast != "o" && tast != "h");

                if (tast == "o")
                {

                    op.PrintLine(pg.debug, "Opret bruger");
                    do
                    {
                        do
                        {
                            op.PrintLine(pg.debug, "Indtast venligst dit navn:");
                            name = Console.ReadLine();
                            nc.CheckChars(pg.debug, name, out valid);
                        } while (!valid);

                        op.Update(input, name);

                        do
                        {
                            op.PrintLine(pg.debug, "Indtast venligst dit nummer:");
                            num = Console.ReadLine();
                            nc.CheckNumber(pg.debug, num, out valid);
                        } while (!valid);

                        op.Update(input, name);
                    } while (!valid);

                    op.PrintLine(pg.debug, "Succes! Din bruger er oprettet!");


                    if (pg.debug)
                    {
                        op.PrintLine(pg.debug, "[O] Opret Fil  |  [S] Slet fil");
                        do
                        {
                            op.KeyPress(out tast);
                            op.ClearCurrentConsoleLine(pg.debug);

                        } while (tast != "o" && tast != "s");

                        if (tast == "o")
                        {
                            fh.Create(pg.debug, file, out path);
                        }
                        else if (tast == "s")
                        {
                            fh.Delete(pg.debug, file);
                        }
                    }
                }
                else if (tast == "l")
                {
                    do
                    {
                        op.PrintLine(pg.debug, "Intast brugernavn: ");
                        name = Console.ReadLine().Trim();

                        op.PrintLine(pg.debug,  "Indtast nummer: ");
                        num = Console.ReadLine();

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
                    op.ClearCurrentConsoleLine(pg.debug);
                    op.PrintLine(pg.debug, "Genvej!");
                    valid = true;
                }
            } while (!valid);

            do
            {
                do
                {
                    if (sl.counter > 0)
                    {
                        
                        Console.WriteLine($"Du har været her {sl.counter} gange");

                        gm.Quit(out tast);
                    }
                    op.PrintLine(pg.debug, "\n[S] Spil | [F] Fodbold");
                    op.KeyPress(out tast);
                    ck.counter(sl.counter);
                } while (tast != "s" && tast != "f");

                if (tast == "s")
                {
                    do
                    {
                        sl.Run();  
                    } while (pg.spil);
                }
                else if (tast == "f")
                {
                    fb.Start();
                } while (pg.Fod);

                Console.Clear();
            } while (!pg.spil);
 

            Console.ReadKey();
        }
    }

    class FileHandlers
    { 
        public void Create(bool debug, string file, out string path)
        {
            OutPut op = new OutPut();
            if (file == "")
            {
                op.PrintLine(debug, "[Opret fil]");
                op.PrintLine(debug, "Indtast et filnavn: ");
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
                    op.PrintLine(debug, $"{file} er nu oprettet");
                }
            }

            path = Path.Combine(Directory.GetCurrentDirectory(), file);
        }

        public void Delete(bool debug, string file)
        {
            OutPut op = new OutPut();
            if (file == "")
            {
                op.PrintLine(debug, "[Slet fil]");
                op.PrintLine(debug, "Indtast et filnavn");
            }

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
            {
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), file));

                if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file)))
                {
                    op.PrintLine(debug, $"{file} er nu slettet");
                }
            }
        }

        public void WriteText(bool debug, string file, string input)
        {
            if (!File.Exists(file))
            {
                Create(debug, file, out string tmp);
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
            OutPut op = new OutPut();
            // A void found on stackoverflow to clear the current line
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            op.PrintLine(debug, new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }


        public void PrintLine(bool debug, string print)
        {
            OutPut op = new OutPut();
            if (debug)
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

            if (!name.Any(Char.IsLetter))
            {
                valid = false;
            }

            if (!valid)
            {
                op.PrintLine(debug, "Invalid name!");
            }
        }

        public void CheckNumber(bool debug, string num, out bool valid)
        {
            OutPut op = new OutPut();
            valid = true;

            if (num.Length != 8)
            {
                valid = false;
            }

            valid = int.TryParse(num, out int tmp);

            if (!valid)
            {
                op.PrintLine(debug, "Invalid number!");
            }
        }
    }
}