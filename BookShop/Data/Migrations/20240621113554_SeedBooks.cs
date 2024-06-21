using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: ["Id", "Title", "Description", "Author", "Price", "CategoryId", "CoverImagePath"],
                values: new object[,]
                {
                    {
                        1,
                        "Kros",
                        "Godinama posle nerazrešenog ubista supruge Marije, psihologa Aleksa Krosa poziva bivši partner Džon Sampson da mu pomogne u slučaju za koji se ispostavlja da je povezan sa Marijinim ubistvom. Da li je konačno dobio priliku da uhvati njenog ubicu?",
                        "Džejms Paterson",
                        1200.0,
                        1,
                        @"\images\predefined\001-kros-dzejms_paterson.jpg"
                    },
                    {
                        2,
                        "Na Drini ćuprija",
                        "Hronika čuvenog mosta na Drini, glavnog junaka Andrićeve višegradske sage tokom četiri stoleća svog postojanja, od gradnje turske ćuprije u XVI veku do austrijskog bombardovanja i Sarajevskog atentata, književno je remek-delo oblikovano kao niz priča, sazdanih od legendi i stvarnih događaja, od predanja i kazivanja poput Šeherezadinih priča iz 1001 noći. Ovaj niz priča, ulančen u skladan i nezadrživ tok poput nemirne Drine, kao simbola granice između dva sveta, s mostom koji ih spaja, oživljava nezaboravne likove višegradskih žitelja i njihovih osvajača, od slavnog graditelja ćuprije rodom iz Sokolovića, hajduka Radisava, sejmena Plevljaka, vlasnice hotela Lotike, do Ćorkana, Tome Galusa, Alihodže, nesrećnog Glasinčanina i drugih protagonista Andrićevog bosanskog karakazana…",
                        "Ivo Andrić",
                        650.0,
                        2,
                        @"\images\predefined\002-na_drini_cuprija-ivo_andric.png"
                    },
                    {
                        3,
                        "Krvavi mesec",
                        "Angažovani su svi resursi policije u Oslu. Nestale su dve devojke. Jedino što ih povezuje je zabava poznatog milijardera i trgovca nekretninama na kojoj su svojevremeno obe bile. Kada pronađe telo jedne od njih, policija primećuje neobičan „potpis“ ubice koji nagoveštava da je to tek početak njegovih monstruoznih namera.",
                        "Ju Nesbe",
                        975.50,
                        3,
                        @"\images\predefined\003-krvavi_mesec-ju_nesbe.jpg"
                    },
                    {
                        4,
                        "Poeme",
                        "Ravnica iz koje je potekao za Antića nije geografsko poreklo već ishodište jednog mentaliteta. Poeme su za njega i kovitlac istorijskih zbivanja i orkan snoviđenja, ono što steže grlo i ono što izlazi iz grla, one su i molitva i psovka, breme uspomena i uzlet do jezera rodnog neba.",
                        "Miroslav Antić",
                        1049.25,
                        4,
                        @"\images\predefined\004-poeme-miroslav_antic.jpg"
                    },
                    {
                        5,
                        "Poslednja Danteova tajna",
                        "Na sahrani Hajnriha VII Dante postaje svestan da su zajedno sa carem umrle i njegove nade za budućnost Italije, kao i mogućnost da se, konačno, vrati u Firencu. U najmračnijem času njegovog života prići će mu nepoznati čovek i reći da u Apuliji postoji vitez koji tvrdi da je direktan potomak velikog Fridriha II. On se krije u blizini Lučere, gde još odolevaju preživeli muslimanski plaćenici koje je car unajmio. S obzirom na to da nema više šta da izgubi, osim životnog dela koje piše godinama, prerušen u hodočasnika Dante će krenuti na dug i opasan put. Na tom putu susrešće tajanstvenu devojku germanskog porekla, upoznati lepote muslimanske kulture i shvatiti da je stigao do prelomne tačke u životu…",
                        "Đulio Leoni",
                        899.25,
                        5,
                        @"\images\predefined\005-poslednja_danteova_tajna-djulio_leoni.jpg"
                    },
                    {
                        6,
                        "Berlin: Pad 1945.",
                        "Entoni Bivor, jedan od najpoznatijih vojnih istoričara današnjice, donosi nam napetu priču o konačnom sukobu Crvene armije i nacističkih snaga ispripovedanu većinom iz ugla običnih vojnika i građana. Koristivši u potpunosti pristup donedavno zapečaćenim sovjetskim arhivima, kao i nemačke, američke, britanske, francuske i švedske izvore, Bivor je rekonstruisao različita iskustva miliona ljudi zahvaćenih samrtnim ropcem Trećeg rajha. Berlin: Pad 1945. prikazuje ne samo surovost i očajanje grada pod opsadom nego i retke trenutke krajnje humanosti i herojstva. Ova knjiga sadrži takođe i nova otkrića o motivima koji su naveli Staljina na užurbani napad na grad. Knjiga će sigurno privući sve čitaoce zainteresovane za vojnu istoriju i Drugi svetski rat, a obrađuje temu na način koji u dogledno vreme neće biti nadmašen.",
                        "Entoni Bivor",
                        1124.25,
                        6,
                        @"\images\predefined\006-berlin_pad_1945.jpg"
                    },
                    {
                        7,
                        "Sedam svetskih čuda",
                        "Sunce se još jednom okreće i narodi će se boriti jedan protiv drugog da pronađu i dođu do nestalog Kamena… ali grupa naroda vođena super-vojnikom Džekom Vestom mlađim, okuplja se kako bi sprečila bilo koju drugu zemlju da dospre do njegovih zastrašujućih moći. Tako počinje najveća potraga za zlatom svih vremena, adrenalinska potera vođena na svetskom bojnom polju.",
                        "Metju Rajli",
                        929.50,
                        1,
                        @"\images\predefined\007-sedam_svetskih_cuda-metju_rajli.jpg"
                    },
                    {
                        8,
                        "Sasvim jednostavno",
                        "Majkl Harison je imao sve: dobar izgled, šarm, vragolast smisao za humor, bio je istinski vođa, a onda je dobio i nju, Ešli, svoju verenicu. Dok je slavio sa grupom prijatelja samo par noći pred svoje venčanje, Majkl se iznenada i neočekivano zatekao u mrtvačkom sanduku opremljen samo baterijskom lampom, erotskim časopisom, voki-tokijem i cevčicom kroz koju jedva može da se diše. To je samo zabava – sitna „osveta“ njegovih prijatelja za sve šale i smicalice koje je on njima priređivao, no zabava prestaje onog trenutka kada četvorica pijanih prijatelja poginu u saobraćajnoj nesreći nekoliko trenutaka nakon što su ostavili Majkla sasvim samog i živog sahranjenog.",
                        "Piter Džejms",
                        1100.0,
                        1,
                        @"\images\predefined\008-sasvim_jednostavno-piter_dzejms.jpg"
                    },
                    {
                        9,
                        "Porodična tragedija",
                        "Bil je samohrani otac osmogodišnje devojčice. Zbog novčanih neprilika prinuđen je da primi podstanara. U njegov stan će se tako useliti Karla, devojka koja napušta majku narkomanku na dalekom severu Švedske ne bi li studirala pravo u Lundu.",
                        "Matijas Edvardson",
                        974.25,
                        5,
                        @"\images\predefined\009-porodicna_tragedija-matijas_edvardson.jpg"
                    },
                    {
                        10,
                        "Kraj priče",
                        "U svetu u kome je fikcija zabranjena, a pisanje zločin, bujna mašta može da bude opasna. Pet godina je prošlo otkako je vlada uvela zabranu književnosti i prognala svu beletristiku iz knjižara i biblioteka. Ni deci ne smeju da se čitaju priče za laku noć, a piscima je strogo zabranjeno da pišu. Među njima je i Rejč Kapri, autorka više bestselera i dobitnica najvećih nagrada. I ona je prinuđena da se povuče u anonimnost i da se bavi nečim sasvim drugim. Rejč, međutim, ipak potajno beleži zbivanja u novom sumornom životu: zastrašujuće posete vladinih službenika; odlaske u skroviti podrum, gde preko telefona čita priče besanoj deci; razgovore s Hanterom, dečakom koji je stalno poziva i koji joj je ukrao srce; posete neobičnog prodavca čaja.",
                        "Luiz Svonson",
                        899.90,
                        5,
                        @"\images\predefined\010-kraj_price-luiz_svonson.jpg"
                    },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValues: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                );
        }
    }
}
