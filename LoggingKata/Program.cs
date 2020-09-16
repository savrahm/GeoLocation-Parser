using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo("Begin parsing");
            //logger.LogInfo($"Lines: {lines[0]}");

            if (lines.Count() == 0)
            {
                logger.LogError("0 lines read from CSV file");
            }
            else if(lines.Count() == 1)
            {
                logger.LogWarning("1 line read from CSV file");
            }

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance

            ITrackable tacoBellA = null;
            ITrackable tacoBellB = null;

            double distance = 0;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);

                for (int x = 1; x < locations.Length; x++)
                {
                    var locB = locations[x];
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);

                    if(corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBellA = locA;
                        tacoBellB = locB;
                    }
                }
            }

            var direction = TacoParser.GetDirection(tacoBellA.Location, tacoBellB.Location);

            Console.WriteLine($"The two Taco Bells with the largest distance between them are {tacoBellA.Name} and {tacoBellB.Name}. They're {Math.Round((distance / 1609.344), 2)} miles apart, and {tacoBellB.Name} is {direction} from {tacoBellA.Name}.");

            ITrackable tacoBellC = null;
            ITrackable tacoBellD = null;

            double shortDistance = 9999999999999999999;

            for (int j = 0; j < locations.Length; j++)
            {
                var locC = locations[j];
                var corC = new GeoCoordinate(locC.Location.Latitude, locC.Location.Longitude);

                for (int y = 1; y < locations.Length; y++)
                {
                    var locD = locations[y];
                    var corD = new GeoCoordinate(locD.Location.Latitude, locD.Location.Longitude);

                    if (corC.GetDistanceTo(corD) < shortDistance && corC.GetDistanceTo(corD) != 0)
                    {
                        shortDistance = corC.GetDistanceTo(corD);
                        tacoBellC = locC;
                        tacoBellD = locD;
                    }
                }
            }

            var shortDirection = TacoParser.GetDirection(tacoBellC.Location, tacoBellD.Location);

            Console.WriteLine($"The two Taco Bells with the shortest distance between them are {tacoBellC.Name} and {tacoBellD.Name}. They're {Math.Round((shortDistance / 1609.344), 2)} miles apart, and {tacoBellD.Name} is {shortDirection} from {tacoBellC.Name}.");

            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations within the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.



        }
    }
}
