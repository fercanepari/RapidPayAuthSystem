# RapidPayAuthSystem

To create database using Entity Framework's code-first approach:

Step 1:
 dotnet ef migrations add InitialCreate --project RapidPayAuthSystem
 
Step 2:
  dotnet ef database update  --project RapidPayAuthSystem


cURL's to test:

Create Card:
curl --location 'https://localhost:7106/api/cards/create' \
--header 'Content-Type: application/json' \
--header 'Username: admin' \
--header 'Password: password' \
--data '"1597"'


Pay:
curl --location 'https://localhost:7106/1/pay' \
--header 'accept: */*' \
--header 'Username: admin' \
--header 'Password: password' \
--header 'Content-Type: application/json' \
--data '1000'


Balance:
curl --location 'https://localhost:7106/1/balance' \
--header 'accept: */*' \
--header 'Username: admin' \
--header 'Password: password'  