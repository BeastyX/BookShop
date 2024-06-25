NUGET CONSOLE:

get-migration // da bi se uverio da inicijalna migracija nije primenjena (apply)
update-database // primenjena migracija i napravljene sve tabele za authenticaton/authorization

- Kada dodajemo domensku klasu (model koji zelimo u bazi) prvo dodamo njegov DB set
u ApplicationDbContext: 
public DbSet<Category> Categories { get; set; } //Categories je naziv tabele u bazi
- Onda radimo:
Add-Migration Ime_Migracije
- Onda pregledam UP i DOWN migracije da se uverim da je sve ok, pa onda:
Update-Database


Login / Register:
- Da bih uopste imao pristup login i register page-ovima mora da se doda scafholding za identity area-u
- Desni taster na Area / Add / Area / sa leve strane biram identity pa desno izaberem identity
- Izaberem koje page-ove hocu, u ovom slucaju login i register, i svoj DB kontekst i Add na kraju.

Areas:
- Admin area-u dodajemo desni klik na areas / Add / Area.
- Dodajemo novo rutiranje pre default sa nazivom "Areas" i pattern-om "{area:exists}/{controller=Home}/{action=Index}/{id?}"

Klijentska Validicija: (Model View)
- U samom modelu atributi Required, EmailAddress...
- U kombinaciji sa asp-validation-for="ime_Property" u samom View-u
- Sve sto je u kontroleru nema veze sa klijentskom validacijom

Serverska validacija: (Controller Model)
- Desava se kad klijent (Korisnik) klikne na Submit form i odlazi u odgovarajaucu akciju u kontroleru
- U samoj akciji kontrolera vrsimo proveru submitovanih podataka
- Npr. HttpPost za Create i Update.

Area:
- Area je deo rute koji omogucuje organizaciju kontrolera u razlicitim podrucjima

Ajax:
- Omogućava slanje i primanje podataka između web preglednika i web servera, 
bez potrebe za osvježavanjem cele stranice.

AntiForgery:
- Ukoliko nemamo antiForgery tokene, onda neko mzoe da nam presretne http request. Npr.
Ako smo u kaficu i koristimo njihov javni Wifi neko moze da nam presretne http request i da vidi koje parametre koristimo.
Onda moze sam da Pozove isti http request sa razlicitim parametrima koji njemu odgovaraju.
Koriscenjem AF tokena mi smo sigurni da samo nas view moze da kumunicira sa kontrolerom a niko od spolja.

--------------------------------------
* Program.cs *

- Services > Ovaj deo koda se koristi se za konfiguraciju i postavljanje servisa u aplikaciji, npr. 
servis koji koristi Entity Framework Core za komunikaciju s SQL Server bazom, dodavanje podrsku za uloge u aplikaciji.
To je tako zvani dependecy injection zahvaljujuce kome u Kontrolerima imamo dostupnost bazama (DBContext), UserManageru...


* Appsettings.Json *

- sluzi za konfiguraciju aplikacije i definisanje veze prema bazi podataka.



* _ViewStart.cshtml *

- Prvi Partial View koji poziva Program.cs.



* _ViewImports *

- Koristi se za uvođenje globalnih direktiva koje se primenjuju na sve Razor stranice. 
@BookShop > omogućava korišćenje svih tipova i metoda



* _Layout *

- Definira osnovnu strukturu i izgled stranica
- Link > Veze prema CSS stilovima
- Partial name > Ukljucuje druge partial view-ove
- RenderBody() >  Postavlja sadržaj specifičan za svaku stranicu koja koristi ovaj layout
- Scripts > uključuje JavaScript datoteke potrebne za funkcionalnosti



* _NavBar *

    /



* _LoginPartial *

- Prikaz zavisi od Autentifikacije. (Prikaz u NavBaru gore desno)



* _Notifications i _SweetConfirmations*

- Notifikacije ucitavamo uvek u aplikaciji, 




* _ShoppingCartItems *

- Sluzi za prikaz itema u cartu
