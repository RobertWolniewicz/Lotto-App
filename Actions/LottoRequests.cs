using Lotto.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Lotto.Actions
{
    public class LottoRequests
    {
        public static IResult GettResults(LottoDbContext Db, [FromRoute] DateTime startDate, [FromRoute] DateTime endDate)
        {
            var AllResults = Db.Results.Where(R => R.DateTime <= endDate || R.DateTime >= startDate).Select(R => R.Results).ToList();
            if (AllResults.Any())
            {
                return Results.Ok(AllResults);
            }
            return Results.NotFound();
        }
    }
}
