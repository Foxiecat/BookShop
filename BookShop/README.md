# BookShop API
BookShop API er en bokhandel API some gir funksjonalitet for å legge til, hente, oppdatere og slette bøker fra bokhandelens lager.

## Database struktur
Vi lager in Database i MySQL for å lagre bøkene. Databasen har tabellen **`Book`** som innerholder disse feltene:

| Kolonnenavn      | Datatype        | Beskrivelse                       |
|------------------|-----------------|-----------------------------------|
| `Id`             | INT (Primary Key, Auto Increment) | Unik identifikasjon for boken     |
| `Title`          | VARCHAR(255)     | Boktittel                         |
| `Author`         | VARCHAR(255)     | Forfatterens navn                 |
| `PublicationYear`| INT              | Året boken ble publisert          |
| `ISBN`           | VARCHAR(13)      | Internasjonalt standard boknummer |
| `InStock`        | INT              | Antall bøker på lager             |

## Opprettelse av Databasen
Gå inn i Setup Database folderen og kjør BookShopDB.sql

## API-Endepunker

### Hente bøker (GET /books)
Henter ut alle bøker
Men du kan også filtrere med Title, Author og/eller PublicationYear

### Hente en bok (GET /books/{id})
Henter en bok basert på ID

### Opprette en bok (POST /books)
Du kan opprette en ny bok

### Oppdatere en bok (PUT /books/{id})
Du kan oppdatere informasjonen for en bok basert på id

### Slette en bok (DELETE /books/{id})
Du kan slette en bok fra databasen basert på id.