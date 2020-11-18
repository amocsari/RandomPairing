using System;

namespace RandomPairer.Common.Exceptions
{
    public class RandomPairerValidationException : Exception
    {
        public RandomPairerValidationException(string message) : base(message)
        {
        }
    }
}
