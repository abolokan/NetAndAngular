# .NET and Angular Applications

This repository contains two separate projects: a .NET backend and an Angular frontend.

## Setup Instructions

Follow these steps to get the projects up and running:

### 1. Create an Empty Database
Create an empty database in your SQL Server.

### 2. Update the App Settings
Add the following settings to your `appsettings.json` file in the .NET project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=.;Initial Catalog=DonkeyDB;User ID=sa;Password=Qazwsx12"
  },
  "AppSettings": {
    "FileStoragePath": "C:\\DonkeyFileStorage"
  }
}


## 3. Choise the project Donkey.DAL in Package Manager Console and run &quot;Update-Database&quot;

## 4. Back App 
- run with IIS
  
## 5. Front App
- run npm i
- npm start

## 6. Testing
- Front app has tests for FileStorageService.ts
- Back app has tests for FileInfoService.cs
