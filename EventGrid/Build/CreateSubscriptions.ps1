$resourceGroup="rg-avanmatest-poc"
$topicName="mytopic"
$functionAppName="Workshop23092020"
$subscriptionEventGridTrigger="subscription4EventGridTrigger"
$subscriptionWebHookTrigger="subscription4HttpTrigger"
 
# Subscription for the EventGrid triggered Azure function
$sourceResourceId = -join('/subscriptions/d1fc3aa5-3154-4ece-84e6-85e2eb598016/resourceGroups/', $resourceGroup, '/providers/Microsoft.EventGrid/topics/', $topicName)
$endpoint = -join('/subscriptions/d1fc3aa5-3154-4ece-84e6-85e2eb598016/resourceGroups/', $resourceGroup, '/providers/Microsoft.Web/sites/', $functionAppName, '/functions/', 'EventGridTriggerFunction')
az eventgrid event-subscription create --source-resource-id $sourceResourceId --name $subscriptionEventGridTrigger --endpoint-type azurefunction --endpoint $endpoint

# Subscription for the HTTP triggered Azure function (Webhook)
$endpoint = -join('https://',$functionAppName,'.azurewebsites.net/api/WebhookTriggerFunction')
az eventgrid event-subscription create --name $subscriptionWebHookTrigger --source-resource-id $sourceResourceId --endpoint $endpoint
