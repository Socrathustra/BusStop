# BusStop
A short demo application. Tracking work items using [Trello](https://trello.com/b/JW4zUTtQ/work-items).

## Description
This is a demo application intended to showcase my use of design principles. It allows a user to see upcoming arrival times for two bus stops, aptly named Bus Stop 1 and Bus Stop 2. Details on how these are calculated can be found in the Requirements section.

## Running the Application

Clone the repository and open the solution file in Visual Studio. Run the application via IIS Express.

## Developer Notes

A good deal of the code has been intentionally over-engineered compared to the task. Reviewers inspecting the code will find that the code is thoroughly unit-tested, and it utilizes multiple application layers to best mimic an enterprise-grade application. In no particular order, the developer would like to call attention to a number of facts:

### Back-End Observations
* The back-end code has been written so that one would only need to swap out the data layer to turn this into a real application. One could swap out the concrete class provided in Startup for the IRouteStopDataService, and the rest of the application would continue to work.
* The above also applies to back-end unit testing. By making a method that exposes only the business classes injected during Startup, a FakeStartup class allows tests to utilize dependency injection instead of creating the systems under test manually.
* This ensures that even the unit tests will continue to function without significant alteration, and it allows developers to add additional dependencies to existing as needed without needing to update potentially dozens of tests.
* The FakeStartup class allows for rapid production of additional unit tests since much of the work setting up the sut is done for the developer.

### Front-End Observations
* The front-end code is also almost production-ready. One would only have to swap out the hard-coded list of bus stop ids with a call to get, say, the user's favorite bus stops.
* The decision to use long-polling over other options was surprisingly sleek. Since arrival times for buses will only update every minute on the minute, it is easy to calculate how long from the initial page load it is to the next minute. The data can refresh at that point and every 60 seconds thereafter. There's no need to keep a websocket open, since updates are predictable. 

## Requirements

* A server-side service that returns estimated arrival times for a requested bus stop.
* A web-based, client-side app to consume the above service.

### Server-Side
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

### Client-Side

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
