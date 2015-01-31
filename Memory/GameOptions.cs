using System.Collections.Generic;

namespace Pairs
{
    public class GameOptions
    {

        private string selectedIconSet = "misc";

        public string SelectedIconSet
        {
            get { return selectedIconSet; }
            set { selectedIconSet = value; }
        }

        private List<string> availableIconSets = new List<string>()
        {
            "misc",
            "monster"
        };

        public List<string> AvailableIconSets
        {
            get { return availableIconSets; }
        }

        private int selectedGameRowSize = 6;
        private int selectedGameColSize = 6;

        public int SelectedGameRowSize
        {
            get { return selectedGameRowSize; }
            set { selectedGameRowSize = value; }
        }

        public int SelectedGameColSize
        {
            get { return selectedGameColSize; }
            set { selectedGameColSize = value; }
        }

        private int minimumGameRowSize = 4;
        private int minimumGameColSize = 4;
        public int MinimumGameRowSize
        {
            get { return minimumGameRowSize; }
            set { minimumGameRowSize = value; }
        }

        public int MinimumGameColSize
        {
            get { return minimumGameColSize; }
            set { minimumGameColSize = value; }
        }

        private int maximumGameRowSize = 6;
        private int maximumGameColSize = 6;
        public int MaximumGameRowSize
        {
            get { return maximumGameRowSize; }
            set { maximumGameRowSize = value; }
        }

        public int MaximumGameColSize
        {
            get { return maximumGameColSize; }
            set { maximumGameColSize = value; }
        }

        private int incrementRow = 2;
        public int IncrementRow
        {
            get { return incrementRow; }
            set { incrementRow = value; }
        }


    }
}
