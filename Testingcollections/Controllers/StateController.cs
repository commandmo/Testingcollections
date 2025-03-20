using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testingcollections.Interfaces;
using Testingcollections.Dto;
using Testingcollections.Models;
using System.Diagnostics.Metrics;


namespace Testingcollections.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StateController : Controller
    {

        private readonly IStateRepository _stateRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly IMapper _mapper;

        public StateController(IStateRepository stateRepository, ISellerRepository sellerRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _sellerRepository = sellerRepository;
            _mapper = mapper;
        }


        // api state  - displays list of states

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<State>))]


        public IActionResult GetStates() //WHAT DO WE WANT TO DISPLAY - DISPLAYS LIST OF states
        {
            var states = _mapper.Map<List<StateDto>>(_stateRepository.GetStates()); //DISPLAYS LIST OF OWNERS (NAME, ID ETC) 


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(states);
        }

        // api state  - displays details of state when state ID entered

        [HttpGet("{stateId}")]
        [ProducesResponseType(200, Type = typeof(State))]
        [ProducesResponseType(400)]

        public IActionResult GetState(int stateId) //DISPLAYS DETAILS OF SELLER WHEN SELLER ID ENTERED
        {
            if (!_stateRepository.StateExists(stateId)) //VALIDATES OWNER ID IF EXISTS
                return NotFound();

            var state = _mapper.Map<StateDto>(_stateRepository.GetState(stateId));  //WHAT INFO TO DISPLAY TO USER


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(state);
        }

        //API /states/{stateID} - DISPLAYS sellers OF state WHEN state ID ENTERED

        [HttpGet("/sellers/{sellerId}")]
        [ProducesResponseType(200, Type = typeof(State))] //TYPE OF XX IS THE THING THAT YOU ARE SOURCING (SELLER IN THIS CASE)
        [ProducesResponseType(400)]

        public IActionResult GetStateOfASeller(int sellerId)
        {

            var state = _mapper.Map<StateDto>(_stateRepository.GetStateBySeller(sellerId));
            //PokemonDTO provides the ID,name (pokemon name) and DOB //Checks for Pokemon using category ID


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(state);

        }

        //CREATE STATE
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateState([FromBody] StateDto stateCreate)
        {
            if (stateCreate == null) ////IS equal to null
                return BadRequest(ModelState);
            var state = _stateRepository.GetStates()
                .Where(c => c.Name.Trim().ToUpper() == stateCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (state != null) //NOT equal to null
            {
                ModelState.AddModelError("", "State already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stateMap = _mapper.Map<State>(stateCreate);

            if (!_stateRepository.CreateState(stateMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("State successfully created");
        }

        //UPDATE STATE

        [HttpPut("{stateId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateState(int stateId, [FromBody] StateDto stateUpdate)
        {
            if (stateUpdate == null) ////IS equal to null
                return BadRequest(ModelState);

            if (stateId != stateUpdate.Id) //IS  ID SAME AS 
                return BadRequest(ModelState);

            if (!_stateRepository.StateExists(stateId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var stateMap = _mapper.Map<State>(stateUpdate);

            if (!_stateRepository.UpdateState(stateMap))
            {
                ModelState.AddModelError("", "Something went wrong updating state");
                return StatusCode(500, ModelState);
            }

            return Ok("State successfully updated");
        }

        //DELETE STATE

        [HttpDelete("{stateId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteState(int stateId)
        {

            if (!_stateRepository.StateExists(stateId)) //checks if seller if exists
            {
                return NotFound(); //if not, returns error
            }

            var stateToDelete = _stateRepository.GetState(stateId);

            if (!ModelState.IsValid) //check model state
                return BadRequest(ModelState); //if request is bad, will send back model state

            if (!_stateRepository.DeleteState(stateToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting state");
            }

            return Ok("State Successfully deleted");

        }




    }
}
