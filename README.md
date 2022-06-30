# C# - Klausurersatzleistung

<img align="right" width="220" height="90" src="https://upload.wikimedia.org/wikipedia/de/thumb/1/1d/DHBW-Logo.svg/541px-DHBW-Logo.svg.png?20110626153129">

[![forthebadge made-with-C-Scharf](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://github.com/DHBW-Inf20/2048/)

## Dokumentation

[Hier gehts zu den Dokumentationen!](https://github.com/FelixWochele/2DO/blob/master/Dokumentation - Wochele Felix - TInf2020.pdf)


## Bei Problemen:

### SQL Light Fehler
Schaune ob die Packet SQLite und System.Data.SQLite in BEIDEN Projekten instaliert sind 

### Debuging funktioniert nicht
Beim Server in den Debug einstellungen Aktivieren

### Keine Verbindung zum WCF Service
Client und Server können nicht in einer gemeinsamen Debuging Instanz von VS laufen. 
In den einstellungen darauf achten, dass es unterschiedliche Instanzen sind.

### Fehlende Bilder 
Projekt neu clonen oder Bilder als "Build Action" : "None" und dann wieder "Ressource" oder "Content" makieren

## Git:

### clone:
```shell
git clone https://github.com/FelixWochele/2048.git
```
### commit & push:
```shell
git add * 
git commit -m "Commit-Message"
git push origin main
```
