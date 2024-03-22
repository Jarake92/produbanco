Feature: TelefonosController

    @telefonosController
    Scenario: Retrieve a list of telefonos
        Given I have a list of telefonos
        When I retrieve the list of telefonos
        Then the result should be a list of telefonos

    @telefonosController
    Scenario: Retrieve a telefono by id
        Given I have a telefono with id 07f65539-57a4-46b0-a7ec-540bbda4d468
        When I retrieve the telefono with id 07f65539-57a4-46b0-a7ec-540bbda4d468
        Then the result should be a telefono with id 07f65539-57a4-46b0-a7ec-540bbda4d468

    @telefonosController
    Scenario: Add a new telefono
        When I add a new telefono with the following data
          | numero    | operadora | tipo |
          | 123456789 | 2         | 2    |
        Then the result should be a new telefono with the following data
          | numero    | operadora | tipo |
          | 123456789 | 2         | 2    |

    @telefonosController
    Scenario: Update a telefono
        Given I have a telefono with id 07f65539-57a4-46b0-a7ec-540bbda4d468
        When I update the telefono with id 07f65539-57a4-46b0-a7ec-540bbda4d468 with the following data
          | numero    | operadora | tipo |
          | Modified | 2         | 2    |
        Then the result should be a telefono with the following data
          | numero    | operadora | tipo |
          | Modified | 2         | 2    |

    @telefonosController
    Scenario: Delete a telefono
        Given I have a telefono with id 07f65539-57a4-46b0-a7ec-540bbda4d468
        When I delete the telefono with id 07f65539-57a4-46b0-a7ec-540bbda4d468
        Then the result from telefonos controller should be No Content