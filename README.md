![Alt text](https://github.com/LeonBecker1/LimitOrderBook/blob/main/MD-Ressources/%F0%9F%93%88_Order_Book.png "Optional title")

# Table of Contents:
* Functionality
* Architecture
* How to get started
* Milestones

## Functionality

This projetcs intended features can be observed on the follwing screenshot:

![Alt text](https://github.com/LeonBecker1/LimitOrderBook/blob/main/MD-Ressources/Frame.png "Optional title")

### Users can do the following:

* Navigate between different Stocks, to observe their pending orders and spread
* View their own portfolio
* Issue new orders
* View and edit their own pending orders
* See any recent sales for a given stock, that happened between two users.

Additionally, there is also support for a minimalistic form of authentication.

#### Featueres, that could make an interesting addition to the present project-scope include:
* Performance charts
* leader boards
* live chats

## Architecutre

This project was built with a focus on segregating domain specific logic from technical details. For this purpose, a hexagonal architecutre was utilized, as ilustrated through the screenshot bellow.

![Alt text](https://github.com/LeonBecker1/LimitOrderBook/blob/main/MD-Ressources/_Architecture.png "Optional title")

Each of these Areas get represented by their own C# project, as seen in the source code.

Heavy use of dependency injection and the repository pattern furtheremore lead to looser coupling and a stronger seperation of concerns.

### The Database schema is seen bellow
![Alt text](https://github.com/LeonBecker1/OrderBook/blob/master/MD-Ressources/Schema.png "Optional title")

## How to get started

1. Clone the repository
2. Edit the connection string, that can be found in the OrderBookDBContext.cs file, such that you are connected with your preferred local database
3. Update your database by running the migration script.
4. Start the OrderBook.View

## Milestones

0. Identify requirements    ✅ 
1. Create database schema   ✅
2. Run migration    ✅
3. Implement and test data access logic   ⌛
4. Implement and test application layer logic    ⌛
5. Create views, viewmodels and controllers   ⌛

