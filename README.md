# .NET and Angular Applications

This repository contains two projects: a .NET backend and an Angular frontend.

# Here is the task description
The next step in our process is a test task, can you please review the below and let me know if you have any questions?



Test task:
Using .NET Core Web API and Angular (latest) create a simple page to upload/view files. The page is split into two parts: upload area and view area.



Upload area - single for all files. File size and file types that can be uploaded to the server must be configurable. Validation for invalid data should present.



View area consists of tables - one table per permitted file type. Each table must reflect: file name, file size, upload date. Keep in mind that the number of files can be unlimited.



Add at least two unit tests for the back-end and two unit tests for the front-end. Tests must have positive and negative scenarios. Default tests added by angular cli will be ignored during the task evaluation.



API and UI must have layered architecture, code must follow common style guides and development principles



The page should be as generic/reusable as possible



Feel free to choose any storage for uploaded files (it can be DB/File System/etc.) The only requirement here - storage must be persistent during runtime



File download/preview is not required but would be good if implemented.



* project should not contain code that is not related to the task (and do not use any templates from the web)
** all external references must be excluded from the package with the test task project (please do NOT include Node Modules).
Some FAQs:
1.Page should be as generic/reusable as possible - what does it mean exactly?
Make sure everything is componetised and coded in a proper and correct way. Not sure all coded on one screen



2.What types of downloadable files must the application support?
It can support any files, you can make up which files it supports. It only needs to show the metadata of the files not view them



3.User that uploaded file-Does it mean that the application should also support the simplest security (user registration/login)?
No need to do user login, the page does not need security



5.Do I understand right that application does not have to expose file content, but only file metadata listed above?
Correct, only needs to show meta data, no need to show file content

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
```
Update-Database
```

### 4. Run the Backend
- Start the backend application using IIS.
  
### 5. Run the Frontend
- Navigate to the Angular project directory and run the following commands:
```
npm install
npm start
```

## 6. Testing
# Frontend: 
- The frontend application includes tests for FileStorageService.ts. Run the tests using:
# Backend: 
- The backend application includes tests for FileInfoService.cs. Run the tests using your preferred testing framework in Visual Studio.
