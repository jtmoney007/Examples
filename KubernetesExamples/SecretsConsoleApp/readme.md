## Kubernetes Secrets

Kubernetes Secrets are a crucial resource for securely managing sensitive data within a Kubernetes cluster. They are designed to store and control access to confidential information, such as passwords, API keys, tokens, and other application-specific secrets.

### Storage of Sensitive Data

Kubernetes Secrets are used to store sensitive data that applications need to function but should not be exposed in plain text. Examples include database credentials, SSL certificates, and authentication tokens.

### Immutable

Secrets are intended to be immutable, meaning their data should not change after creation. If you need to update a secret, it's best to create a new one and delete the old version.

### Data Formats

Secrets can store binary data or text-based data. For text-based data, such as configuration files or environment variables, Secrets can be created using key-value pairs.

### Base64 Encoding

When storing data in Secrets, Kubernetes expects the data to be base64 encoded. This encoding provides a basic level of obfuscation but is not a form of encryption. Users should not rely on base64 encoding for security but rather on other mechanisms like encryption or access controls.

### Resource Types

Kubernetes provides two resource types for managing secrets: `Secret` and `Opaque`. The `Opaque` type is a generic container for storing data, while `Secret` is more structured and suitable for certain use cases like TLS certificates.

### Secret Types

- `Opaque`: This is the most common type used for general-purpose secrets. It stores arbitrary data as key-value pairs.
- `kubernetes.io/service-account-token`: This type is used automatically to provide service account tokens to pods. You don't need to create these Secrets manually.
- `kubernetes.io/dockercfg`: Used for storing Docker registry credentials.
- `kubernetes.io/dockerconfigjson`: Similar to `dockercfg`, but stores Docker registry credentials as JSON.

### Access Control

Access to Secrets is controlled through Kubernetes' Role-Based Access Control (RBAC) system. You can define who can read and write to specific Secrets using RBAC roles and role bindings.

### Namespaces

Secrets are typically associated with a specific namespace. This means that a Secret is only accessible by pods within the same namespace unless explicitly shared with other namespaces.

### Volume Mounting

Kubernetes provides a way to mount Secrets as volumes into pods. This allows applications running in pods to access secret data as files.

### Secret Management

Managing Secrets should include practices like regular rotation of credentials, monitoring for unauthorized access, and secure transmission of secrets when necessary.

### Secret Managers

In addition to Kubernetes-native Secrets, organizations often use external secret management solutions like HashiCorp Vault, AWS Secrets Manager, or Azure Key Vault for more advanced secret management, access control, and auditing.

### Limitations

Kubernetes Secrets are not intended to be a highly secure vault for storing extremely sensitive information. For more robust security, organizations should consider using dedicated secret management tools designed for this purpose.

## Prerequisites

Before you begin, ensure you have the following prerequisites:

- .NET SDK installed on your local machine.
- A running Kubernetes cluster.
- `kubectl` configured to communicate with your cluster.
- Basic knowledge of Kubernetes concepts, including Secrets.

## Usage

Follow these steps to run the application:

1. Clone this repository to your local machine.

2. Build the application using the following command:

   ```bash
   dotnet build
   ```
   
3. Set the environment variables for your application by specifying the job's namespace, pod name, and the secret name. You can do this in your Kubernetes YAML files when you create the Job.

