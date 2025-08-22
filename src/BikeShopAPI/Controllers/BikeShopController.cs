using Microsoft.AspNetCore.Mvc;
using BikeShopAPI.Entities;
using System.Diagnostics;

namespace BikeShopAPI.Controllers
{
    /// <summary>
    /// API Controller for managing bike shops and their operations.
    /// Provides CRUD operations for bike shops, status management, and bike-related activities.
    /// </summary>
    [Route("api/bikeshop")]
    [ApiController]
    public class BikeShopController : ControllerBase
    {
        private readonly ILogger<BikeShopController> _logger;

        /// <summary>
        /// Initializes a new instance of the BikeShopController with dependency injection.
        /// </summary>
        /// <param name="logger">Logger instance for logging HTTP requests and operations</param>
        public BikeShopController(ILogger<BikeShopController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Static collection of bike shops with sample data for demonstration purposes.
        /// Contains 7 pre-configured bike shops with different categories and bike inventories.
        /// </summary>
        private static readonly List<BikeShop> BikeShops = new()
        {
            new BikeShop
            {
            Id = 1,
            Name = "Fast Wheels",
            Description = "Your one-stop shop for high-speed bikes.",
            Category = "Road",
            HasDelivery = true,
            AddressId = 101,
            Status = ShopStatus.Closed,
            Bikes = new List<Bike>
            {
                new() {
                Id = 1,
                Brand = "Cannondale",
                Model = "Synapse",
                Description = "A road bike that's light, stiff, fast and surprisingly comfortable.",
                Price = 2000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 400,
                ImageUrl = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=400",
                ShopId = 1
                },
                new() {
                Id = 2,
                Brand = "Specialized",
                Model = "Roubaix",
                Description = "A performance road bike that's both comfortable and fast.",
                Price = 2500,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 750,
                ImageUrl = "https://images.unsplash.com/photo-1571068316344-75bc76f77890?w=400",
                ShopId = 1
                }
            }
            },
            new BikeShop
            {
            Id = 2,
            Name = "Eco Riders",
            Description = "Environmentally friendly bikes for the eco-conscious rider.",
            Category = "Electric",
            HasDelivery = true,
            AddressId = 102,
            Status = ShopStatus.Open,
            Bikes = new List<Bike>
            {
                new() {
                Id = 3,
                Brand = "Gazelle",
                Model = "CityZen",
                Description = "A comfortable and fast electric bike for city riding.",
                Price = 3000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1000,
                ImageUrl = "https://images.unsplash.com/photo-1502744688674-c619d1586c9e?w=400",
                ShopId = 2
                },
                new() {
                Id = 4,
                Brand = "Riese & Müller",
                Model = "Supercharger2",
                Description = "A high-performance electric bike for long-distance riding.",
                Price = 4000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1300,
                ImageUrl = "https://images.unsplash.com/photo-1544191696-15693072bb7e?w=400",
                ShopId = 2
                }
            }
            },
            new BikeShop
            {
            Id = 3,
            Name = "City Cruisers",
            Description = "Perfect bikes for the urban jungle.",
            Category = "Urban",
            HasDelivery = false,
            AddressId = 103,
            Status = ShopStatus.Renovating,
            Bikes = new List<Bike>
            {
                new() {
                Id = 5,
                Brand = "VanMoof",
                Model = "S3",
                Description = "A stylish and smart bike for city riding.",
                Price = 1500,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1500,
                ImageUrl = "https://images.unsplash.com/photo-1558903151-e29c0c875e30?w=400",
                ShopId = 3
                },
                new() {
                Id = 6,
                Brand = "Tern",
                Model = "GSD",
                Description = "A compact and powerful e-bike for city riding.",
                Price = 2000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1200,
                ImageUrl = "https://images.unsplash.com/photo-1553978297-833d24db4853?w=400",
                ShopId = 3
                }
            }
            },
            new BikeShop
            {
            Id = 4,
            Name = "Mountain Masters",
            Description = "Top-notch mountain bikes for all terrains.",
            Category = "Mountain",
            HasDelivery = true,
            AddressId = 104,
            Status = ShopStatus.Open,
            Bikes = new List<Bike>
            {
                new() {
                Id = 7,
                Brand = "Trek",
                Model = "Fuel EX",
                Description = "A versatile mountain bike for all terrains.",
                Price = 3500,
                ForkTravel = 150,
                RearTravel = 140,
                WaterInBidon = 800,
                ImageUrl = "https://images.unsplash.com/photo-1538549041-dd9e9cfc3d95?w=400",
                ShopId = 4
                },
                new() {
                Id = 8,
                Brand = "Giant",
                Model = "Trance",
                Description = "A high-performance mountain bike for aggressive riding.",
                Price = 3200,
                ForkTravel = 160,
                RearTravel = 150,
                WaterInBidon = 900,
                ImageUrl = "https://images.unsplash.com/photo-1571068316344-75bc76f77890?w=400",
                ShopId = 4
                }
            }
            },
            new BikeShop
            {
            Id = 5,
            Name = "Urban Edge",
            Description = "Compact and nimble bikes built for city life.",
            Category = "Urban",
            HasDelivery = false,
            AddressId = 105,
            Status = ShopStatus.Open,
            Bikes = new List<Bike>
            {
                new() {
                Id = 9,
                Brand = "Brompton",
                Model = "M6L",
                Description = "Lightweight folding bike perfect for commuting.",
                Price = 1200,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 500,
                ImageUrl = "https://images.unsplash.com/photo-1502744688674-c619d1586c9e?w=400",
                ShopId = 5
                },
                new() {
                Id = 10,
                Brand = "Priority",
                Model = "Classic Plus",
                Description = "Low-maintenance belt-drive bike for daily rides.",
                Price = 900,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 600,
                ImageUrl = "https://images.unsplash.com/photo-1544191696-15693072bb7e?w=400",
                ShopId = 5
                }
            }
            },
            new BikeShop
            {
            Id = 6,
            Name = "Trail Blazers",
            Description = "Adventure bikes for the ultimate off-road experience.",
            Category = "Adventure",
            HasDelivery = true,
            AddressId = 106,
            Status = ShopStatus.Open,
            Bikes = new List<Bike>
            {
                new() {
                Id = 11,
                Brand = "Salsa",
                Model = "Warbird",
                Description = "A gravel bike designed for long-distance adventures.",
                Price = 2800,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1100,
                ImageUrl = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=400",
                ShopId = 6
                },
                new() {
                Id = 12,
                Brand = "Surly",
                Model = "Midnight Special",
                Description = "A versatile bike for road, gravel, and light touring.",
                Price = 1800,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 950,
                ImageUrl = "https://images.unsplash.com/photo-1553978297-833d24db4853?w=400",
                ShopId = 6
                }
            }
            },
            new BikeShop
            {
            Id = 7,
            Name = "Speed Demons",
            Description = "High-performance racing bikes for serious cyclists.",
            Category = "Racing",
            HasDelivery = true,
            AddressId = 107,
            Status = ShopStatus.Open,
            Bikes = new List<Bike>
            {
                new() {
                Id = 13,
                Brand = "Pinarello",
                Model = "Dogma F12",
                Description = "A professional racing bike used by Tour de France champions.",
                Price = 8000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 650,
                ImageUrl = "https://images.unsplash.com/photo-1558903151-e29c0c875e30?w=400",
                ShopId = 7
                },
                new() {
                Id = 14,
                Brand = "Cervélo",
                Model = "S5",
                Description = "An aerodynamic race bike built for pure speed.",
                Price = 6500,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 700,
                ImageUrl = "https://images.unsplash.com/photo-1538549041-dd9e9cfc3d95?w=400",
                ShopId = 7
                }
            }
            }
        };

        /// <summary>
        /// Retrieves all bike shops with their complete inventory.
        /// </summary>
        /// <returns>An ActionResult containing a list of all bike shops with their bikes</returns>
        /// <response code="200">Returns the complete list of bike shops</response>
        [HttpGet]
        public ActionResult<List<BikeShop>> GetAll()
        {
            _logger.LogInformation("GET all 🚲🚲🚲 NO PARAMS 🚲🚲🚲");

            return Ok(BikeShops);
        }

        /// <summary>
        /// Retrieves a specific bike shop by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the bike shop</param>
        /// <returns>An ActionResult containing the bike shop details or NotFound if it doesn't exist</returns>
        /// <response code="200">Returns the bike shop with the specified ID</response>
        /// <response code="404">If the bike shop with the specified ID is not found</response>
        [HttpGet("{id}")]
        public ActionResult<BikeShop> GetById(int id)
        {
            var bikeshop = BikeShops.Find(p => p.Id == id);

            if (bikeshop == null)
            {
                return NotFound();
            }

            return Ok(bikeshop);
        }
 
        /// <summary>
        /// Searches for bike shops by name using case-insensitive partial matching.
        /// Normalizes whitespace by trimming and replacing multiple spaces with single spaces.
        /// </summary>
        /// <param name="name">The search term to match against bike shop names</param>
        /// <returns>An ActionResult containing matching bike shops or appropriate error responses</returns>
        /// <response code="200">Returns bike shops matching the search criteria</response>
        /// <response code="400">If the search term is null or empty</response>
        /// <response code="404">If no bike shops match the search criteria</response>
        // Search shops by name
        [HttpGet("search")]
        public ActionResult<List<BikeShop>> SearchByName(string name)
        {
            _logger.LogInformation($"Searching bike shops by name: {name}");

            // Handle null or empty search term
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Search term cannot be null or empty.");
            }

            // Normalize whitespace: trim and replace multiple spaces with single space
            name = System.Text.RegularExpressions.Regex.Replace(name.Trim(), @"\s+", " ");

            var results = BikeShops.Where(s => s.Name!.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!results.Any())
            {
                return NotFound("No bike shops found.");
            }

            return Ok(results);
        }

