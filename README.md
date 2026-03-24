# Azure SDK C# Example – Provisioning Resources Programmatically

This project demonstrates how to create Azure resources programmatically using the **Azure SDK for .NET**.

Instead of using the Azure Portal or Infrastructure-as-Code tools (like Terraform or Bicep), this example shows how to provision infrastructure directly from a C# application.

---

## 📌 What This Project Does

The application performs the following steps:

1. Authenticates to Azure using `DefaultAzureCredential`
2. Connects to your Azure subscription
3. Creates (or updates) a **Resource Group**
4. Creates (or updates) a **Storage Account**
5. Outputs details about the created resources

---

## 🧠 Why This Matters

In real-world applications, you often need to automate infrastructure creation instead of doing it manually in the Azure Portal.

This approach allows you to:

* Automate environment setup (dev/test/prod)
* Ensure consistency across deployments
* Integrate infrastructure provisioning into CI/CD pipelines
* Avoid manual errors from portal configuration

---

## 🏗️ What Resources Are Created

### 1. Resource Group

A logical container used to group Azure resources.

Example:

```
rg-my-app-dev
```

---

### 2. Storage Account

A core Azure service used for storing:

* Files (Blob Storage)
* Logs
* Images
* Backups
* Application assets

This example creates a **StorageV2 account** with:

* **SKU**: Standard_LRS (locally redundant, low cost)
* **Access Tier**: Hot (frequent access)

---

## ⚙️ Prerequisites

* .NET 6 or later
* Azure subscription
* Azure CLI installed and authenticated:

```bash
az login
```

---

## 🔐 Configuration

The application uses environment variables for configuration.

Set the following:

```bash
export AZURE_SUBSCRIPTION_ID="your-subscription-id"
export AZURE_RESOURCE_GROUP_NAME="your-resource-group"
export AZURE_STORAGE_ACCOUNT_NAME="yourstorageaccountname"
export AZURE_LOCATION="westeurope"
```

⚠️ Notes:

* Storage account names must be globally unique
* Use only lowercase letters and numbers

---

## ▶️ Running the Application

```bash
dotnet run
```

---

## 📤 Example Output

```
Resource group created:
  rg-demo

Storage account created:
  Name: mystorage123
  Location: westeurope
  Kind: StorageV2
  Blob endpoint: https://mystorage123.blob.core.windows.net/
```

---

## 🔍 How Authentication Works

This project uses:

```csharp
DefaultAzureCredential
```

This means it will automatically try multiple authentication methods:

* Azure CLI (`az login`)
* Visual Studio / VS Code login
* Managed Identity (if deployed in Azure)

No credentials are hardcoded.

---

## 🧱 Key Concepts

### Azure Resource Manager (ARM)

The layer used to create and manage Azure resources programmatically.

### ArmClient

Main entry point for interacting with Azure resources.

### ResourceGroup

A container that groups related resources.

### StorageAccount

A scalable storage service used by most Azure applications.

---

## ⚠️ Important Notes

* `CreateOrUpdate` will **not always create new resources**; it updates if they already exist
* This project only provisions infrastructure — it does **not upload or read data**
* Costs may apply depending on your Azure subscription usage

---

## 🚀 When Would You Use This?

* Setting up infrastructure dynamically from an application
* Creating resources on demand (multi-tenant apps, automation tools)
* Integrating infrastructure provisioning into backend services

---

## 🔄 Related Approaches

This project is part of a broader concept:

| Approach                 | Description                        |
| ------------------------ | ---------------------------------- |
| Terraform                | Multi-cloud Infrastructure as Code |
| Bicep                    | Azure-native declarative templates |
| Azure SDK (this project) | Programmatic resource creation     |

---

## 📌 Summary

This project shows how to:

> Use C# to automate the creation of Azure infrastructure (Resource Groups and Storage Accounts) without using the Azure Portal.

---

## 📎 Next Steps

To make this more practical, you can extend the project to:

* Upload files to Blob Storage
* Read files from storage
* Create Blob Containers
* Integrate with a Web API

---
