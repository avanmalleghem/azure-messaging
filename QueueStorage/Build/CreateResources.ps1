$resourceGroup="rg-avanmatest-poc"
$storageAccountName="wsstorageaccount2020"
 
# Create a storage account in the resource-group
az storage account create --name $storageAccountName `
                          --resource-group $resourceGroup `
                          --kind StorageV2 `
                          --sku Standard_LRS `
                          --location westeurope `
                          --https-only true