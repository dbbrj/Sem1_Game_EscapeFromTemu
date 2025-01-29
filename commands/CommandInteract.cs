// Command for interactions
class CommandInteract : BaseCommand, ICommand{
    public CommandInteract() {
        description = "Interact with people or items";
    }
  
    public void execute(Context context, char command) {
        object? interaction = context.getCurrent.interaction;
        switch(interaction) {
            case Person:
                context.drawMap();
                ((Person) interaction).interact(context.getCurrent.layer, Items.pocket);
                break;
            case Item:
                if(Items.pocket == null) {
                    Items.pocket = (Item) interaction;
                    context.getCurrent.interaction = null;
                    context.drawMap();
                } else {
                    Console.WriteLine();
                    context.drawMap("You have no more room in your pocket");
                }
                break;
            default:
                context.drawMap(""+interaction);
                break;
        }
        
    }
}