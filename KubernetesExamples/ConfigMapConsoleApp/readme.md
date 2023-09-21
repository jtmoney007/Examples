# ConfigMap Reader for Kubernetes

This C# console application demonstrates how to read data from a Kubernetes ConfigMap in a specified namespace. It uses the official Kubernetes C# client library to interact with the Kubernetes API.

## Working with Kubernetes ConfigMaps

Kubernetes ConfigMaps allow you to decouple configuration data from container images, making it easier to manage and update configuration settings without changing your application code.

ConfigMap values are structured with a name/key format for straightforward creation and efficient management.

### Reading Data from a ConfigMap
This application can read data from a ConfigMap specified by its name and namespace. Set the JOB_NAMESPACE and JOB_CONFIGMAP environment variables when you create your Kubernetes Job YAML.

### Prerequisites

Before you begin, ensure you have the following prerequisites:

- .NET SDK installed on your local machine.
- A running Kubernetes cluster.
- `kubectl` configured to communicate with your cluster.
- Basic knowledge of Kubernetes concepts, including ConfigMaps.

## Usage

Follow these steps to run the application:

1. Clone this repository to your local machine.

2. Build the application using the following command:

   ```bash
   dotnet build
   ```
   
3. Set the environment variables for your application by specifying the job's namespace, name, and the target ConfigMap. You can do this in your Kubernetes YAML files when you create the Job.

