using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.telefono.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Operadora = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Telefonos",
                columns: new[] { "Id", "IdCliente", "Numero", "Operadora", "Tipo" },
                values: new object[,]
                {
                    { new Guid("02fa36f2-6737-41e2-97db-87cd66167254"), new Guid("df051501-395e-9edc-c7e8-2d09ec3a16d1"), "05-896-228-706", 0, 1 },
                    { new Guid("09344f11-b5f3-440b-b2f6-63cc3dd4c314"), new Guid("4fb82775-9e32-be46-a9d9-de1b2dd45431"), "05-917-525-861", 1, 0 },
                    { new Guid("09fe5056-73d4-4e2a-add6-bd2cde782990"), new Guid("f23d65af-5641-95e0-0dcb-d1415c2d988e"), "01-601-334-555", 2, 1 },
                    { new Guid("0a07aa67-a742-448e-a8fd-5832f6824e24"), new Guid("6754e126-59c0-0138-7e09-9a6412818c12"), "05-287-367-352", 1, 0 },
                    { new Guid("0b24a920-aaed-4ec5-a166-7fe0f99ef5c7"), new Guid("9aafdd84-3424-28ee-bb84-8f634f79c82b"), "04-563-678-397", 2, 0 },
                    { new Guid("0c7074b4-6ae1-4c28-a425-d7d805e097f7"), new Guid("e0dd95a6-0a30-f65f-1594-c092d3359089"), "05-509-832-643", 2, 1 },
                    { new Guid("1170ad27-5468-4534-a360-2c3cb7b4c4f0"), new Guid("7085457f-207c-256c-3340-580aeb0fa9f3"), "03-044-735-784", 1, 1 },
                    { new Guid("1369453b-de26-474b-a751-b4005e79803c"), new Guid("2e608ec6-280b-d4fe-cc4e-6e2e349b032b"), "03-019-264-945", 1, 0 },
                    { new Guid("15c37a7f-5949-449a-8b00-248f5d5e9ae5"), new Guid("db3c5ba5-1b47-436d-5158-935d17b9e219"), "00-992-136-671", 3, 1 },
                    { new Guid("16c146f1-f4a7-43c4-9b22-7e8bc81d612c"), new Guid("0a20ad77-8a5d-3e63-b01f-5ef7861224a1"), "01-073-191-621", 1, 0 },
                    { new Guid("18911cc4-aa08-468f-89cc-0cc108b2b7e9"), new Guid("b4264b21-b24b-734c-e303-eca201c82406"), "02-499-206-959", 2, 0 },
                    { new Guid("1a74d2c6-f709-49aa-81d9-ebb52846d082"), new Guid("eee621de-ec7c-56c8-1e54-10a25eda030d"), "03-083-651-637", 3, 1 },
                    { new Guid("1a74ef53-3186-4468-b707-4bcf0d6a3ee8"), new Guid("05b3007a-e304-a09e-152c-99302f0b077c"), "09-674-531-911", 3, 0 },
                    { new Guid("1ddb0647-0867-4bd2-8062-014d7da87857"), new Guid("4ef26e19-2cee-9c75-72ad-70c8a4c37749"), "01-939-106-667", 2, 0 },
                    { new Guid("1e7519aa-d187-408d-a6ce-b9aac4148f65"), new Guid("2b2ed985-bfc1-c9e4-e269-ea1ea7a6d7ef"), "03-854-065-997", 2, 0 },
                    { new Guid("1ed2cc06-7f51-4803-98cb-a75b673e8ce3"), new Guid("eb5dfdfb-b947-1852-884f-9fbc8393c268"), "00-028-117-515", 3, 1 },
                    { new Guid("25ec6419-b0c3-4a1f-bc06-6c8c7eef97f6"), new Guid("fc90924f-7506-90a5-af8a-4628f1dfdc8a"), "06-940-091-387", 0, 0 },
                    { new Guid("2aa5d2ee-52cf-4507-9871-c43f209de99d"), new Guid("94742ce7-95da-c438-f431-bb8d0d8db015"), "09-539-036-828", 3, 1 },
                    { new Guid("2e12868e-ef6c-47d3-9d9b-ddd920980fae"), new Guid("6973d4ce-ab1a-2927-667b-230d931085a9"), "00-208-973-224", 0, 1 },
                    { new Guid("2f072408-b1e2-4c02-9ab8-c27623868fa9"), new Guid("5533c8fa-7f2f-fc8f-25df-486a57c175e0"), "08-194-519-027", 0, 1 },
                    { new Guid("3134b4d8-f335-4d6f-842e-9795a95cc5e5"), new Guid("d408eabd-a25a-d4b5-ef07-ce4a3ba704cc"), "01-895-073-952", 0, 0 },
                    { new Guid("36dcebd9-ba07-482d-96cb-6e1ddab5e8e7"), new Guid("d7b0a339-ed52-d2b1-0afa-6ed02a641a5a"), "04-765-351-709", 1, 1 },
                    { new Guid("37ddf6b1-a959-4c8f-94b9-d055d44039ce"), new Guid("4fd6a6a4-8b75-805b-44ee-d8bb2dacc673"), "01-663-840-678", 0, 1 },
                    { new Guid("402088cb-a0fe-4c30-86e4-f13c53e2a545"), new Guid("44f0519c-aec4-336b-11ce-6ac4430c54c4"), "07-908-597-306", 0, 1 },
                    { new Guid("433399be-e27c-4296-82fd-d3d24ee3bb96"), new Guid("379c7d70-75f1-e74b-a9a3-74dc1c135988"), "04-785-202-552", 0, 0 },
                    { new Guid("4e973d8e-f5eb-49aa-a54e-9e74887e72b5"), new Guid("5a71f042-c594-94fc-c8da-6dfc951b16ee"), "02-194-771-470", 0, 1 },
                    { new Guid("4f3208d4-cd35-4093-8ab5-2de133a516df"), new Guid("3eb14d44-346e-a263-5447-41c1436ac427"), "03-045-964-841", 0, 0 },
                    { new Guid("506bcc71-59b7-4622-ae41-dce68b910667"), new Guid("61b2ee52-cb90-607c-b0cf-ba2b0b0bd0f7"), "03-872-822-892", 1, 0 },
                    { new Guid("507cf962-18eb-4611-8773-fea4257badc7"), new Guid("5372d61d-134d-7a75-571c-1cedb2896e2c"), "03-889-431-578", 3, 0 },
                    { new Guid("5c6e52d4-9cf2-43f5-8f35-db843fbdfbb1"), new Guid("127610dd-7d11-b228-236b-defd83c885a1"), "01-274-148-884", 1, 0 },
                    { new Guid("5daa11a9-3a9c-41bb-978e-3da4af330489"), new Guid("779055a4-22d8-68cd-2d92-b2e176f9b5ff"), "02-768-631-281", 2, 0 },
                    { new Guid("5e8000f7-82d4-4dc2-8c5b-6c3bc401a42e"), new Guid("d4535d27-bb52-d443-939a-701800a2e630"), "08-531-201-473", 2, 0 },
                    { new Guid("618a75ad-1363-4368-90f5-15b59984ef02"), new Guid("6ca3182a-6d19-6a16-6cc9-1e59b97769f2"), "05-465-914-487", 3, 1 },
                    { new Guid("63a2fb3a-eaf3-4fef-acbe-99c5faaca12c"), new Guid("0f0ed5b7-0c60-f8bf-37ce-8f9bf524ff7f"), "09-339-182-523", 2, 1 },
                    { new Guid("63ab728a-428b-47f9-8cb9-729c133c14a8"), new Guid("487bb131-0b63-015d-0be3-6c97827f7c0e"), "03-671-477-428", 2, 1 },
                    { new Guid("64591c75-1afb-4959-a981-5fae19b68e72"), new Guid("2d6757ad-0858-1406-fb3c-27d1d13b32d5"), "02-195-564-561", 3, 1 },
                    { new Guid("68802895-82ab-4f15-98bd-e79b933ef511"), new Guid("4b667aeb-6765-9c15-aa9b-699c159bbf35"), "04-044-109-721", 3, 1 },
                    { new Guid("69e5903a-07f4-4783-9bc4-170563976867"), new Guid("2eacc6c2-f158-29d5-93fa-9466de3b2db2"), "07-691-005-169", 0, 1 },
                    { new Guid("6d954a33-9a28-4a33-8228-0588d0747a6a"), new Guid("7a0efd61-7629-9557-0402-5d3173427539"), "00-855-552-859", 0, 1 },
                    { new Guid("74f710e2-e989-472b-a3dd-bfd8c00b7d9f"), new Guid("0eeab85c-2950-3253-e248-0a3e06289b67"), "06-665-206-694", 1, 1 },
                    { new Guid("7866b6f6-3b76-4a96-ad78-ac3609641a2f"), new Guid("2d545dc6-73de-a59c-c6b4-1671d11161ca"), "06-014-861-710", 0, 1 },
                    { new Guid("7cd28d5d-34e1-44db-87f2-966d8feb05ed"), new Guid("36390d88-5dc2-55e6-5cde-828fd9a046f0"), "02-304-252-132", 0, 0 },
                    { new Guid("81539739-97b4-4fcb-b221-fab9d533b025"), new Guid("3fbbb959-313a-464a-2ed8-ddfebbe4ccd0"), "00-573-285-868", 2, 1 },
                    { new Guid("8445279c-11aa-4c45-bff6-2c7829d63c53"), new Guid("79dbb017-a597-6b5a-d64c-9f9eaaa3cbdf"), "03-314-414-871", 0, 0 },
                    { new Guid("84991de9-c2a3-4fad-a689-66aea18f04c6"), new Guid("26a5eb8b-d3b7-e89b-466e-7ca25c0560a9"), "06-394-521-322", 3, 1 },
                    { new Guid("8694e88a-f050-45ac-a3a8-ca087e6d3c06"), new Guid("5bc5df73-5011-58f1-8770-569113745d6d"), "06-491-548-905", 1, 0 },
                    { new Guid("86fdbdfd-2d99-4c5d-a7b0-0571bb820e06"), new Guid("4f81e8b0-1437-f72a-5274-d4c4e39753d4"), "08-264-086-735", 3, 1 },
                    { new Guid("8754c37b-e4a9-4ab3-ab2b-2309ad87d8ef"), new Guid("3710e0bb-306d-6b49-d742-e1949bc327b2"), "08-156-854-089", 1, 0 },
                    { new Guid("87fd8721-c436-4b2a-becd-f175e0ccb14e"), new Guid("7cf5d9ce-2611-cc45-145f-9b761623e501"), "09-858-560-611", 2, 1 },
                    { new Guid("8877c51c-540e-4202-b974-dafffa3eb6f8"), new Guid("c050b66d-5dd7-a6d4-3f7e-6616d3bd4c3f"), "08-522-978-128", 3, 1 },
                    { new Guid("88d11958-b429-45df-a1f9-959fb1a58749"), new Guid("d805e24a-90b0-e89a-e8d7-5f10f44696ec"), "01-056-365-425", 1, 0 },
                    { new Guid("89a95a15-f821-43e1-abdb-1f07c8fa97fc"), new Guid("528482ee-c76b-33e2-3eb6-2bd4a885abb9"), "09-939-779-935", 3, 1 },
                    { new Guid("89c4cc5f-544d-4052-b3c7-c216cb0e2b6c"), new Guid("984ccefe-ee18-03e3-0246-6bd498831bb9"), "09-780-003-877", 3, 1 },
                    { new Guid("8af0c0f9-d51e-4348-9e87-7bd9f0d3629d"), new Guid("2df36e52-d12b-491c-2917-9bb96fb334bf"), "03-535-283-722", 0, 0 },
                    { new Guid("8f7ddbba-345c-4dd7-9cd4-5895cf82a81b"), new Guid("0c90f048-2c41-5c40-2ebb-e48ef5117367"), "08-749-830-475", 0, 0 },
                    { new Guid("9221fa5d-71e9-4128-b036-63af304e086d"), new Guid("b972cc90-2716-e9e3-70b9-7e92fb4470f8"), "08-480-744-401", 3, 0 },
                    { new Guid("92a96e66-6973-43f9-81cd-3d17fe8652bf"), new Guid("b5c07805-d5bf-c6e6-e32e-6ed5a474670a"), "07-400-123-508", 0, 0 },
                    { new Guid("92f56f69-ebb0-46ab-be0b-892513cbc521"), new Guid("0b690921-f012-3945-d29f-1715bd2ae2d6"), "02-381-869-312", 3, 1 },
                    { new Guid("94ba0584-7781-4e1e-8101-245a031a0e8f"), new Guid("cb2349c9-5daa-a3e1-27f4-fd8a5eb516ba"), "04-092-230-859", 0, 0 },
                    { new Guid("970c6d75-bdc8-43be-b624-fb5b60bb666c"), new Guid("63f7d292-860d-4759-6306-9f1fa6bd3671"), "09-570-995-585", 0, 0 },
                    { new Guid("9ba54476-8e1c-4f22-b6a2-970c79966f19"), new Guid("97d848aa-e74f-0d47-a351-4587452916e8"), "00-709-873-235", 3, 0 },
                    { new Guid("9d9839e4-fa5b-4a5d-afb8-f1c2c26442df"), new Guid("5e100eed-12db-4106-2078-fef0006008d5"), "01-702-154-220", 2, 0 },
                    { new Guid("a404f63a-dd28-40fd-acba-d5e24a0dd955"), new Guid("58156cc5-2b99-5824-2000-324378333c0a"), "03-278-374-846", 3, 1 },
                    { new Guid("a5eb8b61-74e6-4861-aa7d-f2781e8d8d08"), new Guid("27f84d45-4339-dfdb-23ec-359078dd4e52"), "09-331-530-720", 3, 0 },
                    { new Guid("a7db2f9d-36b0-42ee-b9d8-7e71ace088bb"), new Guid("92d91f87-0273-fdb1-e58f-6703d98756f1"), "02-566-119-329", 2, 1 },
                    { new Guid("abbfaaa8-d570-45bb-9d8f-1f5311c99a46"), new Guid("1cc17c20-f013-3309-04c8-74cdb85fe8ec"), "06-045-558-999", 3, 1 },
                    { new Guid("ac27460b-74af-4967-9c33-9bfa547b5eac"), new Guid("837f2164-b775-136d-84fb-3d53a03158e2"), "04-967-977-696", 0, 0 },
                    { new Guid("ac916d8d-dbec-43ac-bbcb-cdf6ed33ed0f"), new Guid("711b3993-e491-f026-bafc-69bd83ae1a16"), "09-687-605-259", 1, 1 },
                    { new Guid("ae3fe8f1-56d7-4abd-b787-567e470b094e"), new Guid("a413f325-b803-0f65-1fd5-f14b0d4dfbf8"), "07-880-366-452", 0, 0 },
                    { new Guid("afbd03a9-d013-40a4-8023-0742f21dc631"), new Guid("9e97f48a-6c9c-da3f-9995-38e2e0f02412"), "05-536-380-965", 2, 0 },
                    { new Guid("b0588e63-ecbb-4958-a34c-5fe746dc53a4"), new Guid("7fbbb29d-8148-e893-7454-7ade182727d4"), "00-806-632-708", 0, 1 },
                    { new Guid("b1124917-d3cd-4925-914c-4565d56443bc"), new Guid("60101ba3-4373-eb31-ef77-72c1a7252d22"), "01-255-224-671", 0, 0 },
                    { new Guid("b476dce7-fa74-478b-9643-ab4c1de54618"), new Guid("e0d3d86f-0f1e-e0a6-f1a7-8da6c289c40a"), "05-820-377-107", 1, 1 },
                    { new Guid("bcd61b31-fda9-43be-b5b1-a62788dd4ac9"), new Guid("46d9d069-5b62-db5d-f813-8923c6b0e0ac"), "04-054-915-575", 2, 1 },
                    { new Guid("bed411f4-4cb9-4410-b70b-771462cba769"), new Guid("c8229ce9-397e-cab3-72c8-3dd9e6c4f59e"), "02-995-740-961", 1, 0 },
                    { new Guid("bef179e2-04e9-4705-8779-395e24011078"), new Guid("214b9c40-637b-ca01-437a-81f284633390"), "05-085-976-347", 0, 0 },
                    { new Guid("c250a967-581d-44d9-bc82-ccf4bd7be18f"), new Guid("7e37faed-aaca-1a15-9333-40a16df5baf4"), "07-507-246-173", 2, 1 },
                    { new Guid("c2e4dc15-48be-452d-8704-6e5d7bf0712f"), new Guid("7df3f0b1-141f-593b-1a4e-fbd51aa959fd"), "09-807-924-937", 0, 0 },
                    { new Guid("c37d2c81-e592-4de5-96db-e0976fa8cbcd"), new Guid("f2168584-457f-18d7-f563-d46069d03998"), "09-705-138-667", 3, 1 },
                    { new Guid("c3caa8b1-a84e-44ba-b215-ebe8a57f1fed"), new Guid("b6fb276c-8712-9f2a-43d9-d9b8a1d1a1f0"), "08-114-856-670", 1, 0 },
                    { new Guid("cd029800-86c5-47db-835e-3a80d1249687"), new Guid("f06feb6f-2366-4460-e364-10061d600862"), "03-333-567-359", 1, 1 },
                    { new Guid("d058b7cd-00cd-4f2b-b2d4-7df7fbc4ed7a"), new Guid("65a9550b-4d77-5a6e-9c86-2956e1d9ffc8"), "09-693-775-515", 1, 0 },
                    { new Guid("d0940f54-2b05-45f4-98dc-d8ca5fdfd70b"), new Guid("a0ebf794-3589-60bc-2482-6caa42c2113c"), "03-709-422-342", 3, 1 },
                    { new Guid("d8514bb7-5299-4d73-a2b6-5ac6a46b4105"), new Guid("33b41c29-196a-67e9-5992-182f72e8df7e"), "02-071-526-102", 1, 1 },
                    { new Guid("df564627-6817-4e78-ad9f-83b40a566389"), new Guid("84d1c415-62df-d9e7-a649-fdf97d48ad8a"), "00-283-315-669", 0, 1 },
                    { new Guid("df686b7e-4a29-4589-b982-cffc9b42bede"), new Guid("38c649bf-3213-2447-66d1-b7ea7b7d9b75"), "01-187-416-700", 2, 0 },
                    { new Guid("e0a497e0-0e70-4fbd-ab81-d2e6e32eef4c"), new Guid("3dd8f5fa-36f7-25b8-34ae-450eb5621d63"), "06-439-407-156", 1, 1 },
                    { new Guid("e347e216-717c-4106-b8db-4fcd5414be6d"), new Guid("69111ed9-c4c3-4c95-43bc-89fdbdc4419d"), "02-855-076-528", 2, 0 },
                    { new Guid("e4ceb1d7-e104-44eb-831a-fec4e594a55e"), new Guid("cb6409c8-0de4-fb1d-e1c1-913d016f0a09"), "09-098-638-283", 3, 1 },
                    { new Guid("e8e0427c-b65a-4db4-8684-49c9541e3d55"), new Guid("e56dd9d9-ba2b-92da-f2a5-9836e53729ed"), "01-623-342-180", 1, 1 },
                    { new Guid("f021f2dc-aa08-467f-ba7a-178247b44e60"), new Guid("73ce1fcc-1065-b685-2b71-2f43c6aa6363"), "07-901-759-240", 3, 1 },
                    { new Guid("f1347e74-d9f9-42ba-972a-5f2f761f1db3"), new Guid("7b8ab9e3-2fc6-7beb-e2d3-94b803318447"), "07-427-503-152", 3, 1 },
                    { new Guid("f1a4d8ee-a11d-41ac-b310-7da258fe5a4c"), new Guid("49cff53b-9222-7017-aa1c-ea889a74a149"), "05-397-494-004", 0, 0 },
                    { new Guid("f4c75712-e93b-4b31-a383-b332b4653e04"), new Guid("bdcdb319-1142-3595-c98d-3a69b6586e28"), "09-012-909-870", 2, 1 },
                    { new Guid("f4d7662e-2d65-430d-8f1d-5a923bde6792"), new Guid("ea5798c9-0c00-b5e3-1593-eac39e5c0294"), "09-635-187-224", 3, 1 },
                    { new Guid("f96ed829-a2fd-4a0b-9bac-71d918774447"), new Guid("3b0ff909-3e94-b9f4-ced6-f0d30e48b40f"), "09-651-668-702", 2, 0 },
                    { new Guid("fb5b0b1a-8cce-4f4c-9e65-ed77f8fa108d"), new Guid("90b6968d-5566-b549-2417-2b200383dcf7"), "08-837-237-411", 1, 0 },
                    { new Guid("fbe41d4b-9e94-4b85-b689-a24ad565a152"), new Guid("740c1137-be24-e8a2-1626-f607d33d9c09"), "04-268-720-196", 2, 1 },
                    { new Guid("fdfcd8a0-3b95-467a-9abd-2afee1fdfe16"), new Guid("6e535878-c284-f51e-d64e-d0f69ffa9edd"), "00-149-031-397", 0, 1 },
                    { new Guid("ff58dde5-a0ff-4490-9297-e7e9f2534792"), new Guid("8dab723c-5d64-964e-8fa1-6e0aa2e1de14"), "08-444-872-377", 0, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telefonos");
        }
    }
}
