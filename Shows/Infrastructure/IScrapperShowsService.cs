﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Shows.DataModels;

namespace Shows.Infrastructure
{
    public interface IScrapperShowsService
    {
        Task<IEnumerable<Show>> LoadShowsAsync(int pageNumber = 0);
    }
}