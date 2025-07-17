using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace todo
{

    // ----------------------------------------------------------------------------------------------------------
    // Prerequisites -
    //
    // 1. An Azure Cosmos account - 
    //    https://docs.microsoft.com/azure/cosmos-db/create-cosmosdb-resources-portal
    //
    // 2. Azure.Cosmos NuGet package - 
    //    http://www.nuget.org/packages/Azure.Cosmos/ 
    // ----------------------------------------------------------------------------------------------------------
    public class Program
    {
        // <Constants>
        private const string EndpointUrl = "https://localhost:8081/";
        // TASK 1: Replace the AuthorizationKey with your Cosmos DB account key to try and connect to local database (if possible)
        private const string AuthorizationKey = "";
        // TASK 2: Add FamilyDatabase as the value to DatabaseId and add FamilyContainer as the value to ContainerId
        private const string DatabaseId = "";
        private const string ContainerId = "";
        // </Constants>

        // <Main>
        static async Task Main(string[] args)
        {
            CosmosClientOptions options = new CosmosClientOptions
            {
                RequestTimeout = TimeSpan.FromSeconds(60)
            };

            // TASK 3: Write a try catch block to handle CosmosException specifying Cosmos DB error: error message, then Exception specifying General error: error message
            CosmosClient cosmosClient = new CosmosClient(EndpointUrl, AuthorizationKey, options);
            await Program.CreateDatabaseAsync(cosmosClient);
            await Program.CreateContainerAsync(cosmosClient);
            await Program.AddItemsToContainerAsync(cosmosClient);
            await Program.QueryItemsAsync(cosmosClient);
            await Program.ReplaceFamilyItemAsync(cosmosClient);
            await Program.DeleteFamilyItemAsync(cosmosClient);
            await Program.DeleteDatabaseAndCleanupAsync(cosmosClient);
        }
        // </Main>

        // <CreateDatabaseAsync>
        /// <summary>
        /// Create the database if it does not exist
        /// </summary>
        private static async Task CreateDatabaseAsync(CosmosClient cosmosClient)
        {
            // Create a new database  
            DatabaseResponse databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(Program.DatabaseId);
            Console.WriteLine("Created Database: {0}\n", databaseResponse.Database.Id);
        }
        // </CreateDatabaseAsync>

        // <CreateContainerAsync>
        /// <summary>
        /// Create the container if it does not exist. 
        /// Specify "/LastName" as the partition key since we're storing family information, to ensure good distribution of requests and storage.
        /// </summary>
        /// <returns></returns>
        private static async Task CreateContainerAsync(CosmosClient cosmosClient)
        {
            // Create a new container  
            Container container = await cosmosClient.GetDatabase(Program.DatabaseId).CreateContainerIfNotExistsAsync(Program.ContainerId, "/LastName");
            Console.WriteLine("Created Container: {0}\n", container.Id);
        }
        // </CreateContainerAsync>

        // <AddItemsToContainerAsync>
        /// <summary>
        /// Add Family items to the container
        /// </summary>
        private static async Task AddItemsToContainerAsync(CosmosClient cosmosClient)
        {
            // Create a family object for the Andersen family
            Family andersenFamily = new Family
            {
                Id = "Andersen.1",
                LastName = "Andersen",
                Parents = new Parent[]
                {
                    new Parent { FirstName = "Thomas" },
                    new Parent { FirstName = "Mary Kay" }
                },
                Children = new Child[]
                {
                    new Child
                    {
                        FirstName = "Henriette Thaulow",
                        Gender = "female",
                        Grade = 5,
                        Pets = new Pet[]
                        {
                            new Pet { GivenName = "Fluffy" }
                        }
                    }
                },
                Address = new Address { State = "WA", County = "King", City = "Seattle" },
                IsRegistered = false
            };

            Container container = cosmosClient.GetContainer(Program.DatabaseId, Program.ContainerId);
            try
            {
                // Read the item to see if it exists.  
                ItemResponse<Family> andersenFamilyResponse = await container.ReadItemAsync<Family>(andersenFamily.Id, new PartitionKey(andersenFamily.LastName));
                Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource.Id);
            }
            catch (CosmosException ex) when (ex.SubStatusCode == (int)HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
                ItemResponse<Family> andersenFamilyResponse = await container.CreateItemAsync<Family>(andersenFamily, new PartitionKey(andersenFamily.LastName));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse.
                Console.WriteLine("Created item in database with id: {0}\n", andersenFamilyResponse.Resource.Id);
            }

            // Create a family object for the Wakefield family
            Family wakefieldFamily = new Family
            {
                Id = "Wakefield.7",
                LastName = "Wakefield",
                Parents = new Parent[]
                {
                    new Parent { FamilyName = "Wakefield", FirstName = "Robin" },
                    new Parent { FamilyName = "Miller", FirstName = "Ben" }
                },
                Children = new Child[]
                {
                    new Child
                    {
                        FamilyName = "Merriam",
                        FirstName = "Jesse",
                        Gender = "female",
                        Grade = 8,
                        Pets = new Pet[]
                        {
                            new Pet { GivenName = "Goofy" },
                            new Pet { GivenName = "Shadow" }
                        }
                    },
                    new Child
                    {
                        FamilyName = "Miller",
                        FirstName = "Lisa",
                        Gender = "female",
                        Grade = 1
                    }
                },
                Address = new Address { State = "NY", County = "Manhattan", City = "NY" },
                IsRegistered = true
            };

            // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
            ItemResponse<Family> wakefieldFamilyResponse = await container.UpsertItemAsync<Family>(wakefieldFamily, new PartitionKey(wakefieldFamily.LastName));

            // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
            Console.WriteLine("Created item in database with id: {0}\n", wakefieldFamilyResponse.Resource.Id);

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
        }

        // </AddItemsToContainerAsync>

        // <QueryItemsAsync>
        /// <summary>
        /// Run a query (using Azure Cosmos DB SQL syntax) against the container
        /// </summary>
        private static async Task QueryItemsAsync(CosmosClient cosmosClient)
        {
            // TASK 7: Can you refactor this query to use a parameterized query instead of hardcoding the LastName value? 
            var sqlQueryText = "SELECT * FROM c WHERE c.LastName = 'Andersen'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            Container container = cosmosClient.GetContainer(Program.DatabaseId, Program.ContainerId);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);

            List<Family> families = new List<Family>();

            FeedIterator<Family> feedIterator = container.GetItemQueryIterator<Family>(queryDefinition);

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<Family> response = await feedIterator.ReadNextAsync();
                foreach (Family family in response)
                {
                    // Process each family item
                    families.Add(family);
                    Console.WriteLine("\tRead {0}\n", family);
                }
            }
        }
        // </QueryItemsAsync>

        // <ReplaceFamilyItemAsync>
        /// <summary>
        /// Replace an item in the container
        /// </summary>
        private static async Task ReplaceFamilyItemAsync(CosmosClient cosmosClient)
        {
            Container container = cosmosClient.GetContainer(Program.DatabaseId, Program.ContainerId);

            ItemResponse<Family> wakefieldFamilyResponse = await container.ReadItemAsync<Family>("Wakefield.7", new PartitionKey("Wakefield"));
            Family itemBody = wakefieldFamilyResponse;

            // update registration status from false to true
            itemBody.IsRegistered = true;
            // update grade of child
            itemBody.Children[0].Grade = 6;

            // replace the item with the updated content
            wakefieldFamilyResponse = await container.ReplaceItemAsync<Family>(itemBody, itemBody.Id, new PartitionKey(itemBody.LastName));

            Console.WriteLine("Updated Family [{0},{1}].\n \tBody is now: {2}\n", itemBody.LastName, itemBody.Id, wakefieldFamilyResponse.Resource);
        }
        // </ReplaceFamilyItemAsync>

        // <DeleteFamilyItemAsync>
        /// <summary>
        /// Delete an item in the container
        /// </summary>
        private static async Task DeleteFamilyItemAsync(CosmosClient cosmosClient)
        {
            Container container = cosmosClient.GetContainer(Program.DatabaseId, Program.ContainerId);

            string partitionKeyValue = "Wakefield";
            string familyId = "Wakefield.7";

            // Delete an item. Note we must provide the partition key value and id of the item to delete
            ItemResponse<Family> wakefieldFamilyResponse = await container.DeleteItemAsync<Family>(familyId, new PartitionKey(partitionKeyValue));
            Console.WriteLine("Deleted Family [{0},{1}]\n", partitionKeyValue, familyId);
        }
        // </DeleteFamilyItemAsync>

        // <DeleteDatabaseAndCleanupAsync>
        /// <summary>
        /// Delete the database and dispose of the Cosmos Client instance
        /// </summary>
        private static async Task DeleteDatabaseAndCleanupAsync(CosmosClient cosmosClient)
        {
            Database database = cosmosClient.GetDatabase(Program.DatabaseId);
            DatabaseResponse databaseResourceResponse = await database.DeleteAsync();

            Console.WriteLine("Deleted Database: {0}\n", Program.DatabaseId);
        }
        // </DeleteDatabaseAndCleanupAsync>

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

        // TASK 10: How would you handle the case when the Cosmos DB service is unavailable at that specificmoment, what can you do next? Just describe as pseudocode.
    }
}