        /// <summary>
        /// Creates a new bike shop and adds it to the collection.
        /// </summary>
        /// <param name="bikeShop">The bike shop object to create</param>
        /// <returns>An ActionResult containing the created bike shop with location header</returns>
        /// <response code="201">Returns the newly created bike shop</response>
        /// <response code="400">If the bike shop data is invalid</response>
        [HttpPost]
        public ActionResult<BikeShop> Create(BikeShop bikeShop)
        {
            _logger.LogInformation("POST 🚲🚲🚲 NO PARAMS 🚲🚲🚲");
            BikeShops.Add(bikeShop);
            return CreatedAtAction(nameof(GetById), new { id = bikeShop.Id }, bikeShop);
        }

        /// <summary>
        /// Updates an existing bike shop with new information.
        /// </summary>
        /// <param name="id">The unique identifier of the bike shop to update</param>
        /// <param name="updatedBikeShop">The updated bike shop data</param>
        /// <returns>An ActionResult containing the updated bike shop or NotFound if it doesn't exist</returns>
        /// <response code="200">Returns the updated bike shop</response>
        /// <response code="404">If the bike shop with the specified ID is not found</response>
        [HttpPut("{id}")]
        public ActionResult<BikeShop> Update(int id, BikeShop updatedBikeShop)
        {
            var bikeShop = BikeShops.Find(b => b.Id == id);
            if (bikeShop == null)
            {
                return NotFound();
            }

            bikeShop.Name = updatedBikeShop.Name;
            bikeShop.Description = updatedBikeShop.Description;
            bikeShop.Category = updatedBikeShop.Category;
            bikeShop.HasDelivery = updatedBikeShop.HasDelivery;
            bikeShop.AddressId = updatedBikeShop.AddressId;
            bikeShop.Status = updatedBikeShop.Status;
            bikeShop.Bikes = updatedBikeShop.Bikes;

            return Ok(bikeShop);
        }

