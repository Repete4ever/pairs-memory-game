using System.Collections.Generic;

namespace Pairs
{
    public class GameOptions
    {
        public string SelectedIconSet { get; set; } = "misc";

        public List<string> AvailableIconSets { get; } = new List<string>()
        {
            "misc",
            "monster"
        };

        public int SelectedGameRowSize { get; set; } = 6;

        public int SelectedGameColSize { get; set; } = 6;

        public int MinimumGameRowSize { get; set; } = 4;

        public int MinimumGameColSize { get; set; } = 4;

        public int MaximumGameRowSize { get; set; } = 6;

        public int MaximumGameColSize { get; set; } = 6;

        public int IncrementRow { get; set; } = 2;
    }
}
