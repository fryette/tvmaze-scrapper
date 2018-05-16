# TvMazeScraper

Api works similar to TvMaze:
{host}/api/shows

## Pagination

Pagination is supported in the similar way:
{host}/api/shows?page=1

Currently api return not 250, but 25 results per page. As some TvMaze pages doesn't containt all 250 items in each, so each 10th page would also contain not 25 items in each.

## Cache

Shows and cast information are cached using in memory database for simplicity. It can be switched to any cache destination, when needed. Caching appears for each request and stores only requested items.
Ideally as TvMaze suggests the app can use a scheduled service to cache and update all items and return results without intermediate requests.

## Errors handling

Currently exceptions are handled in production with specific action in the controller, but this can be extended if needed.
