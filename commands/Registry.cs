
class Registry {
    private Context context;
    private Dictionary<char, ICommand> commands = new Dictionary<char, ICommand>();

    public Registry(Context context) {
        this.context = context;
    }

    public void register(char name, ICommand command) {
        commands.Add(name, command);
    }

    private void help() {
        context.drawMap();
        char[] commandInputs = commands.Keys.ToArray();
        foreach (char input in commandInputs) {
            string description = commands[input].getDescription();
            Console.WriteLine($"{input}  -  {description}");
        }
    }

    public void dispatch(char input) {
        if (commands.ContainsKey(input)) {
            commands[input].execute(context, input);
        } else {
            help();
        }
    }
}

