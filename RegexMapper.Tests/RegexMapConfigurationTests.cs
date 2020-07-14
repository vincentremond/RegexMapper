namespace RegexMapper.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class RegexMapConfigurationTests
    {
        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        [TestCase("Test", ExpectedResult = "Test")]
        [TestCase("Test ", ExpectedResult = "Test")]
        [TestCase(" Test", ExpectedResult = "Test")]
        [TestCase(" Test ", ExpectedResult = "Test")]
        public string ProcessGlobalStringOperations_Trim(string value)
        {
            var config = new RegexMapConfiguration {GlobalStringOperation = StringOperation.Trim};

            return config.ProcessGlobalStringOperations(value);
        }

        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        [TestCase("test", ExpectedResult = "Test")]
        [TestCase("Test", ExpectedResult = "Test")]
        [TestCase("åäö", ExpectedResult = "Åäö")]
        public string ProcessGlobalStringOperations_UppercaseFirst(string value)
        {
            var config = new RegexMapConfiguration {GlobalStringOperation = StringOperation.UpperCaseFirst};

            return config.ProcessGlobalStringOperations(value);
        }

        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        [TestCase("Test", ExpectedResult = "Test")]
        [TestCase("&aring;&auml;&ouml;", ExpectedResult = "åäö")]
        public string ProcessGlobalStringOperations_HtmlDecode(string value)
        {
            var config = new RegexMapConfiguration {GlobalStringOperation = StringOperation.HtmlDecode};

            return config.ProcessGlobalStringOperations(value);
        }

        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        [TestCase("test", ExpectedResult = "Test")]
        [TestCase("&aring;&auml;&ouml;", ExpectedResult = "Åäö")]
        public string ProcessGlobalStringOperations_HtmlDecode_UpperCaseFirst(string value)
        {
            var config = new RegexMapConfiguration
                {GlobalStringOperation = StringOperation.HtmlDecode | StringOperation.UpperCaseFirst};

            return config.ProcessGlobalStringOperations(value);
        }

        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" test ", ExpectedResult = "Test")]
        [TestCase(" &aring;&auml;&ouml; ", ExpectedResult = "Åäö")]
        public string ProcessGlobalStringOperations_HtmlDecode_UpperCaseFirst_Trim(string value)
        {
            var config = new RegexMapConfiguration
            {
                GlobalStringOperation =
                    StringOperation.HtmlDecode | StringOperation.UpperCaseFirst | StringOperation.Trim
            };

            return config.ProcessGlobalStringOperations(value);
        }
    }
}
