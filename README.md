# SigmaTest
It was mentioned in the description that "The application should contain just one API endpoint". So it has only one API endpoint.

# Project Overview

This project follows a modular monolith and vertical slice architecture approach. The architecture is designed to promote modularity, maintainability, and scalability.

## Sigma.API

The Sigma.API module is structured to serve as an API gateway with minimal changes required. It provides a centralized entry point for accessing various features and functionalities of the application.

## CandidateHub

CandidateHub is implemented as a separate API feature module. It is designed to be deployed independently as a microservice if needed, with minimal modifications. This approach allows for better isolation and scalability of the candidate management functionality.

## Database Approach

The project adopts a database-first approach, but it is flexible enough to accommodate a code-first approach if required. This allows for seamless integration with existing databases and better alignment with database design practices.

## CQRS Pattern

The Command Query Responsibility Segregation (CQRS) pattern is employed in the project architecture. This pattern separates the responsibility for handling commands (write operations) from queries (read operations), promoting better scalability and performance optimization.

## Authentication and Authorization

Authentication and authorization features are not enabled by default in the project. However, the architecture is designed to easily integrate with various authentication solutions such as Identity Core, Keycloak, or IdentityServer. This provides flexibility in implementing authentication and authorization mechanisms based on project requirements and security policies.

## Unit Testing
Unit test is implemented on the major logical section. Most of the codes are related to middleware, service registration and project setup which are rarely changed so unit testing has been avoided.
