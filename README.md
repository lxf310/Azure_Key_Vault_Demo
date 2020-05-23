# Introduction
This a demo to show how to manage secret via Azure Key Vault. The whole demo is implemented by .Net framework.

# Preparation
To run this demo, you will need:
* Visual Studio 2019
* Azure subscription

# Add access policies for your application
* To run the demo locally, an Azure app registration is needed. You need to add secret related access policy for this registration via Azure Key Vault management page on https://portal.azure.com/.
* To run the demo on Azure web app, the system-managed identity should be enabled. And then you need to add secret related access policy for this identity.