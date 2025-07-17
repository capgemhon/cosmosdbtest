using System.Text.Json;
using System.Text.Json.Serialization;

namespace cosmosdbtest.Models
{
    public class Family
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        public string LastName { get; set; }
        public Parent[] Parents { get; set; }
        public Child[] Children { get; set; }
        public Address Address { get; set; }
        public bool IsRegistered { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        public static Family FromJson(string json)
        {
            return JsonSerializer.Deserialize<Family>(json);
        }
    }

    public class Parent
    {
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
    }

    public class Child
    {
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public int Grade { get; set; }
        public Pet[] Pets { get; set; }

        // TASK 4: Add an Address property called School for the Child and School Name asstring called SchoolName
    }

    public class Pet
    {
        public string GivenName { get; set; }
    }

    public class Address
    {
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
    }
}