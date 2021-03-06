﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cast.DataModels;
using Cast.Infrastructure;
using Cast.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Polly;

namespace Cast.Services
{
    public class MazeCastClient : IMazeCastClient
    {
        private const string CAST_API_ENDPOINT = "http://api.tvmaze.com/shows/{0}/cast";
        private readonly Policy _exponentialRetryPolicy =
            Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    2,
                    attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)),
                    //TODO: Should be used microservice for logging instedof Console
                    (ex, _) => Console.WriteLine(ex.ToString()));

        private readonly IsoDateTimeConverter _dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = Defines.DATE_FORMAT };
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(CAST_API_ENDPOINT) };

        public async Task<DataModels.Cast> FetchCastByShowIdAsync(int showId)
        {
            var personsInfoData = Enumerable.Empty<PersonInfo>();

            await _exponentialRetryPolicy.ExecuteAsync(
                async () =>
                {
                    var response = await _client.GetStringAsync(string.Format(CAST_API_ENDPOINT, showId));
                    personsInfoData = JsonConvert.DeserializeObject<List<PersonInfo>>(response, _dateTimeConverter);
                });

            return new DataModels.Cast { ShowId = showId, Persons = personsInfoData.Select(x => x.Person).ToList() };
        }
    }
}