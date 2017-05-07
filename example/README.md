# Executing the sample

1) Download the source and navigation to the example directory
2) Change the connectiong string in the publish profile to your local sql instance
3) Create a database on your local machine with the name `CoffeeHouse`
4) `.\deploy.ps1` from the root of the example directory


## Expected output
``` powershell
Build
========================================
Executing task: Build
Microsoft (R) Build Engine version 14.0.25420.1
Copyright (C) Microsoft Corporation. All rights reserved.

Finished executing task: Build

========================================
Script
========================================
Executing task: Script
Generating publish script for database 'CoffeeHouse' on server 'YOUR_SERVER'.
Successfully generated script to file C:\Source\github\Cake.SqlPackage\example\scripts\CoffeeHouse.sql
.     
Finished executing task: Script
========================================
Publish
========================================
Executing task: Publish
Publishing to database 'CoffeeHouse' on server 'YOUR_SERVER'.
Initializing deployment (Start)
Initializing deployment (Complete)
Analyzing deployment plan (Start)
Analyzing deployment plan (Complete)
Updating database (Start)
Update complete.
Updating database (Complete) 
Successfully published database.
Finished executing task: Publish
========================================
Export
========================================
Executing task: Export
Connecting to database 'CoffeeHouse' on server 'YOUR_SERVER'.
Extracting schema
Extracting schema from database
Resolving references in schema model
Validating schema model
Validating schema model for data package
Validating schema
Exporting data from database
Exporting data
Processing Export.
Processing Table '[dbo].[CoffeeBeans]'.
Processing Table '[dbo].[CoffeeBlends]'.
Successfully exported database and saved it to file 'C:\Source\github\Cake.SqlPackage\example\export\C
offeeHouse.bacpac'.
Finished executing task: Export
```