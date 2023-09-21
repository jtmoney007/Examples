# Kubernetes Local File System Example

This repository contains Kubernetes YAML files that demonstrate how to set up a local file system for reading and writing to a common file within a Kubernetes cluster. This example uses a CronJob to run a simple console application that reads and writes data to a shared volume. The setup includes the usage of Persistent Volumes (PVs), Persistent Volume Claims (PVCs), and Storage Classes, allowing you to manage and persist data efficiently within your Kubernetes environment.

*Persistent Volumes (PVs)* represent physical storage volumes within the cluster, such as local storage or network-attached storage (NAS).

*Persistent Volume Claims (PVCs)* are requests for storage made by pods and define the required storage capacity, access mode, and optional storage class.

*Storage Classes* define different storage tiers and policies within the cluster, enabling dynamic provisioning of PVs based on predefined templates.

By integrating PVs, PVCs, and Storage Classes into this example, you can better manage your Kubernetes storage resources and ensure data persistence for your applications.


# Kubernetes Persistent Volumes (PV), Persistent Volume Claims (PVC), and Storage Classes

## Introduction

In Kubernetes, managing and persisting data is crucial for stateful applications. **Persistent Volumes (PVs)**, **Persistent Volume Claims (PVCs)**, and **Storage Classes** are Kubernetes resources used to manage and provision storage in a Kubernetes cluster.

### Persistent Volumes (PV)

- **What are PVs?**: PVs are cluster-level resources that represent physical storage volumes, such as local storage, network-attached storage (NAS), or cloud storage, in a Kubernetes cluster.

- **Why Use PVs?**: PVs abstract the underlying storage infrastructure, providing a standardized interface for developers to request storage without needing to know its specifics.

### Persistent Volume Claims (PVC)

- **What are PVCs?**: PVCs are requests for storage made by pods. They define the required storage capacity, access mode, and optional storage class.

- **Why Use PVCs?**: PVCs allow developers to claim and use PVs without worrying about the specifics of the underlying storage, making applications more portable across different environments.

### Storage Classes

- **What are Storage Classes?**: Storage Classes are used to define different storage tiers and policies within a cluster. They provide a way to dynamically provision PVs based on predefined templates.

- **Why Use Storage Classes?**: Storage Classes enable dynamic provisioning, allowing Kubernetes to automatically create PVs that match the requested storage class, reducing administrative overhead.

## How PV, PVC, and Storage Classes Work Together

1. A PV represents a physical storage volume in the cluster.
2. A Storage Class defines storage properties, such as performance and redundancy, and references a specific provisioner (e.g., NFS, AWS EBS).
3. A PVC requests storage with specific requirements, including storage class, access mode, and capacity.
4. When a PVC is created, it references a storage class, which triggers dynamic provisioning if necessary.
5. Kubernetes provisions a PV based on the storage class and attaches it to the requesting PVC.
6. Pods can then use the PVC as a volume to persist data.


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
kubectl delete cronjob consoleapp1-cronjob -n my-local-namespace-localfs
kubectl delete pvc my-persistent-volume-claim -n my-local-namespace-localfs
kubectl delete pv my-persistent-volume -n my-local-namespace-localfs
kubectl delete storageclass my-local-hostpath -n my-local-namespace-localfs
kubectl delete namespace my-local-namespace-localfs

```