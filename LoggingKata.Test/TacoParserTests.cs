using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.ComponentModel;
using Xunit;
using static LoggingKata.TacoParser;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldParseAtAll()
        {
            // TODO: Complete Something, if anything

            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("33.671982,-85.826674,Taco Bell Annisto...", -85.826674)]
        [InlineData("32.92496,-85.961342,Taco Bell Alexander Cit...", -85.961342)]
        [InlineData("33.524131, -86.724876, Taco Bell Birmingham...", -86.724876)]
        public void ShouldParseLongitude(string line, double expected)
        {
            // TODO: Complete - "line" represents input data we will Parse to
            //       extract the Longitude.  Your .csv file will have many of these lines,
            //       each representing a TacoBell location

            //Arrange
            var test = new TacoParser();

            //Act
            var actual = test.Parse(line).Location.Longitude;

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        [InlineData("33.671982,-85.826674,Taco Bell Annisto...", 33.671982)]
        [InlineData("32.92496,-85.961342,Taco Bell Alexander Cit...", 32.92496)]
        [InlineData("33.524131,-86.724876,Taco Bell Birmingham...", 33.524131)]
        public void ShouldParseLatitude(string line, double expected)
        {
            //Arrange
            var test = new TacoParser();

            //Act
            var actual = test.Parse(line).Location.Latitude;

            //Assert
            Assert.Equal(actual, expected);
        }
           
        [Theory]
        [InlineData("33.748550,-84.391502, Fake Atlanta Taco Bell", "34.021650, -84.361670, Fake Roswell Taco Bell", "northeast")]
        [InlineData("33.5778631,-101.8551665, Fake Lubbock Taco Bell", "33.5873164, -102.37796, Fake Levelland Taco Bell", "northwest")]
        [InlineData("33.5778631,-101.8551665, Fake Lubbock Taco Bell", "30.531974, -91.150378, Fake Baton Rouge Taco Bell", "southeast")]
        [InlineData("33.5778631,-101.8551665, Fake Lubbock Taco Bell", "38.256081, -85.751572, Fake Louisville Taco Bell", "northeast")]
        [InlineData("33.5778631,-101.8551665, Fake Lubbock Taco Bell", "45.516020, -122.681430, Fake Portland Taco Bell", "northwest")]
        [InlineData("-27.470933,153.023502, Fake Brisbane Taco Bell", "45.516020, -122.681430, Fake Portland Taco Bell", "northeast")]
        [InlineData("38.256081,-85.751572,Fake Louisville Taco Bell", "52.628101, 1.299349, Fake Norwich Taco Bell", "northeast")]
        public void DirectionChecker(string line, string line2, string expected)
        {
            //Arrange
            var test1 = new TacoParser();
            var parsed1 = test1.Parse(line).Location;

            var test2 = new TacoParser();
            var parsed2 = test2.Parse(line2).Location;

            //Act
            var actual = TacoParser.GetDirection(parsed1, parsed2);

            //Assert
            Assert.Equal(actual, expected);
        }
    }
}
