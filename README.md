### Here is a quick installation guide:

Installing
Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)

#### You will need the following tools:

  Visual Studio 2019

  .Net Core 3.1 SDK or later

  Docker Desktop(optional)

  Clone the repository

  Redis(optional)

  At the src directory, where folders for the projects are present run below command:

#### Install Redis using Docker File
  - Redis cache is optional in this web application.
  - Redis is turned off by default.

##### To turn on redis:
    - change IsCacheActivated option to true in appSettings.json file
    - docker-compose -f docker-compose.yml up -d

#### Install web app
  - navigate to AugustusMartinWebApp directory
  run following command:
    - dotnet build
    - dotnet run

#### You can launch the app with below url:
  https://localhost:5001

#### You can launch the swagger UI with below url:
  https://localhost:5001/swagger
