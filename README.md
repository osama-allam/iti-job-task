# ASP Core Web API For Generating Products

A ASP Core v2.2 web API that provides a list of products within a variety of companies.

* Supports only `application/json` or `application/xml` responses
* For the companies routes:
  * `http://localhost:port/api/companies`  this will return a list of companies and the company object in the following format
  * `http://localhost:port/api/companies/{id}`  this will return a
single company matching the `{id}`

  * JSON response:

    ```json
    {
        "id": 0,
        "name": "string"
    }
    ```

* For the products routes:

  * `http://localhost:port/api/products` this will return a list of products and the company object in the following format

  * `http://localhost:port/api/products/{id}` this will return a
      single company matching the `{id}`.

    * Retrieve a product by `{id}` with GET request
    * Update a product by `{id}` and also provide a `product` object in the request body

  * JSON response:

      ```json
      {
          "id": 0,
          "name": "string",
          "imageUrl": "string",
          "price": 0,
          "companyId": 0,
          "companyName": "string"
      }
      ```

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to setup the project

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/)
* [ASP Core 2.2 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2)
* [SQL Server 2016](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installing

A step by step series of examples that tell you how to get a development env running

### How to create the database

1. After cloning the project check **back-end** branch and open the solution in **_ProductAPICore_** folder with visual studio 2017
2. Open **_Package Manager Console_** from

    ```text
    Tools -> NuGet Package Manager -> Package Manager Console
    ```

3. Then type **`Update-Database`** to create database from migrations

4. Then run the project using :arrow_forward: IIS Express this will seed the database using seeding method in `Startup.cs`

    ```csharp
    unitOfWork.EnsureSeedDataForContext();
    ```

5. Project will open on the `Swagger` *(Swashbuckle)* documentation page where there is a full description on how to use the API

> **Note:** That's it, Make sure to to follow these steps carefully!

## Running the tests

### To run the unit tests included in the project:

1. Open the solution in Visual Studio 2017

2. In **_Tests_** solution folder open `ProductAPICore`

3. You will find two test `classes`

   * `CompaniesControllerTest.cs` and `ProductsController.cs`

4. To :arrow_forward: run the unit tests open **`Test Explorer`**

    ```text
    Tests -> Windows -> Tests Explorer
    ```

5. From **`Test Explorer`** you can the any of the provided tests

> ### **_Note: End-to-End Testing_**
> To isolate unit testing from **SQL Server** database we used [InMemory Provider](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/) to create database in memory to avoid manipulating the actual database  

## Authors

* **Osama Shawky Allam** - *Initial work* - [Email](osama_allam@ymail.com) - [LinkedIn](https://www.linkedin.com/in/osama-allam/)

See also the list of [contributors](https://github.com/osama-allam/iti-job-task/graphs/contributors) who participated in this project.
