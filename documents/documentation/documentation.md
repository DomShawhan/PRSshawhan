# Documentation for PRSshawhan system  
## Api endpoints  
### Users  
|Method |Endpoint       |Returns   |
|------ |--------       |-------   |
|GET    |/api/users     |All Users |
|GET    |/api/users/{id}|One User  |
|POST   |/api/users     |New User  |
|PUT    |/api/users/{id}|*n/a*     |
|DELETE |/api/users/{id}|*n/a*     |  

#### GET  
RECEIVE:  
```json
[
    {
        "id": 1,
        "username": "dshawhan",
        "password": "mysecret",
        "firstname": "Dominic",
        "lastname": "Shawhan",
        "phone": "859-412-0590",
        "email": "dfshawhan@yahoo.com",
        "reviewer": true,
        "admin": true
    },
    {
        "id": 2,
        "username": "gmoose",
        "password": "noway2gues",
        "firstname": "Stuffed",
        "lastname": "Moose",
        "phone": "123-456-7890",
        "email": "moose@glacier.com",
        "reviewer": true,
        "admin": false
    }
]
```
#### GET BY ID  
RECEIVE:  
```json
{
    "id": 1,
    "username": "dshawhan",
    "password": "mysecret",
    "firstname": "Dominic",
    "lastname": "Shawhan",
    "phone": "859-412-0590",
    "email": "dfshawhan@yahoo.com",
    "reviewer": true,
    "admin": true
}
```
#### POST  
SEND:  
``` json
{
    "username": "myfirstuser",
    "password": "firstpass",
    "firstname": "Me",
    "lastname": "NotU",
    "phone": "123-456-7890",
    "email": "email@thisemail.com",
    "reviewer": false,
    "admin": false
}
```
RECEIVE:  
```json
{
    "id": 6,
    "username": "myfirstuser",
    "password": "firstpass",
    "firstname": "Me",
    "lastname": "NotU",
    "phone": "123-456-7890",
    "email": "email@thisemail.com",
    "reviewer": false,
    "admin": false
}
```
#### PUT  
SEND:  
```json
{
    "id": 6,
    "username": "myfirstuser",
    "password": "firstpass",
    "firstname": "Me",
    "lastname": "NotU",
    "phone": "123-456-7890",
    "email": "email@thisemail.com",
    "reviewer": false,
    "admin": false
}
```
RECEIVE:  
```json
```
#### DELETE
RECEIVE:
```json
```
### Vendors  
|Method |Endpoint         |Returns     |
|------ |--------         |-------     |
|GET    |/api/vendors     |All vendors |
|GET    |/api/vendors/{id}|One vendor  |
|POST   |/api/vendors     |New vendor  |
|PUT    |/api/vendors/{id}|*n/a*       |
|DELETE |/api/vendors/{id}|*n/a*       | 

#### GET  
RECEIVE:  
```json
[
    {
        "id": 1,
        "code": "AMAZON",
        "name": "Amazon",
        "address": "123 Amazon Ave.",
        "city": "Amazon",
        "state": "CA",
        "zip": "12345",
        "phone": "123-456-7890",
        "email": "theamazonemail@amazon.com"
    },
    {
        "id": 2,
        "code": "mCENTER",
        "name": "MicroCenter",
        "address": "11755 Mosteller Rd",
        "city": "Sharonville",
        "state": "OH",
        "zip": "45241",
        "phone": null,
        "email": null
    }
]
```
#### GET BY ID  
RECEIVE:  
```json
{
    "id": 1,
    "code": "AMAZON",
    "name": "Amazon",
    "address": "123 Amazon Ave.",
    "city": "Amazon",
    "state": "CA",
    "zip": "12345",
    "phone": "123-456-7890",
    "email": "theamazonemail@amazon.com"
}
```
#### POST  
SEND:  
``` json
{
    "code": "STP",
    "name": "Staples",
    "address": "123 Main Ave",
    "city": "Cincinnati",
    "state": "OH",
    "zip": "45211",
    "phone": "098-765-4321",
    "email": "email@staples.com"
}
```
RECEIVE:  
```json
{
    "id": 4,
    "code": "STP",
    "name": "Staples",
    "address": "123 Main Ave",
    "city": "Cincinnati",
    "state": "OH",
    "zip": "45211",
    "phone": "098-765-4321",
    "email": "email@staples.com"
}
```
#### PUT  
SEND:  
```json
{
    "id": 4,
    "code": "STP",
    "name": "Staples",
    "address": "123 Main Ave",
    "city": "Cincinnati",
    "state": "OH",
    "zip": "45211",
    "phone": "098-765-4321",
    "email": "email@staples.com"
}
```
RECEIVE:  
```json
```
#### DELETE
RECEIVE:
```json
```