# Kubernetes Secrets Example

This repository contains Kubernetes YAML files that demonstrate how to set up various resources within a Kubernetes cluster, including a Namespace, Secrets, Role, and a Job. These resources are used together to illustrate a configuration scenario.

## Setup Namespace

The Namespace my-local-namespace-secrets is created to isolate resources for this configuration example.

```bash
kubectl apply -f kubernetes_setup.yaml
```

## Setup Secret

The Secrets my-secret is defined with sample configuration data used by applications within the namespace.

```bash
kubectl apply -f kubernetes_secrets.yaml
```

## Setup Role

A Role named secrets-reader-role is created, allowing get and list operations on ConfigMaps within the namespace.

```bash
kubectl apply -f kubernetes_role.yaml
```

## Setup Role Binding

A RoleBinding named read-secrets-binding is created as a service account, allowing get and list operations on Secrets within the secret reader role.

```bash
kubectl apply -f kubernetes_rolebinding.yaml
```

## Create Job

A Job named secretsconsoleapp-job is defined to run a container, illustrating how to access Secret values using environment variables.

```bash
kubectl apply -f kubernetes_job.yaml
```

## Usage

After the job has executed, a POD will show the secret base64 values in the log terminal.

To access a pod's logs in Kubernetes, you can utilize the kubectl logs command.

### Cleanup

To remove the deployed resources, you can delete the Kubernetes objects:
```bash
kubectl delete job secretsconsoleapp-job -n my-local-namespace-secrets
kubectl delete rolebinding read-secrets-binding -n my-local-namespace-secrets
kubectl delete role secrets-reader-role -n my-local-namespace-secrets
kubectl delete secrets my-secret -n my-local-namespace-secrets
kubectl delete namespace my-local-namespace-secrets
```