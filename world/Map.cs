
class Map {
    public static int roomsizer;
    public static int roomAmount;
    public static int buildRow;

    private Space start; 

    public static Space[,] spaceMap = new Space[0,0];

    public Map(int rooms, int width, int roomSize) {
        roomsizer = roomSize;
        roomAmount = rooms;
        buildRow = rooms*roomSize+1;
        spaceMap  = new Space[buildRow, width];

        int entryCoord = width/2;
        byte layer = 1;
        int[] wing = [0,0];
        Random rnd = new Random();

        for (int row = 0; row < buildRow; row++) {
            bool rommSizer = row%roomSize == 0;

            if (rommSizer && row != 0) {
            /*
                This is where the door entry point is choosen, plus, the layer integer.
                Layer is responsible for showing what layer the player is moving is
                and what layer for the map to get drawn
            */
                layer++;
                entryCoord = rnd.Next(wing[0], wing[1]);
            } else {
                /*
                    From the entry before, two numbers are now chosen.
                    E.g: The first entry point is 10, then wing[0] is between, 1 to 10
                    and wing[1] is between 10 and the width size, -1. 
                    The -1 makes sure it looks like there always is walls
                */
                wing[0] = rnd.Next(1, entryCoord);
                wing[1] = rnd.Next(entryCoord, width-1);
            }

            for (int col = 0; col < width; col++) {
                Key key = keyGen(rommSizer, col%width, wing, entryCoord);

                // keys helps with both render and what type of space you're in.
                // Render is later set through a switch case in Node
                spaceMap[row,col] = new Space(key, row, col, layer); 

                if (row > 1 && col > 0) {
                    mapSpace(row, col);
                }
            }
        }
        start = spaceMap[1, width/2];
    }

    // Validates if the current column is between wing[0] amd wing[1]
    private bool between(int col, int[] wing) => col >= wing[0] && col <= wing[1];

    private Key keyGen(bool roomSizer, int col, int[] wing, int entry) {
    /*
        Returns a key from between 0, 1 & 2.
        The keys returned are defined as either being a room or door.
        Otherwise the key defines the space as a wall. The roomsizer bool
        comes from the wanted size of the room, which uses modulus % to size.

        When the modulus of the current row == 0, then the row is now a door entry.
        This time, only one point is chosen as a playable space, and get the key 2.
        
        If none of theese is defined, then the space is a wall and wont be added as edges.
    */
        if (roomSizer && entry == col) {
            return Key.door;
        }
        else if (!roomSizer && between(col, wing)) {
            return Key.ground;
        } 
        else {
            return Key.wall;
        }
    }

    private void mapSpace(int row, int col) {
    /*
        This method adds edges to the up left space, from the current generated one
        [fRow] and [fCol] defines the up left position by going one subtracting 1 from each.

        The if statements below checks if the up left space is not a wall. If found not to be
        a wall, then it will add edges around it that is also not a wall.
        The edges are addes like a cross + since the player moves with (w a s d).
    */
        int fRow = row-1;
        int fCol = col-1;

        Space upLeftSpace = spaceMap[fRow, fCol];

        /*
            The key 0 defines the space as a wall
            Adds space above with the current space name plus direction
            E.g: Current space is [2,3] and the space above is not a wall.
            The edge [1,3] is now [w2,3] because you can move up from [2,3]
        */
        if (upLeftSpace.key != Key.wall) {
            if (spaceMap[fRow-1, fCol].key != Key.wall) {
                // Add the space above as an edge to the [upLeftSpace] if key is not 0
                upLeftSpace.addEdge($"w{fRow},{fCol}", spaceMap[fRow-1, fCol]);
            }
            if(spaceMap[fRow, fCol-1].key != Key.wall) {
                // Add the left space as an edge to the [upLeftSpace] if key is not 0
                upLeftSpace.addEdge($"a{fRow},{fCol}", spaceMap[fRow, fCol-1]);
            }
            if (spaceMap[row, fCol].key != Key.wall) {
                // Add the space below as an edge to the [upLeftSpace] if key is not 0
                upLeftSpace.addEdge($"s{fRow},{fCol}", spaceMap[row, fCol]);
            } 
            if(spaceMap[fRow, col].key != Key.wall) {
                // Add the right space as an edge to the [upLeftSpace] if key is not 0
                upLeftSpace.addEdge($"d{fRow},{fCol}", spaceMap[fRow, col]);
            }
        }
    }

    public Space getStart() => start;
}

enum Key {
    wall,
    ground,
    door,
}