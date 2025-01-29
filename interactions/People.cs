
class People : Reader {    
    private Dictionary<string, string> getPeople => readFile("interactions/data/People.txt");

    public List<Person> createPeople() {
        Dictionary<string, string> peopleDict = getPeople;
        HashSet<string> names = new HashSet<string>();
        List<Person> peopleFormatted = new List<Person>(names.Count);

        Items rndItem = new Items();

        foreach (string name in peopleDict.Keys) {
            names.Add(name[1..]);
        }

        foreach (string name in names) {
            string[] stories = new string[5];
            for(int i = 0; i <= 4; i++) {
                stories[i] = peopleDict[$"{i}{name}"];
            }
            peopleFormatted.Add(new Person(name, stories, rndItem.randomItem()));
        }

        Random rnd = new Random();
        peopleFormatted = peopleFormatted.OrderBy(_ => rnd.Next()).ToList();

        return peopleFormatted;
    }
}

