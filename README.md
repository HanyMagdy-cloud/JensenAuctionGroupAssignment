
# Auction Website

A full-stack auction website built from scratch. React is used for the frontend and communicates with a .NET API integrated with a relational MSSQL database.

## Technical Details

### Security
- JWT is used for user authentication
- Hashing+salting is applied to all passwords
### Database ( SQL Server )
- Three tables  - Auctions, Bids, Users
- Stored Procedures for all actions
### .NET API 
- Dapper as micro-ORM
- Repository-esque pattern
- Unit Testing
- CORS & Swagger
### React Website
- Based on React Router and Context API
- Standalone CSS
- Clean Separation of Concerns

## Stuff to Implement/Fix

- email verification on signup and rate-limits for scalability
- when JWT tokens expire, redirect and inform user properly
- Link with legitimate payment methods for tracking user balances
- Make the API conform to standards (service layer & JSON as body on all HTTP reqs)
