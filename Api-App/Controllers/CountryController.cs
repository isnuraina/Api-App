using Api_App.Data;
using Api_App.DTOs.Country;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;

        public CountryController(AppDbContext context, IMapper mapper, ICountryService countryService)
        {
            _context = context;
            _mapper = mapper;
            _countryService = countryService;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CountryCreateDto request)
        //{
        //    await _context.Countries.AddAsync(new Country { Name = request.Name });
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(Create), "Created success");
        //}


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountryCreateDto request)
        {
            await _countryService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Created success");
        }



        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var countries = await _context.Countries.Select(m => new CountryDto { Id = m.Id, Name = m.Name }).ToListAsync();
        //    return Ok(countries);
        //}



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _countryService.GetAllAsync());
        }



        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById([FromRoute]int id)
        //{
        //    var country = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);
        //    if (country is null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(new CountryDto { Id = country.Id, Name = country.Name });
        //}


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _countryService.GetByIdAsync(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromQuery]int id)
        //{
        //    var country = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);
        //    if (country ==null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Countries.Remove(country);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _countryService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(new { message = "Country deleted successfully." });
        }




        //[HttpGet]
        //public async Task<IActionResult> Search([FromQuery]string searchText)
        //{
        //    var searchingCountries = await _context.Countries.Where(m => m.Name.Trim().ToLower().
        //    Contains(searchText.Trim().ToLower())).Select(m=>new CountryDto { Id = m.Id, Name = m.Name }).ToListAsync();
        //    return Ok(searchingCountries);
        //}



        //[HttpGet]
        //public async Task<IActionResult> Search([FromQuery] string searchText)
        //{
        //    return Ok(_mapper.Map<IEnumerable<CountryDto>>(await _context.Countries.Where(m => m.Name.Trim().ToLower().
        //    Contains(searchText.Trim().ToLower())).Select(m => new CountryDto { Id = m.Id, Name = m.Name }).ToListAsync()));
        //}


        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            return Ok(_mapper.Map<IEnumerable<CountryDto>>(await _context.Countries.Where(m => m.Name.Trim().ToLower().
            Contains(searchText.Trim().ToLower())).Select(m => new CountryDto { Id = m.Id, Name = m.Name }).ToListAsync()));
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]CountryEditDto request)
        //{
        //    var data = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
        //    if (data is null)
        //    {
        //        return NotFound();
        //    }
        //    data.Name = request.Name;
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CountryEditDto request)
        //{
        //    var data = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
        //    if (data is null)
        //    {
        //        return NotFound();
        //    }
        //    _mapper.Map(request, data);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CountryEditDto request)
        {
            var result = await _countryService.EditAsync(id, request);
            if (!result)
                return NotFound();

            return Ok();
        }

    }
}
