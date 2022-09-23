namespace FilmLibrary
{
    class Program
    {
        //class  logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string file = "films.csv";
            logger.Info("Program started");

            FilmFile filmFile = new FilmFile(file);
            string usersChoice = "";
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please choose one of the following options: ");
                Console.WriteLine("1. Show all Films");
                Console.WriteLine("2. Add a Film");
                Console.WriteLine("3. Delete a Film");
                Console.WriteLine("4. Press Enter to close the program");

                //input
                usersChoice = Console.ReadLine();
                logger.Info("User choice: {Choice}", usersChoice);
                if (usersChoice == "2")
                {
                    // Add Film
                    Film film = new Film();
                    Console.WriteLine("Please enter the title of the film you wish to add");
                    film.title = Console.ReadLine();

                    if (filmFile.isNotAlreadyInTheLibrary(film.title))
                    {
                        string input;
                        do
                        {
                            //enter the name of the director
                            Console.WriteLine("Please enter the director of the film (or type 'done' to move on)");
                            //input
                            input = Console.ReadLine();
                            //add the director to the file

                            if (input != "done" && input.Length > 0)
                            {
                                film.director.Add(input);
                            }
                        } while (input != "done");

                        if (film.director.Count == 0)
                        {
                            film.director.Add("(no directors listed");
                        }
                        do
                        {
                            //enter genre
                            Console.WriteLine("Please enter the film genre (or type 'done' to quit the program)");
                            //input
                            input = Console.ReadLine();
                            // asks for additional genres until "done" is typed
                            if (input != "done" && input.Length > 0)
                            {
                                film.genres.Add(input);
                            }
                        } while (input != "done");

                        if (film.genres.Count == 0)
                        {
                            film.genres.Add("(no genres listed)");
                        }

                        // add Film
                        filmFile.AddFilm(film);
                    }
                    else
                    {
                        Console.WriteLine("Film title already exists\n");
                    }
                }

                // Delete Film

                // else if (usersChoice == "3")
                // {
                //     Film film = new Film();
                //     film.title = Console.ReadLine();

                //     if (filmFile.isInTheLibrary(filmFile.title))
                //     {
                //         string input;
                //         do
                //         {
                //             //enter the title of the film you wish to remove
                //             Console.WriteLine("Please enter the title of the film you wish to remove (or type 'done' to quit the program)");
                //             //enter the title of the film
                //             input = Console.ReadLine();
                //             //Checking to remove title
                //             if (input != "done" && input.Length > 0)
                //             {
                //                 film.title.Remove(input);
                //             }

                //         } while (input != "done");
                //     }
                // }

                else if (usersChoice == "1")
                {
                    // Show all the Films in the library
                    foreach (Film m in filmFile.Films)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
            } while (usersChoice == "1" || usersChoice == "2");

            logger.Info("Program ended");
        }
    }
}
