# OrchardCore.MassTransit

OrchardCore.MassTransit is an open-source project that demonstrates the integration of MassTransit with Orchard Core. This project provides a comprehensive example of how to configure and use MassTransit within an Orchard Core application.

## Features

- **MassTransit Integration**: Demonstrates how to set up and configure MassTransit in an Orchard Core application.
- **Event Handling**: Includes examples of event consumers and event publishing.
- **Configuration**: Shows how to configure MassTransit using `appsettings.json`.
- **Dependency Injection**: Utilizes Orchard Core's dependency injection framework to manage MassTransit components.

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Orchard Core](https://orchardcore.net/)
- [MassTransit](https://masstransit-project.com/)

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/emrahtokalak/OrchardCore.MassTransit.git
    cd OrchardCore.MassTransit
    ```

2. Restore the dependencies:
    ```sh
    dotnet restore
    ```

3. Build the project:
    ```sh
    dotnet build
    ```

### Configuration

Configure MassTransit in the `appsettings.json` file located in the [OrchardCore.MassTransit](http://_vscodecontentref_/0) directory. Example configuration:

```json
{
  "OrchardCore_MassTransit": {
    "RabbitMQ": {
      "Configuration": "amqp://guest:guest@localhost:5672"
    }
  }
}
