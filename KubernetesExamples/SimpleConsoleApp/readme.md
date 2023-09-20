# Simple Console App

## Overview

The Simple Console App is a lightweight C# application that demonstrates basic file read and write operations. This console application provides functionality to write a timestamped message to a file and read the last line from the same file. It also allows you to specify a custom name for the application.

## Features

- **File Writing:** Write timestamped messages to a text file.

- **File Reading:** Read the last line from the text file.

- **Customization:** Specify a custom name for the console application.

## Getting Started

To use this Simple Console App, follow these steps:

1. Clone or download the project to your local machine.

2. Open the project in your preferred C# development environment (e.g., Visual Studio).

3. Build and run the application.

4. Optionally, you can set the `POD_NAME` environment variable to customize the name displayed when writing and reading from the file.

## Usage

The Simple Console App offers two primary functions:

### Writing to File

To write a timestamped message to the file, run the application without any arguments. It will create a 'data' folder (if it doesn't exist) and write a message to a 'file.txt' located inside the 'data' folder.

### Reading from File

To read the last line from the file, again run the application without any arguments. It will display the last message written to the 'file.txt'.

## Customization

You can customize the name displayed in the console output by setting the `POD_NAME` environment variable before running the application. For example:

```bash
export POD_NAME="MyCustomApp"
dotnet run
