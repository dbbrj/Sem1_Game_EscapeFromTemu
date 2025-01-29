// Command for transitioning between spaces
class CommandMove : BaseCommand, ICommand {
    public CommandMove(string direction) {
        description = $"Moves {direction}";
    } 
  
    public void execute(Context context, char input) {
        // Gets the current position of row and column 
        int cRow = context.getCurrent!.row;
        int cCol = context.getCurrent.col;

        /* 
            Gets the input command, w a s or d, and makes a string together woth row and column
            Resulting output will be space name e.g: w2,2, which means that the player moves from
            2,2 to 1,2, because of the naming when generating the map
        */ 
        context.transition(input, cRow, cCol);
    }
}
