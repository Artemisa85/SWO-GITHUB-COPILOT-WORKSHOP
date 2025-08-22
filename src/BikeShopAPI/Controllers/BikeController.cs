/// <summary>
/// API controller that exposes CRUD endpoints for managing Bike resources.
/// </summary>
/// <remarks>
/// Routes for this controller are rooted at "api/bike". This controller uses a private static in-memory collection
/// as its data store for demonstration and workshop purposes. The in-memory store is shared across requests and is
/// not thread-safe or durable; replace it with a repository/DB for production scenarios.
/// </remarks>
/// <example>
/// Examples:
/// GET /api/bike            -> returns all bikes
/// GET /api/bike/{id}       -> returns bike with given id
/// POST /api/bike           -> creates a new bike
/// PUT /api/bike/{id}       -> updates existing bike
/// DELETE /api/bike/{id}    -> deletes bike
/// </example>

/// <summary>
/// In-memory collection that holds the current set of <see cref="Bike"/> instances.
/// </summary>
/// <remarks>
/// Initialized with sample data for demonstration. Because this field is static it is shared application-wide.
/// This collection does not provide concurrency control, validation, or persistence.
/// </remarks>

/// <summary>
/// Retrieves all bikes from the in-memory collection.
/// </summary>
/// <returns>
/// An <see cref="ActionResult{IEnumerable{Bike}}"/> containing the collection of bikes and an HTTP 200 (OK) response.
/// </returns>
/// <response code="200">Returns the list of bikes.</response>

/// <summary>
/// Retrieves a single bike by its identifier.
/// </summary>
/// <param name="id">The unique identifier of the bike to retrieve.</param>
/// <returns>
/// An <see cref="ActionResult{Bike}"/>:
/// - HTTP 200 (OK) with the <see cref="Bike"/> when found.
/// - HTTP 404 (NotFound) when no bike with the specified id exists.
/// </returns>
/// <response code="200">Bike found and returned.</response>
/// <response code="404">No bike with the supplied id exists.</response>

/// <summary>
/// Creates a new bike and adds it to the in-memory collection.
/// </summary>
/// <param name="bike">The <see cref="Bike"/> to create. The caller is expected to provide the identifier in this demo implementation.</param>
/// <returns>
/// An <see cref="ActionResult{Bike}"/> that produces HTTP 201 (Created) with a Location header pointing to the newly created resource
/// (via the GetById action) and the created <see cref="Bike"/> in the response body.
/// </returns>
/// <remarks>
/// This method does not perform validation or check for identifier conflicts in the demo implementation.
/// In production code, validate the input, generate server-side identifiers, and return appropriate error responses (e.g., 400 Bad Request).
/// </remarks>
/// <response code="201">Bike successfully created.</response>

/// <summary>
/// Updates an existing bike with the supplied values.
/// </summary>
/// <param name="id">The identifier of the bike to update.</param>
/// <param name="bike">A <see cref="Bike"/> instance containing the updated values.</param>
/// <returns>
/// - HTTP 204 (NoContent) when the update completes successfully.
/// - HTTP 404 (NotFound) when no bike with the specified id exists.
/// </returns>
/// <remarks>
/// This method overwrites the existing bike's properties with the provided object's values. It does not perform
/// validation or partial updates. For partial updates, consider implementing PATCH. Ensure concurrency and validation
/// are handled in production scenarios.
/// </remarks>
/// <response code="204">Update succeeded and no content is returned.</response>
/// <response code="404">Bike to update was not found.</response>

/// <summary>
/// Deletes the bike with the given identifier from the in-memory collection.
/// </summary>
/// <param name="id">The identifier of the bike to delete.</param>
/// <returns>
/// - HTTP 204 (NoContent) when the bike was successfully removed.
/// - HTTP 404 (NotFound) when the bike does not exist.
/// </returns>
/// <remarks>
/// Deletion is immediate in the in-memory collection. In production, consider soft deletes, audit logging, and
/// transactional guarantees depending on requirements.
/// </remarks>
/// <response code="204">Bike deleted successfully.</response>
/// <response code="404">Bike to delete was not found.</response>
using Microsoft.AspNetCore.Mvc;
using BikeShopAPI.Entities;

namespace BikeShopAPI.Controllers
{
    [Route("api/bike")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private static List<Bike> _bikes = new List<Bike>
        {
            new Bike 
            { 
                Id = 1, 
                Brand = "Cannondale", 
                Model = "Synapse", 
                Description = "A road bike that's light, stiff, fast and surprisingly comfortable.", 
                Price = 2000, 
                ForkTravel = 0, 
                RearTravel = 0, 
                WaterInBidon = 400,
                ShopId = 1 
            },
            new Bike 
            { 
                Id = 2, 
                Brand = "Specialized", 
                Model = "Roubaix", 
                Description = "A performance road bike that's both comfortable and fast.", 
                Price = 2500, 
                ForkTravel = 0, 
                RearTravel = 0, 
                WaterInBidon = 750,
                ShopId = 1 
            },
            new Bike 
            { 
                Id = 3, 
                Brand = "Gazelle", 
                Model = "CityZen", 
                Description = "A comfortable and fast electric bike for city riding.", 
                Price = 3000, 
                ForkTravel = 0, 
                RearTravel = 0, 
                WaterInBidon = 1000,
                ShopId = 2 
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Bike>> GetAll()
        {
            return Ok(_bikes);
        }

        [HttpGet("{id}")]
        public ActionResult<Bike> GetById(int id)
        {
            var bike = _bikes.FirstOrDefault(b => b.Id == id);
            if (bike == null)
            {
                return NotFound();
            }
            return Ok(bike);
        }

        [HttpPost]
        public ActionResult<Bike> Create(Bike bike)
        {
            _bikes.Add(bike);
            return CreatedAtAction(nameof(GetById), new { id = bike.Id }, bike);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Bike bike)
        {
            var existingBike = _bikes.FirstOrDefault(b => b.Id == id);
            if (existingBike == null)
            {
                return NotFound();
            }
            
            existingBike.Brand = bike.Brand;
            existingBike.Model = bike.Model;
            existingBike.Description = bike.Description;
            existingBike.Price = bike.Price;
            existingBike.ForkTravel = bike.ForkTravel;
            existingBike.RearTravel = bike.RearTravel;
            existingBike.WaterInBidon = bike.WaterInBidon;
            existingBike.ShopId = bike.ShopId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bike = _bikes.FirstOrDefault(b => b.Id == id);
            if (bike == null)
            {
                return NotFound();
            }
            _bikes.Remove(bike);
            return NoContent();
        }
    }
}