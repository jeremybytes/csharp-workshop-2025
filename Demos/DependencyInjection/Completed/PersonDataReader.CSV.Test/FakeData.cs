namespace PersonDataReader.CSV.Test;

public class TestData
{
    public static IReadOnlyCollection<string> WithGoodRecords =>
        [
                "1,John,Koenig,1975/10/17,6,",
                "3,Leela,Turanga,1999/3/28,8,{1} {0}",
        ];

    public static IReadOnlyCollection<string> WithGoodAndBadRecords =>
        [
                "1,John,Koenig,1975/10/17,6,",
                "INVALID DATA",
                "3,Leela,Turanga,1999/3/28,8,{1} {0}",
                "MORE INVALID DATA",
        ];

    public static IReadOnlyCollection<string> WithOnlyBadRecords =>
        [
                "INVALID DATA",
                "MORE INVALID DATA",
        ];
}
