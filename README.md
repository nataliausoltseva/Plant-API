# Plant API

Created my own replica of [Trefle](https://github.com/treflehq/trefle-api) since Trefle is not under support or development anymore.

## What is Plant API?
It is a JSON REST API for plant species, with support of getting all species by pagination (10 species per page), getting one species by ID and searching by providing a query param to the end point.

## How to setup?
The dump file can be found here: https://github.com/treflehq/dump

All the API code located under `Plant-API` directory. Full support for all the columns that are provided by the dump file. You will need to update the path for your CSV file in the `DataImporter.cs` file.

**Note: the columns are not perfectly split by `\t` and require manual job to sort out the CSV file.**

Once you build the project, your API is accessable through https://localhost:7072/index.html
![landing page of API swagger](/screenshots/image.png)