using System;
using System.Net;

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

        public static String GetDirection(Point p1, Point p2)  
        {
           
            string northsouth;
            string eastwest;

            if (p1.Latitude < p2.Latitude)
                northsouth = "north";
            else
                northsouth = "south";

            if (p1.Longitude > 0 && p2.Longitude < 0) //This if+nested if makes longitude work if p1 is in the eastern hemisphere and p2 is in the western hemisphere
            {
                if (p1.Longitude - p2.Longitude > 180)
                    eastwest = "east";
                else
                    eastwest = "west";
            }
            else
            {
                if (p1.Longitude > p2.Longitude)
                    eastwest = "west";
                else
                    eastwest = "east";
            }

            return String.Concat(northsouth, eastwest);
        }
    }
}