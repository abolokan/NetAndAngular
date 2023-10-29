# .Net and Angular apps 

I created two separate projects

# Please do these steps to start:

## 1. Create an empty DB

## 2. Provide these settings in the app.settings file

&quot;ConnectionStrings&quot;: {
&quot;DefaultConnection&quot;: &quot;Data Source=.;Initial Catalog=DonkeyDB;User ID=sa;Password=Qazwsx12&quot;
},
&quot;AppSettings&quot;: {
&quot;FileStoragePath&quot;: &quot;C:\\DonkeyFileStorage&quot;
}
## 3. Choise the project Donkey.DAL in Package Manager Console and run &quot;Update-Database&quot;

## 4. Back App: run with IIS
## 5. Front App: run npm i and npm start;

## 6. Testing
- Front app has tests for FileStorageService.ts
- Back app has tests for FileInfoService.cs
