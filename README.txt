Pre pokretanja:
	U okviru foldera gde se nalazi projekat, dijagram i dokumentacija projekat potrebno je dodati folder Logs
	Kad se otvori projekat u Visual studiou potrebno je promeniti putanje u nekim fajlovima

Korisni linkovi:
	https://www.youtube.com/watch?v=ayp3tHEkRc0&ab_channel=IAmTimCorey
	https://www.sqlite.org/datatype3.html
	https://www.youtube.com/watch?v=ub3P8c87cwk&ab_channel=IAmTimCorey
	https://www.youtube.com/watch?v=DwbYxP-etMY&t=1054s&ab_channel=IAmTimCorey
	
Objasnjenje zasto padaju ona dva testa:
	Na pocetku metoda koje se testiraju ceka se da se iz baze vrate neki podaci... U pokretanju aplikacije dobicemo neke vrednosti.
	S obzirom da mi zavisimo od metode LoadRecords koja se nalazi u klasi SQLDataAccess (pricam za testiranje metode CalculatePower iz PowerConsumptionCalculator klase)
	mi moramo da je mokujemo tj da napravimo instancu te klase koja imitira njeno postojanje ali ne imitira njene funcionalnosti...
	Kad ne imitira njene funcionalnosti onda i ne vraca dobre rezultate i tada ce ona svaki put vratiti null
	
	Mi mozemo staviti da neki uslov ako je null da izadje iz metode ali svejedno postoji deo koda koji je ispod tog uslova koji nije i dalje testiran
	a nece nikad ni biti jer je ta lista uvek null i pucanje kod. I zbog toga nam se smanjuje pokrivenost koda.
