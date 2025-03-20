using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testingcollections.Interfaces;
using Testingcollections.Dto;
using Testingcollections.Models;
using System.Diagnostics.Metrics;
using Testingcollections.Repository;
using System.Collections.Generic;


namespace Testingcollections.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : Controller
    {

        private readonly IAdvertRepository _advertRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly IMapper _mapper;

        public AdvertController(IAdvertRepository advertRepository, ISellerRepository sellerRepository, IMapper mapper)
        {
            _advertRepository = advertRepository;
            _sellerRepository = sellerRepository;
            _mapper = mapper;
        }


        // API 1  - get list of all adverts

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Advert>))]

        public async Task<IActionResult> GetAdvertsAsync() //WHAT DO WE WANT TO DISPLAY - DISPLAYS LIST OF adverts
        {
            // before caching: var adverts = _mapper.Map<List<AdvertDto>>(_advertRepository.GetAdvertsAsync()); //DISPLAYS LIST OF OWNERS (NAME, ID ETC) 
            var adverts = await _advertRepository.GetAdvertsAsync();  // Await the repository call FIRST before mapping

            var advertsDtos = _mapper.Map<List<AdvertDto>>(adverts);  // Then map the result using _mapper

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(advertsDtos);
        }

        //API 2 - Get advert by advert ID

        [HttpGet("{advertId}")]
        [ProducesResponseType(200, Type = typeof(Advert))]
        [ProducesResponseType(400)]


        public IActionResult GetAdvert(int advertId)
        {
            if (!_advertRepository.AdvertExists(advertId))
                return NotFound();

            var advert = _mapper.Map<AdvertDto>( //CategoryDTO provides the ID and name (power name)
                _advertRepository.GetAdvert(advertId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(advert);
        }


        //API 3 - Get seller by advert ID

        [HttpGet("{advertId}/seller")]
        [ProducesResponseType(200, Type = typeof(Advert))] //TYPE OF XX IS THE THING THAT YOU ARE SOURCING (SELLER IN THIS CASE)
        [ProducesResponseType(400)]

        public IActionResult GetSellerByAdvert(int advertId) //DISPLAYS ADVERT OF SELLER WHEN SELLER ID ENTERED
        {

            if (!_advertRepository.AdvertExists(advertId)) //VALIDATES SELLER ID 
            {
                return NotFound();
            }

            var advert = _mapper.Map<List<SellerDto>>( //WHAT INFO TO DISPLAY TO USER
                _advertRepository.GetSellerByAdvert(advertId));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(advert);

        }


        //CREATE ADVERT
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> CreateAdvert([FromBody] AdvertDto advertCreate)
        {
            if (advertCreate == null) ////IS equal to null
                return BadRequest(ModelState);

            var advert = (await _advertRepository.GetAdvertsAsync())
                .Where(c => c.Make.Trim().ToUpper() == advertCreate.Make.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (advert != null) //NOT equal to null
            {
                ModelState.AddModelError("", "Advert already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var advertMap = _mapper.Map<Advert>(advertCreate);

            if (!_advertRepository.CreateAdvert(advertMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Advert successfully created");
        }

        //UPDATE advert

        [HttpPut("{advertId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateAdvert(int advertId, [FromBody] AdvertDto advertUpdate)
        {
            if (advertUpdate == null) ////IS equal to null
                return BadRequest(ModelState);

            if (advertId != advertUpdate.Id) //IS  ID SAME AS 
                return BadRequest(ModelState);

            if (!_advertRepository.AdvertExists(advertId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var advertMap = _mapper.Map<Advert>(advertUpdate);

            if (!_advertRepository.UpdateAdvert(advertMap))
            {
                ModelState.AddModelError("", "Something went wrong updating advert");
                return StatusCode(500, ModelState);
            }

            return Ok("Advert successfully updated");
        }

        //DELETE STATE

        [HttpDelete("{advertId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteAdvert(int advertId)
        {

            if (!_advertRepository.AdvertExists(advertId)) //checks if seller if exists
            {
                return NotFound(); //if not, returns error
            }

            var stateToDelete = _advertRepository.GetAdvert(advertId);

            if (!ModelState.IsValid) //check model state
                return BadRequest(ModelState); //if request is bad, will send back model state

            if (!_advertRepository.DeleteAdvert(stateToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting advert");
            }

            return Ok("Advert Successfully deleted");

        }


    }
}
