using System;
namespace Capstone_Project_441101_2223
{
    /* Begin Reference - Copied and Modified Code
     * University of Hull (Grey, Simon) - File Loading Strategies Lecture
     */
    public class Filer
    {
        private Dictionary<string, FileLoader> _loaders = new Dictionary<string, FileLoader>();

        public void RegisterLoader(FileLoader loader)
        {
            if (_loaders.ContainsKey(loader.Extension))
            {
                return;
            }

            _loaders.Add(loader.Extension, loader);
        }


        public Filer Load(string filename)
        {
            string extension = filename.Substring(filename.LastIndexOf('.') + 1);

            if (!_loaders.ContainsKey(extension))
            {
                return null;
            }

            return _loaders[extension].Load(filename);
        }

    }

    public abstract class FileLoader
    {
        public string Extension
        {
            get; private set;
        }

        protected FileLoader(string extension)
        {
            Extension = extension;
        }

        public abstract Filer Load(string filename);

    }


    public class TXTLoader : FileLoader
    {
        public TXTLoader() : base("txt")
        {

        }

        public override Filer Load(string filename)
        {
            
            string line;

            bool loaded = true;
            ProjectManager _manager = ProjectManager.GetInstance();

            bool fileExists = File.Exists(filename);

            if (!fileExists)
            {
                Console.WriteLine("\n" + filename + " not found.\n");
                loaded = false;
                return null;
            }

            using (StreamReader reader = new StreamReader(filename))
            {
               
                while ((line = reader.ReadLine()) != null)
                {
                    
                    string[] components = line.Split(',');

                    if (components.Length != 3)
                    {
                        Console.WriteLine("\nError: Line does not contain 3 components.\n");
                        Console.WriteLine($"\n{filename} encountered an error when loading, check project data and remove incomplete projects\n");
                        loaded = false;
                        break;
                    }

                    int intValue;
                    char charValue;
                    float floatValue;

                    if (!int.TryParse(components[0], out intValue))
                    {
                        Console.WriteLine("\nError: First component is not an integer.\n");
                        Console.WriteLine($"\n{filename} encountered an error when loading, check project data and remove incomplete projects\n");
                        loaded = false;
                        break;
                    }

                    if (!char.TryParse(components[1], out charValue))
                    {
                        Console.WriteLine("\nError: Second component is not a character.\n");
                        Console.WriteLine($"\n{filename} encountered an error when loading, check project data and remove incomplete projects\n");
                        loaded = false;
                        break;
                    }

                    if (!float.TryParse(components[2], out floatValue))
                    {
                        Console.WriteLine("\nError: Third component is not a float.\n");
                        Console.WriteLine($"\n{filename} encountered an error when loading, check project data and remove incomplete projects\n");
                        loaded = false;
                        break;
                    }


                    try
                    {
                        Project _project = new Project(int.Parse(components[0]), char.Parse(components[1]), float.Parse(components[2]));

                        //Console.WriteLine(_project.ToString());

                        _manager.AddProject(_project);
                    }
                    catch { loaded = false;  Console.WriteLine($"\n{filename} encountered an error when loading, check project data and remove incomplete projects\n"); }

                    //Console.WriteLine(filename + " loaded successfully");
                }

                if (loaded == true)
                {
                    Console.WriteLine($"\n{filename} loaded successfully\n");
                }
                

                return null;
            }
           
        }
    }
    
    public class TMLLoader : FileLoader
    {
        public TMLLoader() : base("tml")
        {

        }

        public override Filer Load(string filename)
        {
            string line;
            bool loaded = true;

            ProjectManager _manager = ProjectManager.GetInstance();

            bool fileExists = File.Exists(filename);

            if (!fileExists)
            {
                Console.WriteLine("\n" + filename + " not found.\n");
                loaded = false;
                return null;
            }

            using (StreamReader reader = new StreamReader(filename))
            {

                while ((line = reader.ReadLine()) != null)
                {

                    char TypeCode =  ' ';
                    //int ProjectID;
                    //float Transaction;


                    //TYPECODE
                    if(line.StartsWith("L") | line.StartsWith("S") | line.StartsWith("P") | line.StartsWith("R"))
                    {
                        if (line.StartsWith("L")) { TypeCode = 'L'; }
                        if (line.StartsWith("S")) { TypeCode = 'S'; }
                        if (line.StartsWith("P")) { TypeCode = 'P'; }
                        if (line.StartsWith("R")) { TypeCode = 'R'; }
                    }
                    else
                    {
                        Console.WriteLine("\nError: File formatted incorrectly - must be Land, Sale, Purchase or Renovation\n");
                        Console.WriteLine($"\n{filename} encountered an error when loading, check project data and remove incomplete projects\n");
                        loaded = false;
                        break;
                    }
                    
                    //PROJECTID
                    string extractedValue = "";
                    int startIndex = line.IndexOf("(");
                    int endIndex = line.IndexOf(")");

                    if (startIndex == -1 || endIndex == -1)
                    {
                        Console.WriteLine("\nError: no brackets found in input string.\n");
                        Console.WriteLine($"\n{filename} encountered an error when loading, check project data and remove incomplete projects\n");
                        loaded = false;
                        break;
                    }

                    if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                    {
                        extractedValue = line.Substring(startIndex + 1, endIndex - startIndex - 1);
                        
                    }


                    //TRANSACTION
                    string extractedValue1 = "";
                    int startIndex1 = line.IndexOf("=");
                    int endIndex1 = line.IndexOf(";");

                    if (startIndex1 == -1 || endIndex1 == -1)
                    {
                        Console.WriteLine("\nError: no brackets found in input string.\n");
                        Console.WriteLine($"\n{filename} encountered an error when loading, check project data and remove incomplete projects\n");
                        loaded = false;
                        break;
                    }

                    if (startIndex1 != -1 && endIndex1 != -1 && endIndex1 > startIndex1)
                    {
                        extractedValue1 = line.Substring(startIndex1 + 1, endIndex1 - startIndex1 - 1);
                        
                    }


                    try
                    {
                        Project _project = new Project(int.Parse(extractedValue), TypeCode, float.Parse(extractedValue1));

                        //Console.WriteLine(_project.ToString());

                        _manager.AddProject(_project);
                    }

                    catch { loaded = false; Console.WriteLine($"\n{filename} encountered an error when loading, check project data and remove incomplete projects\n"); }
                   
                }

                if (loaded == true)
                {
                    Console.WriteLine($"\n{filename} loaded successfully\n");
                   
                }

                return null;
            }
        }
    }
    
}
    
/* End Reference */