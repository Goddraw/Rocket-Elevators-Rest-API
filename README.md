# Rocket-Elevators-Rest-API

This project includes following 9 Rest APIs to query the MYSQL database of Rocket Elevators. 9 APIs were deployed on https://rocketelevatorsapi.azurewebsites.net for public access.

1. Retrieving the current status of a specific Battery
    GET api/batteries/status/{id}
2. Changing the status of a specific Battery
    PUT api/batteries/status/{id}?status={status}
3. Retrieving the current status of a specific Column
    GET api/columns/status/{id}
4. Changing the status of a specific Column
    PUT api/columns/status/{id}?status={status}
5. Retrieving the current status of a specific Elevator
    GET api/elevators/status/{id}
6. Changing the status of a specific Elevator
    PUT api/elevators/status/{id}?status={status}
7. Retrieving a list of Elevators that are not in operation at the time of the request
    GET api/elevators/offline
8. Retrieving a list of Buildings that contain at least one battery, column or elevator requiring intervention
    GET api/buildings/intervention
9. Retrieving a list of Leads created in the last 30 days who have not yet become customers.
    GET api/leads/newleads

Details of the API is in this documentation: https://documenter.getpostman.com/view/6936445/UVyrVx3M
#### Please note when you do 'PUT' requests using curl commands, please also include '-d' in your command. For example: curl -d --location --request PUT 'https://rocketelevatorsapi.azurewebsites.net/api/columns/status/1?status=online'

