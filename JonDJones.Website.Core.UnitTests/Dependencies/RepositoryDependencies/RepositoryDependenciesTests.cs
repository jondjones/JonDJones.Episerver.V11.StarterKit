namespace JonDJones.Website.Core.UnitTests.Dependencies.RepositoryDependencies
{
    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_RepositoryDependencies_ : TestBaseClass
    {
        private EpiserverContentRepositories episerverContentRepositories;
        
        private Mock<IMenuPageRepository> menuRepo;

        private Mock<IStartPageRepository> startPageRepo;

        private Mock<IKeyValueRepository> keyValueRepository;

        private Mock<ISiteSettingsPageRepository> siteSettingsPageRepository;

        [SetUp]
        public void SetUp()
        {
            menuRepo = new Mock<IMenuPageRepository>();
            startPageRepo = new Mock<IStartPageRepository>();
            keyValueRepository = new Mock<IKeyValueRepository>();
            siteSettingsPageRepository = new Mock<ISiteSettingsPageRepository>();

            episerverContentRepositories = new EpiserverContentRepositories(
                menuRepo.Object,
                startPageRepo.Object,
                siteSettingsPageRepository.Object,
                keyValueRepository.Object);                
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(EpiserverContentRepositories));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(EpiserverContentRepositories));
        }

        [Test]
        public void MenuPageRepository_Should_Be_Correctly_Set()
        {
            episerverContentRepositories.MenuPageRepository.Should().NotBeNull();
        }

        [Test]
        public void StartPageRepository_Should_Be_Correctly_Set()
        {
            episerverContentRepositories.StartPageRepository.Should().NotBeNull();
        }
    }
}
