namespace Pokebot.Models
{
    public class Symbol
    {
        public long Address { get; }
        public char Letter { get; }
        public int Size { get; }
        public string Name { get; }
        public string? Domain { get; }

        public Symbol(long address, char letter, int size, string name, string? domain = null)
        {
            Address = address;
            Letter = letter;
            Size = size;
            Name = name;
            Domain = domain;
        }
    }
}
