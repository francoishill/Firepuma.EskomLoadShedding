namespace Firepuma.EskomLoadShedding.FunctionApp.Status.Models.ValueObjects;

public enum LoadSheddingStatus
{
    Unknown = -1,
    NotLoadshedding = 1,
    Stage1 = 2,
    Stage2 = 3,
    Stage3 = 4,
    Stage4 = 5,
    Stage5 = 6,
    Stage6 = 7,
    Stage7 = 8,
    Stage8 = 9,
}