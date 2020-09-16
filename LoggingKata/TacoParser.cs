using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            //logger.LogInfo("Begin parsing");

            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                logger.LogWarning($"Something went wrong for {line}--array length less than 3");
                // Log that and return null
                // Do not fail if one record parsing fails, return null
                return null; // TODO Implement
            }

            // grab the latitude from your array at index 0
            var latitude = double.Parse(cells[0]);
            // grab the longitude from your array at index 1
            var longitude = double.Parse(cells[1]);
            // grab the name from your array at index 2
            var name = cells[2];

            // DONE -You're going to need to parse your string as a `double`
            // which is similar to parsing a string as an `int`
            // DONE - You'll need to create a TacoBell class
            // that conforms to ITrackable

            // Then, you'll need an instance of the TacoBell class
            // With the name and point set correctly
            var location = new Point() { Latitude = latitude, Longitude = longitude };
            var tb = new TacoBell() { Name = name, Location = location };
            // Then, return the instance of your TacoBell class
            // Since it conforms to ITrackable

            return tb;
        }

        public enum Direction
        {
            North = 0,
            South = 4,
            East = 6,
            West = 2,
            Northeast = 7,
            Northwest = 1,
            Southeast = 5,
            Southwest = 3,
            Undefined = -1
        }

        public static Direction GetDirection(Point p1, Point p2)
        {
            double rad = Math.Atan2(p2.Latitude - p1.Latitude, p2.Longitude - p1.Longitude);

            // Ajust result to be between 0 to 2*Pi
            if (rad < 0)
                rad = rad + (2 * Math.PI);

            var deg = rad * (180 / Math.PI);

            if (deg >= 0 && deg <= 45)
                return Direction.East;
            else if (deg > 45 && deg <= 90)
                return Direction.Northeast;
            else if (deg > 90 && deg <= 135)
                return Direction.North;
            else if (deg > 135 && deg <= 180)
                return Direction.Northwest;
            else if (deg > 180 && deg <= 225)
                return Direction.West;
            else if (deg > 225 && deg <= 270)
                return Direction.Southwest;
            else if (deg > 270 && deg <= 315)
                return Direction.South;
            else if (deg > 315 && deg < 360)
                return Direction.Southeast;
            else
            {
                Console.WriteLine($"{deg} is being returned as the degree of the angle for the direction");
                return Direction.Undefined;
            }
            
        }
    }
}