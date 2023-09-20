# Kubernetes Local File System Example

This repository contains Kubernetes YAML files that demonstrate how to set up a local file system for reading and writing to a common file within a Kubernetes cluster. This example uses a CronJob to run a simple console application that reads and writes data to a shared volume.

## Prerequisites

Before you begin, ensure you have the following prerequisites:

- A running Kubernetes cluster.
- `kubectl` configured to communicate with your cluster.
- Basic knowledge of Kubernetes concepts.

## Step-by-Step Guide

Follow these steps to set up and run the example:

### Step 1: Create the Namespace

The first step is to create a `Namespace` that defines namespace for the project.

```bash
kubectl apply -f kubernetes_setup.yaml
```

### Step 2: Create the Storage

Next, we are going to create a StorageClass, Persistent Volume and a Persisten Volume Claim. Make sure to adjust the hostPath to match your local filesystem.

> **Note:** For windows machines make sure to add the following before the starting of your drive letter (ie: c) "/run/desktop/mnt/host/"

```bash
kubectl apply -f kubernetes_storage.yaml
```

### Step 3: Deploy the CronJob
Finally, deploy the CronJob that runs the console application. This application reads and writes data to the shared volume. The POD_NAME environment variable is set based on the pod's name.

```bash
kubectl apply -f kubernetes_cronjob.yaml
```

### Cleanup

To remove the deployed resources, you can delete the Kubernetes objects:
```bash
kubectl delete cronjob consoleapp1-cronjob -n my-local-namespace
kubectl delete pvc my-persistent-volume-claim -n my-local-namespace
kubectl delete pv my-persistent-volume -n my-local-namespace
kubectl delete storageclass my-local-hostpath -n my-local-namespace
```