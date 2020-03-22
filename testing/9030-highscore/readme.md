# Highscore Exercise

## Introduction

You have a given browser game and your job is to add the possibility to store a highscore list for it. You have to slightly extend the game (not the core part of this exercise), write a backend for storing and querying highscores (including automated tests), and package everything up in Docker containers.

Writing the backend is part of your POSE class. The Docker part counts for your NVS MC grade.

**Teamwork is allowed.** Teams must consist of up to three people.

**Note** that you can earn up to six extra points for your POSE and NVS MC classes. Because of the exceptional situation, I will suspend the limit of 10 extra points per year. **All gathered extra points will count for your final grade.**

## The Game

The browser game is a simple *Space Shooter*. You can find a description for how to build it [on GitHub](https://linz.coderdojo.net/trainingsanleitungen/web/space-shooter.html). You do *not* need to follow the exercise step by step. You can immediately switch to [the ready-made game](https://github.com/rstropek/ts-space-shooter-starter/tree/08-game-over) that you can find in the *08-game-over* Git branch.

Don't forget to run `npm install` to get all the necessary dependencies. Run `npm start` to start the game. Run `npm run build-dev` to build a test release (including sourcemaps for debugging). Run `npm run build` to build a release version.

## The Highscore List

The game is not ready for adding a highscore list. You have to extend it in the following ways:

* Extend the game so that it counts points. For each shooted meteor the player should get one point.
* Add a possibility to enter player initials (three letters). You are free to design this feature as you want. Possible options would be e.g.:
  * Let the user enter the player initials in a HTML textbox before the game starts.
  * Let the user enter the player initials in a HTML textbox after the game ended.
  * Extend the game and add a more classic Arcade-style entering of player initials:<br/>
    ![Arcade-style player initials](https://www.mikesarcade.com/estore/prod/frogger/images/hsentry-1.png)

Use .NET Core to implement a backend web API that can store highscores for the game. Here are the functional requirements:

* Store points associated with the initials of the player.
* Store up to ten entries in the highscore list. There can be less than ten highscores, but there must not be more than ten. If an 11th is added, the lowest highscore entry is deleted.
* Write *at least five* meaningful automated tests for your API.
* Extend the browser game so that it uses the web API to store highscores.
* Display the current highscore list (sorted descending) after the game ended. You are free to design this feature as you want (e.g. HTML table, display list in the game, etc.).

If you implemented all of the above requirements, notify me via GitHub issue. You will get one additional point for your grade (POSE).

## Containerize the App

* Write a Dockerfile for the extended game so that a user can start a webserver with the game installed and ready to play.
* Write a Dockerfile for the .NET Core backend.
* Write a *readme.md* file describing how to work the the Docker-related assets (e.g. building images, starting and linking containers, etc.).

If you implemented all of the above requirements, notify me via GitHub issue. You will get one additional point for your grade (NVS MC).

## Extra Challenges

### POSE

* Earn one extra point by storing the highscore list persistently, not just in memory. Choose any storage technology that you like (file, SQL database, NoSQL database, etc.).<br/> **Recommendation:** To practice for your final exam, use *SQL Server* with *Entity Framework*.
* Earn one extra point by protecting your API using [Google's *reCAPTCHA*](https://developers.google.com/recaptcha/intro). Without that, a player could easily cheat and use e.g. Postman to write his name into the highscore list. Using *reCAPTCHA* makes that not impossible, but harder.

### NVS MC

* Earn one extra point by writing a *docker-compose* file with which a user can start the backend, (if necessary) the associated data store (e.g. SQL Server), and the webserver with the game.
* Earn one extra point by deploying the game and the highscore list in Azure. You can use [Azure's Free Tier for Linux Web Apps](https://azure.microsoft.com/en-us/blog/azure-app-service-update-free-linux-tier-python-and-java-support-and-more/) for that. If you want to store the highscore list persistently, consider using the new [Azure CosmosDB Free Tier](https://azure.microsoft.com/en-us/updates/azure-cosmos-db-free-tier-is-now-available/).

### Still Bored?

Try to fully automated building and deploying the app using *Azure DevOps Pipelines* or *GitHub Actions*. You will not get extra points for that. You do that for improving your skills and earning respect.
