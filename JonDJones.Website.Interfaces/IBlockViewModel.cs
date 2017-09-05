namespace JonDJones.Website.Interfaces
{
    using EPiServer.Core;

    public interface IBlockViewModel<out T> where T : BlockData
    {
        T CurrentBlock { get; }
    }
}