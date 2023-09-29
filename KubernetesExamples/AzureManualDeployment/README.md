# Azure Manual Deployment of kubernetes (AKS)

## Overview

In this example we will create an azure resource group that houses all of our resources which will include an azure registry (similar to docker registry but azures version) and a cluster called AKS.

Afterwards we are going to push our code to the AKS registry and publish our kubernetes yaml on the cluster.

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
az group create --name "YourResourceGroupName" --location eastus
```

### 5. **Create a Kubernetes Cluster (AKS)**

--resource-group: The resource group you created in the previous step.
--name: The name you want to give to your AKS cluster.
--node-count: The number of nodes in your cluster (1 in this example).
--enable-addons: Add-ons to enable. Here, we've enabled the monitoring add-on.
--generate-ssh-keys: This will generate SSH keys if they don't exist in the ~/.ssh directory.

```bash
az aks create --resource-group "YourResourceGroupName" --name "YourClusterName" --node-count 1 --enable-addons monitoring --generate-ssh-keys
```

### 6. **Create an Azure Registry**

Replace YourResourceGroupName this is the resource group you created in the previous steps.

--sku: Determines the tier of the registry. Options include Basic, Standard, and Premium. They have varying levels of storage, throughput, and replication capabilities.

--admin-enabled true: This enables the admin user for the ACR, which is required for some CI/CD scenarios. However, be cautious with this in production scenarios. If you don't want to enable the admin account, you can omit this flag.

```bash
az acr create --resource-group "YourResourceGroupName" --name "YourRegistryName" --sku Basic --admin-enabled true
```

### 7. **Verify Resources**

At this point we have a resourece group, an AKS cluster, and a registry to store the image. 

Lets verify we have these resources:
```bash
az acr list --resource-group "YourResourceGroupName"  --output table
az aks list --resource-group "YourResourceGroupName"  --output table
```

### 7. **Create your Dockerfile and compose file**

Make sure your image follows the following naming convention in your compose file:
"YourRegistryName".azurecr.io/helloworldapp

Now lets build:
```bash
docker compose up --build
```

### 8. **Pointing to our registry**

```bash
az acr login --name "YourRegistryName"
```

### 9. **Pushing to registry**

```bash
docker push "YourRegistryName".azurecr.io/helloworldapp
```

If we try and push via the above command we will get access denied. We will need docker to login to the resource first before we can push.

Lets get our username and password via:
```bash
az acr credential show --name "YourRegistryName"
```

Now lets login to docker using that username/password:
```bash
docker login "YourRegistryName".azurecr.io --username "UserNameFromAbove" --password "PasswordFromAbove"
```

Great! Now docker is pointed to the azure registry. We can begin pushing our image to the respository via the following:

```bash
docker push "YourRegistryName".azurecr.io/helloworldapp
```

Now lets check if our repository has been created:

```bash
az acr repository list --name "YourRegistryName" --output table
```

### 9. **Deploying our Kubernetes YAML**

1. Install kubectl is not installed
```bash
az aks install-cli
```

2. Connect to your AKS Cluster:
```bash
az aks get-credentials --resource-group "YourResourceGroupName" --name "YourRegistryName"
```

3. Get Nodes:
```bash
kubectl get nodes
```

4. Create Kubernetes Deployment file

5. Deploy Kubernetes deployment file

```bash
kubectl apply -f deployment.yaml
```

Note: in some cases you may need to tie the registry with the cluster. permission wise.

```bash
az aks update -n "YourClusterName" -g "YourResourceGroupName" --attach-acr "YourRegistryName"
```

