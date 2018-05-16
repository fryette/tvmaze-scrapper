﻿using System.Threading.Tasks;
using TvMazeScrapper.Models;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Infrastructure.Interfaces.DataServices
{
    public interface IPageRepository
    {
        Task<PageModel> TryGetPageAsync(int pageNumber);
        Task SavePageAsync(PageModel pageModel);
    }
}