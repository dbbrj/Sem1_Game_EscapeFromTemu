
class Items : Reader {
    public static Item? pocket;
    protected void createItems() {
        Dictionary<string, string> getItems = readFile("interactions/data/Items.txt");
        foreach (KeyValuePair<string, string> item in getItems) {
            availableItems.Add(new Item(int.Parse(item.Key), item.Value));
        }
    }
    private static List<Item> availableItems = new List<Item>();

    public Item randomItem() { //Returns an amount of items, based on the amount integer
        Random rnd = new Random();

        int rndItem = rnd.Next(0, availableItems.Count());

        Item item = availableItems[rndItem];

        availableItems.RemoveAt(rndItem);

        return item;
    }
}