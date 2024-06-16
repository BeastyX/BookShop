Nuget console:
get-migration // da bi se uverio da inicijalna migracija nije primenjena (apply)
update-database // primenjena migracija i napravljene sve tabele za authenticaton/authorization

Login / Register:
- Da bih uopste imao pristup login i register page-ovima mora da se doda scafholding za identity area-u
- Desni taster na Area / Add / Area / sa leve strane biram identity pa desno izaberem identity
- Izaberem koje page-ove hocu, u ovom slucaju login i register, i svoj DB kontekst i Add na kraju.