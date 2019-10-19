namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public sealed class Selection<T>
    {
        public Selection()
        {
        }

        public Selection(T item, bool selected, string display)
        {
            Item = item;
            Selected = selected;
            Display = display;
        }

        public T Item { get; set; }
        public bool Selected { get; set; }
        public string Display { get; set; }

        public static Selection<T> CreateUnselected(T item, string displayName)
        {
            return new Selection<T>(item, false, displayName);
        }

        public static Selection<T> CreateSelected(T item, string displayName)
        {
            return new Selection<T>(item, true, displayName);
        }
    }
}