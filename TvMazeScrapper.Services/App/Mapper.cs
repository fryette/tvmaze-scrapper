using System.Collections.Generic;
using AutoMapper;
using TvMazeScrapper.Domain;
using TvMazeScrapper.Domain.App;
using TvMazeScrapper.Domain.TvMaze;
using TvMazeScrapper.Models.App;
using TvMazeScrapper.Services.Api.TvMazeApi.DataModels;

namespace TvMazeScrapper.Services.App
{
    public class Mapper : Infrastructure.Interfaces.App.IMapper
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper()
        {
            _mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<ShowModel, Show>();
                    cfg.CreateMap<Show, ShowModel>();
                    cfg.CreateMap<Page, PageModel>();
                    cfg.CreateMap<PageModel, Page>();
                    cfg.CreateMap<Person, PersonModel>();
                    cfg.CreateMap<PersonModel, Person>();
                    cfg.CreateMap<PersonData, PersonModel>();
                    cfg.CreateMap<ShowData, ShowModel>();
                    cfg.CreateMap<ShowModel, TvMazeShow>();
                    cfg.CreateMap<PageModel, TvMazePage>();
                }).CreateMapper();
        }

        public TTarget Map<TTarget>(object source) where TTarget : class
        {
            return _mapper.Map<TTarget>(source);
        }

        public IEnumerable<TTarget> MapCollection<TSource, TTarget>(IEnumerable<TSource> source) where TSource : class
            where TTarget : class
        {
            return _mapper.Map<IEnumerable<TTarget>>(source);
        }
    }
}
