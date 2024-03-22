Feature: ClientesController

    @clientesController
    Scenario: Retrieve a list of clients
        Given I have a list of clients
        When I retrieve the list of clients
        Then the result should be a list of clients

    @clientesController
    Scenario: Retrieve a client by id
        Given I have a client with id a81a773c-df44-44d1-9e29-dc79e12b293b
        When I retrieve the client with id a81a773c-df44-44d1-9e29-dc79e12b293b
        Then the result should be a client with id a81a773c-df44-44d1-9e29-dc79e12b293b

    @clientesController
    Scenario: Add a new client
        When I add a new client with the following data
          | name | lastName | dateBirth  |
          | John | Doe      | 1980-01-01 |
        Then the result should be a new client with the following data
          | name | lastName | dateBirth  |
          | John | Doe      | 1980-01-01 |

    @clientesController
    Scenario: Update a client
        Given I have a client with id a81a773c-df44-44d1-9e29-dc79e12b293b
        When I update the client with id a81a773c-df44-44d1-9e29-dc79e12b293b with the following data
          | name | lastName | dateBirth  |
          | John | Modified | 1980-01-01 |
        Then the result should be a client with the following data
          | name | lastName | dateBirth  |
          | John | Modified | 1980-01-01 |

    @clientesController
    Scenario: Delete a client
        Given I have a client with id a81a773c-df44-44d1-9e29-dc79e12b293b
        When I delete the client with id a81a773c-df44-44d1-9e29-dc79e12b293b
        Then the result from clientes controller should be No Content