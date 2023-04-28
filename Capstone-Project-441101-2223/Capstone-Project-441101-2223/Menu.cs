using System.Text;
namespace Capstone_Project_441101_2223
{

    /* Begin Reference - Copied Code
     * University of Hull - Summitive Assessment 14
     */
    public static class ConsoleHelpers
    {
        public static int GetIntegerInRange(int pMin, int pMax, string pMessage)
        {
            if (pMin > pMax)
            {
                throw new Exception($"\nMinimum value {pMin} cannot be greater than maximum value {pMax}");
            }

            int result;

            do
            {
                Console.WriteLine(pMessage);
                Console.WriteLine($"\nPlease enter a number between {pMin} and {pMax} inclusive.");

                string userInput = Console.ReadLine();

                try
                {
                    result = int.Parse(userInput);
                }
                catch
                {
                    Console.WriteLine($"{userInput} is not a number");
                    continue;
                }

                if (result >= pMin && result <= pMax)
                {
                    return result;
                }
                Console.WriteLine($"\n{result} is not between {pMin} and {pMax} inclusive.");
            } while (true);
        }
    }

    abstract class ConsoleMenu : MenuItem
    {
        protected List<MenuItem> _menuItems = new List<MenuItem>();

        public bool IsActive { get; set; }

        public abstract void CreateMenu();

        public override void Select()
        {
            IsActive = true;
            do
            {
                CreateMenu();
                string output = $"{MenuText()}{Environment.NewLine}";
                int selection = ConsoleHelpers.GetIntegerInRange(1, _menuItems.Count, this.ToString()) - 1;
                _menuItems[selection].Select();
            } while (IsActive);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(MenuText());
            for (int i = 0; i < _menuItems.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {_menuItems[i].MenuText()}");
            }
            return sb.ToString();
        }
    }

    abstract class MenuItem
    {
        public abstract string MenuText();
        public abstract void Select();
    }

    class ExitMenuItem : MenuItem
    {
        private ConsoleMenu _menu;

        public ExitMenuItem(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        public override string MenuText()
        {
            return "Exit";
        }

        public override void Select()
        {
            _menu.IsActive = false;
        }
    }
}

/*  End Reference */