# Exercise: HttpClient for the Library Manager REST API

This exercise will modify our previous console application so the Library Manager can use an HTTPClient against our API instead of direct database access. This is similar to how desktop and mobile applications interact with real-life REST APIs.

You can find the initial setup in the **401\Exercises\03-LibraryManagement-With-HttpClient\Start\\** folder if you haven't completed the previous exercise.

## Add the Necessary Dependencies

To gain access to the `HttpClient` class, you will need to add the `System.Net.Http` NuGet package to the console application.

You will also need the ability to serialize and deserialize JSON. To be able to do so, you will need to install the `System.Text.Json` NuGet package.

## Remove Unnecessary Dependencies

Since you will no longer be accessing services directly from the console application, you will need to remove the reference to the class library that represents the application layer. However, you will still need to reference the core library, as it has the data transfer objects that are exchanged between the REST API and the client.

## Adding a Separate Set of Client Settings

Since your client will now be a separate application from your server, it will need to have a separate `appsettings.json` file with different settings. The client will no longer have direct access to the database; therefore it will no longer need to have any database-specific settings, such as database mode and the connection string. However, it needs to have the setting that contains the base address of the Web API application, which can be found in the `launchSettings.json` file of the application.

All the code that reads the settings inside the console app will need to be modified accordingly.

## Replace Direct Service Access With HTTP Requests

All the workflow and menu logic inside the console app will remain the same. However, instead of interacting with the service classes directly, you will now be sending HTTP requests to the REST API. To do so, you will need to instantiate the `HttpClient` class with the base address read from the settings. Then for each of the REST API endpoints, you will need to invoke the appropriate method (`GetAsync()` for a `GET` endpoint, `PostAsync()` for a `POST` endpoint, etc.)

Different API endpoints will return different response codes. However, any response code that indicates success will begin with 2 (200, 201, 204, etc). The `HttpResponseMessage` object that gets returned from any of the above methods has a convenient way of checking if the request was successful without having to verify any particular response code. You can you the `IsSuccessStatusCode` property to do so. It will be set to `true` if the request is successful.

You can read the response data by invoking the `ReadAsStringAsync()` method and reading the `Result` property of the returned object.

## Add JSON Serialization and Deserialization

The `ReadAsStringAsync()` method will return a string representation of the data returned from the REST API. This is OK if you expect the data to contain an error message. However, this is not what you need if you need to interact with objects. You will need to deserialize data first. To do so, you can use the static `JsonSerializer.Deserialize<T>()` method, where you would replace `T` with the data type you expect to be returned, e.g. `List<Borrower>`, `Media`, etc.

If you need to add data to a `POST` or `PUT` request, you will need to serialize the data into a JSON string. However, you will also need to populate the right request headers. The easiest way to do so is to execute the following code:

```c#
var payload = JsonSerializer.Serialize(data);
var requestData = new StringContent(payload, Encoding.UTF8, "application/json");
```

In this setup, the `data` variable can be an instance of any object, such as `Media`. The `requestData` variable can then be used as a parameter in either `PostAsync()` or `PutAsync()` invocation of the `HttpClient`.

### Configure the JSON Serializer

While deserializing objects, you need to make sure that the deserializer is configured to recognize the property names in the JSON string. While the property names are expected to match those of the corresponding C# DTO class, the case may be mismatched. To make sure the deserializer recognizes all the properties correctly, you need to do either of these:

1. Add the `JsonPropertyName` attribute to all the properties in the DTO classes with the property name defined as its value, such as this:

```c#
public class Borrower
{
    [JsonPropertyName("BorrowerID")]
    public int BorrowerID { get; set; }

    [JsonPropertyName("FirstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("LastName")]
    public string LastName { get; set; }

    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("Phone")]
    public string Phone { get; set; }
}
```

2. Configure the JSON deserializer to ignore the case in the property names by passing the following object into the `JsonSerializer.Deserialize<T>()` method:

```c#
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
};
```

This way, a class property with the name of `BorrowerID` will be matched to any of these JSOn fields:

* `borrowerId`
* `borrowerID`
* `BORROWERID`
* `BorrowerID`
* etc.