        /// <summary>
        /// Deletes a bike shop from the collection.
        /// </summary>
        /// <param name="id">The unique identifier of the bike shop to delete</param>
        /// <returns>An ActionResult indicating success or failure</returns>
        /// <response code="204">If the bike shop was successfully deleted</response>
        /// <response code="404">If the bike shop with the specified ID is not found</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation($"DELETE bike shop with ID: {id}");
            var bikeShop = BikeShops.Find(b => b.Id == id);
            if (bikeShop == null)
            {
                return NotFound();
            }

            BikeShops.Remove(bikeShop);
            return NoContent();
        }

        /// <summary>
        /// Updates the operational status of a bike shop with business rule validation.
        /// Enforces state transition rules: cannot reopen or close while renovating, 
        /// must close before starting renovations.
        /// </summary>
        /// <param name="id">The unique identifier of the bike shop</param>
        /// <param name="newStatus">The new operational status to set</param>
        /// <returns>An ActionResult indicating success or validation error</returns>
        /// <response code="200">If the status was successfully updated</response>
        /// <response code="400">If the status transition violates business rules</response>
        /// <response code="404">If the bike shop with the specified ID is not found</response>
        [HttpPost("{id}/status")]
        public ActionResult UpdateShopStatus(int id, ShopStatus newStatus)
        {
            _logger.LogInformation($"Updating status of bike shop with ID: {id} to {newStatus}");
            
            var bikeShop = FindBikeShopById(id);
            if (bikeShop == null)
            {
                return NotFound("Bike shop not found.");
            }

            var validationResult = ValidateStatusTransition(bikeShop.Status, newStatus);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            bikeShop.Status = newStatus;
            return Ok($"Bike shop status updated to {newStatus}.");
        }

