/* 
  Context class to hold all context relevant to a session.
*/

class Context : Interactables {

    public static Space? currentSpace;
    public bool done = false;
    public Space? getCurrent => currentSpace;
    public static int[] layerEventCount = new int[Map.roomsizer+1];

    private int lastLayer = 1;

    public Context(Space node) {
        currentSpace = node;
        startInteractions();
    }

    public void drawMap(string text = "") {
        Console.Clear();

        int cRow = currentSpace!.row;
        int cCol = currentSpace.col;
        int cLayer = currentSpace.layer;

        Space[,] map = Map.spaceMap; 
        int roomSize = Map.roomsizer;

        string inventory = pocket != null ? $"Pocket: {pocket.name}" : "";

        Console.WriteLine($"Room {cLayer}/{Map.roomAmount}\t{inventory}");

        // This method draws the map with the use of layer amount.
        for (int row = cLayer*roomSize-roomSize; row < cLayer*roomSize+1; row++) {
            for (int col = 0; col < map.GetLength(1); col++) {
                if(cRow == row && cCol == col) {
                    Console.Write("¤ ");
                } else {
                    Console.Write(toRender(map[row, col]));
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("\n"+text);
    }
    
    private string toRender(Space cSpace) {
        string interaction = $"{cSpace.render} ";
        switch(cSpace.interaction) {
            case Person:
                interaction = objective((Person) cSpace.interaction);
                break;
            case Item:
                interaction = "· ";
                break;
        }
        return interaction;
    }

    private string objective(Person check) {
        if (check.itemFound) {
            return "¡ ";
        } else if (pocket != null) {
            if(check.item.id == pocket!.id) {
                return "? ";
            }
        }
        return "! ";
    }

    public void transition(char input, int row, int col) {
        Space? next = currentSpace!.move($"{input}{row},{col}");

        if (next != null) {
            currentSpace = next;
            done = currentSpace.edges.Count == 0;
        }

        if (!done) {
            object? interaction = currentSpace.interaction;
            
            Console.WriteLine();
            string foundInteractions ;

            foundInteractions  = interaction switch {
                Person => $"{((Person) interaction).dialog[0]} \nMaybe you should talk to them.",
                Item => $"You've found {((Item) interaction).name}. Maybe somebody is looking for it?",
                string => getEvent(interaction),
                _ => "",
            };

            drawMap(foundInteractions);
                
            if (currentSpace.layer != lastLayer) {
                lastLayer = currentSpace.layer;
            } 

        }
    }

    private string getEvent(object? interaction) {
        string read = "";
        if (currentSpace!.layer != lastLayer) {
            read = interaction+"";
        } else if (currentSpace.key != Key.door) {
            read = interaction+"";
        } 
        return read;
    }
}

