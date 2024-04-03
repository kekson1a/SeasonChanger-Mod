using SeasonChanger.UI;

using GameConsole;

namespace SeasonChanger.ConsoleCommands
{
    internal class OpenSeasonMenu : ICommand
    {
        public string Name => "Setdate";

        public string Description => "Command for opening the 'SET DATE' menu (SeasonChanger mod)";

        public string Command => "setdate";

        public void Execute(GameConsole.Console con, string[] args)
        {
            DateMenu.Instance.OpenSeasonMenu();
        }
    }
}
