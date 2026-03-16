using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager.Storage.Models;

public class Program
{
    static async Task Main()
    {
        string subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID")
            ?? throw new InvalidOperationException("AZURE_SUBSCRIPTION_ID is not set.");

        string resourceGroupName = Environment.GetEnvironmentVariable("AZURE_RESOURCE_GROUP_NAME")
            ?? "<your-resource-group-name>";

        string storageAccountName = Environment.GetEnvironmentVariable("AZURE_STORAGE_ACCOUNT_NAME")
            ?? "<your-storage-account-name>";

        string location = Environment.GetEnvironmentVariable("AZURE_LOCATION")
            ?? "<your-azure-region>";

        TokenCredential credential = new DefaultAzureCredential();
        ArmClient armClient = new ArmClient(credential, subscriptionId);

        // Get the subscription resource
        SubscriptionResource subscription = await armClient.GetDefaultSubscriptionAsync();

        // 1. Create resource group
        ResourceGroupCollection resourceGroups = subscription.GetResourceGroups();

        ArmOperation<ResourceGroupResource> rgOperation =
            await resourceGroups.CreateOrUpdateAsync(
                WaitUntil.Completed,
                resourceGroupName,
                new ResourceGroupData(location));

        ResourceGroupResource resourceGroup = rgOperation.Value;

        // 2. Create storage account (Blob-capable)
        StorageAccountCollection storageAccounts = resourceGroup.GetStorageAccounts();

        var storageSku = new StorageSku(StorageSkuName.StandardLrs);

        var storageData = new StorageAccountCreateOrUpdateContent(
            storageSku,
            StorageKind.StorageV2,
            location)
        {
            AccessTier = StorageAccountAccessTier.Hot
        };

        ArmOperation<StorageAccountResource> storageOperation =
            await storageAccounts.CreateOrUpdateAsync(
                WaitUntil.Completed,
                storageAccountName,
                storageData);

        StorageAccountResource storageAccount = storageOperation.Value;

        Console.WriteLine("Resource group created:");
        Console.WriteLine($"  {resourceGroup.Data.Name}");

        Console.WriteLine("Storage account created:");
        Console.WriteLine($"  Name: {storageAccount.Data.Name}");
        Console.WriteLine($"  Location: {storageAccount.Data.Location}");
        Console.WriteLine($"  Kind: {storageAccount.Data.Kind}");
        Console.WriteLine($"  Blob endpoint: https://{storageAccount.Data.Name}.blob.core.windows.net/");
    }
}