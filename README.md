# Azure Messaging Sandbox
This repository is a sandbox for the following Azure messaging services : Service Bus, Queue Storage, Event Hubs, Event Grid

* InstallAzureCLI.ps1 : if Azure CLI isn't installed on your local host yet, run this powershell command.

## Queue Storage Sandbox
This sandbox contains 2 projects : a sender and a receiver.
To use it, you need to create a **Storage Account** in a **Resource Group**. You can use the **Build/CreateResources.ps1** file (change the variable according to your needs).

You can then update the 2 files **Program.cs** with the connection string associated with your storage account.

## Service Bus Sandbox
This sandbox contains 3 projects : a sender, a receiver and a consumer for the involved deadletter queue.
To use it, you need to create a **Namespace** in a **Resource Group**. In this Namespace, you must create a **Topic** and a **Subscription**. You can use the **Build/CreateResources.ps1** file (change the variable according to your needs).

You can then update the 3 files **Program.cs** (static variables).

## Event Hubs Sandbox
**ToDo**

## Event Grid Sandbox
This sandbox contains 2 projects :
* EventSource : this project contains a publisher of events. To use it, you need to create an **event grid topic**. You can then update the static variables.
* EventHandler : this project contains 2 azure functions (handlers) :
    * EventGridTriggerFunction.cs : This azure function uses the **EventGridTrigger** (Azure function handler)
    * WebhookTriggerFunction.cs : This azure function uses the **HttpTrigger** (Webhook handler)
    
**ToDo** : powershell file to create the event Grid Topic and the 2 event grid subscriptions (+ binding with the azure functions).