        /// <summary>
        /// Finds a bike shop by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the bike shop</param>
        /// <returns>The bike shop if found, null otherwise</returns>
        private BikeShop? FindBikeShopById(int id)
        {
            return BikeShops.Find(b => b.Id == id);
        }

        /// <summary>
        /// Validates whether a status transition is allowed based on business rules.
        /// </summary>
        /// <param name="currentStatus">The current status of the bike shop</param>
        /// <param name="newStatus">The desired new status</param>
        /// <returns>A validation result indicating success or failure with error message</returns>
        private static (bool IsValid, string ErrorMessage) ValidateStatusTransition(ShopStatus currentStatus, ShopStatus newStatus)
        {
            // Same status transition is always allowed
            if (currentStatus == newStatus)
            {
                return (true, string.Empty);
            }

            return newStatus switch
            {
                ShopStatus.Open when currentStatus == ShopStatus.Renovating => 
                    (false, "Cannot reopen, shop is currently renovating."),
                
                ShopStatus.Closed when currentStatus == ShopStatus.Renovating => 
                    (false, "Cannot close, shop is currently renovating."),
                
                ShopStatus.Renovating when currentStatus == ShopStatus.Open => 
                    (false, "Shop must be closed before starting renovations."),
                
                ShopStatus.Open or ShopStatus.Closed or ShopStatus.Renovating => 
                    (true, string.Empty),
                
                _ => (false, "Unknown or unsupported shop status.")
            };
        }

        /// <summary>
        /// Simulates taking a bike for a ride, consuming water from the bike's bidon.
        /// Consumes 50ml of water per kilometer ridden. Throws an exception if the rider becomes dehydrated.
        /// </summary>
        /// <param name="shopId">The unique identifier of the bike shop</param>
        /// <param name="bikeId">The unique identifier of the bike</param>
        /// <param name="rideLength">The distance to ride in kilometers</param>
        /// <returns>An ActionResult with ride summary or error if bike/shop not found</returns>
        /// <exception cref="Exception">Thrown when the bike runs out of water during the ride</exception>
        /// <response code="200">Returns ride summary with remaining water</response>
        /// <response code="404">If the bike or shop is not found</response>
        [HttpPost("{shopId}/bikes/{bikeId}/takeRide/{rideLength}")]
        public ActionResult TakeRide(int shopId, int bikeId, int rideLength)
        {
            var bikeShop = BikeShops.Find(b => b.Id == shopId);

            var bike = bikeShop?.FindBikeById(bikeId);

            if (bike == null)
            {
                return NotFound("Bike not found in the specified bike shop.");
            }

            int waterConsumptionPerKm = 50;
            int kilometersRidden = 0;

            for (int i = 0; i < rideLength; i++)
            {
                if (bike.WaterInBidon < waterConsumptionPerKm)
                {
                    return BadRequest($"Ride stopped after {kilometersRidden} km. Rider dehydrated due to lack of water. Water remaining: {bike.WaterInBidon} ml.");
                }
                
                bike.WaterInBidon -= waterConsumptionPerKm;
                kilometersRidden++;
            }

            return Ok($"Bike rode {rideLength} kilometers. Water left in bidon: {bike.WaterInBidon} ml.");
        }

