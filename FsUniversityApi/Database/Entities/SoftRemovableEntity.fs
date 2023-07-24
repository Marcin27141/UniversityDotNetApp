namespace FsUniversityApi.Database.Entities

module ISoftRemovableEntity = 
    type ISoftRemovableEntity = 
        abstract SoftDeleted : bool
        abstract SetSoftDeleted : bool -> ISoftRemovableEntity