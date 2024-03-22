Feature: DireccionesController

    @direccionesController
    Scenario: Retrieve a list of direcciones
        Given I have a list of direcciones
        When I retrieve the list of direcciones
        Then the result should be a list of direcciones

    @direccionesController
    Scenario: Retrieve a direccion by id
        Given I have a direccion with id 1444c789-5cca-4790-997a-0615194f7bde
        When I retrieve the direccion with id 1444c789-5cca-4790-997a-0615194f7bde
        Then the result should be a direccion with id 1444c789-5cca-4790-997a-0615194f7bde

    @direccionesController
    Scenario: Add a new direccion
        When I add a new direccion with the following data
          | provincia | canton | callePrincipal |
          | lorem     | ipsum  | dolor          |
        Then the result should be a new direccion with the following data
          | provincia | canton | callePrincipal |
          | lorem     | ipsum  | dolor          |

    @direccionesController
    Scenario: Update a direccion
        Given I have a direccion with id 1444c789-5cca-4790-997a-0615194f7bde
        When I update the direccion with id 1444c789-5cca-4790-997a-0615194f7bde with the following data
          | provincia | canton | callePrincipal |
          | Modified  | ipsum  | dolor          |
        Then the result should be a direccion with the following data
          | provincia | canton | callePrincipal |
          | Modified  | ipsum  | dolor          |

    @direccionesController
    Scenario: Delete a direccion
        Given I have a direccion with id 1444c789-5cca-4790-997a-0615194f7bde
        When I delete the direccion with id 1444c789-5cca-4790-997a-0615194f7bde
        Then the result from direcciones controller should be No Content