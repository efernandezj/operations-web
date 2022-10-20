# Operations Web
Local dev deployment

## Requirements for local dev
* [Install Docker Desktop](https://www.docker.com/products/docker-desktop/)
* Install Visual Studio 2022
* [Install Visual Studion Code](https://code.visualstudio.com/)
* [Install NodeJS](https://nodejs.org/)
* [Install Git Bash](https://git-scm.com/downloads)

## Build data base
Data base in dev is build in a docker container

* Open docker desktop
* Go into the 'db' folder to run the docker compose file
```
docker compose up
```
* Open API NET.CORE project located at the 'api' folder
* Open the visual studio console and run the migration
```
update-database
```


## Run API
* Open API NET.CORE project located at the 'api' folder usin Visual Studio 2022
* Run the project.

## Run Angular App
* Open Angular project in Visual Studio Code
* Install dependecies
```
npm install
```
* Run the app
```
ng serve -o
```