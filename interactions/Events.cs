class Events : Reader {
    public Dictionary<string, string> getEvents => readFile("interactions/data/Events.txt");
    
    private List<string> getRooms() { //returns a list of rooms, created from the Events.txt file
        List<string> rooms = getEvents.Keys.ToList();
        HashSet<string> names = new HashSet<string>(); // We use a hashset so we can filter duplicates faster.

        for (int i = 0; i < rooms.Count; i++) {
            names.Add(rooms[i][1..]); // Removes the first character of the string and keeps the rest
        }

        return names.ToList();
    }
    
    public List<string> randomizeRooms() {
        List<string> listOfRooms = getRooms(); //gets the list of all rooms
        Random rnd = new Random();

        List<string> rndRoomSeq = new List<string>(new string[Map.roomAmount]); //list of the rooms used in an instance of the game
        rndRoomSeq[0] = listOfRooms[0]; //sets the first room to sleeping hall
    
        for (int i = 1; i < Map.roomAmount; i++) {
            rndRoomSeq[i] = listOfRooms[rnd.Next(1, listOfRooms.Count)]; //sets the rooms (except the first one) to a random room from the list of all rooms
            listOfRooms.Remove(rndRoomSeq[i]); //removes the used rooms, so there are no dupes
        }
        return rndRoomSeq;
    }
}