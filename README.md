# Casus: Sociale Premie Regeling

Dit is mijn inzending voor de casus: Sociale Premie Regeling. Ik zal mijn code hier even kort toelichten.

Ik heb een enkele methode geschreven die de 3 gevraagde waarden teruggeeft. De belangrijkste argumenten zijn de start- en stop tijdstippen van de te berekenen periode. 
Daarnaast kan een object meegegeven worden (optional) die beschrijft voor welke periode een ex partner aanspraak kan maken op de helft van de inleg en rendementen.

De aanpak waarvoor ik heb gekozen om de berekening efficienter te maken is om terug te werken van eindperiode naar begin, 
en daarbij de factoren die het resultaat per periode bepalen per stap terug te brengen naar een enkele factor. 
Op deze manier kan per periode, voor het berekenen van het kapitaal, worden volstaan met een constant aantal vermenigvuldigingen. 

Het totale rendement is dan het verschil tussen de totale inleg en het kapitaal aan het eind van de gekozen periode.

Mijn oplossing voor het berekenen van het beschermingsrendement is op dezelfde principes gebaseerd: Door de invloed van voorafgaande berekeningen terug te brengen
tot 1 factor wordt het aantal benodigde berekeningen constant gehouden. Ik twijfel echter aan de juistheid van mijn aanpak hier. Je zou namelijk verwachten dat,
bij gelijke waarden voor `b` en `o`, het beschermingsrendement de helft van het totale rendement zou bedragen. En dat is bij mij niet het geval. 

Om het vraagstuk van ex-partners te adresseren heb ik gekozen om, voor de periode waar de ex-partner aanspraak kan maken, een extra factor van 0.5 toe te passen
op het periode resultaat. 

Disclaimer: Ik heb tijdens het schrijven van deze code me niet gefocussed op onderhoudbaarheid of design principles. 
