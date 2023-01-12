## How to run the App
- Clone or download the source code.
- Run docker-compose up -d from /src folder

## Accessing the Apis
Once the app is running open browser
- Navigate to http://localhost:8081/swagger

## Accessing the logs
- Navigate to http://localhost:8082/#/events

## Accessing health check endpoint
- Navigate to http://localhost:8081/health or http://localhost:8081/healthz

## Techincal Details
Developed using
- ASP.NET Core 
- .Net 7.0
- Maria DB
- Seq - to write and store logs
- Quartz.Net for scheduling task

## Automated Testing
- For Unit tests -> xunit.net
- For Acceptance Tests -> SpecFlow -> https://specflow.org/
Acceptance Tests also fires up Docker Compose and shutsdown when test run completes.

## Navigating Source Code
- Best to use Visual Studio 2022 or Visual Studio Code
- Timer Api -> src/TimerApi
- Unit Tests -> src/UnitTests
- Acceptance Tests -> src/AcceptanceTests
- Docker Compose -> src/docker-compose.yaml
