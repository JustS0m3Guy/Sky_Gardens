namespace SkyGarden
{
    public class Pair<TFirst, TSecond>
    {
        public TFirst First { get; set; }
        public TSecond Second { get; set; }

        public Pair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }
        public Pair(TFirst first)
        {
            First = first;
            Second = default!;
        }
    }
}