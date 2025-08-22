using Microsoft.AspNetCore.Mvc;
using BikeShopAPI.Controllers;
using BikeShopAPI.Entities;
using Xunit;

namespace BikeShopAPI.Test.Controllers
{
    public class BikeControllerTest
    {
        private readonly BikeController _controller;

        public BikeControllerTest()
        {
            _controller = new BikeController();
        }

        [Fact]
        public void GetAll_ReturnsAllBikes()
        {
            var actionResult = _controller.GetAll();
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            var items = Assert.IsType<List<Bike>>(result.Value);
            Assert.NotEmpty(items);
            Assert.Equal(3, items.Count());
        }

        [Fact]
        public void GetById_ReturnsBike()
        {
            var actionResult = _controller.GetById(1);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            var bike = Assert.IsType<Bike>(result.Value);
            Assert.Equal(1, bike.Id);
            Assert.Equal("Cannondale", bike.Brand);
            Assert.Equal("Synapse", bike.Model);
        }

        [Fact]
        public void GetById_ReturnsNotFound()
        {
            var result = _controller.GetById(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_CreatesNewBike()
        {
            var newBike = new Bike
            {
                Id = 4,
                Brand = "Trek",
                Model = "Domane",
                Description = "An endurance road bike for long rides.",
                Price = 2800,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 800,
                ShopId = 1
            };

            var actionResult = _controller.Create(newBike);
            var result = actionResult.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.IsType<Bike>(result.Value);
            Assert.Equal("GetById", result.ActionName);
        }

        [Fact]
        public void Update_UpdatesExistingBike()
        {
            var updatedBike = new Bike
            {
                Id = 1,
                Brand = "Cannondale Updated",
                Model = "Synapse Updated",
                Description = "Updated description.",
                Price = 2200,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 500,
                ShopId = 1
            };

            var result = _controller.Update(1, updatedBike);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_ReturnsNotFound()
        {
            var updatedBike = new Bike
            {
                Id = 999,
                Brand = "NonExistent",
                Model = "NonExistent",
                Description = "Non-existent bike.",
                Price = 1000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 500,
                ShopId = 1
            };

            var result = _controller.Update(999, updatedBike);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_DeletesExistingBike()
        {
            var result = _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNotFound()
        {
            var result = _controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetById_ReturnsCorrectBikeData()
        {
            var actionResult = _controller.GetById(2);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            var bike = Assert.IsType<Bike>(result.Value);
            Assert.Equal(2, bike.Id);
            Assert.Equal("Specialized", bike.Brand);
            Assert.Equal("Roubaix", bike.Model);
            Assert.Equal(2500, bike.Price);
            Assert.Equal(750, bike.WaterInBidon);
        }

        [Fact]
        public void GetById_ReturnsThirdBike()
        {
            var actionResult = _controller.GetById(3);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            var bike = Assert.IsType<Bike>(result.Value);
            Assert.Equal(3, bike.Id);
            Assert.Equal("Gazelle", bike.Brand);
            Assert.Equal("CityZen", bike.Model);
            Assert.Equal(3000, bike.Price);
            Assert.Equal(1000, bike.WaterInBidon);
            Assert.Equal(2, bike.ShopId);
        }
    }
}