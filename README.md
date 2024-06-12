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
```

## 3. Apply Database Migrations
- In Visual Studio, open the Package Manager Console, select the Donkey.DAL project, and run:
  ```Update-Database```

## 4. Run the Backend
- Start the backend application using IIS.
  
## 5. Run the Frontend
- Navigate to the Angular project directory and run the following commands:
```
npm install
npm start
```

## 6. Testing
- Frontend: The frontend application includes tests for FileStorageService.ts. Run the tests using:
- Backend: The backend application includes tests for FileInfoService.cs. Run the tests using your preferred testing framework in Visual Studio.
