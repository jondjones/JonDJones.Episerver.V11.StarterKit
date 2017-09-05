namespace JonDJones.Fixtures.Entities
{
    public class StandardPageDetails
    {
        public string Name { get; set; }

        public string SeoTitle { get; set; }

        public string Keywords { get; set; }

        public string Description { get; set; }

        public void PopulateAll(string populate)
        {
            Name = populate;
            SeoTitle = populate;
            Keywords = populate;
            Description = populate;
        }
    }
}