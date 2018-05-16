# TvMazeScraper

## API
Shows available by the following address

>{host}/api/shows

![alt text](https://github.com/fryette/tvmaze-scrapper/blob/master/screenshot.PNG)

## Storage
To simplify solution I used memory cache database. In any time we have possibility to change database.

## Pagination

Pagination is supported by the following approach
>{host}/api/shows?page=1

We have 10 items per page. I would like to show, that in any time we can change number of items per page regardless source.

## Cast Ordering
Cast in accordance with the task sorted by bithday. Persons who don't have birthday located on the top of the cast list.
