namespace JonDJones.Website.UnitTests.Helper
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Moq;

    using NUnit.Framework;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Idioms;

    public static class ParameterValidationHelper
    {
        public static void ShouldNotAcceptNullConstructorArguments(Type type)
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            assertion.Verify(type.GetConstructors());
        }

        public static void ConstructorMustThrowArgumentNullException(Type type)
        {
            foreach (var constructor in type.GetConstructors())
            {
                var parameters = constructor.GetParameters();
                var mocks = parameters.Select(
                    p =>
                        {
                            var mockType = typeof(Mock<>).MakeGenericType(p.ParameterType);
                            return (Mock)Activator.CreateInstance(mockType);
                        }).ToArray();

                for (var i = 0; i < parameters.Length; i++)
                {
                    var mocksCopy = mocks.Select(m => m.Object).ToArray();
                    mocksCopy[i] = null;
                    try
                    {
                        constructor.Invoke(mocksCopy);
                        Assert.Fail(
                            "ArgumentNullException expected for parameter {0} of constructor, but no exception was thrown",
                            parameters[i].Name);
                    }
                    catch (TargetInvocationException ex)
                    {
                        Assert.AreEqual(
                            typeof(ArgumentNullException),
                            ex.InnerException.GetType(),
                            $"ArgumentNullException expected for parameter {parameters[i].Name} of constructor, but exception of type {ex.InnerException.GetType()}  was thrown");
                    }
                }
            }
        }
    }
}
