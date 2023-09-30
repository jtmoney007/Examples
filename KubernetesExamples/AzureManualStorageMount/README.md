# Azure AKS & Azure Storage Mount

## Overview

In this example we will create an azure storage mount and link it with AKS.

## Features

### 1. **Install Azure CLI**

https://docs.microsoft.com/en-us/cli/azure/install-azure-cli

### 2. **Log In**

Launch cmd prompt (ie: terminal) and login.

```bash
az login
```

### 3. **Set the Subscription (optional but recommended)**

Ideal to pick the subscription you will be installing your services under to keep things organized (and assuming you have more than 1 subscription).

```bash
az account set --subscription "Your-Subscription-ID-Or-Name"
```


### 4. **Create a Resource Group**

Replace MyResourceGroup with your preferred name and eastus/westus/etc with your preferred Azure region.

```bash
az group create --name MyResourceGroup --location eastus
```

### 5. **Create a Kubernetes Cluster (AKS)**

```bash
az aks create --resource-group MyResourceGroup --name MyAKSCluster --node-count 1 --enable-addons monitoring --generate-ssh-keys
```

### 6. **Install kubectl**

```bash
az aks install-cli
```

### 7. **Connect to Cluster**

```bash
az aks get-credentials --resource-group MyResourceGroup --name MyAKSCluster
```

### 8. **Create an Azure Storage Account and File Share**

```bash
# Create storage account
az storage account create --name mystorageaccount --resource-group MyResourceGroup --location eastus --sku Standard_LRS

# Get storage account key
$STORAGE_KEY=$(az storage account keys list --resource-group MyResourceGroup --account-name mystorageaccount --query '[0].value' --output tsv)

# Create file share
az storage share create --name myfileshare --account-name mystorageaccount --account-key $STORAGE_KEY
```

Note: Use powershell terminal

### 9. **Create a Kubernetes Secret for Azure Storage**

```bash
kubectl create secret generic azure-secret --from-literal=azurestorageaccountname=mystorageaccount --from-literal=azurestorageaccountkey=$STORAGE_KEY
```

### 10. **Create a Pod with Azure File Share mounted**

```bash
apiVersion: v1
kind: Pod
metadata:
  name: azure-mount
spec:
  containers:
  - image: nginx
    name: azure-mount-container
    volumeMounts:
    - mountPath: "/mnt/azure"
      name: volume
  volumes:
  - name: volume
    azureFile:
      secretName: azure-secret
      shareName: myfileshare
      readOnly: false
```

Note: keyword "file share"

```bash
kubectl apply -f azure-pod.yaml
```

### 11. **Lets take it for a spin**

```bash
kubectl get pods
kubectl exec -it azure-mount -- /bin/bash
cd /mnt/azure
echo "Hello, Azure File Share!" > sample.txt
ls
```

Note: Your file is now visible in the Azure portal under this storage account. Additionally, when the storage is mounted and you execute the ls command, the file will be listed.