        /// <summary>
        /// Simulates a bike malfunction by causing infinite recursion.
        /// ⚠️ WARNING: This method contains a deliberate bug that will cause a stack overflow exception.
        /// It demonstrates an infinite recursion problem for testing/debugging purposes.
        /// </summary>
        /// <param name="shopId">The unique identifier of the bike shop</param>
        /// <param name="bikeId">The unique identifier of the bike</param>
        /// <returns>This method will never return due to infinite recursion</returns>
        /// <response code="404">If the bike or shop is not found</response>
        /// <response code="500">Stack overflow due to infinite recursion</response>
        [HttpPost("{shopId}/bikes/{bikeId}/bikeMalfunction")]
        public ActionResult BikeMalfunction(int shopId, int bikeId)
        {
            var bikeShop = BikeShops.Find(b => b.Id == shopId);

            var bike = bikeShop?.FindBikeById(bikeId);

            if (bike == null)
            {
                return NotFound("Bike not found in the specified bike shop.");
            }

            // Simulate a malfunction causing recursion on the bicycle 
            BikeMalfunction(shopId, bikeId);

            return Ok($"Recovered from bicycle malfunction.");
        }

        /// <summary>
        /// Calculates the range of a bike using optimized prime number computation.
        /// This method is designed for performance testing and benchmarking purposes.
        /// It calculates prime numbers between (shopId + bikeId) and 300,000 using the Sieve of Eratosthenes.
        /// </summary>
        /// <param name="shopId">The unique identifier of the bike shop</param>
        /// <param name="bikeId">The unique identifier of the bike</param>
        /// <returns>An ActionResult indicating calculation completion with performance metrics</returns>
        /// <response code="200">Returns confirmation that range calculation completed with timing information</response>
        [HttpPost("{shopId}/bikes/{bikeId}/calculateRange")]
        public ActionResult CalculateRange(int shopId, int bikeId)
        {
            var stopwatch = Stopwatch.StartNew();
            
            int start = Math.Max(2, shopId + bikeId); // Ensure start is at least 2
            const int end = 300000;
            
            var primes = CalculatePrimesOptimized(start, end);
            
            stopwatch.Stop();
            
            var result = new
            {
                PrimesFound = primes.Count,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds,
                ElapsedSeconds = stopwatch.ElapsedMilliseconds / 1000.0,
                Range = $"{start} to {end}"
            };
            
            _logger.LogInformation("Range calculation completed: {Result}", result);
            
            return Ok(result);
        }

        /// <summary>
        /// Calculates all prime numbers within a specified range using the Sieve of Eratosthenes algorithm.
        /// This optimized approach has O(n log log n) complexity, significantly faster than trial division.
        /// </summary>
        /// <param name="start">The starting number of the range (inclusive)</param>
        /// <param name="end">The ending number of the range (inclusive)</param>
        /// <returns>A list of all prime numbers found within the range</returns>
        public static List<int> CalculatePrimesOptimized(int start, int end)
        {
            if (end < 2) return new List<int>();
            
            // Use Sieve of Eratosthenes for better performance
            var sieve = new bool[end + 1];
            Array.Fill(sieve, true);
            sieve[0] = sieve[1] = false;
            
            // Sieve of Eratosthenes
            for (int i = 2; i * i <= end; i++)
            {
                if (sieve[i])
                {
                    for (int j = i * i; j <= end; j += i)
                    {
                        sieve[j] = false;
                    }
                }
            }
            
            // Collect primes in the specified range
            var primes = new List<int>();
            for (int i = Math.Max(2, start); i <= end; i++)
            {
                if (sieve[i])
                {
                    primes.Add(i);
                }
            }
            
            return primes;
        }

        /// <summary>
        /// Determines whether a given number is prime using optimized trial division.
        /// This implementation uses O(√n) algorithm, checking only up to the square root.
        /// </summary>
        /// <param name="number">The number to test for primality</param>
        /// <returns>True if the number is prime, false otherwise</returns>
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number <= 3) return true;
            if (number % 2 == 0 || number % 3 == 0) return false;
            
            // Check for divisors from 5 up to √number, skipping even numbers
            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }
            
            return true;
        }
    }
}
