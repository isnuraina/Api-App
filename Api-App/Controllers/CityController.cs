using Api_App.Data;
using Api_App.DTOs.City;
using Api_App.Models;
using Api_App.Services;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;

        public CityController(AppDbContext context, IMapper mapper, ICityService cityService)
        {
            _context = context;
            _mapper = mapper;
            _cityService = cityService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityCreateDto request)
        {
            await _cityService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Created success");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cityService.GetAllAsync());

        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById([FromRoute] int id)
        //{
        //    var data = await _context.Cities.Include(x => x.Country).FirstOrDefaultAsync(m => m.Id == id);
        //    return data is not null ? Ok(_mapper.Map<CityDto>(data)) : NotFound();
        //}


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _cityService.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }



        //[HttpPut("{id}")]
        //public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CityEditDto request)
        //{
        //    var editedCity = await _context.Cities.Include(m => m.Country).FirstOrDefaultAsync(m => m.Id == id);
        //    if (editedCity == null)
        //    {
        //        return NotFound();
        //    }
        //    var existCountry = await _context.Cities.FirstOrDefaultAsync(x => x.CountryId == request.CountryId);
        //    if (existCountry is null)
        //    {
        //        return NotFound();
        //    }
        //    _mapper.Map(request, editedCity);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CityEditDto request)
        {
            var result = await _cityService.EditAsync(id, request);
            if (!result)
                return NotFound();

            return Ok();
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    var city = await _context.Cities.Include(m=>m.Country).FirstOrDefaultAsync(m=>m.Id==id);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Cities.Remove(city);
        //    await _context.SaveChangesAsync();
        //    var result = _mapper.Map<CityDto>(city);
        //    return Ok(result);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _cityService.DeleteAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

    }
}
