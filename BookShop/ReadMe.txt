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

Klijentska Validicija:
- U samom modelu atributi Required, EmailAddress...
- U kombinaciji sa asp-validation-for="ime_Property" u samom View-u
- Sve sto je u kontroleru nema veze sa klijentskom validacijom

Serverska validacija:
- Desava se kad klijent (Korisnik) klikne na Submit form i odlazi u odgovarajaucu akciju u kontroleru
- U samoj akciji kontrolera vrsimo proveru submitovanih podataka
- Npr. HttpPost za Create i Update.