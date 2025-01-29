
class Game {
    private static int rooms = 6; //max is 9 - check the number of rooms in the Events.txt file
    private static int width = 26;
    private static int roomSize = 6;
    static Map world = new Map(
        rooms,   // rooms
        width,   // width
        roomSize // roomsize
    );

    static Space start = world.getStart();
    static Context context = new Context(start);
    static Registry registry = new Registry(context);

    private static void InitRegistry() {
        registry.register('w', new CommandMove("up"));
        registry.register('s', new CommandMove("down"));
        registry.register('a', new CommandMove("left"));
        registry.register('d', new CommandMove("right"));
        registry.register('i', new CommandInteract());
    }

    static void Main() {
        Console.Clear();
        Console.WriteLine("Welcome to Escape From Temu!\n");
        Console.Write("Please tell me your name: ");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("HA! You really think your name matters here?\n\nGet back to work!\n");
        Console.WriteLine("Press any key to start game.");
        Console.CursorVisible = false;
        Console.ReadKey();
        
        InitRegistry();
        context.drawMap();

        while (!context.done) {
            char key = Console.ReadKey().KeyChar;
            registry.dispatch(key);
        }

        Console.Clear();

        if(context.getCurrent!.row > 0) {
            Console.WriteLine("You made it out!\n");
            Console.WriteLine("Your score is: {0:P}\n", Score.getScore()/(rooms*2));
        } else {
            Console.WriteLine("You gave up and went back into the factory..\n");      
        }
    }
}
