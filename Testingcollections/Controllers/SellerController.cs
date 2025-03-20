using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testingcollections.Interfaces;
using Testingcollections.Dto;
using Testingcollections.Models;


namespace Testingcollections.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : Controller
    {

        private readonly ISellerRepository _sellerRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public SellerController(ISellerRepository sellerRepository, IStateRepository stateRepository, IMapper mapper)
        {
            _sellerRepository = sellerRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
        }


        // api seller  - displays list of sellers

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Seller>))]


        public IActionResult GetSellers() //WHAT DO WE WANT TO DISPLAY - DISPLAYS LIST OF OWNERS
        {
            var sellers = _mapper.Map<List<SellerDto>>(_sellerRepository.GetSellers()); //DISPLAYS LIST OF OWNERS (NAME, ID ETC) 


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sellers);
        }

        // api seller  - displays details of seller when seller ID entered

        [HttpGet("{sellerId}")]
        [ProducesResponseType(200, Type = typeof(Seller))]
        [ProducesResponseType(400)]

        public IActionResult GetSeller(int sellerId) //DISPLAYS DETAILS OF SELLER WHEN SELLER ID ENTERED
        {
            if (!_sellerRepository.SellerExists(sellerId)) //VALIDATES OWNER ID IF EXISTS
                return NotFound();

            var seller = _mapper.Map<SellerDto>(_sellerRepository.GetSeller(sellerId));  //WHAT INFO TO DISPLAY TO USER


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seller);
        }

        //API /sellers/{sellerID} - DISPLAYS adverts OF SELLER WHEN seller ID ENTERED

        [HttpGet("{sellerId}/advert")]
        [ProducesResponseType(200, Type = typeof(Seller))] //TYPE OF XX IS THE THING THAT YOU ARE SOURCING (SELLER IN THIS CASE)
        [ProducesResponseType(400)]

        public IActionResult GetAdvertBySeller(int sellerId) //DISPLAYS ADVERT OF SELLER WHEN SELLER ID ENTERED
        {

            if (!_sellerRepository.SellerExists(sellerId)) //VALIDATES SELLER ID 
            {
                return NotFound();
            }

            var seller = _mapper.Map<List<AdvertDto>>( //WHAT INFO TO DISPLAY TO USER
                _sellerRepository.GetAdvertBySeller(sellerId)); 


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seller);

        }

        //CREATE SELLER
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateSeller([FromQuery] int stateId, [FromBody] SellerDto sellerCreate)
        {
            if (sellerCreate == null) ////IS equal to null
                return BadRequest(ModelState);
            var seller = _sellerRepository.GetSellers()
                .Where(o => o.LastName.Trim().ToUpper() == sellerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (seller != null) //NOT equal to null
            {
                ModelState.AddModelError("", "Seller already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sellerMap = _mapper.Map<Seller>(sellerCreate);

            sellerMap.State = _stateRepository.GetState(stateId);

            if (!_sellerRepository.CreateSeller(sellerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Seller successfully created");
        }


        //UPDATE SELLER

        [HttpPut("{sellerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateSeller(int sellerId, [FromBody] SellerDto sellerUpdate)
        {
            if (sellerUpdate == null) ////IS equal to null
                return BadRequest(ModelState);

            if (sellerId != sellerUpdate.Id) //IS  ID SAME AS 
                return BadRequest(ModelState);

            if (!_sellerRepository.SellerExists(sellerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var sellerMap = _mapper.Map<Seller>(sellerUpdate);

            if (!_sellerRepository.UpdateSeller(sellerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating seller");
                return StatusCode(500, ModelState);
            }

            return Ok("Seller successfully updated");
        }

        //DELETE SELLER

        [HttpDelete("{sellerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteSeller(int sellerId)
        {
           
            if (!_sellerRepository.SellerExists(sellerId)) //checks if seller if exists
            {
                return NotFound(); //if not, returns error
            }

            var sellerToDelete = _sellerRepository.GetSeller(sellerId); 

            if (!ModelState.IsValid) //check model state
                return BadRequest(ModelState); //if request is bad, will send back model state

            if (!_sellerRepository.DeleteSeller(sellerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting seller");
            }

            return Ok("Successfully deleted");

        }

    }
}
