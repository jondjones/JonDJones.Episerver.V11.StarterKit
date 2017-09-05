namespace JonDJones.Fixtures.Helpers
{
    using JonDJones.Fixtures.Fixtures.Factory;
    using JonDJones.Website.Shared.Helpers;

    public class ContentHelper
    {
        private readonly BlockFixturesFactory _blockFixturesFactory;

        public ContentHelper(BlockFixturesFactory blockFixturesFactory)
        {
            Guard.ValidateObject(blockFixturesFactory);
            _blockFixturesFactory = blockFixturesFactory;
        }

        public static string LoremIpsum =>
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

        public static string LoremIpsumShort => "Lorem Ipsum is simply dummy";
    }
}
