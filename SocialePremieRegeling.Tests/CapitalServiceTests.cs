using SocialePremieRegeling.Services;

namespace SocialePremieRegeling.Tests
{
    public class CapitalServiceTests
    {
        private readonly CapitalService _sut;

        public CapitalServiceTests()
        {
            _sut = new CapitalService();    
        }

        [Theory]
        [InlineData(0, 5, null, null)]
        [InlineData(0, 5, 0, 5)]
        public void It_calculates_total_Capital_for_a_period(int start, int end, int? exPartnerStart, int? exPartnerEnd)
        {
            CapitalAndReturns result;

            result = exPartnerStart.HasValue ? _sut.CalculateCapitalAndReturns(start, end, new ExPartnerConfig(exPartnerStart.GetValueOrDefault(), exPartnerEnd.GetValueOrDefault())) : _sut.CalculateCapitalAndReturns(start, end);

            Assert.NotNull(result);
        }
    }
}