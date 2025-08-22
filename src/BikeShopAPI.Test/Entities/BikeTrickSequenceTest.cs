using BikeShopAPI.Entities;
using Xunit;

namespace BikeShopAPI.Test.Entities
{
    public class BikeTrickSequenceTest
    {
        [Fact]
        public void Parse_ShouldCalculateCorrectDifficulty_ForExample1()
        {
            // Arrange
            var signature = "L4B-H2C-R3A-S1D-T2E";
            
            // Act
            var result = BikeTrickSequence.Parse(signature);
            
            // Assert
            Assert.Equal(5, result.Tricks.Count);
            Assert.Equal("360", result.Tricks[0].Action);
            Assert.Equal(4, result.Tricks[0].RepetitionCount);
            Assert.Equal('B', result.Tricks[0].DifficultyModifier);
            Assert.Equal(4.8, result.Tricks[0].Score); // 4 * 1.2 = 4.8
            
            Assert.Equal("Tuck No-Hander", result.Tricks[1].Action);
            Assert.Equal(2.8, result.Tricks[1].Score); // 2 * 1.4 = 2.8
            
            Assert.Equal("Cash Roll", result.Tricks[2].Action);
            Assert.Equal(3.0, result.Tricks[2].Score); // 3 * 1.0 = 3.0 (no double because previous was H, not L)
            
            Assert.Equal("Barspin", result.Tricks[3].Action);
            Assert.Equal(1.6, result.Tricks[3].Score); // 1 * 1.6 = 1.6 (no triple because previous was R, not T)
            
            Assert.Equal("Table", result.Tricks[4].Action);
            Assert.Equal(3.6, result.Tricks[4].Score); // 2 * 1.8 = 3.6
            
            Assert.Equal(15.8, result.Difficulty); // 4.8 + 2.8 + 3.0 + 1.6 + 3.6 = 15.8
        }

        [Fact]
        public void Parse_ShouldCalculateCorrectDifficulty_ForExampleWithSpecialRules()
        {
            // Arrange - Example from requirements: L4B-R3A-H2C-T2E-S1D
            var signature = "L4B-R3A-H2C-T2E-S1D";
            
            // Act
            var result = BikeTrickSequence.Parse(signature);
            
            // Assert
            Assert.Equal(5, result.Tricks.Count);
            
            // 360: 4 * 1.2 = 4.8
            Assert.Equal(4.8, result.Tricks[0].Score);
            
            // Cash Roll: 3 * 1 * 2 (Cash Roll after a 360) = 6.0
            Assert.Equal(6.0, result.Tricks[1].Score);
            
            // Tuck No-Hander: 2 * 1.4 = 2.8
            Assert.Equal(2.8, result.Tricks[2].Score);
            
            // Table: 2 * 1.8 = 3.6
            Assert.Equal(3.6, result.Tricks[3].Score);
            
            // Barspin: 1 * 1.6 * 3 (Barspin after a Table) = 4.8
            Assert.Equal(4.8, result.Tricks[4].Score);
            
            // Total Difficulty: 22
            Assert.Equal(22.0, result.Difficulty);
        }

        [Fact]
        public void Parse_ShouldHandleSimpleSequence()
        {
            // Arrange
            var signature = "L1A-H1B-R1C-T1E";
            
            // Act
            var result = BikeTrickSequence.Parse(signature);
            
            // Assert
            Assert.Equal(4, result.Tricks.Count);
            Assert.Equal(1.0, result.Tricks[0].Score); // 1 * 1.0 = 1.0
            Assert.Equal(1.2, result.Tricks[1].Score); // 1 * 1.2 = 1.2
            Assert.Equal(1.4, result.Tricks[2].Score); // 1 * 1.4 = 1.4 (no double, previous was H)
            Assert.Equal(1.8, result.Tricks[3].Score); // 1 * 1.8 = 1.8
            Assert.Equal(5.4, result.Difficulty); // 1.0 + 1.2 + 1.4 + 1.8 = 5.4
        }

        [Fact]
        public void Parse_ShouldRoundDifficultyToTwoDecimalPlaces()
        {
            // Arrange
            var signature = "L2A-R1B"; // Should trigger Cash Roll after 360 rule
            
            // Act
            var result = BikeTrickSequence.Parse(signature);
            
            // Assert
            // 360: 2 * 1.0 = 2.0
            // Cash Roll: 1 * 1.2 * 2 = 2.4
            // Total: 4.4
            Assert.Equal(4.4, result.Difficulty);
        }
    }
}
