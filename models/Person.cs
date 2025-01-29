class Person : Interactables {
    public string name;
    public string[] dialog = new string[5];
    public Item item;
    public bool itemFound = false;
    private bool first = true;
    private bool second = false;
    private bool third = false;
    private bool last = false;


    public Person (string name, string[] dialog, Item item) {
        this.name = name; 
        this.dialog = dialog;
        this.item = item;
    }

    public void interact(int layer, Item? found) {
        if (!first && found != null && found!.id == item!.id) {
            itemFound = true;
            pocket = null;
            Score.increment(last ? 2 : 1);
        }

        if (itemFound) {
            if (third) {
                Console.WriteLine($"\"Thank you so much, for finding my {item.name}!\"\n"); 
            } 
            Console.WriteLine(dialog[4]);
            third = false;
        }if (third) {
            Console.WriteLine(dialog[3]);
            Console.WriteLine($"\nFind: {item.name}");
            last = true;
        } if (second) {
            Console.WriteLine(dialog[2]);
            Console.WriteLine($"\n\"Have you seen my {item.name}?\"");
            spawnItem(item, layer);
            second = false;
            third = true;
        } if (first) {
            Console.WriteLine(dialog[1]);
            first = false;
            second = true;
        }
    }
}
