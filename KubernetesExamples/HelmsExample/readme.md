# Helms Chart Example

This repository contains a quick example on how to use helms charts. We will create a basic configmap by combining values and a configmap template.

## Basic Project Structure

```bash
my-helm-chart/
│
├── templates/
│   └── configmap.yaml
│
└── values.yaml
```

## Chart.yaml

```bash
apiVersion: v2
name: my-chart
version: 1.0.0
description: My Helm Chart
```

## values.yaml

```bash
appName: my-application
config:
  database_url: "http://default-database-url.com"
```

## templates/configmap.yaml:

```bash
apiVersion: v1
kind: ConfigMap
metadata:
  name: {{ .Values.appName }}-config
data:
  DATABASE_URL: {{ .Values.config.database_url | quote }}

```

## Usage

```bash
helm install my-release ./my-helm-chart
```

The above will create a ConfigMap resource named my-application-config with a key DATABASE_URL that has the value http://default-database-url.com.

## Overriding Values:

You can also override the values when installing or upgrading the chart:

```bash
helm install my-release ./my-helm-chart --set config.database_url=http://new-database-url.com
```

With this command, the created ConfigMap will have a DATABASE_URL of http://new-database-url.com.

The power of Helm templates and values is in their flexibility and ability to parameterize almost any part of the Kubernetes manifests, allowing for reusable and easily configurable charts.

## Usage

After the job has executed, a POD will show the configuration map values in the log terminal.

To access a pod's logs in Kubernetes, you can utilize the kubectl logs command.

### Cleanup

To clean up a Helm chart deployment and all its associated Kubernetes components, you would use the helm uninstall command.

```bash
helm uninstall [RELEASE_NAME]
helm uninstall my-release
```