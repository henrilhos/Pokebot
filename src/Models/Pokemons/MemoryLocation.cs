namespace Pokebot.Models.Pokemons
{
    public record MemoryLocation(long Address, int Offset, int Size, string? Domain = null);
}
