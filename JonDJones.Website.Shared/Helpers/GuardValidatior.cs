namespace JonDJones.Website.Shared.Helpers
{
    using System;

    public static class Guard
    {
        public static void ValidateObject(object objectToTest)
        {
            if (objectToTest == null)
            {
                throw new ArgumentNullException("object passed in has not been instantiated.");
            }
        }
    }
}
