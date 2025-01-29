
class Interactables : Items
{
    protected static List<Space> spawnableRooms = new List<Space>();

    protected static List<Space> spawnableEntries = new List<Space>();

    Random rnd = new Random();

    protected void startInteractions()
    {
        createItems();
        spawnPeople();
        spawnEvents();
    }

    private void spawnPeople()
    {
        People people = new People();
        List<Person> npcList = people.createPeople();
        for (int layer = 1; layer < Map.roomAmount + 1; layer++)
        {
            Space newSpace = randomTileLayer(true, layer);
            newSpace.interaction = npcList[layer];
            replaceSpace(newSpace);
        }
    }

    protected void spawnItem(Item? item, int layer)
    {
        Space newSpace = randomTileLayer(false, layer);
        newSpace.interaction = item;
        replaceSpace(newSpace);
    }

    protected void spawnEvents()
    {
        Events events = new Events();
        Dictionary<string, string> eventDict = events.getEvents;
        List<string> rooms = events.randomizeRooms();

        int layer = 1;

        foreach (string room in rooms)
        {
            for (int i = 1; i <= 2; i++) // Only spawn 3 events per room
            {
                Space newSpace = randomTileLayer(true, layer);
                newSpace.interaction = eventDict[i + room];
                replaceSpace(newSpace);
            }

            // Entry event for each room
            Space entry = spawnableEntries[layer - 1];
            entry.interaction = eventDict[0 + room];
            replaceSpace(entry);
            layer++;
        }
    }

    private Space randomTileLayer(bool include, int layer)
    {
        List<Space> spaceToLayer = new List<Space>();

        foreach (Space space in spawnableRooms)
        {
            if (include)
            {
                if (space.layer == layer)
                {
                    spaceToLayer.Add(space);
                }
            }
            else
            {
                if (space.layer != layer)
                {
                    spaceToLayer.Add(space);
                }
            }
        }

        Space spawnedSpace = spaceToLayer[rnd.Next(0, spaceToLayer.Count)];
        spawnableRooms.Remove(spawnedSpace);
        return spawnedSpace;
    }

    private void replaceSpace(Space newSpace)
    {
        Map.spaceMap[newSpace.row, newSpace.col] = newSpace;
    }
}