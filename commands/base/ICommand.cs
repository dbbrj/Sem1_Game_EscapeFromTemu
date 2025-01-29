
interface ICommand {
    void execute(Context context, char command);
    string getDescription();
}

