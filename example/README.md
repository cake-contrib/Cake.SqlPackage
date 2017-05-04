# Executing the sample

1) Download the source and navigation to the example directory
2) Change the connectiong string in the publish profile to your local sql instance
3) Create a database on your local machine with the name `CoffeeHouse`
4) `.\deploy.ps1` from the root of the example directory


## Expected output
``` powershell
----------------------------------------
Setup
----------------------------------------
Executing custom setup action...
Target Cake Task: Default

========================================
Build
========================================
Executing task: Build
Microsoft (R) Build Engine version 14.0.25420.1
Copyright (C) Microsoft Corporation. All rights reserved.

Finished executing task: Build

========================================
Sql-Package
========================================
Executing task: Sql-Package
Publishing to database 'CoffeeHouse' on server 'YOUR SERVER NAME'.
Initializing deployment (Start)
Initializing deployment (Complete)
Analyzing deployment plan (Start)
Analyzing deployment plan (Complete)
Updating database (Start)
Update complete.
Updating database (Complete)
Successfully published database.
Finished executing task: Sql-Package

========================================
Default
========================================
Executing task: Default
Finished executing task: Default

----------------------------------------
Teardown
----------------------------------------
Executing custom teardown action...
Target Cake Task: Default
Build Completion Time: 21:47:32.7218769

Task                          Duration
--------------------------------------------------
Build                         00:00:03.0614776
Sql-Package                   00:00:08.3471048
Default                       00:00:00.0014405
--------------------------------------------------
Total:                        00:00:11.4100229
```