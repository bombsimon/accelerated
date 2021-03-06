module Domain.FateAccelerated

open Domain.System

type ApproachName = ApproachName of string

type Approach = {
    Name: ApproachName
    Rank: Rank
}

let defaultApproachList = [
    "Careful";
    "Clever";
    "Flashy";
    "Forceful";
    "Quick";
    "Sneaky"
]

let defaultApproachRanks =
    [
        yield! rankTimes 3 1
        yield! rankTimes 2 2
        yield! rankTimes 1 2
    ]

let createApproaches rank names =
    names
    |> List.map (fun name -> {
        Name = ApproachName name
        Rank = rank})

type StressBoxType =
| Single
| Complex

let createSingleStressBoxes stressType boxes  =
    [1 .. boxes]
    |> List.map (fun _ -> {
        Type = stressType
        Usable = true
        Filled = false
        Stress = Boxes 1
    })

let createStressBox stressType stressBoxType boxes =
    match stressBoxType with
    | Single
        -> createSingleStressBoxes stressType boxes
    | Complex
        -> [{
            Type = stressType
            Usable = true
            Filled = false
            Stress = Boxes boxes
        }]

let createStressBoxes stressTypes stressBoxType highestStressBox =
    stressTypes
    |> List.collect (fun stressType ->
        validateCount highestStressBox
        |> List.collect (createStressBox stressType stressBoxType)
    )

type Stunt = {
    Name: StuntName
    Description: string
    Approach: Approach option
    Action: Action option
    Activation: StuntActivation option
}

let internal createStunts count =
    validateCount count
    |> List.map (fun _ -> {
        Name = StuntName ""
        Description = ""
        Approach = None
        Action = None
        Activation = None
    })