using FluentAssertions;

using Moq;

using NUnit.Framework;

using JonDJones.Website.TestShared.Base;

using JonDJones.Website.Core.Dependencies.RepositoryDependencies;
using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
using JonDJones.Website.UnitTests.Helper;

namespace JonDJones.Website.Core.UnitTests.Dependencies.RepositoryDependencies
{
    [TestFixture]
    public class When_I_Instantiate_RepositoryDependencies_ : TestBaseClass
    {
        private PageTypeServices episerverContentRepositories;

        private Mock<IMenuService> menuRepo;

        private Mock<IStartPageService> startPageRepo;

        private Mock<IKeyValueService> keyValueRepository;

        private Mock<ISiteSettingsService> siteSettingsPageRepository;

        private Mock<IContentPageService> contentPageService;

        [SetUp]
        public void SetUp()
        {
            menuRepo = new Mock<IMenuService>();
            startPageRepo = new Mock<IStartPageService>();
            keyValueRepository = new Mock<IKeyValueService>();
            siteSettingsPageRepository = new Mock<ISiteSettingsService>();
            contentPageService = new Mock<IContentPageService>();

            episerverContentRepositories = new PageTypeServices(
                menuRepo.Object,
                startPageRepo.Object,
                siteSettingsPageRepository.Object,
                contentPageService.Object,
                keyValueRepository.Object);
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(PageTypeServices));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(PageTypeServices));
        }

        [Test]
        public void MenuPageRepository_Should_Be_Correctly_Set()
        {
            episerverContentRepositories.MenuService.Should().NotBeNull();
        }

        [Test]
        public void StartPageRepository_Should_Be_Correctly_Set()
        {
            episerverContentRepositories.StartPageService.Should().NotBeNull();
        }
    }
}
