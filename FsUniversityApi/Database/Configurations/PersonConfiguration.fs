namespace FsUniversityApi.Database.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open System
open FsUniversityApi.Database.Entities.Person
open ApiDtoLibrary.Person

type PersonConfiguration() =
    static member initialPersonInfos : PersonInfo list = [
        //{ PersonInfoId = Guid.NewGuid(); ApplicationUserId = Guid.NewGuid(); FirstName = "Alan"; LastName = "Turner";
        //PESEL = "01111111111"; MotherLand = "USA"; Birthday = DateTime(1980,12,1); SoftDeleted = false; PersonStatus = PersonStatus.Professor};
        //{ PersonInfoId = Guid.NewGuid(); ApplicationUserId = Guid.NewGuid(); FirstName = "Bonnie"; LastName = "Clyde";
        //PESEL = "02222222222"; MotherLand = "USA"; Birthday = DateTime(1975,6,4); SoftDeleted = false; PersonStatus = PersonStatus.Professor};
        //{ PersonInfoId = Guid.NewGuid(); ApplicationUserId = Guid.NewGuid(); FirstName = "Celina"; LastName = "Domczyk";
        //PESEL = "03333333333"; MotherLand = "Poland"; Birthday = DateTime(1990,1,2); SoftDeleted = false; PersonStatus = PersonStatus.Professor};
        //{ PersonInfoId = Guid.NewGuid(); ApplicationUserId = Guid.NewGuid(); FirstName = "Daniel"; LastName = "Danielczyk";
        //PESEL = "04444444444"; MotherLand = "Poland"; Birthday = DateTime(2000,10,4); SoftDeleted = false; PersonStatus = PersonStatus.Student};
        //{ PersonInfoId = Guid.NewGuid(); ApplicationUserId = Guid.NewGuid(); FirstName = "Hans"; LastName = "Zammer";
        //PESEL = "05555555555"; MotherLand = "Germany"; Birthday = DateTime(1999,4,23); SoftDeleted = false; PersonStatus = PersonStatus.Student};
        //{ PersonInfoId = Guid.NewGuid(); ApplicationUserId = Guid.NewGuid(); FirstName = "Juan"; LastName = "Garcia";
        //PESEL = "06666666666"; MotherLand = "Spain"; Birthday = DateTime(2001,11,13); SoftDeleted = false; PersonStatus = PersonStatus.Student}
        ]

    interface IEntityTypeConfiguration<PersonInfo> with
        member this.Configure(builder : EntityTypeBuilder<PersonInfo>) =
            builder.HasData(PersonConfiguration.initialPersonInfos) |> ignore