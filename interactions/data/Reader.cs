
abstract class Reader {
    public Dictionary<string, string> readFile(string dir) {
        Dictionary<string, string> events = new Dictionary<string, string>();
        try {
            string[] allLines = File.ReadAllLines(dir);
            for (int i = 0; i < allLines.Length; i++) {
                string[] parts = allLines[i].Split(" £ ");
                events.Add(parts[0], parts[1]);
            }
            return events;
        }
        
        catch (Exception ex){
            Console.WriteLine("Couldn't read file");
            Console.WriteLine(ex);
            return [];
        }
    } 
}