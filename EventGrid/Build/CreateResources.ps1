$resourceGroup="rg-avanmatest-poc"
$topicName="mytopic"
$functionAppName="Workshop23092020"
$storageAccountName="saworkshop23092020"
 
# Create an event grid topic in the resource-group
az eventgrid topic create --name $topicName --resource-group $resourceGroup --location francecentral

# Create a function app for the Event Handlers
az storage account create --name $storageAccountName --resource-group $resourceGroup
az functionapp create --name $functionAppName --resource-group $resourceGroup --storage-account $storageAccountName --functions-version 3 --consumption-plan-location francecentral