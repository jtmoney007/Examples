# Kubernetes Configuration Example

This repository contains Kubernetes YAML files that demonstrate how to set up various resources within a Kubernetes cluster, including a Namespace, ConfigMap, ClusterRoles, and a Job. These resources are used together to illustrate a configuration scenario.

## Setup Namespace

The Namespace my-local-namespace-configmap is created to isolate resources for this configuration example.

```bash
kubectl apply -f kubernetes_setup.yaml
```

## Setup ConfigMap

The ConfigMap my-config-map is defined with sample configuration data used by applications within the namespace.

```bash
kubectl apply -f kubernetes_configmap.yaml
```

## Setup Cluster Roles

A ClusterRole named configmap-reader-role is created, allowing get and list operations on ConfigMaps within the namespace.

```bash
kubectl apply -f kubernetes_clusterrole.yaml
```

## Setup Cluster Role Binding

A ClusterRoleBinding named configmap-reader-binding is created as a service account, allowing get and list operations on ConfigMaps within the config map reader role.

```bash
kubectl apply -f kubernetes_clusterrolebinding.yaml
```

## Create Job

A Job named configmapconsoleapp-job is defined to run a container, illustrating how to access ConfigMap values using environment variables.

```bash
kubectl apply -f kubernetes_job.yaml
```

## Usage

After the job has executed, a POD will show the configuration map values in the log terminal.

To access a pod's logs in Kubernetes, you can utilize the kubectl logs command.

### Cleanup

To remove the deployed resources, you can delete the Kubernetes objects:
```bash
kubectl delete job configmapconsoleapp-job -n my-local-namespace-configmap
kubectl delete clusterrolebinding configmap-reader-binding -n my-local-namespace-configmap
kubectl delete clusterrole configmap-reader-role -n my-local-namespace-configmap
kubectl delete configmap my-config-map -n my-local-namespace-configmap
kubectl delete namespace my-local-namespace-configmap
```