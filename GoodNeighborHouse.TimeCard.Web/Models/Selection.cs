namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public sealed class Selection<T>
    {
        public Selection()
        {
        }

        public Selection(T item, bool selected)
        {
            Item = item;
            Selected = selected;
        }

        public T Item { get; set; }
        public bool Selected { get; set; }

        public static Selection<T> CreateUnselected(T item)
        {
            return new Selection<T>(item, false);
        }

        public static Selection<T> CreateSelected(T item)
        {
            return new Selection<T>(item, true);
        }
    }
}