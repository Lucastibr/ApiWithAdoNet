using Microsoft.AspNetCore.Mvc;
using System;

namespace CrudAdoDotNet.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PetShopController : ControllerBase
    {
        private readonly IPetShopDal _petShop;
        public PetShopController(IPetShopDal petShop)
        {
            _petShop = petShop;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var pet = _petShop.GetAll();
            return Ok(pet);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {

            return Ok(_petShop.GetByName(name));
        }

        [HttpPost]
        public IActionResult Add(PetShop petShop)
        {
            try
            {
                _petShop.Add(petShop);
                return Ok(petShop);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        public IActionResult Update([FromBody]PetShop petShop)
        {
            try
            {
                _petShop.Update(petShop);
                return Ok(petShop);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid? id)
        {
            _petShop.Delete(id);
            return Ok("Deletado com sucesso");
        }
    }
}