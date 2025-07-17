# cosmosdbtest
cosmosdbtest

# This project is a test exercises to demonstrate the use of Azure Cosmos DB to connect, store and access data from a .NET console application with a custom data class structure
The project is partially working with the blueprint code to connect, store and retrieve family data from Azure Cosmos DB, but to improve the application further you must complete the below exercise tasks to connect and more efficiently store and query the data.
If you do run out of the allocated time, you can describe how you would complete the tasks in comments or verablly following it up.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/7db19e48-7cf4-4dcb-abdd-4e39cf0aec93" />

Git clone the project https://github.com/capgemhon/cosmosdbtest in Visual Studio

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/3b72b2ab-dda2-4928-9fd2-6a65ed6235fe" />

The project should automatically install the .NET Library Dependencies, if it doesn't though, install the below packages from Nuget in Tools -> Nuget Package Manager -> Manage Nuget Packages For Solution: 

Microsoft.Azure.Cosmos (version 3.5.2)
Newtonsoft.Json (version 13.0.3)
NSubstitute (version 5.3.0)

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/a33cb825-8012-4927-a49b-d920a321c84b" />



These are 10 tasks to try and complete below:
# Tasks

// TASK 1: Replace the AuthorizationKey with your Cosmos DB account key to try and connect to local database (if possible). If you cant connect, can you type use the method GenerateRandomBase64String with 32 bytes instead below and remove const.

// TASK 2: Add FamilyDatabase as the value to DatabaseId and add FamilyContainer as the value to ContainerId. What is the purpose of const keyword?

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

// TASK 8: Add a method to print the contents of a Family object in a readable format containing each of their attributes including Id, LastName, Address, and IsRegistered, and if you can Parents and Children. You can describe as pseudocode as an alternative approach.
// E.g. an example of pseudocode can be below

// Class Family:
//     Attributes:
//     Id: Integer

// Method PrintDetails() :
//     Print "Family Details:"
//     Print "----------------"
//     Print "ID: " + Id


// TASK 9: How would you retrieve the EndpointUrl, and AuthorisationKey in a more secure way? Just describe as pseudocode.

// TASK 10: How would you handle the case when the Cosmos DB service is unavailable at that specific moment, what can you do next? Just describe as pseudocode.

// TASK 11: Can you explain why we have used NSubstitute as a framework throughout in the codebase?

// UNIT TESTS. You can write this as a comment

// TASK 12: Add an assert to check that the first name of first parent is Bob

// TASK 13: Add an assert to check that the first name of second parent is Mary

// TASK 14: Add an assert to check that the first name of first child is Jan

// TASK 15: Add an assert to check that the country of family address is Santa Clara
