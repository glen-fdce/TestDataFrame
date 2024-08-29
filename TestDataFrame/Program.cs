using Microsoft.Data.Analysis;

namespace TestDataFrame;

internal static class Program
{
    private static void Main()
    {
        var dataPath = Path.GetFullPath(@"./input/confirmed-planets.csv");
        DataFrame df = DataFrame.LoadCsv(dataPath);
        
        // How many rows are there?
        Console.WriteLine("Rows: " + df.Rows.Count().ToString());
        
        // What is the radius of the largest planet?
        Console.WriteLine("Largest Planet Radius:" + df["pl_rade"].Max());
        
        // What is the name of the largest planet?
        // Python eqivalent would be df["pl_name"][df["pl_rade"].idxmax()]
        int largestPlanetIndex = GetIndexOfLargestPlanet(df);
        Console.WriteLine("Largest Planet Name: " + df["pl_name"][largestPlanetIndex]);
    }

    private static int GetIndexOfLargestPlanet(DataFrame df)
    {
        int largestPlanetIndex = 0;
        double largestPlanetRadius = 0;
        for (int x = 0; x < df.Rows.Count(); x++ )
        {
            if (string.IsNullOrEmpty(df["pl_rade"][x]?.ToString())) { continue; }
            if (!double.TryParse(df["pl_rade"][x]?.ToString(), out double radius)) continue;
            if (!(radius > largestPlanetRadius)) continue;
            
            largestPlanetRadius = radius;
            largestPlanetIndex = x;
        }
        return largestPlanetIndex;
    }
}