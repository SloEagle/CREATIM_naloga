CREATIM naloga:

Poudarek na tej nalogi je na zasnovi aplikacije in ne toliko na konkretni kodi.
Proste roke pri rešitvi puščamo, ker ne želimo, da porabiš 1 teden dela, za nekaj kar je samo demonstracija
tvojega znanja, zato poudarjamo, da nam je najbolj pomemben programerski koncept in arhitektura rešitve, kako
se classi (razredi) povezujejo med seboj, kako so definirani vmesniki, kako so uporabljeni principi spletnega
(MVC) in objektnega programiranja (polimorfizem, dedovanje,... in ostali,
glej http://www.codeproject.com/Articles/22769/Introduction-to-Object-Oriented-Programming-Concep).
Nič nimamo proti konkretni kodi, ker na ta način še bolje spoznamo tvoje veščine, a je ta manj pomembna od
omenjenih ciljev. Če imaš čas in voljo, lahko tudi vse sprogramiraš (kar je lahko tudi dobra vaja za naprej, ker so
to pogosti problemi, ki jih rešujejo programerji).
Če je karkoli nejasno oz. imaš vprašanja, prosim vprašaj.
NALOGA:
Napiši spletno aplikacijo, ki omogoča pregledovanje, dodajanje, urejanje in brisanje fizičnih in pravnih oseb.
Fizične osebe imajo podatke:
 ime in priimek (v enem polju)
 email
 tel. številka
Pravne osebe pa imajo še dodaten podatek:
 davčna številka
Pri vnašanju in urejanju validiraj vhodne podatke: email, tel.številka, davčna številka.
Aplikacija naj omogoča dodajanje oseb v skupine. Urejanja skupin ni potrebno implementirati. Dovolj je, da je
mogoče ustvariti novo skupino, v kateri so poljubno izbrane osebe.
Aplikacija omogoča pošiljanje SMSov posameznim osebam IN/ALI skupinam. Za pošiljanje SMSov se uporablja
eksterne storitve (sms providerje), s katerimi se komunicira preko APIjev, ki jih definira provider.
V začetni fazi je potrebno podpreti 2 SMS providerja, vendar se lahko zgodi, da bodo kasneje v življenju projekta
dodani novi (npr. cenejši oz. zanesljivejši). Dovolj je, da implementiraš funkcijo pošiljanja SMS sporočila, en
provider naj komunicira preko REST vmesnika, drugi pa preko SOAP. Lahko predpostavljaš, da SOAP client že
obstaja.
Logika za pošiljanje je sledeča:
- sms se pošlje prvemu providerju na spisku providerjev
- če je bilo preko providerja poslanih že 100 smsov, se trenutno prvi provider premakne na zadnje mesto na
spisku providerjev, drugi provider na spisku postane prvi
Frontend-a (html, javascript,...) ni potrebno programirati, naj pa bodo nakazani oz. definirani vmesniki za
komunikacijo s frontendom. Po domače povedano, naj se nakaže kako se prikaže GUI in katere podatke se
pošlje na GUI, npr.:
public void handleSubmit() {
   // preberi podatke o imenu, emailu, ki se submitajo
   rezultat = funkcijaKiNekajNarediSPodatki ( ime, email, davcna );
   // prikazi rezultat na frontendu
}
Koda je lahko napisana v poljubnem jeziku oz. psevdojeziku (da ni treba pisati veliko trivialne kode), primer:
public function sendEmail (string toEmail, string message) {
  if (email != null) {
     // Email library pošlje email na toEmail z vsebino message
  }
}
v zgornjem primeru npr. ni potrebno importat in inicilizirat konkretne knjižnice za pošiljanje emailov, ker je to
trivialno in ni namen te naloge.
Poleg kode, prosim dodaj še definicijo tabel, ki jih boš uporabil. Uporabiš lahko poljubno relacijsko pod. bazo

(MySQL, MariaDB, DB2, MSSQL, Oracle, Postgre,...) ali pa mi samo napišeš imena tabel, stolpcev, ki jih
vsebuje, tipe podatkov, primarne ključe, tuje (foreign) ključe in indekse.
Z bazo lahko komuniciraš preko SQL stavkov ali preko poljubne ORM knjižnice, preferirana je uporaba ORM.
Sama uporaba je lahko nakazana, ni potrebno inicializirat knjižnic, če uporabiš SQL je dovolj takšna koda:
rows = DBLibrary.fetchQuery(&quot;SELECT a,b,c FROM d WHERE e&quot;);
za ORM pa:
rows = ORMLibrary.someOrmMethodThatFetchesSomethingFromDatabaseAndReturnsEntityObjects(...);
Predpostavljaj, da aplikacija teče na že postavljenem webserverju (poljubnem, lahko tudi namišljenem), da so
vse knjižnice na voljo. Ni potrebno pisati import stavkov (lahko pa).
Aplikacija teče na strežniku, ki nima neomejenih resursov (ram, cpu, disk...), tako da je optimizacija zaželjena, ni
pa potrebna.
Aplikacija naj bo napisana tako, da je skalabilna, da je razširljiva, modularna in prilagodljiva (več kot ima teh
lastnosti, boljše).
Aplikacija naj čimbolj &quot;elegantno&quot; upravlja z napakami (tako predvidljive kot nepredvidljive). Elegantno pomeni,
da če se zgodi napaka, da napaka ne uide na frontend, kjer končni uporabnik dobi na zaslon stack trace ali pa
kak &quot;Null pointer exception&quot;. Bonus točke je nakazano logiranje napak.
