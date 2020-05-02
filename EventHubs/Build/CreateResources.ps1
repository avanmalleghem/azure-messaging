$resourceGroup="rg-avanmatest-poc"
$namespaceName="nn-avanmatest-poc"
$eventhubsName="eh-avanmatest-poc"
$storageAccountName="sa-avanmatest-poc"
$containerName="c-avanmatest-poc"
 
# Create a namespace in the resource-group
az eventhubs namespace create --name $namespaceName --resource-group $resourceGroup

# Create an event hubs in the namespace
az eventhubs eventhub create --name $eventhubsName --namespace-name $namespaceName

# Create a storage account in the resource-group for the EventProcessorHost
az storage account create --name $storageAccountName `
                          --resource-group $resourceGroup `
                          --kind StorageV2 `
                          --sku Standard_LRS `
                          --location westeurope `
                          --https-only true

# Create a container in the storage account for the EventProcessorHost
az storage container create --name $containerName `
                            --account-name $storageAccountName
