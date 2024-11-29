# Route

To run the code make sure you have an api key for the Google route api.

## Steps: 
- Clone the repo.
- `dotnet restore`
- `dotnet user-secrets init`
- `dotnet user-secrets set "GoogleApiKey" "{}API_KEY"`
- `dotnet run`


Now the API is running. A swagger site should open such that the endpoints in the API can be seen.

There are two endpoints:
1. One for adding a new stop
1. One for calculating the route

A stop should have a name, latitude and longitude.

The response of the route is a list in order of the route. 

