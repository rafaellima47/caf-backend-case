# Face Comparison

This project consists of two separate APIs for face comparison: the Anonymization Service built with Flask, and the Comparison Service built with ASP.NET.

## Anonymization Service (Flask)

Follow these steps to set up and run the Anonymization Service:

### 1. Open the terminal

Open a terminal window to interact with the command line.

### 2. Navigate to the service folder

Change the directory to the Anonymization Service folder using the following command:

```bash
cd /anonymization-service
```

### 3. Create the virtual environment

```bash
python -m venv venv
```

### 4. Activate the virtual environment

```bash
source venv/bin/activate 
venv\Scripts\activate 
```

### 5. Install dependencies

```bash
pip install -r requirements.txt
```

### 6. Start the API

```bash
python app.py
```

---

## Comparison Service (ASP.NET)

Follow these steps to set up and run the Comparison Service:

### 1. Open another terminal window

Open a new terminal window to run commands for the Comparison Service.

### 2. Set the database connection string

Edit the appsettings.json file to set the correct database connection string.

### 3. Run the migrations

Apply the database migrations with the following command:

```bash
dotnet ef database update
```

### 4. Comparion Threhold

You can adjust the comparison threshold if necessary. The default value is set to 10.0.

### 5. Start the service

Start the ASP.NET service with the following command:

```bash
dotnet run
```
