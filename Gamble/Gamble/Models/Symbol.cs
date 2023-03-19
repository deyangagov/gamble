namespace Gamble.Models
{
    public class Symbol
    {
        public int Chance { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
        public bool IsWildcard { get; private set; }
        public Symbol(string name, double value, int chance, bool isWildCard = false)
        {
            Name = name; 
            Value = value; 
            Chance = chance;
            IsWildcard = isWildCard;
        }
    }
}
