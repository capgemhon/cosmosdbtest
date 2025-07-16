# cosmosdbtest
cosmosdbtest

# This project is a test exercises to demonstrate the use of Azure Cosmos DB to connect, store and access data from a .NET console application with a custom data class structure
The project is partially working with the blueprint code to connect, store and retrieve family data from Azure Cosmos DB, but to improve the application further you must complete the below exercise tasks to connect and more efficiently store and query the data.
If you do run out of the allocated time, you can describe how you would complete the tasks in comments or verablly following it up.
These are 10 tasks to try and complete below:
# Tasks

// TASK 1: Replace the AuthorizationKey with your Cosmos DB account key to try and connect to local database (if possible)

// TASK 2: Add FamilyDatabase as the value to DatabaseId and add FamilyContainer as the value to ContainerId

// TASK 3: Write a try catch block to handle CosmosException specifying Cosmos DB error: error message, then Exception specifying General error: error message

// TASK 4: Add an Address property called School for the Child and School Name asstring called SchoolName

// TASK 5: Create an item in the container representing the Brady family with Id Brady.3
// Parents - Bob Brady, Mary Brady
// Children - Jan Brady, Bobby Brady, Cindy Brady
// Address - State: CA, County: Santa Clara, City: San Jose
// School Address - State: CA, County: Santa Clara, City: Santa Clara with SchoolName: Santa Clara High School
// Pets - Fluffy, and Shadow

// TASK 6: Place this into a refactored method as follows (if ppssible)
//public Family CreateFamily(
//    string familyId,
//    string lastName,
//    bool isRegistered,
//    Parent[] parents,
//    Child[] children,
//   Address address)
//        {
//
//return new Family.....
//}

// TASK 7: Can you refactor this query to use a parameterized query instead of hardcoding the LastName value? 

// TASK 8: Add a method to print the contents of a Family object in a readable format containing each of their attributes including Id, LastName, Address, and IsRegistered, and if you can Parents and Children.

// TASK 9: How would you store the EndpointUrl, and AuthorisationKey in a more secure way? Just describe as comments.

// TASK 10: How would you handle the case when the Cosmos DB service is unavailable at that moment? Just describe as comments.