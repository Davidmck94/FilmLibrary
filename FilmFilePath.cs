namespace FilmLibrary
{
    public class FilmFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // public property
        public string filePath { get; set; }
        public List<Film> Films { get; set; }

        public FilmFile(string path)
        {
            Films = new List<Film>();
            filePath = path;
            // reading info from file
            try
            {
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Film film = new Film();
                    string line = sr.ReadLine();
                    int index = line.IndexOf('"');
                    if (index == -1)
                    {
                        
                        // film properties will be separated with ','
                        string[] filmDetails = line.Split(',');
                        film.filmId = Int64.Parse(filmDetails[0]);
                        film.title = filmDetails[1];
                        film.director = filmDetails[2].Split('|').ToList();
                        film.genres = filmDetails[3].Split('|').ToList();
    
                    }
                    else
                    {
                        
                        // take out the filmId
                        film.filmId = Int64.Parse(line.Substring(0, index - 1));
                        // remove filmId and the ',' from the string
                        line = line.Substring(index + 1);
                        // find the next quote
                        index = line.IndexOf('"');
                        // take ou the filmTitle
                        film.title = line.Substring(0, index);
                        // remove title and final ',' from the string
                        line = line.Substring(index + 2);
                        // replace the "|" with ", "
                        film.director = line.Split('|').ToList();
                        film.genres = line.Split('|').ToList();
                    }
                    Films.Add(film);
                }
                // close the file when finished
                sr.Close();
                logger.Info("Films in file {Count}", Films.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // public method
        public bool isNotAlreadyInTheLibrary(string title)
        {
            if (Films.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate film title {Title}", title);
                return false;
            }
            return true;
        }

        public bool isInTheLibrary(string title)
        {
            if (Films.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Film title is present: {Title}", title);
                return true;
            }
            return false;
        }

        public void AddFilm(Film film)
        {
            try
            {
                // make a film id
                film.filmId = Films.Max(m => m.filmId) + 1;
                // add title
                string title = film.title;
                // add director
                List<string> director = film.director;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{film.filmId},{film.title},{film.director},{string.Join("|", film.genres)}");
                sw.Close();
                // add film properties to csv file
                Films.Add(film);
                // log
                logger.Info("Film id {Id} added", film.filmId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
