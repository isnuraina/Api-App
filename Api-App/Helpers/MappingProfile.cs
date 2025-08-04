using Api_App.DTOs.Author;
using Api_App.DTOs.Book;
using Api_App.DTOs.City;
using Api_App.DTOs.Country;
using Api_App.DTOs.Employee;
using Api_App.DTOs.Student;
using Api_App.Models;
using AutoMapper;

namespace Api_App.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<CountryCreateDto, Country>();
            CreateMap<CountryEditDto, Country>();
            CreateMap<CityCreateDto, City>();
            CreateMap<City, CityDto>().ForMember(dest=>dest.CountryName,
                opt=>opt.MapFrom(src=>src.Country.Name));
            CreateMap<CityEditDto, City>().ReverseMap();
            CreateMap<BookCreateDto, Book>().ForMember(dest => dest.BookAuthors,
             opt => opt.MapFrom(src => src.AuthorIds.Select(m=>new BookAuthor { AuthorId=m}).ToList()));

            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentDto>();
            CreateMap<Book, BookDto>()
     .ForMember(dest => dest.AuthorNames,
         opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author.Name).ToList()));
            CreateMap<BookCreateDto, Book>()
    .ForMember(dest => dest.BookAuthors, opt =>
        opt.MapFrom(src => src.AuthorIds.Select(id => new BookAuthor { AuthorId = id })));

            CreateMap<BookEditDto, Book>()
                .ForMember(dest => dest.BookAuthors, opt =>
                    opt.MapFrom(src => src.AuthorIds.Select(id => new BookAuthor { AuthorId = id })));

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorNames, opt =>
                    opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author.Name).ToList()));

            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorEditDto, Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<Employee, EmployeeDto>();

        }
    }
}
