namespace SocialePremieRegeling.Services;

public class CapitalService
{
    private const decimal Deposit = 500;
    private readonly IDictionary<int, (decimal Protection, decimal Over)> _returnPercentagesPerPeriod = new Dictionary<int, (decimal, decimal)>();

    public CapitalService()
    {
        _returnPercentagesPerPeriod[5] = (10m, 10m);
        _returnPercentagesPerPeriod[4] = (10m, 10m);
        _returnPercentagesPerPeriod[3] = (10m, 10m);
        _returnPercentagesPerPeriod[2] = (10m, 10m);
        _returnPercentagesPerPeriod[1] = (10m, 10m);
        _returnPercentagesPerPeriod[0] = (10m, 10m);
    }

    public CapitalAndReturns CalculateCapitalAndReturns(int start, int end, ExPartnerConfig? exPartnerConfig = null)
    {
        var totalProtectionReturn = 0m;
        var capital = 0m;
        var totalDeposit = 0m;

        var accumulatedTotalReturnFactor = 1m;
        var accumulatedProtectionReturnFactor = 1m;

        for (var t = end; t >= start; t--)
        {
            decimal exPartnerModifier;
            if (t >= exPartnerConfig?.StartPeriod && t <= exPartnerConfig.EndPeriod)
            {
                exPartnerModifier = 0.5m;
            }
            else
            {
                exPartnerModifier = 1m;
            }

            if (t == end) 
            {
                capital += Deposit * exPartnerModifier;
                totalDeposit += Deposit * exPartnerModifier;
                continue;
            }

            // Get the returnPercentages for the following period t + 1
            var returnPercentages = _returnPercentagesPerPeriod[t + 1];

            var totalReturnFactor = GetReturnFactor(returnPercentages.Protection + returnPercentages.Over);
            var protectionReturnFactor = GetReturnFactor(returnPercentages.Protection);

            // Calculate the new accumulated ReturnFactors
            var updatedAccumulatedTotalReturnFactor = totalReturnFactor * accumulatedTotalReturnFactor;
            var updatedAccumulatedProtectionReturnFactor = protectionReturnFactor * accumulatedProtectionReturnFactor;

            totalProtectionReturn += Deposit * exPartnerModifier * updatedAccumulatedProtectionReturnFactor - (Deposit * exPartnerModifier);

            capital += Deposit * exPartnerModifier * updatedAccumulatedTotalReturnFactor;
            totalDeposit += Deposit * exPartnerModifier;

            accumulatedTotalReturnFactor = updatedAccumulatedTotalReturnFactor;
            accumulatedProtectionReturnFactor = updatedAccumulatedProtectionReturnFactor;
        }

        return new CapitalAndReturns(capital, capital - totalDeposit, totalProtectionReturn, totalDeposit);
    }

    private decimal GetReturnFactor(decimal returnPercentage)
    {
        return 1 + returnPercentage / 100;
    }
}

public class CapitalAndReturns
{
    public CapitalAndReturns(decimal capital, decimal totalReturns, decimal protectionReturns, decimal totalDeposit)
    {
        Capital = capital;
        TotalReturns = totalReturns;
        ProtectionReturns = protectionReturns;
        TotalDeposit = totalDeposit;
    }

    public decimal Capital { get; }
    public decimal TotalReturns { get; }
    public decimal ProtectionReturns { get; }
    public decimal TotalDeposit { get; }
}

public class ExPartnerConfig
{
    public ExPartnerConfig(int startPeriod, int endPeriod)
    {
        StartPeriod = startPeriod;
        EndPeriod = endPeriod;
    }

    public int StartPeriod { get; }
    public int EndPeriod { get; }
}