using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testingcollections.Interfaces;
using Testingcollections.Dto;
using Testingcollections.Models;
using System.Diagnostics.Metrics;
using Testingcollections.Repository;


namespace Testingcollections.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VerticalController : Controller
    {

        private readonly IVerticalRepository _verticalRepository;
        private readonly IMapper _mapper;

        public VerticalController(IVerticalRepository verticalRepository, IMapper mapper)
        {
            _verticalRepository = verticalRepository;
            _mapper = mapper;
        }

        // api vertical  - displays list of verticals

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vertical>))]


        public IActionResult GetVerticals() 
        {
            var verticals = _mapper.Map<List<VerticalDto>>(_verticalRepository.GetVerticals()); 


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(verticals);
        }

        // api vertical  - Displays vertical by vertical ID

        [HttpGet("{verticalId}")]
        [ProducesResponseType(200, Type = typeof(Vertical))]
        [ProducesResponseType(400)]

        public IActionResult GetVerticals(int verticalId) //DISPLAYS DETAILS OF SELLER WHEN SELLER ID ENTERED
        {
            if (!_verticalRepository.VerticalExists(verticalId)) //VALIDATES OWNER ID IF EXISTS
                return NotFound();

            var vertical = _mapper.Map<VerticalDto>(_verticalRepository.GetVertical(verticalId));  //WHAT INFO TO DISPLAY TO USER


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(vertical);
        }

        //API /adverts/{verticalId} - DISPLAYS adverts of a vertical with vertical ID

        [HttpGet("/adverts/{verticalId}")]
        [ProducesResponseType(200, Type = typeof(Vertical))] 
        [ProducesResponseType(400)]

        public IActionResult GetVertical(int verticalId)
        {

            var adverts = _mapper.Map<List<AdvertDto>>(
                _verticalRepository.GetAdvertByVertical(verticalId));
           


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(adverts);

        }

        //CREATE VERTICAL
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateVertical([FromBody] VerticalDto verticalCreate)
        {
            if (verticalCreate == null) ////IS equal to null
                return BadRequest(ModelState);
            var vertical = _verticalRepository.GetVerticals()
                .Where(c => c.Name.Trim().ToUpper() == verticalCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (vertical != null) //NOT equal to null
            {
                ModelState.AddModelError("", "Vertical already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var verticalMap = _mapper.Map<Vertical>(verticalCreate);

            if (!_verticalRepository.CreateVertical(verticalMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Vertical successfully created");
        }

        //UPDATE STATE

        [HttpPut("{verticalId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateVertical(int verticalId, [FromBody] VerticalDto verticalUpdate)
        {
            if (verticalUpdate == null) ////IS equal to null
                return BadRequest(ModelState);

            if (verticalId != verticalUpdate.Id) //IS  ID SAME AS 
                return BadRequest(ModelState);

            if (!_verticalRepository.VerticalExists(verticalId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var verticalMap = _mapper.Map<Vertical>(verticalUpdate);

            if (!_verticalRepository.UpdateVertical(verticalMap))
            {
                ModelState.AddModelError("", "Something went wrong updating state");
                return StatusCode(500, ModelState);
            }

            return Ok("State successfully updated");
        }

        //DELETE STATE

        [HttpDelete("{verticalId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteVertical(int verticalId)
        {

            if (!_verticalRepository.VerticalExists(verticalId)) //checks if seller if exists
            {
                return NotFound(); //if not, returns error
            }

            var verticalToDelete = _verticalRepository.GetVertical(verticalId);

            if (!ModelState.IsValid) //check model state
                return BadRequest(ModelState); //if request is bad, will send back model state

            if (!_verticalRepository.DeleteVertical(verticalToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting vertical");
            }

            return Ok("Vertical Successfully deleted");

        }



    }

}