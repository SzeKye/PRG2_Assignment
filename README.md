# PRG2_Assignment

# IMPORTANT: Remember to commit every time a change is made.

- Clone the whole repo and all files can be found under the folder S10257172B_PRG2Assignment
- Ignore the other folders

## Completed
1) List all customers
- display the information of all the customers
2) List all current orders
- display the information of all current orders in both the gold members and regular queue
3) Register a new customer
- prompt user for the following information for the customer: name, id number, date of birth
- create a customer object with the information given
- create a Pointcard object
- assign Pointcard object to the customer
- append the customer information to the customers.csv file
- display a message to indicate registration status

## In progress...
4) Create a customer’s order
- list the customers from the customers.csv
- prompt user to select a customer and retrieve the selected customer
- create an order object
- prompt user to enter their ice cream order (option, scoops, flavours, toppings)
- create the proper ice cream object with the information given
- add the ice cream object to the order
- prompt the user asking if they would like to add another ice cream to the order, repeating the previous three steps if [Y] or continuing to the next step if [N]
- link the new order to the customer’s current order
- if the customer has a gold-tier Pointcard, append their order to the back of the gold members order queue. Otherwise append the order to the back of the regular order queue
- display a message to indicate order has been made successfully

Validations (and feedback)
- The program should handle all invalid entries by the user
e.g. invalid option, invalid number of scoops, invalid flavour, etc.
- If user made a mistake in the entry, the program should inform the user via appropriate feedback

## Not done!!!
5) Display order details of a customer
- list the customers
- prompt user to select a customer and retrieve the selected customer
- retrieve all the order objects of the customer, past and current
- for each order, display all the details of the order including datetime received, datetime fulfilled (if applicable) and all ice cream details associated with the order
6) Modify order details
- list the customers
- prompt user to select a customer and retrieve the selected customer’s current order
- list all the ice cream objects contained in the order
- prompt the user to either [1] choose an existing ice cream object to modify, [2] add an entirely new ice cream object to the order, or [3] choose an existing ice cream object to delete from the order
o if [1] is selected, have the user select which ice cream to modify then prompt the user for the new information for the modifications they wish to make to the ice cream selected: option, scoops, flavours, toppings, dipped cone (if applicable), waffle flavour (if applicable) and update the ice cream object’s info accordingly
o if [2] is selected prompt the user for all the required info to create a new ice cream object and add it to the order
o if [3] is selected, have the user select which ice cream to delete then remove that ice cream object from the order. But if this is the only ice cream in the order, then simply display a message saying they cannot have zero ice creams in an order
- display the new updated order


4. ADVANCED FEATURES – 20% INDIVIDUAL
(a) Process an order and checkout
- dequeue the first order in the queue
- display all the ice creams in the order
- display the total bill amount
- display the membership status & points of the customer
- check if it is the customer’s birthday, and if it is, calculate the final bill while having the most expensive ice cream in the order cost $0.00
- check if the customer has completed their punch card. If so, then calculate the final bill while having the first ice cream in their order cost $0.00 and reset their punch card back to 0
- check Pointcard status to determine if the customer can redeem points. If they cannot, skip to displaying the final bill amount
if the customer is silver tier or above, prompt user asking how many of their points they want to use to offset their final bill
- redeem points, if necessary
- display the final total bill amount
- prompt user to press any key to make payment
- increment the punch card for every ice cream in the order (if it goes above 10 just set it back down to 10)
- earn points
- while earning points, upgrade the member status accordingly
- mark the order as fulfilled with the current datetime
- add this fulfilled order object to the customer’s order history

(b) Display monthly charged amounts breakdown & total charged amounts for the year
- prompt the user for the year
- retrieve all order objects that were successfully fulfilled within the inputted year
- compute and display the monthly charged amounts breakdown & the total charged amounts for the input year

(c) Recommend a feature to be implemented (bonus marks are only awarded if advanced feature is completed)
- you may gain up to 5 bonus marks if you propose and successfully implement an additional feature. You are REQUIRED to check with your tutor about your idea before implementing.