using Absa.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Absa.Application.UnitTests.Common.Exceptions
{
    public class ValidationExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }
    }
}
