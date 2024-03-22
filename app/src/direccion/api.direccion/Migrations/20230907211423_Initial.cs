using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.direccion.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Canton = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallePrincipal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Direcciones",
                columns: new[] { "Id", "CallePrincipal", "Canton", "IdCliente", "Provincia" },
                values: new object[,]
                {
                    { new Guid("01a855ae-5c4a-4bd3-85d1-246e2f17a8a1"), "124 Adella Station", "Grimeston", new Guid("402576fd-86f4-582a-e655-3de5ffef03d5"), "Michigan" },
                    { new Guid("0371c6ba-3f2b-4158-b3f6-6c3cd776ba6b"), "34065 Wolff Way", "Chasityberg", new Guid("4080978d-b970-9d90-609f-a5e42b05c1e4"), "Idaho" },
                    { new Guid("073c2a7d-8df6-424e-a9d3-ddaf3a82e360"), "196 Turcotte Plains", "Rolfsonchester", new Guid("80619bfa-3eb0-98c5-e297-9607512bbf82"), "Virginia" },
                    { new Guid("075e162c-cf5b-44fb-9d04-095c11c019b6"), "22278 Rickie Flats", "Manteton", new Guid("1cae78d4-32e2-bda2-ecba-5643da4bc9e7"), "New York" },
                    { new Guid("0c53f2d0-ffda-4982-b16e-13a49738053b"), "7250 Durgan Ridges", "East Macey", new Guid("385b7434-cc34-dcc4-1ee4-5c8c774d2cc0"), "Florida" },
                    { new Guid("0db00e9d-7e17-4882-a009-a32e6a8c2bbb"), "321 Hackett Street", "Port Kacie", new Guid("a5631a24-ffad-4d38-3e6a-786fab7361f7"), "Wyoming" },
                    { new Guid("14063475-1119-449d-8342-6d7e207fa18c"), "2530 Ryan Falls", "Port Shadshire", new Guid("0ef36186-6dd0-da0f-a615-c9c7e09b03cb"), "Minnesota" },
                    { new Guid("158d4499-8131-4d05-ab14-f2a858aacea3"), "4809 O'Hara Knoll", "Maraview", new Guid("223e69ec-e4e2-859f-62d7-1554d56c6790"), "South Dakota" },
                    { new Guid("19274564-a58b-4734-90fa-0fd4980e91b4"), "81396 Ondricka Prairie", "Myrtisberg", new Guid("6b15a268-365c-d624-587a-e2b4d007f241"), "Wyoming" },
                    { new Guid("1acd84c6-ec50-4380-97dc-e51f3b10145c"), "040 Alison Passage", "New Lewisberg", new Guid("d2ad4a92-fa99-74bc-d0c9-109d66b516fb"), "Delaware" },
                    { new Guid("1b3e131d-bff5-4284-b07f-7d86bfd06211"), "20193 Misty Isle", "Port Kirsten", new Guid("9986847d-74c6-ad77-cb99-0bdeb86cd2b3"), "Kansas" },
                    { new Guid("1bd1c1c5-817c-4e89-ba96-ebe74ede47a1"), "70794 Hirthe Hills", "East Novamouth", new Guid("78406b3c-53da-b99d-6ba1-200c8fc83d04"), "Oklahoma" },
                    { new Guid("299f1209-78fc-4bcd-9451-83432b9d88eb"), "040 Victor Mountains", "New Aubrey", new Guid("7d608c65-8938-c338-4b5a-2cef4ee1c937"), "Delaware" },
                    { new Guid("2d9d4bbb-9cf1-4d2a-af93-0ae76fbd42da"), "3200 Hilpert Knolls", "Amarahaven", new Guid("2d964f8c-fe32-28d2-1fb2-7f6a70538adf"), "Pennsylvania" },
                    { new Guid("2fe301ec-f8f2-4ae6-87e8-03feafea630c"), "2617 Martin Stream", "Camdenstad", new Guid("aa2406d6-e77c-9a31-79b3-5f162020a045"), "Iowa" },
                    { new Guid("31eacd8a-872f-42dc-a06f-79a451640cd6"), "3678 Korey Shoals", "Titoshire", new Guid("9d04aa19-08dd-b708-7c0a-8cb4204bdd39"), "Montana" },
                    { new Guid("34861b5f-34e9-4487-b7cc-f7af19fbc57c"), "275 Courtney Ranch", "Clairburgh", new Guid("2aa563b4-7f05-682f-25cf-a3114a8837f3"), "Alaska" },
                    { new Guid("34f4f359-c33d-4646-9e03-2b1b64144157"), "401 Morar Creek", "Jenkinsmouth", new Guid("9f64d9e2-1ac7-57ef-e87a-bc322ac1c01b"), "North Dakota" },
                    { new Guid("35a2679a-9307-4141-b222-49324ab37170"), "7441 Eleanore Street", "Gerholdmouth", new Guid("a324b16b-d32c-bd93-b46e-166613c0019f"), "North Dakota" },
                    { new Guid("368317e4-36b7-4f6b-b208-9c968b8010ba"), "093 Germaine Rest", "Maynardchester", new Guid("16ff6c82-cdcc-93bc-90e6-9a5cf414716e"), "Delaware" },
                    { new Guid("3857b746-43d1-428a-ac53-daf613141814"), "2844 Swaniawski Forge", "Keeganfurt", new Guid("24e05631-fc38-77ad-925b-18602d148457"), "Texas" },
                    { new Guid("39c6e211-a317-47d1-b855-f871fa7d7ce6"), "796 Sonia Highway", "Lake Everardo", new Guid("b2985f7e-68c1-aca1-75d9-2274ae6c828e"), "Wyoming" },
                    { new Guid("39d10125-29e7-44dd-86ba-8dbb9c228c1b"), "7403 Smith Springs", "New Bellaburgh", new Guid("e7a1fb3a-4428-a76b-d73a-324acc50e53f"), "New Hampshire" },
                    { new Guid("3bff18e3-c29a-4189-b88c-e24d74e00ae8"), "80602 Laila Glens", "Hirtheborough", new Guid("7b81b053-99c2-2117-2b81-6fe7e8bcf456"), "Illinois" },
                    { new Guid("3e01adc8-f38c-401f-96e7-fc97f917702d"), "8183 Shields Highway", "New Felipashire", new Guid("afdf1f47-0a3c-dd8a-ba61-7dbdb2f95a84"), "Florida" },
                    { new Guid("3f111043-6dab-47d7-a89f-4dc8f8b29781"), "9287 Gudrun Roads", "New Dudley", new Guid("39255d70-901e-3189-ea77-0f805e9180a8"), "Michigan" },
                    { new Guid("416ed595-cbf3-4484-829d-34a888d74f67"), "841 Luettgen Oval", "Stantonside", new Guid("692f39b4-b9db-5061-d2c2-24c7e87dba9a"), "Iowa" },
                    { new Guid("418f3dd9-adb5-4953-89ae-6aca67f10aec"), "11015 Jaquan Cove", "Kesslerberg", new Guid("021067b0-e23c-daff-d90e-47aaa665f8ba"), "Nevada" },
                    { new Guid("434cba92-c467-451b-85fe-08ecead09180"), "991 Lia Spurs", "Norafort", new Guid("49e7bcd3-27d9-f3fa-1663-3131834cf6d4"), "Iowa" },
                    { new Guid("4411dff1-2a7a-4779-8f85-d89715d79b64"), "536 Vandervort Extensions", "Port Layne", new Guid("46e29e90-f4b9-db86-c39b-8a9653756701"), "Delaware" },
                    { new Guid("47a997f6-e53c-4bff-96f8-986f78bd88d6"), "2315 Cremin Fields", "Lake Luther", new Guid("80bcbe8f-7cf8-3346-6452-f158f9a0bb9b"), "West Virginia" },
                    { new Guid("47b679e6-5066-416d-a5ce-8f2059ccace9"), "9988 Harvey Ports", "West Donnafurt", new Guid("ffffe25e-9a36-21d4-2624-11c70d1f5fbc"), "Montana" },
                    { new Guid("48a9ad56-3ce8-4efd-bd62-3d3d3d756cb2"), "1502 Justyn Plains", "East Lonny", new Guid("2c1c9f1d-1513-4f28-3f61-d45b5bd04ba0"), "Indiana" },
                    { new Guid("4992ccf4-eab0-4429-a261-820003c25dea"), "59828 Jerod Forge", "Adelbertburgh", new Guid("624e2d42-a332-f475-c8e1-2284c55551ed"), "Utah" },
                    { new Guid("4bd153bc-3057-4b84-b56b-53b3e71cfdf5"), "32175 Klocko Keys", "Pollichview", new Guid("a75a6e0e-2d81-b1c8-b297-d10b1ead1e60"), "Illinois" },
                    { new Guid("50661287-1aa3-4a49-b244-2083936fa3cd"), "092 Ferry Road", "Dooleyton", new Guid("984ea0aa-366a-7261-a6c4-5c9d4fc5e1b8"), "Massachusetts" },
                    { new Guid("50b6f2af-bb8b-4127-abf7-4beb867ed6db"), "42643 Deckow Curve", "Myrticeville", new Guid("b923891b-70d8-f52a-9044-33def8e3fd5c"), "Indiana" },
                    { new Guid("5284dd21-1077-49f7-8e3c-c672823f2824"), "20350 Olson Spring", "Pollichhaven", new Guid("a270ebc5-abfc-4595-0f95-6b9b79f6991c"), "Minnesota" },
                    { new Guid("5475ddfd-1630-431c-8408-8594224b9de3"), "798 Reyna Hill", "North Ayanachester", new Guid("78b0b1df-75bf-54bc-296a-34e096158dfa"), "Minnesota" },
                    { new Guid("5c399056-48ee-4995-9b62-4c7b8f53663a"), "00032 Graciela Forge", "West Damarishaven", new Guid("6aa32c2b-370d-88f2-b3b0-091762c7868e"), "Utah" },
                    { new Guid("60016788-c2e4-494c-93ac-2eae5b852848"), "17055 Boyle Crossroad", "Littleport", new Guid("4de00e50-336a-aebb-a68e-b60a1047d6be"), "Mississippi" },
                    { new Guid("650d1ddb-0ca7-4b94-b4fc-621a32ba9d28"), "0051 Schamberger Pike", "New Lloyd", new Guid("e4198786-7474-f904-610e-a6e20c90b784"), "New Mexico" },
                    { new Guid("65232150-fba2-4c49-a70e-1ed9c8e034e6"), "35144 Ally Mill", "South Jordaneview", new Guid("0698f844-1a78-56df-fac9-80a9bf4aa6f4"), "Vermont" },
                    { new Guid("6a89e259-fa17-446d-b500-dcc3b788fffe"), "24338 Jakubowski Mill", "West Gerry", new Guid("e900ec7c-86dc-4ca2-bba2-c41f8ff861d6"), "Nevada" },
                    { new Guid("6d6ac7ae-cbf5-49a6-aa88-961054f904a3"), "3444 Russel Loaf", "New Sebastian", new Guid("8cbb5183-2fc2-e9a8-e36b-fb83aa139aac"), "Georgia" },
                    { new Guid("717f5b60-8a29-498b-ba9b-4523a53caaa0"), "176 Orval Freeway", "Schadenshire", new Guid("838d9e32-8109-a6f0-1c23-6eabd023740d"), "Maine" },
                    { new Guid("75c72b6f-4b8d-4897-93c4-990a23a96b27"), "993 Bauch Manors", "East Vicentamouth", new Guid("d4b94705-14e9-8e40-da09-64a44a9d217a"), "Rhode Island" },
                    { new Guid("7675f618-2ca4-41e9-83b0-ca9c4acdf0eb"), "95048 Unique Mall", "New Estrella", new Guid("b73331ac-6d39-2aea-9095-e57b99292fca"), "Illinois" },
                    { new Guid("7909bba7-d384-41f4-b155-87aa8596504b"), "956 Kip Coves", "Flatleybury", new Guid("a251b0cd-e93a-0eb4-8008-04706415ccc2"), "Florida" },
                    { new Guid("7d4a52ec-061a-46fa-b557-5fc3b96b4c25"), "3461 Thad Ridge", "North Marilyne", new Guid("0e467935-3ac4-d285-30a6-58bf0da80006"), "North Dakota" },
                    { new Guid("7d5b2526-aed0-4bcd-86b4-101b4b8bbdd8"), "781 Reichert Coves", "South Lilianaborough", new Guid("5d63f637-4825-c060-92f5-c786023ff8eb"), "Georgia" },
                    { new Guid("82a6fa23-ce39-4f93-a6b0-e9f28f8e7e68"), "2151 Zena Roads", "Port Isai", new Guid("614fd058-8f37-1537-6246-a8dbf7e1fbe2"), "Utah" },
                    { new Guid("854c380b-653c-485a-8f93-99cfaf38b383"), "16198 Fahey Centers", "Port Erin", new Guid("8e761e49-d92d-4810-d2a4-c84622e1455b"), "South Dakota" },
                    { new Guid("862d7e23-a4e6-4906-bdb5-417531e98515"), "24569 Alycia Views", "Kaileyton", new Guid("5c166904-f9ff-aa67-5359-840c9e109079"), "South Dakota" },
                    { new Guid("86afb0ab-c457-4df6-80d3-3cf2fa974e7c"), "4388 Meta Radial", "Albertafurt", new Guid("554b785d-5e0b-3374-db4f-e0f55de41b90"), "Hawaii" },
                    { new Guid("878cf158-fdd3-4e23-98fa-eaf866e6a0f7"), "87409 Macejkovic Via", "Brandomouth", new Guid("0e623685-a8af-4dae-f9eb-fe780836232b"), "Vermont" },
                    { new Guid("8e0a7dee-e718-496b-8714-cf7c242081b6"), "916 Maverick Fork", "Effertzshire", new Guid("59a7b707-16d4-db56-fedf-c3543510cef9"), "North Dakota" },
                    { new Guid("90aaaa8e-b361-4760-bc0f-b73765cc1e17"), "7946 Jolie Springs", "East Alexzandershire", new Guid("6e382e9b-a19a-a43b-573b-4422d2661e8e"), "Louisiana" },
                    { new Guid("9526d15d-87d5-48a3-9604-46b01e791703"), "9542 Jaylin Crossing", "Juliabury", new Guid("02599240-953c-9024-2763-1410f0315ccf"), "Wyoming" },
                    { new Guid("981955b5-7860-4b4d-8ed7-4652d95ee5bb"), "695 Virgie Crossing", "Strackeburgh", new Guid("91a45a7f-b473-5f50-5e68-69f7fc6b7a1c"), "Wisconsin" },
                    { new Guid("9de3b835-751e-482e-bc7c-4a1d2fa12076"), "03839 O'Conner Shore", "Schmelerton", new Guid("bbf01b9e-9a9f-52af-a38c-b41afdfedc88"), "New Mexico" },
                    { new Guid("9e2f0e07-b6e5-4fe7-8c41-5fcc1e78b777"), "760 Morgan Squares", "Zulaufstad", new Guid("e87842a3-ec12-7601-7ed7-61fd3ab53d63"), "Iowa" },
                    { new Guid("a00f93c4-0843-44e8-bd9c-a941a9ecb7e9"), "94730 Naomie Street", "Lake Missouritown", new Guid("2d9714f7-1d9e-d16b-c3f8-52355fba2aed"), "Oregon" },
                    { new Guid("a050a2c9-a5bd-4b19-8fea-12990605b771"), "1295 Ortiz Light", "Keithshire", new Guid("7da7a12b-a70d-5000-7869-53d2cb8583d2"), "North Carolina" },
                    { new Guid("a5b2a441-9941-440a-a89a-86ff47a69417"), "256 Cronin Heights", "Runolfssonville", new Guid("58468c0f-4a28-0c82-734e-f7ccdaef9cbd"), "Indiana" },
                    { new Guid("a8248682-5ec0-4aaa-8ee0-646ebf346f90"), "98918 Deckow Ville", "West Maxie", new Guid("68d84fc0-d284-86d3-999c-cf0db28d343a"), "North Carolina" },
                    { new Guid("aa2085a3-181b-42a9-aec6-eff57cd96f5b"), "8313 O'Keefe Inlet", "North Noreneport", new Guid("10e9c9c5-d6f6-1b42-68e3-f0d5dfa1ea20"), "Wyoming" },
                    { new Guid("aabf74b2-e93d-457a-9477-98cce6952a84"), "492 Mckenna Points", "Lionelside", new Guid("7bdeb108-e875-1b88-0250-9831234162c3"), "West Virginia" },
                    { new Guid("acb1a760-dbcb-40ce-862f-a7422bff7e7d"), "019 Towne Roads", "Lake Wallace", new Guid("e09c66ab-7159-39f8-4cd8-aec7b30f5a5a"), "Nebraska" },
                    { new Guid("adb919b5-ae1d-440b-ac0a-51ee2fd994a9"), "525 Meggie Throughway", "Kozeychester", new Guid("df1c5f96-7c8f-23a6-06f3-13fd77350566"), "North Dakota" },
                    { new Guid("ae78df94-0ce4-4d96-8516-d1b62db4cc4d"), "629 Kutch Cove", "Barrettshire", new Guid("59fd2a12-9361-8f5d-1eda-4ad4ebf8c0ee"), "Alaska" },
                    { new Guid("b1eccff8-c771-481d-bf80-da5d72d9d2a9"), "43980 Lucius Cliffs", "Port Lawrence", new Guid("340c28fd-041b-c052-ea64-fcf71b7d7105"), "Minnesota" },
                    { new Guid("b35a9b7f-22cf-4229-bf11-2728bec58b60"), "125 Sherwood Streets", "South Rhiannaburgh", new Guid("27949454-3b12-974f-80fa-e6d99c5c2308"), "West Virginia" },
                    { new Guid("b70d158a-7ef0-46a7-9d6f-c1ce6ddf3344"), "84172 Emmanuelle Drive", "Connieview", new Guid("e7ae3a65-3505-ed8d-30e7-e3a00cf462c8"), "North Dakota" },
                    { new Guid("b71c76ba-5bb1-47d2-b2b9-06ca3d627fe4"), "697 Aufderhar Ways", "West Lisette", new Guid("cf5b1f35-2970-4983-6e1a-a2d2e4d48548"), "Nevada" },
                    { new Guid("bcf8c8bd-703d-4d44-9821-17fa83be361d"), "091 Stokes Manor", "East Angelica", new Guid("6d8adc6e-34cc-bc51-ff6d-65e3b8415bbf"), "Iowa" },
                    { new Guid("c0a022d8-c5b5-4329-9f53-2b295a66cf63"), "1801 Hane Vista", "Lorineville", new Guid("16492ec1-2526-105c-c9bc-fe7f9d5fa871"), "Massachusetts" },
                    { new Guid("c6423b94-a222-4a14-af1e-7a47efe6065e"), "7274 Macejkovic Radial", "East Rebeka", new Guid("f173ce41-8f66-e27b-d63b-a439dfbafd11"), "Missouri" },
                    { new Guid("c7a393cd-1939-4162-82da-64c71a4be017"), "40649 Maxie Ranch", "Port Alexandrinemouth", new Guid("8a33a878-a72c-3932-090d-b2f03c9170d0"), "Vermont" },
                    { new Guid("c84ffb39-92cb-4575-8c6d-d209c4464993"), "4312 White Underpass", "Cadechester", new Guid("3e64b950-a9aa-51b0-da32-3482626f34ef"), "New Jersey" },
                    { new Guid("ccfdc0eb-997f-431e-85f2-f2bca20b276e"), "20250 Suzanne Groves", "Abbottmouth", new Guid("6e317fd5-db10-3c4c-41d5-0f269eba3891"), "Tennessee" },
                    { new Guid("cfe60852-e9f9-4b01-b7b3-e6dac3d01e58"), "44802 Lamont Plaza", "West Cristianville", new Guid("e481a75f-1986-05ad-3780-460e204b2243"), "South Carolina" },
                    { new Guid("d3b72de4-460b-4e9b-bc90-2724436f3d1e"), "190 Dedrick Spurs", "Ankundingfurt", new Guid("0b39cd91-9a8f-d9e9-938e-429a62bec09f"), "Connecticut" },
                    { new Guid("d4298a36-7cec-405b-9442-311445492713"), "527 Rice Fork", "Davismouth", new Guid("51b6384e-20ca-bd5e-b75d-b9a4707d55c6"), "Massachusetts" },
                    { new Guid("d9baeadb-45e8-4a66-ad32-6694c0cf1f20"), "9215 Schulist Key", "New Camryn", new Guid("92de20ab-7988-01a9-060e-2d31dadf96fb"), "Maine" },
                    { new Guid("dcf98fc6-5149-42b7-bde2-f02d7fb6b230"), "38533 Cormier Springs", "South Rahul", new Guid("2f380976-dba3-91d3-6c1f-6da52621d016"), "Ohio" },
                    { new Guid("ddef0c0f-9337-4745-890d-a12a378bcbfb"), "60017 Huel Bypass", "West Mauricio", new Guid("6c76e3e4-ce58-cdc4-64eb-d323fb58756a"), "West Virginia" },
                    { new Guid("e7f34715-d531-4619-b96c-919a1ffe57c8"), "72198 Hailey Points", "Hesselfurt", new Guid("1d9ac7a9-0dea-316d-10b6-7fe74b3f8a8c"), "Delaware" },
                    { new Guid("e9a2404a-71d5-421f-a2ca-86429baee305"), "513 Tomasa Isle", "East Belle", new Guid("7cefb691-4245-11fb-54cb-13bdd947dd8d"), "Virginia" },
                    { new Guid("eaf00218-fbcb-47f4-8781-b251f8a2f6a2"), "33750 O'Connell Stream", "Jeramyhaven", new Guid("cd59fa7f-bb62-74f3-ba5e-66846d0f6c50"), "New York" },
                    { new Guid("ebdd52fd-a8a2-47c8-876d-5939c098b64a"), "57560 Abernathy Crest", "Skylashire", new Guid("845e4d10-e4b3-fee0-a5de-77889d713657"), "Mississippi" },
                    { new Guid("edb02024-2d7f-4a61-9b08-8e22904aa727"), "142 Nicolas Alley", "Deonteport", new Guid("b0ec0a27-1676-1a96-d720-e209116af5ef"), "Iowa" },
                    { new Guid("ef148f9e-139a-4dd1-aee4-d7b31f8653ee"), "18465 Kuhlman Dam", "Russellfort", new Guid("6bb4bd8c-9e4a-21e9-105b-b44320be5e00"), "Michigan" },
                    { new Guid("f01fc7e7-ec3e-4568-852f-de188ef7bb7d"), "6760 Wilderman Walks", "North Maxime", new Guid("daef04a7-80db-d7c5-de6b-017d749af018"), "New Jersey" },
                    { new Guid("f0cc88cb-be4f-40f3-8d2b-fd518bc1f99e"), "93497 Opal Vista", "Port Toby", new Guid("14c257ab-4fcc-2f24-f1d6-9f7343705d4f"), "South Carolina" },
                    { new Guid("f399741b-b9d1-42c0-b01e-6ce3321e30ba"), "62923 Wuckert Isle", "West Maria", new Guid("f3a3155c-e8f7-ed6b-2e2f-cf3b1d0b0927"), "Georgia" },
                    { new Guid("f6a55c45-cf0f-48f9-b95f-6734c205a3c6"), "190 Grimes Well", "East Nolamouth", new Guid("f40581a5-9eee-a196-efd4-a351b7960fa9"), "Nevada" },
                    { new Guid("f87dc01a-5850-44c4-b51b-695590e0081a"), "13702 Bradtke Bypass", "East Emiliechester", new Guid("c31d490d-e396-1a30-9b27-1e23b6b47281"), "Texas" },
                    { new Guid("fa149a5d-a0a3-4f4c-b36c-ac1bcb33cc8d"), "33403 Hermann Brook", "Marquardtburgh", new Guid("c0c5cea5-18fd-919c-c9bd-ed310ea0a0fb"), "Oregon" },
                    { new Guid("fea483ae-a6bc-4d07-8c67-a4610d8163f7"), "530 Ethyl Knolls", "Makaylatown", new Guid("f2aa6148-ab8c-689d-f808-1f2c7087f446"), "Arkansas" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Direcciones");
        }
    }
}
