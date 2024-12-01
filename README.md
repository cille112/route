# Route

To run the code make sure you have an api key for the Google route api.

## Steps: 
- Clone the repo.
- `dotnet restore`
- `dotnet user-secrets init`
- `dotnet user-secrets set "GoogleApiKey" "{}API_KEY"`
- `dotnet run`


Now the API is running. A swagger site should open such that the endpoints in the API can be seen.

There are three endpoints:
1. One for adding a new stop
1. One for adding a list of stops
1. One for calculating the route

A stop should have a name, and a point that contains latitude and longitude.

The response of the route is a list in order of the route. 


Use postman or similar to test the functionality, a list of stops that can be used is located in the list.json file.

## Assumptions
I made som eassumption during the task.
- The distance is the same from A->B and B->A
- Route optimization is done only one distance not time since I assumed that the longer the distance the longer the time
- An adress is a point with langitude and longitude, not actual stress adress. (For simplicity)
- The first stop is the start where the truck is located. 
- The truck should not go back to start but can ed in a different location
- That it is possible to get to every stop from every stop. 