# Airport Ticket Booking
# Airport Ticket Booking System

This is a .NET console application designed to facilitate flight ticket bookings for passengers and provide management tools for administrators. The application utilizes the file system as the storage layer for data.

## Passenger Features:

### 1. Book a Flight:
   - Passengers can select flights based on various search parameters and choose from Economy, Business, or First Class with prices varying accordingly.

### 2. Search for Available Flights:
   - Passengers can search for flights based on parameters like price, departure country, destination country, departure date, departure airport, arrival airport, and class.

### 3. Manage Bookings:
   - Passengers can cancel bookings and view their personal bookings.

## Manager Features:

### 1. Filter Bookings:
   - Managers can filter bookings based on various parameters including flight details, price, departure and destination countries, departure date, departure and arrival airports, passenger, and class.

### 2. Batch Flight Upload:
   - Managers can import a list of flights into the system using a CSV file for efficient management.

### 3. Validate Imported Flight Data:
   - The system applies model-level validations to imported flight data and provides a detailed list of errors to help identify and rectify issues in the imported file.

### 4. Dynamic Model Validation Details:
   - The application dynamically generates details about the validation constraints for each field of the flight data model, aiding in understanding and addressing validation requirements.

## Usage:

- Passengers: 
  - Use the provided console interface to book flights, search for available flights, and manage bookings.
- Managers:
  - Access the manager interface to filter bookings, upload flights in batch, validate imported flight data, and view dynamic model validation details.

## Note:

When cloning the repository, ensure that you update the value of the 'DataFiles' and 'LogsDirectory' keys in the 'AppSettings.json' file to specify the directory where the data files are saved.
