# BusStop
A short demo application. Tracking work items using [Trello](https://trello.com/b/JW4zUTtQ/work-items).

## Requirements
* A server-side service that returns estimated arrival times for a requested bus stop.
* A web-based, client-side app to consume the above service.

## Description
### Server-side
* There are 10 bus stops (Stops 1 - 10)
* Each stop is serviced by three routes: Routes 1, 2, and 3.
* Each stop is serviced every 15 minutes per route, 24 hours per day, and each route starts running 2 minutes after the previous one.
* Each stop is 2 minutes away from the previous one.
* The API should return to the consumer the next 2 arrival times per route for the requested stop.

Example schedule for Stop 1:
>Route 1 12:00, 12:15, 12:30, 12:45 ...
>
>Route 2 12:02, 12:17, 12:32, 12:47 ...
>
>Route 3 12:04, 12:19, 12:34, 12:49 ...
etc..

### Client-side

It should output the updated prediction times every minute until stopped.

Example output when running the app at 3:01PM:

Stop 1:
Route 1 in 14 mins and 29 mins
Route 2 in 1 mins and 16 mins
Route 3 in 3 mins and 18 mins

Stop 2:
Route 1 in 1 mins and 16 mins
Route 2 in 3 mins and 18 mins
Route 3 in 5 mins and 20 mins
