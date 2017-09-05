namespace JonDJones.Website.TestShared.Base
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    public class TestBaseClass
    {
        public TestBaseClass()
        {
            AutoFixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        public IFixture AutoFixture { get; }
    }
}
