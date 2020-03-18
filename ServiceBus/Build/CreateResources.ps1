$resourceGroup="rg-avanmatest-poc"
$namespace="azureservicebus160320"
$topicName="mytopic"
$subscriptionName="receiver1"
 
# Create a service bus namespace ; it's like a virtualhost ! 
# https://docs.microsoft.com/en-us/cli/azure/servicebus/namespace?view=azure-cli-latest
az servicebus namespace create --name $namespace `
                               --resource-group $resourceGroup `
                               --location westeurope `
                               --sku Standard
# Create a topic in my namespace. You can also do it using the Microsoft.Azure.Management.ServiceBus library
# https://docs.microsoft.com/en-us/cli/azure/servicebus/topic?view=azure-cli-latest
az servicebus topic create --name $topicName `
                           --namespace-name $namespace `
                           --resource-group $resourceGroup `
                           --enable-batched-operations false `
                           --enable-duplicate-detection false `
                           --enable-express false `
                           --enable-ordering false `
                           --enable-partitioning true `
                           --max-size 1024 `
                           --status Active

# Create a topic subscription. You can also do it using the Microsoft.Azure.Management.ServiceBus library
# https://docs.microsoft.com/en-us/cli/azure/servicebus/topic/subscription?view=azure-cli-latest
az servicebus topic subscription create --name $subscriptionName `
                                        --namespace-name $namespace `
                                        --resource-group $resourceGroup `
                                        --topic-name $topicName `
                                        --enable-dead-lettering-on-message-expiration true `
                                        --status Active
