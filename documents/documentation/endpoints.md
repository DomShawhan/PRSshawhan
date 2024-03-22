# Documentation for PRSshawhan system  
## Api endpoints  
### Users  
|Method |Endpoint                     |Returns     |
|------ |--------                     |-------     |
|GET    |/api/users                   |All Users   |
|GET    |/api/users/{id}              |One User    |
|GET    |/api/vendors/usersummary/{id}|User summary|
|POST   |/api/users                   |New User    |
|PUT    |/api/users/{id}              |*n/a*       |
|DELETE |/api/users/{id}              |*n/a*       |  
|POST   |/api/users/login             |One User    |

#### GET  
RECEIVE:  
```json
[
    {
        "id": 2,
        "username": "gmoose",
        "password": "noway2gues",
        "firstname": "Stuffed",
        "lastname": "Moose",
        "phone": "123-456-7890",
        "email": "moose@glacier.com",
        "reviewer": true,
        "admin": false,
        "requests": [
            {
                "id": 2,
                "userId": 2,
                "description": "Paper",
                "justification": "Out of Paper",
                "dateNeeded": "2024-06-01T00:00:00",
                "deliveryMode": "Pickup",
                "status": "NEW",
                "total": 0.00,
                "submittedDate": "2024-03-11T14:32:28.06",
                "reasonForRejection": null,
                "user": null,
                "lineItems": null
            }
        ]
    },
]
```
#### GET BY ID  
RECEIVE:  
```json
{
    "id": 2,
    "username": "gmoose",
    "password": "noway2gues",
    "firstname": "Stuffed",
    "lastname": "Moose",
    "phone": "123-456-7890",
    "email": "moose@glacier.com",
    "reviewer": true,
    "admin": false,
    "requests": [
        {
            "id": 2,
            "userId": 2,
            "description": "Paper",
            "justification": "Out of Paper",
            "dateNeeded": "2024-06-01T00:00:00",
            "deliveryMode": "Pickup",
            "status": "NEW",
            "total": 0.00,
            "submittedDate": "2024-03-11T14:32:28.06",
            "reasonForRejection": null,
            "user": null,
            "lineItems": null
        }
    ]
}
```
#### GET BY ID - api/users/usersummary/{id}  
RECEIVE:  
```json
{
    "fullName": "Stuffed Bison",
    "countOfRejectedRequests": 1,
    "countOfApprovedRequests": 1,
    "countOfPendingRequests": 0,
    "approvedTotal": 1799.99,
    "rejectedTotal": 2319.97
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
    "id": 13,
    "username": "myfirstuser",
    "password": "firstpass",
    "firstname": "Me",
    "lastname": "NotU",
    "phone": "123-456-7890",
    "email": "email@thisemail.com",
    "reviewer": false,
    "admin": false,
    "requests": null
}
```
#### PUT  
SEND:  
```json
{
    "id": 13,
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

#### POST Login
SEND:  
``` json
{
    "username": "gmoose",
    "password": "noway2gues"
}
```
RECEIVE:  
```json
{
    "id": 2,
    "username": "gmoose",
    "password": "noway2gues",
    "firstname": "Stuffed",
    "lastname": "Moose",
    "phone": "123-456-7890",
    "email": "moose@glacier.com",
    "reviewer": true,
    "admin": false,
    "requests": null
}
```
### Vendors  
|Method |Endpoint                       |Returns       |
|------ |--------                       |-------       |
|GET    |/api/vendors                   |All vendors   |
|GET    |/api/vendors/{id}              |One vendor    |
|GET    |/api/vendors/vendorsummary/{id}|Vendor summary|
|POST   |/api/vendors                   |New vendor    |
|PUT    |/api/vendors/{id}              |*n/a*         |
|DELETE |/api/vendors/{id}              |*n/a*         | 

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
        "email": "theamazonemail@amazon.com",
        "products": [
            {
                "id": 2,
                "vendorId": 1,
                "partNumber": "123456789P",
                "name": "Paper",
                "price": 15.00,
                "unit": "REAM",
                "photoPath": null,
                "vendor": null
            }
        ]
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
    "email": "theamazonemail@amazon.com",
    "products": [
        {
            "id": 2,
            "vendorId": 1,
            "partNumber": "123456789P",
            "name": "Paper",
            "price": 15.00,
            "unit": "REAM",
            "photoPath": null,
            "vendor": null
        }
    ]
}
```
#### GET BY ID - api/vendors/vendorsummary/{id}  
RECEIVE:  
```json
{
    "code": "AMAZON",
    "name": "Amazon",
    "countOfProducts": 0
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
    "id": 7,
    "code": "STP",
    "name": "Staples",
    "address": "123 Main Ave",
    "city": "Cincinnati",
    "state": "OH",
    "zip": "45211",
    "phone": "098-765-4321",
    "email": "email@staples.com",
    "products": null
}
```
#### PUT  
SEND:  
```json
{
    "id": 7,
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

### Products  
|Method |Endpoint                   |Returns      |
|------ |--------                   |-------      |
|GET    |/api/products              |All products |
|GET    |/api/products/{id}         |One product  |
|POST   |/api/products              |One product  |
|PUT    |/api/products/{id}         |*n/a*        |
|DELETE |/api/products/{id}         |*n/a*        | 

#### GET  
RECEIVE:  
```json
[
    {
        "id": 2,
        "vendorId": 1,
        "partNumber": "123456789P",
        "name": "Paper",
        "price": 15.00,
        "unit": "REAM",
        "photoPath": null,
        "vendor": {
            "id": 1,
            "code": "AMAZON",
            "name": "Amazon",
            "address": "123 Amazon Ave.",
            "city": "Amazon",
            "state": "CA",
            "zip": "12345",
            "phone": "123-456-7890",
            "email": "theamazonemail@amazon.com",
            "products": [
                null
            ]
        }
    }
]
```
#### GET BY ID  
RECEIVE:  
```json
{
    "id": 2,
    "vendorId": 1,
    "partNumber": "123456789P",
    "name": "Paper",
    "price": 15.00,
    "unit": "REAM",
    "photoPath": null,
    "vendor": {
        "id": 1,
        "code": "AMAZON",
        "name": "Amazon",
        "address": "123 Amazon Ave.",
        "city": "Amazon",
        "state": "CA",
        "zip": "12345",
        "phone": "123-456-7890",
        "email": "theamazonemail@amazon.com",
        "products": [
            null
        ]
    }
}
```
#### POST  
SEND:  
``` json
{
    "vendorId": 2,
    "partNumber": "RLOGISCMOUSE",
    "name": "Red Logi soft click mouse",
    "price": 40,
    "unit": "EA",
    "photoPath": null
}
```
RECEIVE:  
```json
{
    "id": 8,
    "vendorId": 2,
    "partNumber": "RLOGISCMOUSE",
    "name": "Red Logi soft click mouse",
    "price": 40,
    "unit": "EA",
    "photoPath": null,
    "vendor": null
}
```
#### PUT  
SEND:  
```json
{
    "id": 4,
    "vendorId": 2,
    "partNumber": "BLOGISCMOUSE",
    "name": "Blue Logi soft click mouse",
    "price": 40,
    "unit": "EA",
    "photoPath": null
}
```
RECEIVE:  
```json
```
#### DELETE
RECEIVE:
```json
```

### Requests  
|Method |Endpoint                   |Returns      |
|------ |--------                   |-------      |
|GET    |/api/requests              |All requests |
|GET    |/api/requests/{id}         |One request  |
|GET    |/api/requests/reviews/{userid} |All requests except those of the specified user |
|POST   |/api/requests/review/{id}  |One request  |
|POST   |/api/requests              |One request  |
|POST   |/api/requests/approve/{id} |One request  |
|POST   |/api/requests/reject/{id}  |One request  |
|PUT    |/api/requests/{id}         |*n/a*        |
|DELETE |/api/requests/{id}         |*n/a*        | 

#### GET  
RECEIVE:  
```json
[
    {
        "id": 1,
        "userId": 3,
        "description": "Laptop",
        "justification": "Old Laptop broke",
        "dateNeeded": "2024-06-01T00:00:00",
        "deliveryMode": "Pickup",
        "status": "NEW",
        "total": 0.00,
        "submittedDate": "2024-03-11T14:32:28.06",
        "reasonForRejection": null,
        "user": {
            "id": 3,
            "username": "mrbison",
            "password": "not4U2know",
            "firstname": "Stuffed",
            "lastname": "Bison",
            "phone": null,
            "email": null,
            "reviewer": false,
            "admin": false,
            "requests": [
                null,
                {
                    "id": 3,
                    "userId": 3,
                    "description": "Laptop and monitor",
                    "justification": "Setting up a home office",
                    "dateNeeded": "2024-06-01T00:00:00",
                    "deliveryMode": "Pickup",
                    "status": "Rejected",
                    "total": 0.00,
                    "submittedDate": "2024-03-11T14:32:28.06",
                    "reasonForRejection": "You do not need a home office.",
                    "user": null,
                    "lineItems": [
                        {
                            "id": 1,
                            "requestId": 3,
                            "productID": 1,
                            "quantity": 1,
                            "request": null,
                            "product": {
                                "id": 1,
                                "vendorId": 2,
                                "partNumber": "1T32RTS4KL",
                                "name": "Laptop",
                                "price": 1799.99,
                                "unit": "EA",
                                "photoPath": "/photos/laptop.jpg",
                                "vendor": null
                            }
                        }
                    ]
                }
            ]
        },
        "lineItems": [
            {
                "id": 3,
                "requestId": 1,
                "productID": 1,
                "quantity": 1,
                "request": null,
                "product": {
                    "id": 1,
                    "vendorId": 2,
                    "partNumber": "1T32RTS4KL",
                    "name": "Laptop",
                    "price": 1799.99,
                    "unit": "EA",
                    "photoPath": "/photos/laptop.jpg",
                    "vendor": null
                }
            }
        ]
    },
]
```
#### GET BY ID  
RECEIVE:  
```json
{
    "id": 3,
    "userId": 3,
    "description": "Laptop and monitor",
    "justification": "Setting up a home office",
    "dateNeeded": "2024-06-01T00:00:00",
    "deliveryMode": "Pickup",
    "status": "Rejected",
    "total": 0.00,
    "submittedDate": "2024-03-11T14:32:28.06",
    "reasonForRejection": "You do not need a home office.",
    "user": {
        "id": 3,
        "username": "mrbison",
        "password": "not4U2know",
        "firstname": "Stuffed",
        "lastname": "Bison",
        "phone": null,
        "email": null,
        "reviewer": false,
        "admin": false,
        "requests": [
            null
        ]
    },
    "lineItems": [
        {
            "id": 1,
            "requestId": 3,
            "productID": 1,
            "quantity": 1,
            "request": null,
            "product": {
                "id": 1,
                "vendorId": 2,
                "partNumber": "1T32RTS4KL",
                "name": "Laptop",
                "price": 1799.99,
                "unit": "EA",
                "photoPath": "/photos/laptop.jpg",
                "vendor": null
            }
        },
        {
            "id": 2,
            "requestId": 3,
            "productID": 3,
            "quantity": 2,
            "request": null,
            "product": {
                "id": 3,
                "vendorId": 2,
                "partNumber": "28I4K144HzM",
                "name": "Monitor",
                "price": 259.99,
                "unit": "EA",
                "photoPath": null,
                "vendor": null
            }
        }
    ]
}
```
#### POST  
SEND:  
``` json
{
    "userId": 2,
    "description": "Paper",
    "justification": "I need more paper",
    "dateNeeded": "2024-06-01T00:00:00"
}
```
RECEIVE:  
```json
{
    "id": 10,
    "userId": 2,
    "description": "Paper",
    "justification": "I need more paper",
    "dateNeeded": "2024-06-01T00:00:00",
    "deliveryMode": "Pickup",
    "status": "NEW",
    "total": 0,
    "submittedDate": "2024-03-20T13:07:55.0384674-04:00",
    "reasonForRejection": null,
    "user": null,
    "lineItems": null
}
```
#### PUT  
SEND:  
```json
{
    "id": 10,
    "userId": 2,
    "description": "Paper",
    "justification": "I need more paper",
    "dateNeeded": "2024-06-01T00:00:00",
    "deliveryMode": "Pickup",
    "status": "NEW",
    "total": 0,
    "submittedDate": "2024-03-20T13:07:55.0384674-04:00",
    "reasonForRejection": null,
    "user": null,
    "lineItems": null
}
```
RECEIVE:  
```json
```
#### DELETE
RECEIVE:
```json
```

#### POST - api/requests/review/{id}  
RECEIVE:  
```json
{
    "id": 11,
    "userId": 3,
    "description": "Laptop and monitor",
    "justification": "Setting up a home office",
    "dateNeeded": "2024-06-01T00:00:00",
    "deliveryMode": "Pickup",
    "status": "REVIEW",
    "total": 75.00,
    "submittedDate": "2024-03-20T13:12:08.61",
    "reasonForRejection": null,
    "user": null,
    "lineItems": null
}
```

#### GET - api/requests/reviews/{userid}
RECEIVE:  
```json
[
    {
        "id": 5,
        "userId": 3,
        "description": "Everything",
        "justification": "I need everything!!!!",
        "dateNeeded": "2024-06-01T00:00:00",
        "deliveryMode": "Pickup",
        "status": "REVIEW",
        "total": 0.00,
        "submittedDate": "2024-03-12T15:45:29.587",
        "reasonForRejection": null,
        "user": {
            "id": 3,
            "username": "mrbison",
            "password": "not4U2know",
            "firstname": "Stuffed",
            "lastname": "Bison",
            "phone": null,
            "email": null,
            "reviewer": false,
            "admin": false,
            "requests": [
                null,
                {
                    "id": 11,
                    "userId": 3,
                    "description": "Laptop and monitor",
                    "justification": "Setting up a home office",
                    "dateNeeded": "2024-06-01T00:00:00",
                    "deliveryMode": "Pickup",
                    "status": "REVIEW",
                    "total": 75.00,
                    "submittedDate": "2024-03-20T13:12:08.61",
                    "reasonForRejection": null,
                    "user": null,
                    "lineItems": null
                }
            ]
        },
        "lineItems": null
    }
]
```

#### POST - api/requests/approve/{id}
RECEIVE
```json
{
    "id": 11,
    "userId": 3,
    "description": "Laptop and monitor",
    "justification": "Setting up a home office",
    "dateNeeded": "2024-06-01T00:00:00",
    "deliveryMode": "Pickup",
    "status": "APPROVED",
    "total": 75.00,
    "submittedDate": "2024-03-20T13:12:08.61",
    "reasonForRejection": null,
    "user": null,
    "lineItems": null
}
```

#### POST - api/requests/reject/{id}
SEND:
```json
"Rejection Reason goes here."
```
RECEIVE:
```json
{
    "id": 12,
    "userId": 3,
    "description": "Laptop and monitor",
    "justification": "Setting up a home office",
    "dateNeeded": "2024-06-01T00:00:00",
    "deliveryMode": "Pickup",
    "status": "REJECTED",
    "total": 75.00,
    "submittedDate": "2024-03-20T13:20:50.617",
    "reasonForRejection": "You do not need a home office.",
    "user": null,
    "lineItems": null
}
```

### LineItems    
|Method |Endpoint                     |Returns       |
|------ |--------                     |-------       |
|GET    |/api/lineitems               |All lineitems |
|GET    |/api/lineitems/{id}          |One lineitem  |
|GET    |/api/lines-for-pr/{requestid}|Returns all items for specifed request  |
|POST   |/api/lineitems               |One lineitem  |
|PUT    |/api/lineitems/{id}          |*n/a*         |
|DELETE |/api/lineitems/{id}          |*n/a*         | 

#### GET  
RECEIVE:  
```json
[
    {
        "id": 1,
        "requestId": 3,
        "productID": 1,
        "quantity": 1,
        "request": {
            "id": 3,
            "userId": 3,
            "description": "Laptop and monitor",
            "justification": "Setting up a home office",
            "dateNeeded": "2024-06-01T00:00:00",
            "deliveryMode": "Pickup",
            "status": "Rejected",
            "total": 0.00,
            "submittedDate": "2024-03-11T14:32:28.06",
            "reasonForRejection": "You do not need a home office.",
            "user": null,
            "lineItems": [
                null,
                {
                    "id": 2,
                    "requestId": 3,
                    "productID": 3,
                    "quantity": 2,
                    "request": null,
                    "product": {
                        "id": 3,
                        "vendorId": 2,
                        "partNumber": "28I4K144HzM",
                        "name": "Monitor",
                        "price": 259.99,
                        "unit": "EA",
                        "photoPath": null,
                        "vendor": null
                    }
                }
            ]
        },
        "product": {
            "id": 1,
            "vendorId": 2,
            "partNumber": "1T32RTS4KL",
            "name": "Laptop",
            "price": 1799.99,
            "unit": "EA",
            "photoPath": "/photos/laptop.jpg",
            "vendor": null
        }
    }
]
```

#### GET BY ID  
RECEIVE:  
```json
{
    "id": 2,
    "requestId": 3,
    "productID": 3,
    "quantity": 2,
    "request": {
        "id": 3,
        "userId": 3,
        "description": "Laptop and monitor",
        "justification": "Setting up a home office",
        "dateNeeded": "2024-06-01T00:00:00",
        "deliveryMode": "Pickup",
        "status": "Rejected",
        "total": 0.00,
        "submittedDate": "2024-03-11T14:32:28.06",
        "reasonForRejection": "You do not need a home office.",
        "user": null,
        "lineItems": [
            null
        ]
    },
    "product": {
        "id": 3,
        "vendorId": 2,
        "partNumber": "28I4K144HzM",
        "name": "Monitor",
        "price": 259.99,
        "unit": "EA",
        "photoPath": null,
        "vendor": null
    }
}
```
#### GET - api/lines-for-pr/{requestid}  
RECEIVE:  
```json
[
    {
        "id": 1,
        "requestId": 3,
        "productID": 1,
        "quantity": 1,
        "request": null,
        "product": {
            "id": 1,
            "vendorId": 2,
            "partNumber": "1T32RTS4KL",
            "name": "Laptop",
            "price": 1799.99,
            "unit": "EA",
            "photoPath": "/photos/laptop.jpg",
            "vendor": null
        }
    },
    {
        "id": 2,
        "requestId": 3,
        "productID": 3,
        "quantity": 2,
        "request": null,
        "product": {
            "id": 3,
            "vendorId": 2,
            "partNumber": "28I4K144HzM",
            "name": "Monitor",
            "price": 259.99,
            "unit": "EA",
            "photoPath": null,
            "vendor": null
        }
    }
]
```
#### POST  
SEND:  
``` json
{
    "requestId": 2,
    "productID": 3,
    "quantity": 2
}
```
RECEIVE:  
```json
{
    "id": 15,
    "requestId": 2,
    "productID": 3,
    "quantity": 2,
    "request": null,
    "product": null
}
```
#### PUT  
SEND:  
```json
{
    "id": 15,
    "requestId": 2,
    "productID": 3,
    "quantity": 2
}
```
RECEIVE:  
```json
```
#### DELETE
RECEIVE:
```json
```