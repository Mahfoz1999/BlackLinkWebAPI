BlackLinkWebAPI

BlackLinkWebAPI is a social web API built with ASP.NET Core 7 and the CQRS pattern using MediatR. It is still under development and not ready for production use yet.
Getting Started
To get started with the project, you will need to have the following prerequisites installed:

.NET SDK 7.x or higher
Git
Once you have the prerequisites installed, you can clone the project from GitHub using the following command:
git clone https://github.com/your-username/BlackLinkWebAPI.git

Then, navigate to the project directory and run the following command to build and run the API:
dotnet run
The API will start listening on https://localhost:5001 by default.

Architecture
The project is built using the CQRS pattern with MediatR. It consists of the following layers:

Presentation layer: Handles incoming HTTP requests and sends commands to the application layer.
Application layer: Contains the business logic of the application and handles commands and queries.
Domain layer: Contains the domain model and business rules.
Infrastructure layer: Contains implementation details such as data access and external service integrations.

Features
The following features are currently implemented in the API:

Authentication: Users can create an account and authenticate with JWT tokens.
Posts: Users can create, read, update, and delete posts.
Comments: Users can create, read, update, and delete comments on posts.
Contributing
Contributions are welcome! If you would like to contribute to the project, please follow these steps:

Fork the repository
Create a new branch for your changes
Make your changes and commit them to your branch
Push your branch to your forked repository
Create a pull request to merge your changes into the main repository
License
This project is licensed under the MIT License. See the LICENSE file for details.

Acknowledgments
This project was inspired by [some other open source project or tutorial that you used to learn about CQRS with MediatR and ASP.NET Core].
