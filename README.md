### LegitHomeAssignment

## 📌 Project Overview

LegitHomeAssignment is an ASP.NET Core application designed to process GitHub webhook events and detect suspicious: code pushes between 14:00 - 16:00, creating teams with the prefix "hacker" and creating a repository and deleting it before 10 minutes had passed. 
When a suspicious event is detected, this repo will log it into the console.

## 🛠️ Installation & Setup

Prerequisites:
1. .NET 8.0 or later.
2. ngrok for gitHub Webhook configuration.

## Steps
in a first terminal 

1. Clone the Repository:
```bash
git clone "https://github.com/liortimocco/LegitHomeAssignment"
cd LegitHomeAssignment
```

### 📜 Configuration
Configure Environment:
Update appsettings.json with your webhook secret.
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Webhook": {
    "Secret": "THE-SECRET"  
  }
}

3. Restore Dependencies
```bash
dotnet restore
``` 

4. Build the Project:
```bash
dotnet build
```

5. Run the Application:
```bash
dotnet run
```

## 📡 Setting Up ngrok
in a second terminal 

make sure you installed ngrok already and that you authenticated yourself.

for running ngrok: 
```bash
ngrok http 5000
```
notice: make sure the port of the repo and the ngrok are the same.

in the ngrok terminal you will see a new redirected url.
something like: 
```bash 
Forwarding  https://47ae-2a0d-6fc2-6070-3600-d89f-60f5-d9f4-672a.ngrok-free.app -> http://localhost:5000               
```

copy the forwarded url and paste it into the webhook "Payload URL" in the repo:
Go to GitHub Organizations > Settings > Webhooks > Payload URL and pase:
{forwarded_url_from_ngrok}/{the_endpoint_of_this_repo}
for example:
https://47ae-2a0d-6fc2-6070-3600-d89f-60f5-d9f4-672a.ngrok-free.app/api/Legit_home_task/github_event/receive

Make sure 'Secret' Matches appsettings.json

