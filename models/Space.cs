/* Node class for modeling graphs
 */

class Space : Interactables {
    public Key key;
    public int row;
    public int col;
    public byte layer;
    public string render;
    public object? interaction;
    public Dictionary<string, Space> edges = new Dictionary<string, Space>();

    public Space(Key key, int row, int col, byte layer) {
        this.key = key;
        this.row = row;
        this.col = col;
        this.layer = layer;
        render = key switch {
            Key.wall => "■",
            Key.ground => spawnableRoom(this),
            Key.door => spawnableEntry(this),
            _ => throw new Exception("Not a valid key for space!"),
        };
    }

    public Item getItem() {
        Item itemGotten = (Item) interaction!;
        interaction = null;
        return itemGotten;
    }

    private string spawnableRoom(Space current) {
        spawnableRooms.Add(current);
        return " ";
    }

    private string spawnableEntry(Space current) {
        spawnableEntries.Add(current);
        return "─";
    }

    public void addEdge(string name, Space space) {
        edges.Add(name, space);
    }

    public Space? move(string direction) {
        return edges.ContainsKey(direction) ? edges[direction] : null;
    }
}

