using System.Collections.Generic;
using AutoMapper;
using TvMazeScrapper.Domain.App;
using TvMazeScrapper.Domain.TvMaze;
using TvMazeScrapper.Models.App;
using TvMazeScrapper.Services.Api.TvMazeApi.DataModels;
using IMapper = TvMazeScrapper.Infrastructure.Interfaces.App.IMapper;

namespace TvMazeScrapper.Services.App
{
    public class Mapper : IMapper
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper()
        {
            _mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<ShowModel, Show>();
                    cfg.CreateMap<Show, ShowModel>();
                    cfg.CreateMap<ShowData, ShowModel>();
                    cfg.CreateMap<ShowData, Show>();
                    cfg.CreateMap<AppPage, PageModel>();
                    cfg.CreateMap<PageModel, AppPage>();
                    cfg.CreateMap<Person, PersonModel>();
                    cfg.CreateMap<PersonModel, Person>();
                    cfg.CreateMap<PersonData, PersonModel>();
                    cfg.CreateMap<PageModel, TvMazePage>();
                    cfg.CreateMap<ShowModel, TvMazeShow>();
                    cfg.CreateMap<TvMazeShow, ShowModel>();
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