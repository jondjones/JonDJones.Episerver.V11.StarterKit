namespace JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3
{
    using AutoDataConnector;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(
                new ParameterValueProvider(
                    new AutoFixtureDataProvider(
                        new AutoMoqCustomization(),
                        new MultipleCustomization())))
        {
        }
    }
}